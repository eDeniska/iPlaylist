using System;
using System.Collections.Generic;
using System.Linq;
using iTunesLib;
using MediaInfoLib;
using UtilityLib;

namespace iTunesInterfaceLib
{
    public class PlaylistBuilder : IPlaylistBuilderInterface
    {
        private IEnumerable<TrackInfo> playlist = null;
        private iTunesApp iTunes = null;
        private MediaInfoKeeper keeper = null;
        private int progress = -1;
        private IMonitorThreadInterface monitor = null;
        
        public PlaylistBuilder(MediaInfoKeeper p_keeper, IMonitorThreadInterface p_monitor)
        {
            try
            {
                iTunes = p_monitor.PlayerObject as iTunesApp;
                monitor = p_monitor;
                keeper = p_keeper;
            }
            catch (Exception e)
            {
                Log.Write(e);
                throw e;
            }
        }

        public void LoadPlaylist()
        {
            try
            {
                List<TrackInfo> list = new List<TrackInfo>();
                IITPlaylist source = null;
                bool playlistFound = true;
                progress = 0;

                if (Parameters.UsePlaylist)
                {
                    // looking for playlist
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
                    if (source == null)
                    {
                        // creating source playlist and loading it with library contents
                        IITUserPlaylist pl = iTunes.CreatePlaylist(Parameters.SourcePlaylist) as IITUserPlaylist;

                        playlistFound = false;

                        progress = 0;
                        int curr = 0;
                        int cnt = iTunes.LibraryPlaylist.Tracks.Count;

                        foreach (IITTrack trk in iTunes.LibraryPlaylist.Tracks)
                        {
                            if ((trk.SampleRate != 0) || (!(Parameters.SkipNonAudio)))
                            {
                                object trkObj = trk;
                                pl.AddTrack(ref trkObj);
                            }
                            progress = curr * 25 / cnt;
                            curr++;
                        }

                        source = pl;
                    }
                }
                else
                {
                    source = iTunes.LibraryPlaylist;
                }

                int total = source.Tracks.Count;
                int current = 0;
                foreach (IITTrack track in source.Tracks)
                {
                    if ((track.SampleRate != 0) || (!(Parameters.SkipNonAudio)))
                    {
                        TrackInfo trackInfo = new TrackInfo(track.Name, track.Artist, track.Album, track.Genre, track.trackID.ToString(), track.Duration, 0);
                        
                        try
                        {
                            trackInfo.Rating = keeper.GetRating(trackInfo);
                        }
                        catch (RatingNotFoundException)
                        {
                            trackInfo.Rating = 0;
                        }

                        trackInfo.TrackObject = track;
                        list.Add(trackInfo);
                    }
                    if (playlistFound)
                    {
                        progress = current * 50 / total;
                    }
                    else
                    {
                        progress = 25 + current * 25 / total;
                    }

                    current++;
                }
                progress = 50;
                playlist = list;
            }
            catch (Exception e)
            {
                Log.Write(e);
                throw e;
            }
        }

        public void ShufflePlaylist()
        {
            if (playlist == null)
            {
                return;
            }
            playlist = keeper.Shuffle(playlist);
        }

        public void SetPlaylist()
        {
            try
            {
                if (playlist == null)
                {
                    return;
                }
                progress = 50;
                // try to delete all "iPlaylist" playlists...
                bool detected = true;
                while (detected)
                {
                    detected = false;
                    foreach (IITSource src in iTunes.Sources)
                    {
                        if ((src.Kind == ITSourceKind.ITSourceKindLibrary) ||
                            (src.Kind == ITSourceKind.ITSourceKindSharedLibrary))
                        {
                            foreach (IITPlaylist pls in src.Playlists)
                            {
                                if (pls.Name.Equals(Parameters.MainPlaylist))
                                {
                                    try
                                    {
                                        pls.Delete();
                                        detected = true;
                                    }
                                    catch (Exception)
                                    {
                                    }
                                }
                            }
                        }
                    }
                }

                IITUserPlaylist pl = iTunes.CreatePlaylist(Parameters.MainPlaylist) as IITUserPlaylist;
                // !!! remove orderby and use proper shuffled playlist

                int total = playlist.Count();
                int current = 0;
                foreach (TrackInfo ti in playlist)
                {
                    object trk = ti.TrackObject;
                    pl.AddTrack(ref trk);
                    progress = 50 + (current * 50 / total);
                    current++;
                }
                progress = 100;
                pl.PlayFirstTrack();
                monitor.RecalculateStatistics();
            }
            catch (Exception e)
            {
                Log.Write(e);
                throw e;
            }
        }

        public void Dispose()
        {
            iTunes = null;
            playlist = null;
        }

        public int Progress
        {
            get
            {
                return progress;
            }
        }

        public void ResetProgress()
        {
            progress = -1;
        }
    }
}
