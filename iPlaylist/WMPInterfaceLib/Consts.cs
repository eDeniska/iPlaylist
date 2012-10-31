using System;

namespace WMPInterfaceLib
{
    public abstract class Consts
    {
        public const int LastSecondsDelta = 2;

        public abstract class Attributes
        {
            public const String Arist = "Author";
            public const String Album = "AlbumID";
            public const String Name = "Title";
            public const String Genre = @"WM/Genre";
            public const String Duration = "Duration";
            public const String TrackID = "TrackingID";
            public const String MediaType = "MediaType";
        }

        public abstract class MediaType
        {
            public const String Audio = "audio";
            public const String Video = "video";
        }
    }
}
