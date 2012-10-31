using System;

namespace MediaInfoLib
{
    public interface IMonitorThreadInterface
    {
        /// <summary>
        /// Start monitor thread
        /// </summary>
        void Start();

        /// <summary>
        /// Stop monitor thread
        /// </summary>
        void Stop();

        /// <summary>
        /// Current track being played
        /// </summary>
        String TrackCurrent { get; }

        /// <summary>
        /// Current track album
        /// </summary>
        String TrackAlbum { get; }

        /// <summary>
        /// Current status (track played, track skipped and so on)
        /// </summary>
        String TrackStatus { get; }

        /// <summary>
        /// Player interface object
        /// </summary>
        object PlayerObject { get; }

        /// <summary>
        /// Skip to next track
        /// </summary>
        void NextTrack();

        /// <summary>
        /// Current track duration
        /// </summary>
        int CurrentDuration { get; }

        /// <summary>
        /// Current track position
        /// </summary>
        int CurrentPosition { get; }

        /// <summary>
        /// Player version
        /// </summary>
        String Version { get; }

        /// <summary>
        /// Total tracks in source playlist
        /// </summary>
        int TotalTracks { get; }

        /// <summary>
        /// Total tracks in media library
        /// </summary>
        int LibraryTracks { get; }

        /// <summary>
        /// List of available playlists
        /// </summary>
        String[] Playlists { get; }

        /// <summary>
        /// Returns error message, or NULL if everything is fine
        /// </summary>
        String ErrorState { get; }

        /// <summary>
        /// Returns monitor thread state
        /// </summary>
        MonitorState State { get; }

        /// <summary>
        /// Returns amount of relevant rating samples in keeper
        /// </summary>
        int RelevantSamples { get; }

        /// <summary>
        /// Initiate background statistics calculation process
        /// </summary>
        void RecalculateStatistics();

        /// <summary>
        /// Pause playback
        /// </summary>
        void Pause();

        /// <summary>
        /// Continue playback
        /// </summary>
        void Play();
        
        /// <summary>
        /// Total source media duration
        /// </summary>
        int TotalDuration { get; }
        
        /// <summary>
        /// Is player now playing, or is it paused
        /// </summary>
        bool IsPlaying { get; }

        /// <summary>
        /// Replay current track
        /// </summary>
        void ReplayTrack();

        /// <summary>
        /// Go back one track
        /// </summary>
        void PreviousTrack();

        /// <summary>
        /// Set current position
        /// </summary>
        void SetPosition(int position);
    }

    public interface IPlaylistBuilderInterface : IDisposable
    {
        /// <summary>
        /// Load playlist from media player into memory
        /// </summary>
        void LoadPlaylist();
        
        /// <summary>
        /// Shuffle playlist according to rating
        /// </summary>
        void ShufflePlaylist();

        /// <summary>
        /// Set shuffled playlist to media player
        /// </summary>
        void SetPlaylist();

        /// <summary>
        /// Reset progress indicator
        /// </summary>
        void ResetProgress();

        /// <summary>
        /// Progress indicator of all operations on 0..100 scale
        /// </summary>
        int Progress { get; }
    }

}
