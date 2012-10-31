using System;
using System.Threading;

namespace MediaInfoLib
{
    public abstract class AbstractMonitorThread : IMonitorThreadInterface
    {
        protected object lckThread = new object();
        protected object lckTracks = new object();
        protected  bool quit = false;
        protected String trackStatus = String.Empty;
        protected String trackCurrent = String.Empty;
        protected String trackAlbum = String.Empty;
        protected Thread checkPosThread = null;
        protected MediaInfoKeeper keeper = null;
        protected TrackInfo playingTrack = null;
        protected String errorMessage = null;
        protected MonitorState state = MonitorState.NotStarted;
        protected int libraryTracks = Consts.Tracks.Counting;
        protected int totalTracks = Consts.Tracks.Counting;
        protected int relevantSamples = Consts.Tracks.Counting;
        protected int totalDuration = Consts.Tracks.Counting;

        #region IMonitorThreadInterface Members to be implemented individually

        public abstract void Start();

        public abstract void Stop();

        public abstract object PlayerObject
        {
            get;
        }

        public abstract void NextTrack();

        public abstract String Version
        {
            get;
        }

        public abstract String[] Playlists
        {
            get;
        }

        public abstract bool IsPlaying
        {
            get;
        }

        public abstract void RecalculateStatistics();

        public abstract void Pause();

        public abstract void Play();

        public abstract void PreviousTrack();

        public abstract void SetPosition(int position);

        #endregion

        #region Implemented members (no need to reimplement)

        public String TrackCurrent
        {
            get
            {
                lock (lckTracks)
                {
                    return trackCurrent;
                }
            }
        }

        public String TrackAlbum
        {
            get 
            {
                lock (lckTracks)
                {
                    return trackAlbum;
                }
            }
        }

        public String TrackStatus
        {
            get
            {
                lock (lckTracks)
                {
                    return trackStatus.Replace("&", "&&");
                }
            }
        }

        public int CurrentDuration
        {
            get
            {
                lock (lckTracks)
                {
                    return (playingTrack == null) ? -1 : playingTrack.Duration;
                }
            }
        }

        public int CurrentPosition
        {
            get
            {
                lock (lckTracks)
                {
                    return (playingTrack == null) ? -1 : playingTrack.Position;
                }
            }
        }

        public String ErrorState
        {
            get { return errorMessage; }
        }
        
        public MonitorState State
        {
            get { return state; }
        }

        public int TotalTracks
        {
            get
            {
                return totalTracks;
            }
        }

        public int LibraryTracks
        {
            get
            {
                return libraryTracks;
            }
        }

        public int RelevantSamples
        {
            get { return relevantSamples; }
        }

        public int TotalDuration
        {
            get { return totalDuration; }
        }

        public void ReplayTrack()
        {
            if (this.state == MonitorState.Started)
            {
                SetPosition(0);
            }
        }

        #endregion

    }
}
