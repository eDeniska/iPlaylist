using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Win32;
using UtilityLib;

namespace MediaInfoLib
{
    /// <summary>
    /// Media rating keeper class
    /// </summary>
    public class MediaInfoKeeper
    {
        private MediaDataSet ds;
        private int countSkipped = -1;
        private bool isLocked = false;
        private object sessionLock = new object();
        private String folder;
        private String version;
        
        #region Object creation, load and save

        public MediaInfoKeeper(String p_version)
            : this(p_version, String.Empty)
        {
        }

        public MediaInfoKeeper(String p_version, String p_fileFolder)           
        {
            version = p_version;

            ds = new MediaDataSet();
            folder = p_fileFolder;
            if ((folder.Length > 0) && (!(folder.EndsWith(Path.DirectorySeparatorChar.ToString()))))
            {
                folder += Path.DirectorySeparatorChar;
            }

            if (Parameters.IgnoreWhenLocked)
            {
                SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
            }
        }

        /// <summary>
        /// Load media data from XML file
        /// </summary>
        public void Load()
        {
            try
            {
                ds.ReadXml(folder + Consts.Files.RatingFile);
            }
            catch (Exception e)
            {
                Log.Write(e);
                ds = new MediaDataSet();
            }
            if (ds.System.Count != 1)
            {
                Log.Write(String.Format("System table contains wrong number of rows - [{0}] instead of one, replacing it with defaults", 
                    ds.System.Count));
                
                // we could convert from previous versions....

                // actually this could mean, that we have too old data set...

                ds.System.Clear();
                MediaDataSet.SystemRow s = ds.System.NewSystemRow();
                s.ApplicationVersion = version;
                s.DataSetVersion = Consts.DataSetVersion;
                ds.System.AddSystemRow(s);
                ds.AcceptChanges();
            }
            // now we should have single entry
            if (ds.System[0].DataSetVersion < Consts.DataSetVersion)
            {
                // TODO: consider conversion from old versions
                Log.Write(String.Format("DataSet has version [{0}] instead of [{1}], upgrading DataSet...", ds.System[0].DataSetVersion,
                    Consts.DataSetVersion));

                ds.System[0].DataSetVersion = Consts.DataSetVersion;
            }
            else if (ds.System[0].DataSetVersion != Consts.DataSetVersion)
            {
                Log.Write(String.Format("DataSet has version [{0}] instead of [{1}], possible data loss!", ds.System[0].DataSetVersion,
                    Consts.DataSetVersion));

                ds.System[0].DataSetVersion = Consts.DataSetVersion;
            }

            if (!(ds.System[0].ApplicationVersion.Equals(version)))
            {
                ds.System[0].ApplicationVersion = version;
                Log.Write(String.Format("Application version is [{0}] instead of [{1}]", ds.System[0].ApplicationVersion, version));
            }
        }

        /// <summary>
        /// Save rating data to XML file
        /// </summary>
        public void Save()
        {
            try
            {
                if (folder.Length > 0)
                {
                    // creating directory if it does not exist
                    if (!(Directory.Exists(folder)))
                    {
                        Directory.CreateDirectory(folder);
                    }
                }
                // main data set
                ds.WriteXml(folder + Consts.Files.RatingFile);
                // TODO: handle insufficient rights when saving file...
            }
            catch (Exception e)
            {
                Log.Write(e);
            }
        }

        /// <summary>
        /// Save summary XML file (for viewing in a spreadsheet program, for example)
        /// </summary>
        public void SaveSummary()
        {
            try
            {
                if (folder.Length > 0)
                {
                    // creating directory if it does not exist
                    if (!(Directory.Exists(folder)))
                    {
                        Directory.CreateDirectory(folder);
                    }
                }

                // summary file for viewing in Excel and other programs
                XElement xmlData = new XElement("Rating",
                    from trk in ds.Tracks
                    orderby trk.ArtistsRow.Name, trk.AlbumsRow.Name, trk.Name
                    select new XElement("track", new XElement("Artist", trk.ArtistsRow.Name),
                        new XElement("Album", trk.AlbumsRow.Name),
                        new XElement("Name", trk.Name),
                        new XElement("Genre", trk.GenresRow.Name),
                        new XElement("Rating", trk.Rating + trk.ArtistsRow.Rating + trk.AlbumsRow.Rating + trk.GenresRow.Rating)));
                xmlData.Save(folder + Consts.Files.SummaryFile);
                // TODO: handle insufficient rights when saving file...
            }
            catch (Exception e)
            {
                Log.Write(e);
            }
        }

