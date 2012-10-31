using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MediaInfoLib;
using UtilityLib;
using Microsoft.Win32;

namespace iPlaylist
{
    public partial class frmMain : Form
    {
        private frmControls playerControls = null;
        private MediaInfoKeeper keeper = null;
        private IMonitorThreadInterface monitor = null;
        private IPlaylistBuilderInterface builder = null;
        private String lastMonitorStatus = String.Empty;
        private DateTime dateStarted = DateTime.Now;
        private KeyHook keyPrevTrack = null;
        private KeyHook keyNextTrack = null;
        private KeyHook keyPlayPause = null;
        private KeyHook keyMediaPrevTrack = null;
        private KeyHook keyMediaNextTrack = null;
        private KeyHook keyMediaPlayPause = null;
        private KeyHook keyMediaStop = null;
        private bool wasPaused = false;
        
        #region Form openning and closing code

        public frmMain()
        {
            if (UtilityLib.Consts.Locales.EnglishCulture.NativeName.ToLower().Equals(Parameters.Language))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = UtilityLib.Consts.Locales.EnglishCulture;
            }
            else if (UtilityLib.Consts.Locales.RussianCulture.NativeName.ToLower().Equals(Parameters.Language))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = UtilityLib.Consts.Locales.RussianCulture;
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentCulture;
            }

            InitializeComponent();
            tlsProgress.Visible = false;

            updateSettingsTab();

            keyPrevTrack = new KeyHook(this.Handle);
            keyNextTrack = new KeyHook(this.Handle);
            keyPlayPause = new KeyHook(this.Handle);
            keyMediaPrevTrack = new KeyHook(this.Handle);
            keyMediaNextTrack = new KeyHook(this.Handle);
            keyMediaPlayPause = new KeyHook(this.Handle);
            keyMediaStop = new KeyHook(this.Handle);
            
            if ((Parameters.MainWindowLeft != -1) && (Parameters.MainWindowTop != -1))
            {
                this.Left = Parameters.MainWindowLeft;
                this.Top = Parameters.MainWindowTop;
            }

            playerControls = new frmControls(this);

