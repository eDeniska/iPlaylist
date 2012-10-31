using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using iTunesLib;
using MediaInfoLib;
using UtilityLib;

namespace iTunesInterfaceLib
{
    public class MonitorThread : AbstractMonitorThread
    {
        private iTunesApp iTunes = null;
        private int trackID = -1;
        
        public MonitorThread(MediaInfoKeeper p_keeper)
        {
            checkPosThread = new Thread(checkPos);
            keeper = p_keeper;
        }

        public override void Start()
        {
            try
            {
                checkPosThread.Start();
                state = MonitorState.Starting;
            }
            catch (Exception e)
            {
                Log.Write(e);
                errorMessage = "Error starting monitor: " + e.Message;
                state = MonitorState.Error;
            }
        }

        public override void Stop()
        {
            try
            {
                lock (lckThread)
                {
                    quit = true;
                }
                checkPosThread.Join();
                iTunes = null;
                state = MonitorState.NotStarted;
            }
            catch (Exception e)
            {
                Log.Write(e);
                throw e;
            }
        }
        
        private void checkPos()
        {
            if (UtilityLib.Consts.Locales.EnglishCulture.NativeName.ToLower().Equals(Parameters.Language))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = UtilityLib.Consts.Locales.EnglishCulture;
            }
            else if (UtilityLib.Consts.Locales.RussianCulture.NativeName.ToLower().Equals(Parameters.Language))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = UtilityLib.Consts.Locales.RussianCulture;
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;
            }

            try
            {
                iTunes = new iTunesAppClass();
                if (!(quit))
                {
                    this.RecalculateStatistics();
                }
            }
            catch (Exception e)
            {
                Log.Write(e);
                errorMessage = "Error contacting player: " + e.Message;
                state = MonitorState.Error;
                return;
            }

            state = MonitorState.Started;