        #endregion

        #region Rating events (track played, track skipped)

        /// <summary>
        /// Track event handler
        /// </summary>
        /// <param name="p_artist">Artist</param>
        /// <param name="p_album">Album</param>
        /// <param name="p_name">Name</param>
        /// <param name="p_duration">Duration</param>
        /// <param name="p_genre">Genre</param>
        /// <param name="p_trackID">TrackID</param>
        /// <param name="p_delta">Delta values</param>
        private void TrackEvent(String p_artist, String p_album, String p_name, int p_duration, String p_genre, String p_trackID, RatingDelta p_delta)
        {
            try
            {
                if (Parameters.IgnoreWhenLocked)
                {
                    lock (sessionLock)
                    {
                        if (isLocked) return;
                    }
                }

                if (p_artist == null)
                {
                    p_artist = String.Empty;
                }

                if (p_album == null)
                {
                    p_album = String.Empty;
                }

                if (p_name == null)
                {
                    p_name = String.Empty;
                }

                if (p_genre == null)
                {
                    p_genre = String.Empty;
                }

                bool newInfo = false;

                MediaDataSet.ArtistsRow artist = ds.Artists.SingleOrDefault(s => (s.Name.Equals(p_artist.Trim())));
                if (artist == null)
                {
                    artist = ds.Artists.NewArtistsRow();
                    artist.Name = p_artist.Trim();
                    artist.Rating = 0;
                    ds.Artists.AddArtistsRow(artist);
                    newInfo = true;
                }

                MediaDataSet.GenresRow genre = ds.Genres.SingleOrDefault(s => (s.Name.Equals(p_genre.Trim())));
                if (genre == null)
                {
                    genre = ds.Genres.NewGenresRow();
                    genre.Name = p_genre.Trim();
                    genre.Rating = 0;
                    ds.Genres.AddGenresRow(genre);
                    newInfo = true;
                }


                MediaDataSet.AlbumsRow album = ds.Albums.SingleOrDefault(s =>
                    ((s.Name.Equals(p_album.Trim())) && (s.ArtistID == artist.ID)));
                if (album == null)
                {
                    album = ds.Albums.NewAlbumsRow();
                    album.Name = p_album.Trim();
                    album.ArtistsRow = artist;
                    album.Rating = 0;
                    ds.Albums.AddAlbumsRow(album);
                    newInfo = true;
                }

                MediaDataSet.TracksRow track = null;
                if (!newInfo)
                {
                    // trying to lookup track in database
                    track = ds.Tracks.SingleOrDefault(s => ((s.Name.Equals(p_name.Trim())) && (s.ArtistID == artist.ID) &&
                        (s.AlbumID == album.ID) && (s.GenreID == genre.ID)));
                    if (track == null)
                    {
                        newInfo = true;
                    }
                }

                if (newInfo)
                {
                    track = ds.Tracks.NewTracksRow();
                    track.Name = p_name.Trim();
                    track.Duration = p_duration;
                    track.TrackID = p_trackID;
                    track.AlbumsRow = album;
                    track.ArtistsRow = artist;
                    track.GenresRow = genre;
                    track.Rating = 0;
                    ds.Tracks.AddTracksRow(track);
                }

                this.UpdateRatings(track, album, artist, genre, p_delta);

                ds.AcceptChanges();
            }
            catch (Exception e)
            {
                Log.Write(e);
            }
        }

        /// <summary>
        /// Track played event handler
        /// </summary>
        /// <param name="info">TrackInfo object</param>
        public void TrackPlayed(TrackInfo info)
        {
            RatingDelta delta = null;

            if (countSkipped > 0)
            {
                delta = RatingUpdate.TrackAfterSkip;
                countSkipped = 0;
            }
            else if (countSkipped == 0)
            {
                delta = RatingUpdate.TrackNextAfterSkip;
                countSkipped = -1;
            }
            else
            {
                GetRating(info);
                if (info.RatingState == TrackRatingState.Track)
                {
                    delta = RatingUpdate.TrackPlayed;
                }
                else
                {
                    delta = RatingUpdate.TrackPlayedFirstTime;
                }
            }

            ShuffleStatistics.PlaysSinceShuffle++;
            ds.AcceptChanges();

            this.TrackEvent(info.Artist, info.Album, info.Name, info.Duration, info.Genre, info.TrackID, delta);
        }

