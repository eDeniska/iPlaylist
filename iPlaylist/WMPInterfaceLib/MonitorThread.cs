using System;
using System.Collections.Generic;
using System.Threading;
using MediaInfoLib;
using UtilityLib;
using WMPLib;
using WMPRemote;
using System.Runtime.InteropServices;

namespace WMPInterfaceLib
{
    public class MonitorThread : AbstractMonitorThread
    {
        private WindowsMediaPlayer wmp;
        private RemotedWindowsMediaPlayer player;
        private String trackID = null;
        
        public MonitorThread(MediaInfoKeeper p_keeper)
        {
            checkPosThread = new Thread(checkPos);
            keeper = p_keeper;
        }

        private void calcStatistics()
        {
            try
            {
                libraryTracks = MediaInfoLib.Consts.Tracks.Counting;
                relevantSamples = MediaInfoLib.Consts.Tracks.Counting;
                totalDuration = MediaInfoLib.Consts.Tracks.Counting;

                IWMPPlaylist source = null;
                if (Parameters.UsePlaylist)
                {
                    IWMPPlaylistArray plsArr = wmp.playlistCollection.getByName(Parameters.SourcePlaylist);
                    if (plsArr.count > 0)
                    {
                        source = plsArr.Item(0);

                    }
                }

                if (source == null)
                {
                    source = wmp.mediaCollection.getAll();
                }

                int cnt = source.count;
                totalTracks = 0;
                for (int i = 0; i < cnt; i++)
                {
                    String mediaType = String.Empty;
                    IWMPMedia media = source.get_Item(i);

                    try
                    {
                        mediaType = media.getItemInfo(Consts.Attributes.MediaType);
                    }
                    catch (Exception)
                    {
                        mediaType = String.Empty;
                    }

                    if ((mediaType.Equals(Consts.MediaType.Audio)) ||
                        ((!(Parameters.SkipNonAudio)) && (mediaType.Equals(Consts.MediaType.Video))))
                    {
                        totalTracks++;
                    }
                }
                // count number of tracks only once...

                IWMPPlaylist s = wmp.mediaCollection.getAll();
                int cnts = s.count;
                int iLibraryTracks = 0;
                int iRelevantSamples = 0;
                int iTotalDuration = 0;

                for (int i = 0; i < cnts; i++)
                {
                    String cTrackID = String.Empty;
                    String cArtist = String.Empty;
                    String cAlbum = String.Empty;
                    String cName = String.Empty;
                    String cGenre = String.Empty;
                    String mediaType = String.Empty;
                    int cDuration = 0;

                    IWMPMedia media = s.get_Item(i);

                    try
                    {
                        mediaType = media.getItemInfo(Consts.Attributes.MediaType);
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        cTrackID = media.getItemInfo(Consts.Attributes.TrackID);
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        cArtist = media.getItemInfo(Consts.Attributes.Arist);
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        cAlbum = media.getItemInfo(Consts.Attributes.Album);
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        cName = media.getItemInfo(Consts.Attributes.Name);
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        cGenre = media.getItemInfo(Consts.Attributes.Genre);
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        cDuration = Int32.Parse(media.getItemInfo(Consts.Attributes.Duration));
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        mediaType = media.getItemInfo(Consts.Attributes.MediaType);
                    }
                    catch (Exception)
                    {
                        mediaType = String.Empty;
                    }

                    if ((mediaType.Equals(Consts.MediaType.Audio)) ||
                        ((!(Parameters.SkipNonAudio)) && (mediaType.Equals(Consts.MediaType.Video))))
                    {
                        iTotalDuration += cDuration;
                        iLibraryTracks++;
                        try
                        {
                            keeper.GetRating(new TrackInfo(cName, cArtist, cAlbum, cGenre, cTrackID, cDuration, 0));
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
                totalDuration = iTotalDuration;
                libraryTracks = iLibraryTracks;
                relevantSamples = iRelevantSamples;
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
                errorMessage = "Error getting statistics: " + e.Message;
                state = MonitorState.Interrupted;
                libraryTracks = MediaInfoLib.Consts.Tracks.Error;
                totalTracks = MediaInfoLib.Consts.Tracks.Error;
                relevantSamples = MediaInfoLib.Consts.Tracks.Error;
            }
        }

        [STAThread]
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

            if (!(quit))
            {
                this.RecalculateStatistics();
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
                    if (wmp.currentMedia != null)
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
                                    lock (lckTracks)
                                    {
                                        if (String.IsNullOrEmpty(playingTrack.Album))
                                        {
                                            trackStatus = String.Format(MediaInfoLib.Resources.InterfaceLabels.TrackReplayedFormat, playingTrack.Artist,
                                                playingTrack.Name, String.Empty);
                                        }
                                        else
                                        {
                                            trackStatus = String.Format(MediaInfoLib.Resources.InterfaceLabels.TrackReplayedFormat, playingTrack.Artist,
                                                playingTrack.Name, String.Format(MediaInfoLib.Resources.InterfaceLabels.AlbumFormat, playingTrack.Album));
                                        }
                                    }

                                }
                            }
                        }

                        String cTrackID = String.Empty;
                        String cArtist = String.Empty;
                        String cAlbum = String.Empty;
                        String cName = String.Empty;
                        String cGenre = String.Empty;
                        int cDuration = 0;
                        int cPosition = 0;
                        IWMPMedia current = wmp.currentMedia;

                        try
                        {
                            cTrackID = current.getItemInfo(Consts.Attributes.TrackID);
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            cArtist = current.getItemInfo(Consts.Attributes.Arist);
                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                            cAlbum = current.getItemInfo(Consts.Attributes.Album);
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            cName = current.getItemInfo(Consts.Attributes.Name);
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            cGenre = current.getItemInfo(Consts.Attributes.Genre);
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            cDuration = (int)Double.Parse(current.getItemInfo(Consts.Attributes.Duration));
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            cPosition = (int)wmp.controls.currentPosition;
                        }
                        catch (Exception)
                        {
                        }

                        if (!(cTrackID.Equals(trackID)))
                        {
                            trackID = cTrackID;

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
                            playingTrack = new TrackInfo(cName, cArtist, cAlbum, cGenre, cTrackID, cDuration, cPosition);
                        }
                        playingTrack.Position = cPosition;
                        if ((playingTrack.Duration - cPosition) < playingTrack.MinimumDelta)
                        {
                            playingTrack.MinimumDelta = playingTrack.Duration - cPosition;
                        }

                        lock (lckTracks)
                        {
                            trackCurrent = String.Format(MediaInfoLib.Resources.InterfaceLabels.CurrentTrackFormat, cArtist, cName);
                            trackAlbum = cAlbum;
                        }
                    }
                }
                catch (COMException)
                {
                    // application is busy... nothing we can do... just wait, when it will become available again
                }
                catch (Exception e)
                {
                    // log exeptions
                    Log.Write(e);
                    lock (lckTracks)
                    {
                        trackStatus = String.Format(MediaInfoLib.Resources.InterfaceLabels.InterfaceErrorFormat, e.Message);
                    }
                }
                Thread.Sleep(1000);
            }
        }

        [STAThread]
        public override void Start()
        {
            try
            {
                state = MonitorState.Starting;
                try
                {
                    player = new RemotedWindowsMediaPlayer();
                    player.CreateControl();
                    wmp = player.GetOcx() as WindowsMediaPlayer;
                }
                catch (Exception e)
                {
                    Log.Write(e);
                    errorMessage = "Error contacting player: " + e.Message;
                    state = MonitorState.Error;
                    return;
                }
                checkPosThread.Start();
            }
            catch (Exception e)
            {
                Log.Write(e);
                errorMessage = "Error starting monitor: " + e.Message;
                state = MonitorState.Error;
                //throw e;
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
                wmp = null;
                player = null;
                state = MonitorState.NotStarted;
            }
            catch (COMException)
            {
                // application is busy... nothing we can do... just wait, when it will become available again
            }
            catch (Exception e)
            {
                Log.Write(e);
                errorMessage = "Error stopping monitor: " + e.Message;
                state = MonitorState.Error;
            }
        }


        public override object PlayerObject
        {
            get
            {
                return wmp;
            }

        }

        public override void NextTrack()
        {
            if (state == MonitorState.Started)
            {
                try
                {
                    wmp.controls.next();
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
                    errorMessage = "Error on NextTrack: " + e.Message;
                    this.Stop();
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
                        return "Windows Media Player " + wmp.versionInfo;
                    }
                    catch (COMException)
                    {
                        // application is busy... nothing we can do... just wait, when it will become available again
                        return "Windows Media Player";
                    }
                    catch (Exception e)
                    {
                        Log.Write(e);
                        errorMessage = "Error on Version: " + e.Message;
                        this.Stop();
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
                if (this.state == MonitorState.Started)
                {
                    try
                    {
                        List<String> list = new List<String>();
                        IWMPPlaylistArray playlists = wmp.playlistCollection.getAll();
                        int count = playlists.count;
                        for (int i = 0; i < count; i++)
                        {
                            list.Add(playlists.Item(i).name);
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
                    wmp.controls.pause();
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
                    if (wmp.currentMedia == null)
                    {
                        IWMPPlaylistArray plsArr = wmp.playlistCollection.getByName(Parameters.MainPlaylist);
                        if (plsArr.count > 0)
                        {
                            wmp.currentPlaylist = plsArr.Item(0);
                        }
                        else
                        {
                            wmp.currentPlaylist = wmp.mediaCollection.getAll();
                        }
                    }
                    wmp.controls.play();
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
                        return (wmp.playState == WMPPlayState.wmppsPlaying);
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
                    if (wmp.controls.currentPosition <= MediaInfoLib.Consts.GoPreviousPosition)
                    {
                        wmp.controls.previous();
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
                    wmp.controls.currentPosition = position;
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
