﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3607
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MediaInfoLib.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class InterfaceLabels {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal InterfaceLabels() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MediaInfoLib.Resources.InterfaceLabels", typeof(InterfaceLabels).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ({0}).
        /// </summary>
        public static string AlbumFormat {
            get {
                return ResourceManager.GetString("AlbumFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} - {1}.
        /// </summary>
        public static string CurrentTrackFormat {
            get {
                return ResourceManager.GetString("CurrentTrackFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Runtime error: {0}.
        /// </summary>
        public static string InterfaceErrorFormat {
            get {
                return ResourceManager.GetString("InterfaceErrorFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to  [{0} point].
        /// </summary>
        public static string PointFormat {
            get {
                return ResourceManager.GetString("PointFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to  [{0} points].
        /// </summary>
        public static string PointsFormat {
            get {
                return ResourceManager.GetString("PointsFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Track played: {0} - {1} {2}.
        /// </summary>
        public static string TrackPlayedFormat {
            get {
                return ResourceManager.GetString("TrackPlayedFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Track replayed: {0} - {1} {2}.
        /// </summary>
        public static string TrackReplayedFormat {
            get {
                return ResourceManager.GetString("TrackReplayedFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Track skipped: {0} - {1} {2}.
        /// </summary>
        public static string TrackSkippedFormat {
            get {
                return ResourceManager.GetString("TrackSkippedFormat", resourceCulture);
            }
        }
    }
}