        /// <summary>
        /// Track replayed event handler
        /// </summary>
        /// <param name="info">TrackInfo object</param>
        public void TrackReplayed(TrackInfo info)
        {
            RatingDelta delta = null;

            if (countSkipped > 0)
            {
                delta = RatingUpdate.TrackAfterSkip;
                countSkipped = 0;
            }
            else if (countSkipped == 0)
            {
                delta = RatingUpdate.TrackNextAfterSkip;
                countSkipped = -1;
            }
            else
            {
                delta = RatingUpdate.TrackReplayed;
            }
            ShuffleStatistics.PlaysSinceShuffle++;
            ds.AcceptChanges();

            this.TrackEvent(info.Artist, info.Album, info.Name, info.Duration, info.Genre, info.TrackID, delta);
        }

        /// <summary>
        /// Track skipped event handler
        /// </summary>
        /// <param name="info">TrackInfo object</param>
        public void TrackSkipped(TrackInfo info)
        {
            RatingDelta delta = null;
            
            if (countSkipped > 0)
            {
                delta = RatingUpdate.TrackNextSkipped;
            }
            else
            {
                if (info.MinimumDelta > info.Duration / 2)
                {
                    delta = RatingUpdate.TrackSkipped;
                    countSkipped = 1;
                }
                else
                {
                    delta = RatingUpdate.TrackHalfPlayed;
                    countSkipped = 0; // little hack, to give less points to next played track
                    // track half-played, substact less points
                }
                
            }
            if ((ShuffleStatistics.SkipsSinceShuffle == -1) && (ShuffleStatistics.SkipsSinceShuffle > 0))
            {

                ShuffleStatistics.SkipsSinceShuffle = 1;
            }
            else
            {
                ShuffleStatistics.SkipsSinceShuffle++;
            }
            ds.AcceptChanges();

            this.TrackEvent(info.Artist, info.Album, info.Name, info.Duration, info.Genre, info.TrackID, delta);
        }

        #endregion

        #region Rating totals

        /// <summary>
        /// Total albums
        /// </summary>
        public int Albums
        {
            get { return ds.Albums.Count; }
        }

        /// <summary>
        /// Total artists
        /// </summary>
        public int Artists
        {
            get { return ds.Artists.Count; }
        }

        /// <summary>
        /// Total genres
        /// </summary>
        public int Genres
        {
            get { return ds.Genres.Count; }
        }

        /// <summary>
        /// Total tracks
        /// </summary>
        public int Tracks
        {
            get { return ds.Tracks.Count; }
        }

        #endregion

        #region Rating information check/update

        private void UpdateRatings(MediaDataSet.TracksRow track, MediaDataSet.AlbumsRow album, MediaDataSet.ArtistsRow artist, MediaDataSet.GenresRow genre, RatingDelta delta)
        {
            track.Rating = UpdateRating(track.Rating, delta.Track);
            album.Rating = UpdateRating(album.Rating, delta.Album);
            artist.Rating = UpdateRating(artist.Rating, delta.Artist);
            genre.Rating = UpdateRating(genre.Rating, delta.Genre);
        }

        /// <summary>
        /// Update rating so that we never exceed int's boundaries
        /// </summary>
        /// <param name="srcRating">Source rating</param>
        /// <param name="updateValue">Update value</param>
        /// <returns>Updated rating</returns>
        private int UpdateRating(int srcRating, int updateValue)
        {
            if (updateValue > 0)
            {
                if ((Int32.MaxValue - updateValue) > srcRating)
                {
                    return srcRating + updateValue;
                }
                else
                {
                    return Int32.MaxValue - 1; // never reach bounds... sorry, that the part of magic :)
                }
            }
            else
            {
                if ((Int32.MinValue - updateValue) < srcRating)
                {
                    return srcRating + updateValue;
                }
                else
                {
                    return Int32.MinValue + 1; // never reach bounds... sorry, that the part of magic :)
                }
            }
        }

