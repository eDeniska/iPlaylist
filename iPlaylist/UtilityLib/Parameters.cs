using System;
using Microsoft.Win32;
using System.Windows.Forms;

namespace UtilityLib
{
    public static class Parameters
    {
        private const String sMainWindowTop = "MainWindowTop";
        private const String sMainWindowLeft = "MainWindowLeft";
        private const String sControlsWindowTop = "ControlsWindowTop";
        private const String sControlsWindowLeft = "ControlsWindowLeft";
        private const String sCurrentPlayerPlugin = "CurrentPlayerPlugin";
        private const String sShowPlayerControls = "ShowPlayerControls";
        private const String sMinimizeOnStart = "MinimizeOnStart";
        private const String sSkipWorstAndBest = "SkipWorstAndBest";
        private const String sIgnoreWhenLocked = "IgnoreWhenLocked";
        private const String sIncreaseRandomization = "IncreaseRandomization";
        private const String sUsePlaylist = "UsePlaylist";
        private const String sSkipNonAudio = "SkipNonAudio";
        private const String sSourcePlaylist = "SourcePlaylist";
        private const String sMainPlaylist = "MainPlaylist";
        private const String sCheckForUpdates = "CheckForUpdates";
        private const String sPauseOnLock = "PauseOnLock";
        private const String sLastUpdateDate = "LastUpdateDate";
        private const String sLanguage = "Language";
        private const String sListenMoreUnratedTracks = "ListenMoreUnratedTracks";
        private const String sAllowGlobalHotKeys = "AllowGlobalHotKeys";

        private const String sHotKeyModifiers = "HotKeyModifiers";
        private const String sHotKeyPlayPause = "HotKeyPlayPause";
        private const String sHotKeyNextTrack = "HotKeyNextTrack";
        private const String sHotKeyPrevTrack = "HotKeyPrevTrack";

        private const String sBranch = @"SOFTWARE\iPlaylist";

        private const String DefaultSourcePlaylist = "iPlaylist source";
        private const String DefaultMainPlaylist = "iPlaylist";

        private static String hotKeyModifiers = String.Empty;

        static Parameters()
        {
            try
            {
                Reset();

                RegistryKey branch = Registry.CurrentUser.OpenSubKey(sBranch);
                if (branch != null)
                {
                    MainWindowTop = (int)branch.GetValue(sMainWindowTop, -1);
                    MainWindowLeft = (int)branch.GetValue(sMainWindowLeft, -1);
                    ControlsWindowTop = (int)branch.GetValue(sControlsWindowTop, -1);
                    ControlsWindowLeft = (int)branch.GetValue(sControlsWindowLeft, -1);
                    ControlsWindowLeft = (int)branch.GetValue(sControlsWindowLeft, -1);

                    AllowGlobalHotKeys = Boolean.Parse(branch.GetValue(sAllowGlobalHotKeys, Boolean.TrueString).ToString());
                    ShowPlayerControls = Boolean.Parse(branch.GetValue(sShowPlayerControls, Boolean.TrueString).ToString());
                    MinimizeOnStart = Boolean.Parse(branch.GetValue(sMinimizeOnStart, Boolean.FalseString).ToString());
                    SkipWorstAndBest = Boolean.Parse(branch.GetValue(sSkipWorstAndBest, Boolean.FalseString).ToString());
                    IncreaseRandomization = Boolean.Parse(branch.GetValue(sIncreaseRandomization, Boolean.FalseString).ToString());
                    UsePlaylist = Boolean.Parse(branch.GetValue(sUsePlaylist, Boolean.FalseString).ToString());
                    IgnoreWhenLocked = Boolean.Parse(branch.GetValue(sIgnoreWhenLocked, Boolean.FalseString).ToString());
                    SkipNonAudio = Boolean.Parse(branch.GetValue(sSkipNonAudio, Boolean.FalseString).ToString());
                    CheckForUpdates = Boolean.Parse(branch.GetValue(sCheckForUpdates, Boolean.TrueString).ToString());
                    PauseOnLock = Boolean.Parse(branch.GetValue(sPauseOnLock, Boolean.FalseString).ToString());
                    ListenMoreUnratedTracks = Boolean.Parse(branch.GetValue(sListenMoreUnratedTracks, Boolean.FalseString).ToString());
                                        
                    SourcePlaylist = branch.GetValue(sSourcePlaylist, DefaultSourcePlaylist).ToString();
                    MainPlaylist = branch.GetValue(sMainPlaylist, DefaultMainPlaylist).ToString();

                    Language = branch.GetValue(sLanguage, String.Empty).ToString();

                    HotKeyPlayPause = (Keys)Enum.Parse(typeof(Keys), branch.GetValue(sHotKeyPlayPause,
                        Keys.Insert.ToString()).ToString(), true);
                    HotKeyNextTrack = (Keys)Enum.Parse(typeof(Keys), branch.GetValue(sHotKeyNextTrack,
                        Keys.PageDown.ToString()).ToString(), true);
                    HotKeyPrevTrack = (Keys)Enum.Parse(typeof(Keys), branch.GetValue(sHotKeyPrevTrack,
                        Keys.PageUp.ToString()).ToString(), true);
                    hotKeyModifiers = branch.GetValue(sHotKeyModifiers, "Win").ToString();

                    CurrentPlayerPlugin = (PlayerPlugin)Enum.Parse(typeof(PlayerPlugin), branch.GetValue(sCurrentPlayerPlugin,
                        PlayerPlugin.Unknown.ToString()).ToString(), true);

                    LastUpdateDate = DateTime.Parse(branch.GetValue(sLastUpdateDate, DateTime.MinValue.ToString()).ToString());
                }
            }
            catch (Exception e)
            {
                Log.Write(e);
            }
        }

