using System;

namespace iPlaylist
{
    public abstract class Consts
    {
        public const String Version = "0.9";

        public abstract class Parameters
        {
            public const String FolderName = "iPlaylist";
        }

        public abstract class Maintenance
        {
            public const int AutoUpdatePeriod = 14;
            public const String TrackerURL = "http://code.google.com/p/iplaylist/issues/list";
            public const String UpdateURL = "http://sites.google.com/site/dtazetdinov/iplaylist/current-release.txt";
        }
        
        public abstract class Labels
        {
            public const int BaloonTimeout = 2000;
            public const String EmptyLabel = "...";
            public const String DurationFormat = "{0}:{1:00}";
            public const String LeftFormat = "-{0}:{1:00}";
        }

        public abstract class PlayerControls
        {
            public const int RotationChars = 1;
            public const int RotationLength = 100;
        }

        public abstract class PlaylistStatus
        {
            public const int ThresholdSmall = 40;
            public const int ThresholdMedium = 80;
            public const int ThresholdGood = 95;
        }

        public abstract class PlaylistQuality
        {
            public const int QualityCountingStart = 10;
            public const int ThresholdMedium = 40;
            public const int ThresholdGood = 75;
        }

        public abstract class RatingTop
        {
            public const int TopTracks = 8;
            public const int TopAlbums = 8;
            public const int TopArtists = 8;
        }

        public abstract class LibrarySize
        {
            public const int Small = 500;
        }


    }
}
