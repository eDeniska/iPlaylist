using System;
using System.Collections.Generic;
using System.Linq;
using MediaInfoLib;
using UtilityLib;
using WMPLib;

namespace WMPInterfaceLib
{
    public class PlaylistBuilder : IPlaylistBuilderInterface
    {
        private IEnumerable<TrackInfo> playlist = null;
        private WindowsMediaPlayer wmp = null;
        private MediaInfoKeeper keeper = null;
        private int progress = -1;
        private IMonitorThreadInterface monitor = null;

        public PlaylistBuilder(MediaInfoKeeper p_keeper, IMonitorThreadInterface p_monitor)
        {
            try
            {
                wmp = p_monitor.PlayerObject as WindowsMediaPlayer;
                keeper = p_keeper;
                monitor = p_monitor;
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
                progress = 0;
                IWMPPlaylist pls = null;
                bool playlistFound = true;

                if (Parameters.UsePlaylist)
                {
                    IWMPPlaylistArray plsArr = wmp.playlistCollection.getByName(Parameters.SourcePlaylist);
                    if (plsArr.count > 0)
                    {
                        pls = plsArr.Item(0);
                        
                    }
                    else
                    {
                        playlistFound = false;
                        pls = wmp.playlistCollection.newPlaylist(Parameters.SourcePlaylist);

                        IWMPPlaylist s = wmp.mediaCollection.getAll();
                        int cnt = s.count;
                        String a = String.Empty;
                        for (int i = 0; i < cnt; i++)
                        {
                            String mediaType = String.Empty;
                            IWMPMedia media = s.get_Item(i);

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
                                pls.appendItem(s.get_Item(i));
                            }
                            progress = 25 * i / cnt;
                        }
                        //Log.Write(a);
                    }
                }
                else
                {
                    pls = wmp.mediaCollection.getAll();
                }

                int current = 0;
                int count = pls.count;
                for (int i = 0; i < count; i++)
                {
                    IWMPMedia media = pls.get_Item(i);
                    
                    String cTrackID = String.Empty;
                    String cArtist = String.Empty;
                    String cAlbum = String.Empty;
                    String cName = String.Empty;
                    String cGenre = String.Empty;
                    String mediaType = String.Empty;
                    int cDuration = 0;

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

                    if ((mediaType.Equals(Consts.MediaType.Audio)) ||
                        ((!(Parameters.SkipNonAudio)) && (mediaType.Equals(Consts.MediaType.Video))))
                    {
                        TrackInfo trackInfo = new TrackInfo(cName, cArtist, cAlbum, cGenre, cTrackID, cDuration, 0);

                        try
                        {
                            trackInfo.Rating = keeper.GetRating(trackInfo);
                        }
                        catch (RatingNotFoundException)
                        {
                            trackInfo.Rating = 0;
                        }

                        trackInfo.TrackObject = media;
                        list.Add(trackInfo);
                    }
                    if (playlistFound)
                    {
                        progress = current * 50 / count;
                    }
                    else
                    {
                        progress = 25 + current * 25 / count;
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
                    IWMPPlaylistArray plsArr = wmp.playlistCollection.getByName(Parameters.MainPlaylist);
                    for (int i = 0; i < plsArr.count; i++)
                    {
                        wmp.playlistCollection.remove(plsArr.Item(i));
                        detected = true;
                    }
                }

                IWMPPlaylist pl = wmp.playlistCollection.newPlaylist(Parameters.MainPlaylist);
                int total = playlist.Count();
                int current = 0;
                
                foreach (TrackInfo ti in playlist)
                {
                    pl.appendItem(ti.TrackObject as IWMPMedia);
                    progress = 50 + (current * 50 / total);
                    current++;
                }
                progress = 100;
                wmp.currentPlaylist = pl;
                wmp.controls.play();
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
            wmp = null;
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