        public static void Reset()
        {
            MainWindowTop = -1;
            MainWindowLeft = -1;
            ControlsWindowTop = -1;
            ControlsWindowLeft = -1;
            ShowPlayerControls = true;
            MinimizeOnStart = false;
            SkipWorstAndBest = false;
            IncreaseRandomization = false;
            UsePlaylist = false;
            CheckForUpdates = true;
            IgnoreWhenLocked = false;
            SkipNonAudio = false;
            PauseOnLock = false;
            ListenMoreUnratedTracks = false;
            AllowGlobalHotKeys = true;

            SourcePlaylist = DefaultSourcePlaylist;
            MainPlaylist = DefaultMainPlaylist;
            Language = String.Empty;
            CurrentPlayerPlugin = PlayerPlugin.Unknown;
            LastUpdateDate = DateTime.MinValue;

            HotKeyPlayPause = Keys.Insert;
            HotKeyNextTrack = Keys.PageDown;
            HotKeyPrevTrack = Keys.PageUp;
            hotKeyModifiers = "Win";
        }

        public static void Save()
        {
            try
            {
                RegistryKey branch = Registry.CurrentUser.OpenSubKey(sBranch, true);
                if (branch == null)
                {
                    branch = Registry.CurrentUser.CreateSubKey(sBranch);
                }

                if (branch != null)
                {
                    branch.SetValue(sMainWindowTop, MainWindowTop, RegistryValueKind.DWord);
                    branch.SetValue(sMainWindowLeft, MainWindowLeft, RegistryValueKind.DWord);
                    branch.SetValue(sControlsWindowTop, ControlsWindowTop, RegistryValueKind.DWord);
                    branch.SetValue(sControlsWindowLeft, ControlsWindowLeft, RegistryValueKind.DWord);
                    branch.SetValue(sShowPlayerControls, ShowPlayerControls.ToString(), RegistryValueKind.String);
                    branch.SetValue(sMinimizeOnStart, MinimizeOnStart.ToString(), RegistryValueKind.String);
                    branch.SetValue(sSkipWorstAndBest, SkipWorstAndBest.ToString(), RegistryValueKind.String);
                    branch.SetValue(sIncreaseRandomization, IncreaseRandomization.ToString(), RegistryValueKind.String);
                    branch.SetValue(sUsePlaylist, UsePlaylist.ToString(), RegistryValueKind.String);
                    branch.SetValue(sIgnoreWhenLocked, IgnoreWhenLocked.ToString(), RegistryValueKind.String);
                    branch.SetValue(sCheckForUpdates, CheckForUpdates.ToString(), RegistryValueKind.String);
                    branch.SetValue(sSkipNonAudio, SkipNonAudio.ToString(), RegistryValueKind.String);
                    branch.SetValue(sPauseOnLock, PauseOnLock.ToString(), RegistryValueKind.String);
                    branch.SetValue(sListenMoreUnratedTracks, ListenMoreUnratedTracks.ToString(), RegistryValueKind.String);
                    branch.SetValue(sAllowGlobalHotKeys, AllowGlobalHotKeys.ToString(), RegistryValueKind.String);
                    
                    branch.SetValue(sSourcePlaylist, SourcePlaylist, RegistryValueKind.String);
                    branch.SetValue(sMainPlaylist, MainPlaylist, RegistryValueKind.String);
                    branch.SetValue(sLastUpdateDate, LastUpdateDate.ToString(), RegistryValueKind.String);
                    branch.SetValue(sLanguage, Language, RegistryValueKind.String);
                    branch.SetValue(sCurrentPlayerPlugin, CurrentPlayerPlugin.ToString(), RegistryValueKind.String);

                    branch.SetValue(sHotKeyPlayPause, HotKeyPlayPause.ToString(), RegistryValueKind.String);
                    branch.SetValue(sHotKeyNextTrack, HotKeyNextTrack.ToString(), RegistryValueKind.String);
                    branch.SetValue(sHotKeyPrevTrack, HotKeyPrevTrack.ToString(), RegistryValueKind.String);
                    branch.SetValue(sHotKeyModifiers, hotKeyModifiers, RegistryValueKind.String);
                }
            }
            catch (Exception e)
            {
                Log.Write(e);
            }
        }