            while (true)
            {
                lock (lckThread)
                {
                    if (quit) return;
                }

                try
                {
                    if (iTunes.CurrentTrack != null)
                    {
                        if (playingTrack != null)
                        {
                            // checking if we're replaying...
                            if (playingTrack.Position <= MediaInfoLib.Consts.ReplayPosition)
                            {
                                if (playingTrack.MinimumDelta < (playingTrack.Duration / 4))
                                {
                                    // this means, that we've listened for more than 3/4 of the song
                                    keeper.TrackReplayed(playingTrack);
                                    playingTrack.MinimumDelta = playingTrack.Duration - playingTrack.Position;
                                }
                            }
                        }

                        if (trackID != iTunes.CurrentTrack.trackID)
                        {
                            trackID = iTunes.CurrentTrack.trackID;

                            if (playingTrack != null)
                            {
                                if (playingTrack.MinimumDelta > Consts.LastSecondsDelta)
                                {
                                    // track definetely was skipped
                                    keeper.TrackSkipped(playingTrack);

                                    lock (lckTracks)
                                    {
                                        if (String.IsNullOrEmpty(playingTrack.Album))
                                        {
                                            trackStatus = String.Format(MediaInfoLib.Resources.InterfaceLabels.TrackSkippedFormat, playingTrack.Artist,
                                                playingTrack.Name, String.Empty);
                                        }
                                        else
                                        {
                                            trackStatus = String.Format(MediaInfoLib.Resources.InterfaceLabels.TrackSkippedFormat, playingTrack.Artist,
                                                playingTrack.Name, String.Format(MediaInfoLib.Resources.InterfaceLabels.AlbumFormat, playingTrack.Album));
                                        }
                                    }
                                }
                                else
                                {
                                    // track played till the end
                                    keeper.TrackPlayed(playingTrack);

                                    lock (lckTracks)
                                    {
                                        if (String.IsNullOrEmpty(playingTrack.Album))
                                        {
                                            trackStatus = String.Format(MediaInfoLib.Resources.InterfaceLabels.TrackPlayedFormat, playingTrack.Artist,
                                                playingTrack.Name, String.Empty);
                                        }
                                        else
                                        {
                                            trackStatus = String.Format(MediaInfoLib.Resources.InterfaceLabels.TrackPlayedFormat, playingTrack.Artist,
                                                playingTrack.Name, String.Format(MediaInfoLib.Resources.InterfaceLabels.AlbumFormat, playingTrack.Album));
                                        }
                                    }
                                }

                                keeper.Save();
                                keeper.SaveSummary();
                            }
                            playingTrack = new TrackInfo(iTunes.CurrentTrack.Name, iTunes.CurrentTrack.Artist, iTunes.CurrentTrack.Album,
                                iTunes.CurrentTrack.Genre, iTunes.CurrentTrack.trackID.ToString(), iTunes.CurrentTrack.Duration, iTunes.PlayerPosition);
                        }
                        playingTrack.Position = iTunes.PlayerPosition;
                        if ((playingTrack.Duration - playingTrack.Position) < playingTrack.MinimumDelta)
                        {
                            playingTrack.MinimumDelta = playingTrack.Duration - playingTrack.Position;
                        }
                        

                        lock (lckTracks)
                        {
                            trackCurrent = String.Format(MediaInfoLib.Resources.InterfaceLabels.CurrentTrackFormat,
                                iTunes.CurrentTrack.Artist, iTunes.CurrentTrack.Name);
                            trackAlbum = iTunes.CurrentTrack.Album;
                        }
                    }
                }
                catch (InvalidCastException e)
                {
                    // COM server is down, application will need to reinitialize
                    // iTunes = new iTunesAppClass();
                    //trackLog = String.Format("iTunes is closed, closing iPlaylist monitor!{0}", Environment.NewLine) + trackLog;
                    lock (lckTracks)
                    {
                        errorMessage = "COM server shutting down error: " + e.Message;
                        state = MonitorState.Interrupted;
                    }
                    return;
                }
                catch (COMException)
                {
                    // application is busy... nothing we can do... just wait, when it will become available again
                }
                catch (Exception e)
                {
                    Log.Write(e);
                    // log exeptions
                    lock (lckTracks)
                    {
                        trackStatus = String.Format(MediaInfoLib.Resources.InterfaceLabels.InterfaceErrorFormat, e.Message);
                    }
                }

                Thread.Sleep(1000);
            }
        }

        private void calcStatistics()
        {
            try
            {
                relevantSamples = MediaInfoLib.Consts.Tracks.Counting;
                totalTracks = MediaInfoLib.Consts.Tracks.Counting;
                totalDuration = MediaInfoLib.Consts.Tracks.Counting;

                libraryTracks = 0;
                foreach (IITTrack trk in iTunes.LibraryPlaylist.Tracks)
                {
                    if ((trk.SampleRate != 0) || (!(Parameters.SkipNonAudio)))
                    {
                        libraryTracks++;
                    }
                }
                IITPlaylist source = null;
                //return iTunes.LibraryPlaylist.Tracks.Count;
                if (Parameters.UsePlaylist)
                {
                    foreach (IITSource src in iTunes.Sources)
                    {
                        foreach (IITPlaylist pls in src.Playlists)
                        {
                            if (pls.Name.Equals(Parameters.SourcePlaylist))
                            {
                                source = pls;
                                break;
                            }

                        }
                    }
                }
                if (source == null)
                {
                    source = iTunes.LibraryPlaylist;
                }
                int iTotalTracks = 0;
                int iRelevantSamples = 0;
                int iTotalDuration = 0;
                // need to check it with keeper...

                foreach (IITTrack trk in source.Tracks)
                {
                    if ((trk.SampleRate != 0) || (!(Parameters.SkipNonAudio)))
                    {
                        iTotalDuration += trk.Duration;
                        iTotalTracks++;
                        try
                        {
                            // this will increase number of relevant samples if GetRating will not throw exception
                            keeper.GetRating(new TrackInfo(trk.Name, trk.Artist, trk.Album, trk.Genre, String.Empty, trk.Duration, 0));
                            iRelevantSamples++;
                        }
                        catch (RatingNotFoundException)
                        {
                        }
                        catch (Exception e)
                        {
                            Log.Write(e);
                        }
                    }
                }
                totalTracks = iTotalTracks;
                relevantSamples = iRelevantSamples;
                totalDuration = iTotalDuration;
            }
            catch (COMException)
            {
                // application is busy... nothing we can do... just wait, when it will become available again
                // statistics is not set, fire new thread...
                this.RecalculateStatistics();
            }
            catch (Exception e)
            {
                Log.Write(e);
                errorMessage = "Error getting media statistics: " + e.Message;
                if (state == MonitorState.Started)
                {
                    this.Stop();
                    state = MonitorState.Interrupted;
                }
                libraryTracks = MediaInfoLib.Consts.Tracks.Error;
                totalTracks = MediaInfoLib.Consts.Tracks.Error;
                relevantSamples = MediaInfoLib.Consts.Tracks.Error;
            }
        }

        public override object PlayerObject
        {
            get
            {
                return iTunes;
            }

        }

        public override void NextTrack()
        {
            if (state == MonitorState.Started)
            {
                try
                {
                    iTunes.NextTrack();
                }
                catch (COMException)
                {
                    // application is busy... nothing we can do... just wait, when it will become available again
                }
                catch (Exception e)
                {
                    Log.Write(e);
                    lock (lckTracks)
                    {
                        trackStatus = String.Format(MediaInfoLib.Resources.InterfaceLabels.InterfaceErrorFormat, e.Message);
                    }
                    this.Stop();
                    errorMessage = "Error on NextTrack: " + e.Message;
                    state = MonitorState.Interrupted;
                }
            }
        }

        public override String Version
        {
            get
            {
                if (state == MonitorState.Started)
                {
                    try
                    {
                        return "iTunes " + iTunes.Version;
                    }
                    catch (COMException)
                    {
                        // application is busy... nothing we can do... just wait, when it will become available again
                        return "iTunes";
                    }
                    catch (Exception e)
                    {
                        Log.Write(e);
                        this.Stop();
                        errorMessage = "Error on Version: " + e.Message;
                        state = MonitorState.Interrupted;
                        return "ERROR";
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public override String[] Playlists
        {
            get
            {
                if (state == MonitorState.Started)
                {
                    try
                    {
                        List<String> list = new List<String>();
                        foreach (IITPlaylist pls in iTunes.LibrarySource.Playlists)
                        {
                            list.Add(pls.Name);
                        }
                        return list.ToArray();
                    }
                    catch (COMException)
                    {
                        // application is busy... nothing we can do... just wait, when it will become available again
                        return null;
                    }
                    catch (Exception e)
                    {
                        Log.Write(e);
                        lock (lckTracks)
                        {
                            trackStatus = String.Format(MediaInfoLib.Resources.InterfaceLabels.InterfaceErrorFormat, e.Message);
                        }
                        this.Stop();
                        errorMessage = "Error on Playlists: " + e.Message;
                        state = MonitorState.Interrupted;
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public override void RecalculateStatistics()
        {
            Thread statsThread = new Thread(calcStatistics);
            statsThread.Start();
        }

        public override void Pause()
        {
            if (this.state == MonitorState.Started)
            {
                try
                {
                    iTunes.Pause();
                }
                catch (COMException)
                {
                    // application is busy... nothing we can do... just wait, when it will become available again
                }
                catch (Exception e)
                {
                    Log.Write(e);
                    lock (lckTracks)
                    {
                        trackStatus = String.Format(MediaInfoLib.Resources.InterfaceLabels.InterfaceErrorFormat, e.Message);
                    }
                    this.Stop();
                    errorMessage = "Error on Pause: " + e.Message;
                    state = MonitorState.Interrupted;
                }
            }
        }

        public override void Play()
        {
            if (this.state == MonitorState.Started)
            {
                try
                {
                    if (iTunes.CurrentTrack == null)
                    {
                        foreach (IITSource src in iTunes.Sources)
                        {
                            if ((src.Kind == ITSourceKind.ITSourceKindLibrary) ||
                                (src.Kind == ITSourceKind.ITSourceKindSharedLibrary))
                            {
                                foreach (IITPlaylist pls in src.Playlists)
                                {
                                    if (pls.Name.Equals(Parameters.MainPlaylist))
                                    {
                                        pls.PlayFirstTrack();
                                        return;
                                    }
                                }
                            }
                        }
                        iTunes.LibraryPlaylist.PlayFirstTrack();
                    }
                    else
                    {
                        iTunes.Play();
                    }
                }
                catch (COMException)
                {
                    // application is busy... nothing we can do... just wait, when it will become available again
                }
                catch (Exception e)
                {
                    Log.Write(e);
                    lock (lckTracks)
                    {
                        trackStatus = String.Format(MediaInfoLib.Resources.InterfaceLabels.InterfaceErrorFormat, e.Message);
                    }
                    this.Stop();
                    errorMessage = "Error on Play: " + e.Message;
                    state = MonitorState.Interrupted;
                }
            }
        }

        public override bool IsPlaying
        {
            get
            {
                if (this.State == MonitorState.Started)
                {
                    try
                    {
                        return (iTunes.PlayerState == ITPlayerState.ITPlayerStatePlaying);
                    }
                    catch (COMException)
                    {
                        // application is busy... nothing we can do... just wait, when it will become available again
                        return false;
                    }
                    catch (Exception e)
                    {
                        Log.Write(e);
                        lock (lckTracks)
                        {
                            trackStatus = String.Format(MediaInfoLib.Resources.InterfaceLabels.InterfaceErrorFormat, e.Message);
                        }
                        this.Stop();
                        errorMessage = "Error on IsPlaying: " + e.Message;
                        state = MonitorState.Interrupted;
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public override void PreviousTrack()
        {
            if (this.state == MonitorState.Started)
            {
                try
                {
                    if (iTunes.PlayerPosition <= MediaInfoLib.Consts.GoPreviousPosition)
                    {
                        iTunes.PreviousTrack();
                    }
                    else
                    {
                        ReplayTrack();
                    }
                }
                catch (COMException)
                {
                    // application is busy... nothing we can do... just wait, when it will become available again
                }
                catch (Exception e)
                {
                    // 
                    Log.Write(e);
                    lock (lckTracks)
                    {
                        trackStatus = String.Format(MediaInfoLib.Resources.InterfaceLabels.InterfaceErrorFormat, e.Message);
                    }
                    this.Stop();
                    errorMessage = "Error on PreviousTrack: " + e.Message;
                    state = MonitorState.Interrupted;
                }
            }
        }

        public override void SetPosition(int position)
        {
            if (this.state == MonitorState.Started)
            {
                try
                {
                    iTunes.PlayerPosition = position;
                    // for faster screen updates
                    playingTrack.Position = position;
                }
                catch (COMException)
                {
                    // application is busy... nothing we can do... just wait, when it will become available again
                }
                catch (Exception e)
                {
                    // 
                    Log.Write(e);
                    lock (lckTracks)
                    {
                        trackStatus = String.Format(MediaInfoLib.Resources.InterfaceLabels.InterfaceErrorFormat, e.Message);
                    }
                    this.Stop();
                    errorMessage = "Error on SetPosition: " + e.Message;
                    state = MonitorState.Interrupted;
                }
            }
        }
    }
}
