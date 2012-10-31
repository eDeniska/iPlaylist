using System;

namespace MediaInfoLib
{
    public abstract class Consts
    {
        public const int DataSetVersion = 2;

        public const int SkipBestTracksRatio = 5;
        public const int SkipWorstTracksRatio = 90;

        public const int ReplayPosition = 1;
        public const int GoPreviousPosition = 1;

        public abstract class Tracks
        {
            public const int Error = -2;
            public const int Counting = -1;
        }
        
        public abstract class Files
        {
            public const String RatingFile = "mediaRating.xml";
            public const String SummaryFile = "summary.xml";
        }

        public abstract class Top
        {
            public const String TopAlbumsFormat = "{0,2}. {1} - {2}";
            public const String TopArtistsFormat = "{0,2}. {1}";
            public const String TopTracksFormat = "{0,2}. {1} - {2} ({3})";
        }

    }
}
