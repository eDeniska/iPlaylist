using System;
using UtilityLib;

namespace MediaInfoLib
{
    public class RatingDelta
    {
        public RatingDelta()
            : this(0, 0, 0, 0)
        {
        }

        public RatingDelta(int p_track, int p_album, int p_artist, int p_genre)
        {
            Track = p_track;
            Album = p_album;
            Artist = p_artist;
            Genre = p_genre;
        }

        public int Track { get; set; }
        public int Album { get; set; }
        public int Artist { get; set; }
        public int Genre { get; set; }
    }


    public static class RatingUpdate
    {
        private const int FuzzyDivider = 10;

        // experiments with issue 47 
        // private static RatingDelta trackPlayed = new RatingDelta(10, 1, 1, 1);
        private static RatingDelta trackPlayedFirstTime = new RatingDelta(10, 1, 1, 1);
        private static RatingDelta trackPlayed = new RatingDelta(-9, 3, 3, 3);

        private static RatingDelta trackSkipped = new RatingDelta(-200, -20, -5, -5);
        private static RatingDelta trackHalfPlayed = new RatingDelta(-100, -10, -1, -1);
        private static RatingDelta trackNextSkipped = new RatingDelta(-100, -10, -1, -1);
        private static RatingDelta trackAfterSkip = new RatingDelta(200, 20, 5, 5);
        private static RatingDelta trackNextAfterSkip = new RatingDelta(100, 10, 1, 1);
        
        private static RatingDelta trackReplayed = new RatingDelta(100, 10, 1, 1);

        private static int fuzzyVariation = 10;

        private static int getFuzziedValue(int initial)
        {
            Random rnd = new Random();
            int max = (Math.Abs(initial) / FuzzyDivider) > 0 ? (Math.Abs(initial) / FuzzyDivider) + 1 : 0;

            int delta = rnd.Next(max);
            if ((rnd.Next() % 2) == 0)
            {
                return initial + delta;
            }
            else
            {
                return initial - delta;
            }
        }

        public static RatingDelta TrackPlayed
        {
            get
            {
                if (Parameters.IncreaseRandomization)
                {
                    return new RatingDelta(getFuzziedValue(trackPlayed.Track), getFuzziedValue(trackPlayed.Album),
                        getFuzziedValue(trackPlayed.Artist), getFuzziedValue(trackPlayed.Genre));
                }
                else
                {
                    return trackPlayed;
                }
            }
        }

        public static RatingDelta TrackPlayedFirstTime
        {
            get
            {
                if (Parameters.IncreaseRandomization)
                {
                    return new RatingDelta(getFuzziedValue(trackPlayedFirstTime.Track), getFuzziedValue(trackPlayedFirstTime.Album),
                        getFuzziedValue(trackPlayedFirstTime.Artist), getFuzziedValue(trackPlayedFirstTime.Genre));
                }
                else
                {
                    return trackPlayedFirstTime;
                }
            }
        }

        public static RatingDelta TrackReplayed
        {
            get
            {
                if (Parameters.IncreaseRandomization)
                {
                    return new RatingDelta(getFuzziedValue(trackReplayed.Track), getFuzziedValue(trackReplayed.Album),
                        getFuzziedValue(trackReplayed.Artist), getFuzziedValue(trackReplayed.Genre));
                }
                else
                {
                    return trackReplayed;
                }
            }
        }

        public static RatingDelta TrackHalfPlayed
        {
            get
            {
                if (Parameters.IncreaseRandomization)
                {
                    return new RatingDelta(getFuzziedValue(trackHalfPlayed.Track), getFuzziedValue(trackHalfPlayed.Album),
                        getFuzziedValue(trackHalfPlayed.Artist), getFuzziedValue(trackHalfPlayed.Genre));
                }
                else
                {
                    return trackHalfPlayed;
                }
            }
        }

        public static RatingDelta TrackSkipped
        {
            get
            {
                if (Parameters.IncreaseRandomization)
                {
                    return new RatingDelta(getFuzziedValue(trackSkipped.Track), getFuzziedValue(trackSkipped.Album),
                        getFuzziedValue(trackSkipped.Artist), getFuzziedValue(trackSkipped.Genre));
                }
                else
                {
                    return trackSkipped;
                }
            }
        }

        public static  RatingDelta TrackNextSkipped
        {
            get
            {
                if (Parameters.IncreaseRandomization)
                {
                    return new RatingDelta(getFuzziedValue(trackNextSkipped.Track), getFuzziedValue(trackNextSkipped.Album),
                        getFuzziedValue(trackNextSkipped.Artist), getFuzziedValue(trackNextSkipped.Genre));
                }
                else
                {
                    return trackNextSkipped;
                }
            }
        }

        public static RatingDelta TrackAfterSkip
        {
            get
            {
                if (Parameters.IncreaseRandomization)
                {
                    return new RatingDelta(getFuzziedValue(trackAfterSkip.Track), getFuzziedValue(trackAfterSkip.Album),
                        getFuzziedValue(trackAfterSkip.Artist), getFuzziedValue(trackAfterSkip.Genre));
                }
                else
                {
                    return trackAfterSkip;
                }
            }
        }

        public static RatingDelta TrackNextAfterSkip
        {
            get
            {
                if (Parameters.IncreaseRandomization)
                {
                    return new RatingDelta(getFuzziedValue(trackNextAfterSkip.Track), getFuzziedValue(trackNextAfterSkip.Album),
                        getFuzziedValue(trackNextAfterSkip.Artist), getFuzziedValue(trackNextAfterSkip.Genre));
                }
                else
                {
                    return trackNextAfterSkip;
                }
            }
        }

        public static int FuzzyVariation
        {
            get
            {
                return fuzzyVariation;
            }
        }
    }
}