        public static int MainWindowTop { get; set; }
        public static int MainWindowLeft { get; set; }
        public static int ControlsWindowTop { get; set; }
        public static int ControlsWindowLeft { get; set; }
        public static PlayerPlugin CurrentPlayerPlugin { get; set; }
        public static bool ShowPlayerControls { get; set; }
        public static bool MinimizeOnStart { get; set; }
        public static bool SkipWorstAndBest { get; set; }
        public static bool IncreaseRandomization { get; set; }
        public static bool UsePlaylist { get; set; }
        public static bool CheckForUpdates { get; set; }
        public static bool IgnoreWhenLocked { get; set; }
        public static bool SkipNonAudio { get; set; }
        public static bool PauseOnLock { get; set; }
        public static bool ListenMoreUnratedTracks { get; set; }
        public static bool AllowGlobalHotKeys { get; set; }
        

        public static Keys HotKeyPlayPause { get; set; }
        public static Keys HotKeyNextTrack { get; set; }
        public static Keys HotKeyPrevTrack { get; set; }

        public static int HotKeyModifiers 
        {
            get
            {
                int mod = 0;
                String[] modifiers = hotKeyModifiers.Split(new String[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (String m in modifiers)
                {
                    if (m.ToUpper().Equals("ALT"))
                    {
                        mod |= KeyHook.Alt;
                    } 
                    else if (m.ToUpper().Equals("CONTROL"))
                    {
                        mod |= KeyHook.Control;
                    } 
                    else if (m.ToUpper().Equals("WIN"))
                    {
                        mod |= KeyHook.Win;
                    } 
                    else if (m.ToUpper().Equals("SHIFT"))
                    {
                        mod |= KeyHook.Shift;
                    } 
                }
                return mod;
            }
        }

        public static String SourcePlaylist { get; set; }
        public static String MainPlaylist { get; set; }
        public static String Language { get; set; }
                
        public static DateTime LastUpdateDate { get; set; }
    }
}