        /// <summary>
        /// Get track rating
        /// Could throw RatingNotFoundException when no information in rating database
        /// </summary>
        /// <param name="trackInfo">TrackInfo object</param>
        /// <returns>Track rating</returns>
        public int GetRating(TrackInfo trackInfo)
        {
            if (trackInfo.Name == null)
            {
                trackInfo.Name = String.Empty;
            }
            if (trackInfo.Artist == null)
            {
                trackInfo.Artist = String.Empty;
            }
            if (trackInfo.Album == null)
            {
                trackInfo.Album = String.Empty;
            }
            if (trackInfo.Genre == null)
            {
                trackInfo.Genre = String.Empty;
            }

            MediaDataSet.TracksRow track = ds.Tracks.SingleOrDefault(s =>
                ((s.Name.Equals(trackInfo.Name)) && (s.AlbumsRow.Name.Equals(trackInfo.Album)) && (s.ArtistsRow.Name.Equals(trackInfo.Artist))));

            if (track != null)
            {
                trackInfo.RatingState = TrackRatingState.Track;
                return track.Rating + track.ArtistsRow.Rating + track.AlbumsRow.Rating + track.GenresRow.Rating;
            }
            else
            {
                // need to check cumulative rating
                bool found = false;
                int rating = 0;
                trackInfo.RatingState = TrackRatingState.NotRated;

                MediaDataSet.GenresRow genre = ds.Genres.SingleOrDefault(s => (s.Name.Equals(trackInfo.Genre)));
                if (genre != null)
                {
                    found = true;
                    rating += genre.Rating;
                    trackInfo.RatingState = TrackRatingState.Genre;
                }

                MediaDataSet.ArtistsRow artist = ds.Artists.SingleOrDefault(s => (s.Name.Equals(trackInfo.Artist)));
                if (artist != null)
                {
                    found = true;
                    rating += artist.Rating;
                    trackInfo.RatingState = TrackRatingState.Artist;
                }
                
                MediaDataSet.AlbumsRow album = ds.Albums.SingleOrDefault(s => 
                    ((s.Name.Equals(trackInfo.Album)) && (s.ArtistsRow.Name.Equals(trackInfo.Artist))));
                if (album != null)
                {
                    found = true;
                    rating += album.Rating;
                    trackInfo.RatingState = TrackRatingState.Album;
                }

                if (found)
                {
                    return rating;
                }
                else
                {
                    throw new RatingNotFoundException();
                }
            }
        }

        /// <summary>
        /// Map integer rating to [0..1] scale
        /// </summary>
        /// <param name="rating">Track rating</param>
        /// <returns>Mapped rating</returns>
        private Double MapRating(int rating, int min, int max)
        {
            int fuzzedRating = 0;

            // some magic here...
            Random rnd = new Random();
            if ((rnd.Next() % 2) == 0)
            {
                fuzzedRating = this.UpdateRating(rating, rnd.Next(RatingUpdate.FuzzyVariation));
            }
            else
            {
                fuzzedRating = this.UpdateRating(rating, -rnd.Next(RatingUpdate.FuzzyVariation));
            }

            return MapValue(fuzzedRating, min, max);
        }

        /// <summary>
        /// Map value to [0..1] interval
        /// </summary>
        /// <param name="value">Value to map</param>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <returns>Mapped value</returns>
        private Double MapValue(int value, int min, int max)
        {
            // for easiness of use, considering that max value is always positive
            if (min < 0)
            {
                return (((Double)value + Math.Abs((Double)min)) / (Math.Abs((Double)max) + Math.Abs((Double)min)));
            }
            else
            {
                return (((Double)value - Math.Abs((Double)min)) / (Math.Abs((Double)max) - Math.Abs((Double)min)));
            }
        }

        #endregion

        #region Shuffling

