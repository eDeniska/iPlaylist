using System;

namespace MediaInfoLib
{
    public class TrackInfo
    {
        public TrackInfo(String name, String artist, String album, String genre, String trackID, int duration, int position, int rating)
        {
            Name = name;
            Artist = artist;
            Album = album;
            Genre = genre;
            Duration = duration;
            TrackID = trackID;
            Position = position;
            Rating = rating;
            TrackObject = null;
            TrackOrder = 0;
            MinimumDelta = Int32.MaxValue;
            RatingState = TrackRatingState.Unknown;
        }


        public TrackInfo()
            : this (String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, 0, 0, 0)
        {
        }

        public TrackInfo(String name, String artist, String album, String genre, String trackID, int duration, int position)
            : this(name, artist, album, genre, trackID, duration, position, 0)
        {
        }

        public String Name { get; set; }
        public String Artist { get; set; }
        public String Album { get; set; }
        public String Genre { get; set; }
        public String TrackID { get; set; }
        public int Duration { get; set; }
        public int Position { get; set; }
        public int MinimumDelta { get; set; }
        public int Rating { get; set; }
        
        public Double TrackOrder { get; set; }
        public object TrackObject { get; set; }
        public TrackRatingState RatingState { get; set; }

        public override String ToString()
        {
            return String.Format("[{0} {1}] {2} - {3} ({4})", this.RatingState.ToString(), this.Rating, 
                this.Artist, this.Name, this.Album);
        }
    }
}