            if (Parameters.MinimizeOnStart)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }

            if (Parameters.CurrentPlayerPlugin == PlayerPlugin.Unknown)
            {
                // need initial setup
                tabsMain.SelectedTab = tabSettings;
                notifyIconMain.ShowBalloonTip(Consts.Labels.BaloonTimeout,
                    String.Format(iPlaylist.Resources.Labels.NotifyBaloonTitleFormat, Consts.Version, String.Empty),
                    iPlaylist.Resources.Labels.NotifyBaloonSettings, ToolTipIcon.Info);
            }
            else
            {
                this.StartOperations(false, false);
            }

            toolTipMainWindow.SetToolTip(pbNowPlaying, String.Empty);
            toolTipMainWindow.Active = true;

            // small easter egg, don't worry :)
            if ((DateTime.Now.Day == 6) && (DateTime.Now.Month == 6))
            {
                pbAbout.Image = iPlaylist.Properties.Resources.AboutHappy;
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            keyNextTrack.Unregister();
            keyPlayPause.Unregister();
            keyPrevTrack.Unregister();
            keyMediaNextTrack.Unregister();
            keyMediaPlayPause.Unregister();
            keyMediaPrevTrack.Unregister();
            keyMediaStop.Unregister();

            if (WindowState == FormWindowState.Normal)
            {
                Parameters.MainWindowLeft = this.Left;
                Parameters.MainWindowTop = this.Top;
            }

            if (playerControls != null)
            {
                playerControls.Close();
            }
            timerScreenUpdate.Stop();


            if (monitor != null)
            {
                // closing in any state to avoid hangs on start
                /* && (monitor.State == MonitorState.Started)) */
                monitor.Stop();
            }

            if (keeper != null)
            {
                keeper.Save();
                keeper.SaveSummary();
            }

            Parameters.Save();
        }

        /// <summary>
        /// Set all controls to correct state according to selected plugin
        /// </summary>
        /// <param name="newPlugin">Plugin was changed in settings</param>
        /// <param name="newKeeper">Keeper settings changed</param>
        private void StartOperations(bool newPlugin, bool newKeeper)
        {
            try
            {
                if ((Parameters.CurrentPlayerPlugin == PlayerPlugin.iTunes) || (Parameters.CurrentPlayerPlugin == PlayerPlugin.WMP))
                {
                    cmbSourcePlaylist.Items.Clear();
                    notifyIconMain.BalloonTipTitle = String.Format(iPlaylist.Resources.Labels.NotifyBaloonTitleFormat, Consts.Version, Parameters.CurrentPlayerPlugin.ToString());
                    notifyIconMain.BalloonTipText = String.Format(iPlaylist.Resources.Labels.NotifyBaloonText);
                    notifyIconMain.Text = String.Format(iPlaylist.Resources.Labels.NotifyBaloonTitleFormat, Consts.Version, Parameters.CurrentPlayerPlugin.ToString());
                    tlsStatusText.Text = iPlaylist.Resources.Labels.DefaultStatus;
                    // force to load version info
                    lblMediaPlayerInfo.Text = Consts.Labels.EmptyLabel;

                    if ((newPlugin) || (newKeeper))
                    {
                        // this means that plugin was changed or usage of profile folder, so we need take care of it...
                        // we need to stop existing monitor and keeper (if needed)
                        if (monitor != null)
                        {
                            if (monitor.State == MonitorState.Started)
                            {
                                monitor.Stop();
                            }
                            monitor = null;

                            if (newKeeper)
                            {
                                if (keeper != null)
                                {
                                    keeper.Save();
                                    keeper.SaveSummary();
                                    keeper = null;
                                }
                            }
                        }
                    }

                    if (keeper == null)
                    {
                        keeper = new MediaInfoKeeper(Consts.Version,
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar +
                            Consts.Parameters.FolderName);
                        keeper.Load();
                    }

                    if (monitor == null)
                    {
                        if (Parameters.CurrentPlayerPlugin == PlayerPlugin.iTunes)
                        {
                            monitor = new iTunesInterfaceLib.MonitorThread(keeper);
                        }
                        else
                        {
                            // this is WMP
                            monitor = new WMPInterfaceLib.MonitorThread(keeper);
                        }

                        if (Parameters.MinimizeOnStart)
                        {
                            notifyIconMain.ShowBalloonTip(Consts.Labels.BaloonTimeout);
                        }
                    }
                    // monitor is set, but not started yet, it will be started by screen update process

                    // starting ticker process, giving it ability to start monitor
                    timerScreenUpdate.Start();

                    if (Parameters.ShowPlayerControls)
                    {
                        showPlayerControlsToolStripMenuItem.Checked = true;
                    }
                    lblVersion.Text = Consts.Version;
                    lblAssembly.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

                    if (Parameters.PauseOnLock)
                    {
                        SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
                    }

                    keyPlayPause.Unregister();
                    keyNextTrack.Unregister();
                    keyPrevTrack.Unregister();
                    keyMediaNextTrack.Unregister();
                    keyMediaPrevTrack.Unregister();
                    keyMediaPlayPause.Unregister();
                    keyMediaStop.Unregister();

                    if (Parameters.AllowGlobalHotKeys)
                    {
                        keyPlayPause.Register(Parameters.HotKeyPlayPause, Parameters.HotKeyModifiers);
                        keyNextTrack.Register(Parameters.HotKeyNextTrack, Parameters.HotKeyModifiers);
                        keyPrevTrack.Register(Parameters.HotKeyPrevTrack, Parameters.HotKeyModifiers);
                        keyMediaNextTrack.Register(Keys.MediaNextTrack, 0);
                        keyMediaPrevTrack.Register(Keys.MediaPreviousTrack, 0);
                        keyMediaPlayPause.Register(Keys.MediaPlayPause, 0);
                        keyMediaStop.Register(Keys.MediaStop, 0);
                    }

                }
                else
                {
                    // TODO: show some error, cannot start operations until plugin is selected
                }
            }
            catch (Exception e)
            {
                Log.Write(e);
            }
        }

        #endregion

        #region Information update (screen update ticks, rating updates)

        /// <summary>
        /// Tick to refresh data in form according to monitor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerScreenUpdate_Tick(object sender, EventArgs e)
        {
            try
            {
                // updating time online
                TimeSpan duration = DateTime.Now.Subtract(dateStarted);
                TimeSpan durShuffle = DateTime.Now.Subtract(keeper.ShuffleStatistics.LastShuffleDate);

                lblTimeOnline.Text = String.Format(iPlaylist.Resources.Labels.TimeOnlineFormat, 
                    duration.Days, duration.Hours, duration.Minutes, duration.Seconds,
                    durShuffle.Days, durShuffle.Hours, durShuffle.Minutes, durShuffle.Seconds);

                if (monitor == null)
                {
                    return;
                }
                else if (monitor.State == MonitorState.Started)
                {
                    if (cmbSourcePlaylist.Items.Count == 0)
                    {
                        cmbSourcePlaylist.Items.AddRange(monitor.Playlists);
                    }
                    
                    // TODO: add autoupdate code and verification
                    if ((Parameters.CheckForUpdates) && (Parameters.LastUpdateDate.AddDays(Consts.Maintenance.AutoUpdatePeriod) < DateTime.Now))
                    {
                        tlsStatusText.Text = iPlaylist.Resources.Labels.UpdateCheckStatus;
                        bgwUpdateCheck.RunWorkerAsync();
                        Parameters.LastUpdateDate = DateTime.Now;
                    }
                    
                    // check, if shuffling is in process, double check, just in case
                    if ((builder != null) && (bgwCreatePlaylist.IsBusy))
                    {
                        tlsProgress.Value = builder.Progress;

                    }
                    else
                    {
                        // touch status bar only when needed
                        // update main window status text (only change if monitor set a new status, do not rewrite other status changes)
                        if ((monitor.TrackStatus.Length > 0) && (!(lastMonitorStatus.Equals(monitor.TrackStatus))))
                        {
                            lastMonitorStatus = monitor.TrackStatus;

                            if (!(tlsStatusText.Text.Equals(monitor.TrackStatus)))
                            {
                                tlsStatusText.Text = monitor.TrackStatus;
                            }
                        }
                    }
                    /*
                    String playTitle = monitor.IsPlaying ? iPlaylist.Resources.PlayerControls.TitlePause : iPlaylist.Resources.PlayerControls.TitlePlay;
                    if (!(btnPlayPause.Text.Equals(playTitle)))
                    {
                        btnPlayPause.Text = playTitle;
                        //toolTipButtons.SetToolTip(btnPlayPause,
                        //    isPlaying ? iPlaylist.Resources.PlayerControls.ToolTipPause : iPlaylist.Resources.PlayerControls.ToolTipPlay);
                    }
                     */
                    if (monitor.IsPlaying)
                    {
                        btnPlayPause.Image = iPlaylist.Properties.Resources.PauseIcon;
                        playPauseToolStripMenuItem.Text = iPlaylist.Resources.PlayerControls.TitlePause;
                    }
                    else
                    {
                        btnPlayPause.Image = iPlaylist.Properties.Resources.PlayIcon;
                        playPauseToolStripMenuItem.Text = iPlaylist.Resources.PlayerControls.TitlePlay;
                    }


                    statisticsChanged();

                    // update Information tab
                    // update Now Playing
                    if (!(lblNowPlaying.Text.Equals(monitor.TrackCurrent)))
                    {
                        lblNowPlaying.Text = monitor.TrackCurrent;
                        if (String.IsNullOrEmpty(monitor.TrackAlbum))
                        {
                            lblAlbumInfo.Text = iPlaylist.Resources.Labels.CurrentNoAlbum;
                            lblAlbumInfo.Text = String.Empty;
                        }
                        else
                        {
                            lblAlbumInfo.Text = String.Format(iPlaylist.Resources.Labels.CurrentAlbumFormat, monitor.TrackAlbum);
                        }

                        // update playlist quality...
                        int played = keeper.ShuffleStatistics.PlaysSinceShuffle;
                        int skipped = keeper.ShuffleStatistics.SkipsSinceShuffle;
                        if (skipped == -1)
                        {
                            skipped = 0;
                        }
                        int total = played + skipped;
                        if (total > 0)
                        {
                            lblPlaylistAccuracy.Text = String.Format(iPlaylist.Resources.Labels.PlaylistQualityFormat, 
                                (played * 100 / total), played, skipped);
                            if (total > Consts.PlaylistQuality.QualityCountingStart)
                            {
                                if ((played * 100 / total) < Consts.PlaylistQuality.ThresholdMedium)
                                {
                                    lblPlaylistAccuracy.ForeColor = Color.Red;
                                }
                                else if ((played * 100 / total) < Consts.PlaylistQuality.ThresholdGood)
                                {
                                    lblPlaylistAccuracy.ForeColor = Color.Orange;
                                }
                                else
                                {
                                    lblPlaylistAccuracy.ForeColor = Color.Green;
                                }
                            }
                            else
                            {
                                lblPlaylistAccuracy.ForeColor = Color.Black;
                            }
                        }
                        else
                        {
                            lblPlaylistAccuracy.ForeColor = Color.Black;
                            lblPlaylistAccuracy.Text = Consts.Labels.EmptyLabel;
                        }
                        // also updating player controls

                        if (playerControls != null)
                        {
                            if (monitor.TrackCurrent.Length > 0)
                            {
                                TimeSpan trackDuration = TimeSpan.FromSeconds(monitor.CurrentDuration);
                                playerControls.NowPlaying = String.Format(iPlaylist.Resources.PlayerControls.NowPlayingFormat,
                                    monitor.TrackCurrent, trackDuration.Minutes, trackDuration.Seconds);
                                
                                /*
                                if (String.IsNullOrEmpty(monitor.TrackAlbum))
                                {
                                    playerControls.NowPlayingToolTip = String.Format(iPlaylist.Resources.PlayerControls.ToolTipNoAlbumFormat,
                                        monitor.TrackCurrent, trackDuration.Minutes, trackDuration.Seconds);
                                }
                                else
                                {
                                    playerControls.NowPlayingToolTip = String.Format(iPlaylist.Resources.PlayerControls.ToolTipFormat,
                                        monitor.TrackCurrent, trackDuration.Minutes, trackDuration.Seconds, monitor.TrackAlbum);
                                }
                                 */ 
                            }
                            else
                            {
                                playerControls.NowPlaying = iPlaylist.Resources.PlayerControls.PlayerInactive;
                            }
                        }

                        // update Notify Icon
                        // update tool tip text
                        notifyIconMain.Text = String.Format(iPlaylist.Resources.Labels.StatsNotifyFormat, Consts.Version, Parameters.CurrentPlayerPlugin.ToString(),
                            (keeper.Albums + keeper.Artists + keeper.Genres + keeper.Tracks));

                        // update Rating tab, if needed
                        this.ratingChanged();

                    }

                    // update playerControls
                    // update NowPlaying label
                    // update NowPlaying tooltip
                    if (playerControls != null)
                    {
                        playerControls.SetPlaying(monitor.IsPlaying);
                        /*
                        if (monitor.TrackCurrent.Length > 0)
                        {
                            if (playerControls.NowPlaying.Length > Consts.PlayerControls.RotationLength)
                            {
                                playerControls.NowPlaying = playerControls.NowPlaying.Substring(Consts.PlayerControls.RotationChars);
                            }
                            else if (playerControls.NowPlaying.Length > 0)
                            {
                                TimeSpan trackDuration = TimeSpan.FromSeconds(monitor.CurrentDuration);

                                playerControls.NowPlaying = String.Format(iPlaylist.Resources.PlayerControls.RotationFormat,
                                    playerControls.NowPlaying.Substring(Consts.PlayerControls.RotationChars),
                                    String.Format(iPlaylist.Resources.PlayerControls.NowPlayingFormat, monitor.TrackCurrent,
                                    trackDuration.Minutes, trackDuration.Seconds));
                            }
                        }
                         */ 
                    }

                    // update progress bar

                    updateProgressBar();

                    // update media player info
                    if (lblMediaPlayerInfo.Text.Equals(Consts.Labels.EmptyLabel))
                    {
                        lblMediaPlayerInfo.Text = monitor.Version;
                        lblMediaVersion.Text = monitor.Version;

                    }

                    // update rating database info
                    lblRatingDatabaseInfo.Text = String.Format(iPlaylist.Resources.Labels.RatingLabelFormat, keeper.Genres, keeper.Artists, keeper.Albums,
                        keeper.Tracks);
                }
                else if (monitor.State == MonitorState.Error)
                {
                    // stopping timer, showing error message and quiting application... this should be a good solution
                    timerScreenUpdate.Stop();

                    // changing settings to select plugin on next start
                    Parameters.CurrentPlayerPlugin = PlayerPlugin.Unknown;

                    this.Show();
                    this.WindowState = FormWindowState.Normal;
                    this.Activate();
                    MessageBox.Show(String.Format(iPlaylist.Resources.Labels.PluginErrorFormat, monitor.ErrorState),
                        iPlaylist.Resources.Labels.FatalErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                else if (monitor.State == MonitorState.Interrupted)
                {
                    // stopping timer, showing error message and quiting application... this should be a good solution
                    timerScreenUpdate.Stop();

                    this.Show();
                    this.WindowState = FormWindowState.Normal;
                    this.Activate();
                    MessageBox.Show(String.Format(iPlaylist.Resources.Labels.PlaybackInterruptedFormat, monitor.ErrorState),
                        iPlaylist.Resources.Labels.FatalErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                else if (monitor.State != MonitorState.Starting)
                {
                    monitor.Start();
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }
        }

        private void updateProgressBar()
        {
            if ((monitor != null) && (monitor.State == MonitorState.Started) && (monitor.CurrentPosition >= 0) && (monitor.CurrentDuration > 0))
            {
                if (pbNowPlaying.Maximum != monitor.CurrentDuration)
                {
                    pbNowPlaying.Maximum = monitor.CurrentDuration;
                }
                if (pbNowPlaying.Value != monitor.CurrentPosition)
                {
                    if (monitor.CurrentPosition > monitor.CurrentDuration) 
                    {
                        pbNowPlaying.Value = monitor.CurrentDuration;   
                    }
                    else
                    {
                        pbNowPlaying.Value = monitor.CurrentPosition;
                    }
                }
                TimeSpan current = TimeSpan.FromSeconds(monitor.CurrentPosition);
                String currentPos = String.Format(Consts.Labels.DurationFormat, current.Minutes, current.Seconds);
                lblCurrentPosition.Text = currentPos;

                // no need for tool tip now
                //toolTipMainWindow.SetToolTip(pbNowPlaying, currentPos);

                TimeSpan left = TimeSpan.FromSeconds(monitor.CurrentDuration - monitor.CurrentPosition);
                lblCurrentLeft.Text = String.Format(Consts.Labels.LeftFormat, left.Minutes, left.Seconds);
                playerControls.SetProgress(monitor.CurrentPosition * 100 / monitor.CurrentDuration);
            }
        }

        private void statisticsChanged()
        {
            if (lbliPlaylistDataStatus.Text.Equals(Consts.Labels.EmptyLabel))
            {
                // update data status info
                if (monitor.TotalTracks == MediaInfoLib.Consts.Tracks.Error)
                {
                    lbliPlaylistDataStatus.ForeColor = Color.Red;
                    lbliPlaylistDataStatus.Text = iPlaylist.Resources.PlaylistStatus.Error;
                }
                else if (monitor.TotalTracks != MediaInfoLib.Consts.Tracks.Counting)
                {
                    int ratio = (100 * (monitor.RelevantSamples)) / monitor.TotalTracks;

                    if (ratio < Consts.PlaylistStatus.ThresholdSmall)
                    {
                        lbliPlaylistDataStatus.ForeColor = Color.Red;
                        lbliPlaylistDataStatus.Text = iPlaylist.Resources.PlaylistStatus.Bad;
                    }
                    else if (ratio < Consts.PlaylistStatus.ThresholdMedium)
                    {
                        lbliPlaylistDataStatus.ForeColor = Color.Orange;
                        lbliPlaylistDataStatus.Text = iPlaylist.Resources.PlaylistStatus.Small;
                    }
                    else if (ratio < Consts.PlaylistStatus.ThresholdGood)
                    {
                        lbliPlaylistDataStatus.ForeColor = Color.Orange;
                        lbliPlaylistDataStatus.Text = iPlaylist.Resources.PlaylistStatus.Medium;
                    }
                    else
                    {
                        lbliPlaylistDataStatus.ForeColor = Color.Green;
                        lbliPlaylistDataStatus.Text = iPlaylist.Resources.PlaylistStatus.Good;
                    }

                    TimeSpan ts = TimeSpan.FromSeconds(monitor.TotalDuration);
                    lblSourceMediaDuration.Text = String.Format(iPlaylist.Resources.Labels.SourceDurationFormat,
                        ts.Days, ts.Hours, ts.Minutes, ts.Seconds, monitor.TotalTracks);

                }
            }
            else if ((monitor.TotalTracks == MediaInfoLib.Consts.Tracks.Counting) || 
                (monitor.TotalTracks == MediaInfoLib.Consts.Tracks.Error))
            {
                lbliPlaylistDataStatus.Text = Consts.Labels.EmptyLabel;
                lbliPlaylistDataStatus.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Put updated rating info into Rating tab
        /// </summary>
        private void ratingChanged()
        {
            // TODO: fill up with the code
            // tracks
            lbTopTracks.Items.Clear();
            lbTopTracks.Items.AddRange(keeper.GetTopTracks(Consts.RatingTop.TopTracks));

            // albums
            lbTopAlbums.Items.Clear();
            lbTopAlbums.Items.AddRange(keeper.GetTopAlbums(Consts.RatingTop.TopAlbums));

            // artists
            lbTopArtists.Items.Clear();
            lbTopArtists.Items.AddRange(keeper.GetTopArtists(Consts.RatingTop.TopArtists));
        }

        #endregion

        #region Settings/Parameters matching

        /// <summary>
        /// Set values on settings tab according to Parameters
        /// </summary>
        private void updateSettingsTab()
        {
            // plugin

            cmbMediaPlayer.Items.Clear();
            cmbMediaPlayer.Items.Add(iPlaylist.Resources.Plugins.iTunes);
            cmbMediaPlayer.Items.Add(iPlaylist.Resources.Plugins.WMP);

            if (Parameters.CurrentPlayerPlugin == PlayerPlugin.iTunes)
            {
                cmbMediaPlayer.Text = iPlaylist.Resources.Plugins.iTunes;
            }
            else if (Parameters.CurrentPlayerPlugin == PlayerPlugin.WMP)
            {
                cmbMediaPlayer.Text = iPlaylist.Resources.Plugins.WMP;
            }
            else
            {
                cmbMediaPlayer.Text = iPlaylist.Resources.Plugins.Unknown;
            }

            // language
            cmbLanguage.Items.Clear();
            cmbLanguage.Items.Add(iPlaylist.Resources.Labels.SystemLocale);
            cmbLanguage.Items.Add(UtilityLib.Consts.Locales.EnglishCulture.NativeName.ToLower());
            cmbLanguage.Items.Add(UtilityLib.Consts.Locales.RussianCulture.NativeName.ToLower());
            if (Parameters.Language.Equals(UtilityLib.Consts.Locales.EnglishCulture.NativeName.ToLower()))
            {
                cmbLanguage.Text = UtilityLib.Consts.Locales.EnglishCulture.NativeName.ToLower();
            }
            else if (Parameters.Language.Equals(UtilityLib.Consts.Locales.RussianCulture.NativeName.ToLower()))
            {
                cmbLanguage.Text = UtilityLib.Consts.Locales.RussianCulture.NativeName.ToLower();
            }
            else
            {
                cmbLanguage.Text = iPlaylist.Resources.Labels.SystemLocale;
            }

            // playlist generation
            cbIgnoreWhenLocked.Checked = Parameters.IgnoreWhenLocked;
            cbSkipBestWorst.Checked = Parameters.SkipWorstAndBest;
            cbIncreaseRandomness.Checked = Parameters.IncreaseRandomization;
            cbUseTwoPlaylists.Checked = Parameters.UsePlaylist;
            if (cbUseTwoPlaylists.Checked)
            {
                lblSourcePlaylist.Enabled = true;
                cmbSourcePlaylist.Enabled = true;
            }
            else
            {
                lblSourcePlaylist.Enabled = false;
                cmbSourcePlaylist.Enabled = false;
            }
            cbSkipNonAudio.Checked = Parameters.SkipNonAudio;

            cmbSourcePlaylist.Text = Parameters.SourcePlaylist;
            tbPlayablePlaylist.Text = Parameters.MainPlaylist;

            cbPauseOnLock.Checked = Parameters.PauseOnLock;

            cbListenMoreUnrated.Checked = Parameters.ListenMoreUnratedTracks;

            // iPlaylist options
            cbShowPlayerControls.Checked = Parameters.ShowPlayerControls;
            cbStartMinimized.Checked = Parameters.MinimizeOnStart;
            cbCheckForUpdates.Checked = Parameters.CheckForUpdates;
            cbAllowGlobalHotKeys.Checked = Parameters.AllowGlobalHotKeys;

            // TODO: temporary disabling buttons, functionality is not ready
        }

        /// <summary>
        /// Update Parameters from Settings tab
        /// </summary>
        private void updateFromSettings()
        {
            // plugin

            if (cmbMediaPlayer.Text.Equals(iPlaylist.Resources.Plugins.iTunes))
            {
                Parameters.CurrentPlayerPlugin = PlayerPlugin.iTunes;
            }
            else if (cmbMediaPlayer.Text.Equals(iPlaylist.Resources.Plugins.WMP))
            {
                Parameters.CurrentPlayerPlugin = PlayerPlugin.WMP;
            }
            else
            {
                Parameters.CurrentPlayerPlugin = PlayerPlugin.Unknown;
            }

            // language
            if (cmbLanguage.Text.Equals(UtilityLib.Consts.Locales.EnglishCulture.NativeName.ToLower()))
            {
                Parameters.Language = cmbLanguage.Text;
            }
            else if (cmbLanguage.Text.Equals(UtilityLib.Consts.Locales.RussianCulture.NativeName.ToLower()))
            {
                Parameters.Language = cmbLanguage.Text;
            }
            else
            {
                Parameters.Language = String.Empty;
            }
            
            // playlist generation
            Parameters.IgnoreWhenLocked = cbIgnoreWhenLocked.Checked;
            Parameters.SkipWorstAndBest = cbSkipBestWorst.Checked;
            Parameters.IncreaseRandomization = cbIncreaseRandomness.Checked;
            Parameters.UsePlaylist = cbUseTwoPlaylists.Checked;
            Parameters.SourcePlaylist = cmbSourcePlaylist.Text;
            Parameters.MainPlaylist = tbPlayablePlaylist.Text;
            Parameters.SkipNonAudio = cbSkipNonAudio.Checked;
            Parameters.PauseOnLock = cbPauseOnLock.Checked;
            Parameters.ListenMoreUnratedTracks = cbListenMoreUnrated.Checked;
            
            // iPlaylist options
            Parameters.ShowPlayerControls = cbShowPlayerControls.Checked;
            Parameters.MinimizeOnStart = cbStartMinimized.Checked;
            Parameters.CheckForUpdates = cbCheckForUpdates.Checked;
            Parameters.AllowGlobalHotKeys = cbAllowGlobalHotKeys.Checked;
        }

        private void cbUseTwoPlaylists_CheckedChanged(object sender, EventArgs e)
        {
            if (cbUseTwoPlaylists.Checked)
            {
                lblSourcePlaylist.Enabled = true;
                cmbSourcePlaylist.Enabled = true;
            }
            else
            {
                lblSourcePlaylist.Enabled = false;
                cmbSourcePlaylist.Enabled = false;
            }
        }

        private void cbPauseOnLock_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPauseOnLock.Checked)
            {
                cbIgnoreWhenLocked.Enabled = false;
            }
            else
            {
                cbIgnoreWhenLocked.Enabled = true;
            }
        }

        /// <summary>
        /// Settings Save button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            PlayerPlugin previousPlugin = Parameters.CurrentPlayerPlugin;
            bool prevIgnore = Parameters.IgnoreWhenLocked;
            bool prevRandom = Parameters.IncreaseRandomization;
            String lang = Parameters.Language;
            
            // gather parameters from settings tab, put into Parameters class
            updateFromSettings();

            Parameters.Save();

            if (!(Parameters.Language.Equals(lang)))
            {
                MessageBox.Show(iPlaylist.Resources.Labels.LanguageChangeText, iPlaylist.Resources.Labels.LanguageChangeTitle, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            this.StartOperations(previousPlugin != Parameters.CurrentPlayerPlugin, 
                ((prevRandom != Parameters.IncreaseRandomization) ||
                 (prevIgnore != Parameters.IgnoreWhenLocked)));
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            PlayerPlugin previousPlugin = Parameters.CurrentPlayerPlugin;
            Parameters.Reset();
            Parameters.CurrentPlayerPlugin = previousPlugin;
            playerControls.Reposition();
            updateSettingsTab();
        }

        #endregion

        #region Library size buttons

        private void btnBigLibrary_Click(object sender, EventArgs e)
        {
            Parameters.UsePlaylist = true;
            Parameters.SkipWorstAndBest = true;
            Parameters.IncreaseRandomization = true;
            Parameters.ListenMoreUnratedTracks = true;

            cbUseTwoPlaylists.Checked = Parameters.UsePlaylist;
            cbSkipBestWorst.Checked = Parameters.SkipWorstAndBest;
            cbIncreaseRandomness.Checked = Parameters.IncreaseRandomization;
        }

        private void btnSmallLibrary_Click(object sender, EventArgs e)
        {
            Parameters.UsePlaylist = false;
            Parameters.SkipWorstAndBest = false;
            Parameters.IncreaseRandomization = false;
            Parameters.ListenMoreUnratedTracks = false;

            cbUseTwoPlaylists.Checked = Parameters.UsePlaylist;
            cbSkipBestWorst.Checked = Parameters.SkipWorstAndBest;
            cbIncreaseRandomness.Checked = Parameters.IncreaseRandomization;
        }

        private void btnAutodetectLibrary_Click(object sender, EventArgs e)
        {
            if ((monitor != null) && (monitor.State == MonitorState.Started) && (monitor.LibraryTracks > 0))
            {
                if (monitor.LibraryTracks > Consts.LibrarySize.Small)
                {
                    btnBigLibrary_Click(sender, e);
                }
                else
                {
                    btnSmallLibrary_Click(sender, e);
                }
            }
        }

        #endregion
        
        #region Public form actions

        public void ActionNextTrack()
        {
            if ((monitor != null) && (monitor.State == MonitorState.Started))
            {
                monitor.NextTrack();
            }
        }

        public void ActionShuffle()
        {
            // TODO: finish buttons

            // protection from running shuffle twice
            if (!(bgwCreatePlaylist.IsBusy))
            {
                bgwCreatePlaylist.RunWorkerAsync();

                // change main window status line
                tlsStatusText.Text = String.Format(iPlaylist.Resources.Labels.PlaylistGenerationStatus);

                // change notify icon tool tip
                notifyIconMain.Text = String.Format(iPlaylist.Resources.Labels.PlaylistGenerationNotifyFormat, Consts.Version, 
                    Parameters.CurrentPlayerPlugin.ToString());
                
                // disable shuffle buttons and menu items for better user experience
                //playerControls.DisableButtons();
                btnShuffle.Enabled = false;
                shufflePlaylistToolStripMenuItem.Enabled = false;
                
                tlsProgress.Value = 0;
                tlsProgress.Visible = true;
            }
        }

        public void ActionExitPlayer()
        {
            // TODO: modify code to exit player?..
            this.Close();
        }

        public void ActionHidePlayerControls()
        {
            showPlayerControlsToolStripMenuItem.Checked = false;
        }

        public void ActionPreviousTrack()
        {
            if ((monitor != null) && (monitor.State == MonitorState.Started))
            {
                monitor.PreviousTrack();
            }
        }

        public void ActionPlayPause()
        {
            if ((monitor != null) && (monitor.State == MonitorState.Started))
            {
                if (monitor.IsPlaying)
                {
                    monitor.Pause();
                }
                else
                {
                    monitor.Play();
                }
            }
        }

        public void ActionPause()
        {
            if ((monitor != null) && (monitor.State == MonitorState.Started))
            {
                if (monitor.IsPlaying)
                {
                    monitor.Pause();
                }
            }
        }

        #endregion

        #region Context menu actions

        private void exitIPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void shufflePlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ActionShuffle();
        }

        private void nextTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ActionNextTrack();
        }

        private void prevTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ActionPreviousTrack();
        }

        private void playPauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ActionPlayPause();
        }

        private void showPlayerControlsToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (showPlayerControlsToolStripMenuItem.Checked)
            {
                playerControls.Show();
                playerControls.WindowState = FormWindowState.Normal;
            }
            else
            {
                playerControls.Hide();
            }
        }

        private void showMainWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            WindowState = FormWindowState.Normal;
            this.Activate();
        }

        #endregion
        
        #region Create playlist background worker

        private void bgwCreatePlaylist_DoWork(object sender, DoWorkEventArgs e)
        {
            if (Parameters.CurrentPlayerPlugin == PlayerPlugin.WMP)
            {
                builder = new WMPInterfaceLib.PlaylistBuilder(keeper, monitor);
            }
            else if (Parameters.CurrentPlayerPlugin == PlayerPlugin.iTunes)
            {
                builder = new iTunesInterfaceLib.PlaylistBuilder(keeper, monitor);
            }
            else
            {
                // TODO: probalby, we'll need to show an error here...
                return;
            }

            builder.LoadPlaylist();
            builder.ShufflePlaylist();
            builder.SetPlaylist();
            builder = null;
        }

        private void bgwCreatePlaylist_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // update status line
            tlsStatusText.Text = String.Format(iPlaylist.Resources.Labels.PlaylistCompleteStatus);

            // update notify icon text
            notifyIconMain.Text = String.Format(iPlaylist.Resources.Labels.PlaylistCompleteNotifyFormat, Consts.Version, Parameters.CurrentPlayerPlugin.ToString());

            // show notify icon baloon
            notifyIconMain.ShowBalloonTip(Consts.Labels.BaloonTimeout,
                String.Format(iPlaylist.Resources.Labels.NotifyBaloonTitleFormat, Consts.Version, Parameters.CurrentPlayerPlugin.ToString()),
                String.Format(iPlaylist.Resources.Labels.PlaylistCompleteBaloonTextFormat),
                ToolTipIcon.Info);

            // reenable buttons and menu items
            btnShuffle.Enabled = true;
            shufflePlaylistToolStripMenuItem.Enabled = true;
            //playerControls.EnableButtons();
            
            tlsProgress.Visible = false;
        }

        #endregion

        #region Auto update check background worker

        private void bgwUpdateCheck_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                UpdateHelper helper = new UpdateHelper(Consts.Maintenance.UpdateURL, Consts.Version);
                bool UpdateAvailable = helper.IsUpdateAvailable();
                e.Result = helper;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                e.Result = ex;
            }
        }

        private void bgwUpdateCheck_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if ((e.Result == null) || (e.Result is Exception))
                {
                    tlsStatusText.Text = iPlaylist.Resources.Labels.UpdateFailedStatus;
                    this.Show();
                    this.WindowState = FormWindowState.Normal;
                    this.Activate();

                    String message = "Unknown error";
                    if (e.Result != null)
                    {
                        message = (e.Result as Exception).Message;
                    }

                    // update process failed
                    MessageBox.Show(String.Format(iPlaylist.Resources.Maintenance.UpdateErrorTextFormat, message),
                        iPlaylist.Resources.Maintenance.UpdateErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    UpdateHelper helper = e.Result as UpdateHelper;
                    if (!(Consts.Version.Equals(helper.AvailableVersion())))
                    {
                        this.Show();
                        this.WindowState = FormWindowState.Normal;
                        this.Activate();

                        if (MessageBox.Show(String.Format(iPlaylist.Resources.Maintenance.UpdateTextFormat, Consts.Version, helper.AvailableVersion()),
                            iPlaylist.Resources.Maintenance.UpdateTitle,
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button1) == DialogResult.OK)
                        {
                            tlsStatusText.Text = iPlaylist.Resources.Labels.UpdateDownloadingStatus;
                            helper.StartUpdate(this);
                        }
                    }
                    else
                    {
                        tlsStatusText.Text = iPlaylist.Resources.Labels.UpdatesNotFoundStatus;
                    }
                }
            }
            catch (Exception ex)
            {
                tlsStatusText.Text = iPlaylist.Resources.Labels.UpdateFailedStatus;
                MessageBox.Show(String.Format(iPlaylist.Resources.Maintenance.UpdateErrorTextFormat, ex.Message),
                    iPlaylist.Resources.Maintenance.UpdateErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Write(ex);
            }
        }

        #endregion

        #region Hiding and restoring main window (not from context menu)
        
        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (Parameters.MinimizeOnStart)
            {
                this.Hide();
            }
        }

        private void notifyIconMain_MouseClick(object sender, MouseEventArgs e)
        {
            // do not handle right mouse button click - it is context-menu
            if (e.Button == MouseButtons.Right)
            {
                return;
            }

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }
        }

        #endregion

        #region Information tab buttons

        private void btnShuffle_Click(object sender, EventArgs e)
        {
            this.ActionShuffle();
        }

        private void btnNextTrack_Click(object sender, EventArgs e)
        {
            this.ActionNextTrack();
        }

        private void btnPrevTrack_Click(object sender, EventArgs e)
        {
            this.ActionPreviousTrack();
        }

        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            this.ActionPlayPause();
        }

        private void pbNowPlaying_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left) && (monitor != null) && (monitor.State == MonitorState.Started))
            {
                if ((e.Location.X >= 0) && (e.Location.X <= pbNowPlaying.Size.Width))
                {
                    int pos = ((e.Location.X /*- pbNowPlaying.Location.X*/) * monitor.CurrentDuration) / pbNowPlaying.Size.Width;
                    monitor.SetPosition(pos);
                    this.updateProgressBar();
                }
            }
        }

        #endregion
           
        #region About buttons

        private void btnBugReport_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Consts.Maintenance.TrackerURL);
            }
            catch (Exception ex)
            {
                // TODO: show message box with error!
                Log.Write(ex);
            }

            /*
            IssueTracker trk = new IssueTracker(Consts.BugReporting.ProjectName);
            String title = String.Empty;
            String description = String.Empty;
            // need to gather information
            if (trk.SubmitIssue(Consts.Version, title, description))
            {
                // TODO: show message box that issues was submitted to tracker
            }
            else
            {
                trk.OpenTrackerLink();
                // TODO: show message box regarding issues with using tracking library
            }
             */
        }

        private void btnCheckForUpdates_Click(object sender, EventArgs e)
        {
            try
            {
                tlsStatusText.Text = iPlaylist.Resources.Labels.UpdateCheckStatus;
                UpdateHelper helper = new UpdateHelper(Consts.Maintenance.UpdateURL, Consts.Version);
                if (helper.IsUpdateAvailable())
                {
                    if (MessageBox.Show(String.Format(iPlaylist.Resources.Maintenance.UpdateTextFormat, Consts.Version, helper.AvailableVersion()),
                        iPlaylist.Resources.Maintenance.UpdateTitle, 
                        MessageBoxButtons.OKCancel, 
                        MessageBoxIcon.Question, 
                        MessageBoxDefaultButton.Button1) == DialogResult.OK)
                    {
                        tlsStatusText.Text = iPlaylist.Resources.Labels.UpdateDownloadingStatus;
                        helper.StartUpdate(this);
                    }

                }
                else
                {
                    tlsStatusText.Text = iPlaylist.Resources.Labels.UpdatesNotFoundStatus;
                    MessageBox.Show(String.Format(iPlaylist.Resources.Maintenance.NoUpdatesTextFormat, Consts.Version),
                        iPlaylist.Resources.Maintenance.NoUpdatesTitle, 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Information);
                }
                Parameters.LastUpdateDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                tlsStatusText.Text = iPlaylist.Resources.Labels.UpdateFailedStatus;
                MessageBox.Show(String.Format(iPlaylist.Resources.Maintenance.UpdateErrorTextFormat, ex.Message),
                    iPlaylist.Resources.Maintenance.UpdateErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Write(ex);
            }
        }

        #endregion

        #region Pause playback on session lock

        private void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if ((monitor != null) && (monitor.State == MonitorState.Started))
            {
                if (e.Reason == SessionSwitchReason.SessionLock)
                {
                    if (monitor.IsPlaying)
                    {
                        monitor.Pause();
                        wasPaused = true;
                    }
                }
                else if (e.Reason == SessionSwitchReason.SessionUnlock)
                {
                    if (wasPaused)
                    {
                        monitor.Play();
                        wasPaused = false;
                    }
                }
            }
        }

        #endregion

        #region WndProc (keyboard hook messages)

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case KeyHook.WM_HOTKEY:
                    if (((short)m.WParam == keyPlayPause.KeyID) || ((short)m.WParam == keyMediaPlayPause.KeyID))
                    {
                        this.ActionPlayPause();
                    }
                    else if (((short)m.WParam == keyNextTrack.KeyID) || ((short)m.WParam == keyMediaNextTrack.KeyID))
                    {
                        this.ActionNextTrack();
                    }
                    else if (((short)m.WParam == keyPrevTrack.KeyID) || ((short)m.WParam == keyMediaPrevTrack.KeyID))
                    {
                        this.ActionPreviousTrack();
                    }
                    else if ((short)m.WParam == keyMediaStop.KeyID)
                    {
                        this.ActionPause();
                    }
                    else
                    {
                        base.WndProc(ref m);
                    }
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        #endregion

    }
}