        /// <summary>
        /// Shuffle tracks according to rating
        /// </summary>
        /// <param name="source">TrackInfo list</param>
        /// <returns>Shuffled TrackInfo list</returns>
        public IEnumerable<TrackInfo> Shuffle(IEnumerable<TrackInfo> source)
        {
            try
            {
                IEnumerable<TrackInfo> list = source;
                IEnumerable<TrackInfo> inserts = null;
                int insertCount = 0;

                // divide into rated and unrated if needed
                if (Parameters.ListenMoreUnratedTracks)
                {
                    // considering genre-rated as unrated
                    list = source.Where(w => ((w.RatingState != TrackRatingState.NotRated) && (w.RatingState != TrackRatingState.Genre)));
                    inserts = source.Where(w => ((w.RatingState == TrackRatingState.NotRated) || (w.RatingState == TrackRatingState.Genre)));
                    insertCount = inserts.Count();
                }

                // if parameter is set, skip some tracks
                if (Parameters.SkipWorstAndBest)
                {
                    int first = source.Count() * Consts.SkipBestTracksRatio / 100;
                    int last = source.Count() * Consts.SkipWorstTracksRatio / 100;
                    List<TrackInfo> newList = new List<TrackInfo>();
                    int i = 0;

                    foreach (TrackInfo ti in list.OrderByDescending(o => (o.Rating)))
                    {
                        if (i > first)
                        {
                            if (i < last)
                            {
                                newList.Add(ti);
                            }
                            else
                            {
                                break;
                            }
                        }
                        i++;
                    }

                    list = newList;
                }

                Random rnd = new Random();

                if (list.Count() > 0)
                {
                    int minRating = this.UpdateRating(list.Min(s => (s.Rating)), -RatingUpdate.FuzzyVariation);
                    int maxRating = this.UpdateRating(list.Max(s => (s.Rating)), RatingUpdate.FuzzyVariation);

                    // shuffling magic begins...
                    foreach (TrackInfo ti in list)
                    {
                        //double k = this.MapValue(rnd.Next(85000, 90000), 85000, 90000);
                        ti.TrackOrder = this.MapRating(ti.Rating, minRating, maxRating) * 10 + rnd.NextDouble();
                    }
                }

                if ((Parameters.ListenMoreUnratedTracks) && (insertCount > 0))
                {
                    // shuffling inserts - absolutely random
                    foreach (TrackInfo ti in inserts)
                    {
                        ti.TrackOrder = rnd.NextDouble();
                    }
                    inserts = inserts.OrderByDescending(o => (o.TrackOrder));

                    List<TrackInfo> finalList = new List<TrackInfo>();
                    int listCount = list.Count();

                    if (listCount == 0)
                    {
                        finalList.AddRange(inserts);
                    }
                    else  if (insertCount > listCount)
                    {
                        int j = 0;
                        int i = listCount + insertCount;

                        // firstly adding tracks with positive rating mixed with unrated
                        // then all unrated
                        // then all negative rating
                        foreach (TrackInfo ti in list.OrderByDescending(o => (o.TrackOrder)))
                        {
                            if (ti.Rating >= 0)
                            {
                                ti.TrackOrder = i;
                                i--;
                                finalList.Add(ti);

                                if (j < insertCount)
                                {
                                    TrackInfo ti2 = inserts.ElementAt(j);
                                    ti2.TrackOrder = i;
                                    finalList.Add(ti2);
                                    i--;
                                    j++;
                                }
                            }
                            else
                            {
                                // need to drop all unrated data
                                for (int k = j; k < insertCount; k++)
                                {
                                    TrackInfo ti2 = inserts.ElementAt(k);
                                    ti2.TrackOrder = i;
                                    finalList.Add(ti2);
                                    i--;
                                }
                                j = insertCount;

                                ti.TrackOrder = i;
                                i--;
                                finalList.Add(ti);
                            }

                        }
                    }
                    else
                    {
                        // going list - and inserting unrated one after one
                        int j = 0;
                        int i = listCount + insertCount;
                        foreach (TrackInfo ti in list.OrderByDescending(o => (o.TrackOrder)))
                        {
                            ti.TrackOrder = i;
                            i--;
                            finalList.Add(ti);

                            if (j < insertCount)
                            {
                                TrackInfo ti2 = inserts.ElementAt(j);
                                ti2.TrackOrder = i;
                                finalList.Add(ti2);
                                i--;
                                j++;
                            }
                        }
                    }
                    list = finalList;
                }

                ShuffleStatistics.LastShuffleDate = DateTime.Now;
                ShuffleStatistics.PlaysSinceShuffle = 0;
                ShuffleStatistics.SkipsSinceShuffle = -1; // prevent first skip (since playback will start skipping last track played)
                ds.AcceptChanges();

                return list.OrderByDescending(o => (o.TrackOrder));
            }
            catch (Exception e)
            {
                Log.Write(e);
                return null;
            }
        }

        #endregion

        #region Rating Top's

        /// <summary>
        /// Get top tracks
        /// </summary>
        /// <param name="amount">Number of tracks to return</param>
        /// <returns>List of tracks</returns>
        public String[] GetTopTracks(int amount)
        {
            try
            {
                List<String> list = new List<String>();
                int i = 0;
                foreach (MediaDataSet.TracksRow track in
                    ds.Tracks.OrderByDescending(o => (o.Rating + o.AlbumsRow.Rating + o.ArtistsRow.Rating + o.GenresRow.Rating)))
                {
                    if (!(String.IsNullOrEmpty(track.Name)))
                    {
                        i++;
                        int points = track.Rating + track.AlbumsRow.Rating + track.ArtistsRow.Rating + track.GenresRow.Rating;
                        if ((i > amount) || (points < 0)) break;
                        String line = String.Format(Consts.Top.TopTracksFormat, i, track.ArtistsRow.Name, track.Name, track.AlbumsRow.Name);
                        if ((points == 1) || (points == -1))
                        {
                            line += String.Format(MediaInfoLib.Resources.InterfaceLabels.PointFormat, points);
                        }
                        else
                        {
                            line += String.Format(MediaInfoLib.Resources.InterfaceLabels.PointsFormat, points);
                        }
                        list.Add(line);
                    }
                }

                return list.ToArray();
            }
            catch (Exception e)
            {
                Log.Write(e);
                return null;
            }
        }

        /// <summary>
        /// Get top albums
        /// </summary>
        /// <param name="amount">Number of albums to return</param>
        /// <returns>List of albums</returns>
        public String[] GetTopAlbums(int amount)
        {
            try
            {
                List<String> list = new List<String>();
                int i = 0;
                foreach (MediaDataSet.AlbumsRow album in
                    ds.Albums.OrderByDescending(o => (o.Rating + o.ArtistsRow.Rating)))
                {
                    if (!(String.IsNullOrEmpty(album.Name)))
                    {
                        i++;
                        int points = album.Rating + album.ArtistsRow.Rating;
                        if ((i > amount) || (points < 0)) break;
                        String line = String.Format(Consts.Top.TopAlbumsFormat, i, album.ArtistsRow.Name, album.Name);
                        if ((points == 1) || (points == -1))
                        {
                            line += String.Format(MediaInfoLib.Resources.InterfaceLabels.PointFormat, points);
                        }
                        else
                        {
                            line += String.Format(MediaInfoLib.Resources.InterfaceLabels.PointsFormat, points);
                        }

                        list.Add(line);
                    }
                }

                return list.ToArray();
            }
            catch (Exception e)
            {
                Log.Write(e);
                return null;
            }
        }

        /// <summary>
        /// Get top artists
        /// </summary>
        /// <param name="amount">Number of artists to return</param>
        /// <returns>List of artists</returns>
        public String[] GetTopArtists(int amount)
        {
            try
            {
                List<String> list = new List<String>();
                int i = 0;
                foreach (MediaDataSet.ArtistsRow artist in
                    ds.Artists.OrderByDescending(o => (o.Rating)))
                {
                    if (!(String.IsNullOrEmpty(artist.Name)))
                    {
                        i++;
                        if ((i > amount) || (artist.Rating < 0)) break;
                        String line = String.Format(Consts.Top.TopArtistsFormat, i, artist.Name);
                        if ((artist.Rating == 1) || (artist.Rating == -1))
                        {
                            line += String.Format(MediaInfoLib.Resources.InterfaceLabels.PointFormat, artist.Rating);
                        }
                        else
                        {
                            line += String.Format(MediaInfoLib.Resources.InterfaceLabels.PointsFormat, artist.Rating);
                        }
                        list.Add(line);
                    }
                }

                return list.ToArray();
            }
            catch (Exception e)
            {
                Log.Write(e);
                return null;
            }
        }

        #endregion

        #region Catch session lock/unlock

        private void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if ((e.Reason == SessionSwitchReason.SessionLock) ||
                (e.Reason == SessionSwitchReason.ConsoleDisconnect) ||
                (e.Reason == SessionSwitchReason.RemoteDisconnect))
                
            {
                lock (sessionLock)
                {
                    isLocked = true;
                }
            }
            else if ((e.Reason == SessionSwitchReason.SessionUnlock) ||
                (e.Reason == SessionSwitchReason.ConsoleConnect) ||
                (e.Reason == SessionSwitchReason.RemoteConnect))
            {
                lock (sessionLock)
                {
                    isLocked = false;
                }
            }
        }

        #endregion

        #region Shuffle statistics

        public MediaDataSet.ShuffleStatsRow ShuffleStatistics
        {
            get
            {
                if (ds.ShuffleStats.Count > 1)
                {
                    Log.Write(String.Format("ShuffleStats table contains [{0}] rows, removing all of them...", ds.ShuffleStats.Count));
                    ds.ShuffleStats.Clear();
                    ds.AcceptChanges();
                }

                if (ds.ShuffleStats.Count == 0)
                {
                    MediaDataSet.ShuffleStatsRow row = ds.ShuffleStats.NewShuffleStatsRow();
                    row.LastShuffleDate = DateTime.Now;
                    row.SkipsSinceShuffle = 0;
                    row.PlaysSinceShuffle = 0;
                    ds.ShuffleStats.AddShuffleStatsRow(row);
                    return row;
                }
                else
                {
                    return ds.ShuffleStats[0];
                }
            }
        }

        #endregion

    }
}
