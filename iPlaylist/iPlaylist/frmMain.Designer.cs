namespace iPlaylist
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.GroupBox groupBox3;
            System.Windows.Forms.GroupBox groupBox4;
            System.Windows.Forms.TableLayoutPanel tableLayoutControls;
            System.Windows.Forms.GroupBox groupBox5;
            System.Windows.Forms.TableLayoutPanel tableLayoutProgressBar;
            System.Windows.Forms.GroupBox groupBox6;
            System.Windows.Forms.Label lblPlaylistAccuracyText;
            System.Windows.Forms.Label lblPlayerInfoText;
            System.Windows.Forms.Label lblPlaylistDataText;
            System.Windows.Forms.Label lblRatingInfoText;
            System.Windows.Forms.Label lblSrcPlaytime;
            System.Windows.Forms.Label lblOnlineText;
            System.Windows.Forms.GroupBox groupBox7;
            System.Windows.Forms.GroupBox groupBox8;
            System.Windows.Forms.Label lblPlayingPlaylsit;
            System.Windows.Forms.GroupBox groupBox9;
            System.Windows.Forms.Label lblLanguage;
            System.Windows.Forms.GroupBox groupBox10;
            System.Windows.Forms.GroupBox groupBox11;
            System.Windows.Forms.Panel panel2;
            System.Windows.Forms.Label lblVersionLabel;
            System.Windows.Forms.Label lblAssemblyLabel;
            System.Windows.Forms.Label lblMediaVersionLabel;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lbTopTracks = new System.Windows.Forms.ListBox();
            this.lbTopArtists = new System.Windows.Forms.ListBox();
            this.lbTopAlbums = new System.Windows.Forms.ListBox();
            this.btnPlayPause = new System.Windows.Forms.Button();
            this.btnPrevTrack = new System.Windows.Forms.Button();
            this.btnNextTrack = new System.Windows.Forms.Button();
            this.btnShuffle = new System.Windows.Forms.Button();
            this.lblAlbumInfo = new System.Windows.Forms.Label();
            this.lblCurrentPosition = new System.Windows.Forms.Label();
            this.pbNowPlaying = new System.Windows.Forms.ProgressBar();
            this.lblCurrentLeft = new System.Windows.Forms.Label();
            this.lblNowPlaying = new System.Windows.Forms.Label();
            this.lblNowPlayingLabel = new System.Windows.Forms.Label();
            this.tblPaneliPlaylistInfo = new System.Windows.Forms.TableLayoutPanel();
            this.lblPlaylistAccuracy = new System.Windows.Forms.Label();
            this.lblTimeOnline = new System.Windows.Forms.Label();
            this.lblMediaPlayerInfo = new System.Windows.Forms.Label();
            this.lbliPlaylistDataStatus = new System.Windows.Forms.Label();
            this.lblRatingDatabaseInfo = new System.Windows.Forms.Label();
            this.lblSourceMediaDuration = new System.Windows.Forms.Label();
            this.cbCheckForUpdates = new System.Windows.Forms.CheckBox();
            this.cbAllowGlobalHotKeys = new System.Windows.Forms.CheckBox();
            this.cbStartMinimized = new System.Windows.Forms.CheckBox();
            this.cbShowPlayerControls = new System.Windows.Forms.CheckBox();
            this.cbListenMoreUnrated = new System.Windows.Forms.CheckBox();
            this.cbIgnoreWhenLocked = new System.Windows.Forms.CheckBox();
            this.cbPauseOnLock = new System.Windows.Forms.CheckBox();
            this.cbSkipNonAudio = new System.Windows.Forms.CheckBox();
            this.cbIncreaseRandomness = new System.Windows.Forms.CheckBox();
            this.cbSkipBestWorst = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.cmbMediaPlayer = new System.Windows.Forms.ComboBox();
            this.cbUseTwoPlaylists = new System.Windows.Forms.CheckBox();
            this.tbPlayablePlaylist = new System.Windows.Forms.TextBox();
            this.cmbSourcePlaylist = new System.Windows.Forms.ComboBox();
            this.lblSourcePlaylist = new System.Windows.Forms.Label();
            this.lblMediaPlayer = new System.Windows.Forms.Label();
            this.tableLayoutPresets = new System.Windows.Forms.TableLayoutPanel();
            this.btnAutodetectLibrary = new System.Windows.Forms.Button();
            this.btnSmallLibrary = new System.Windows.Forms.Button();
            this.btnBigLibrary = new System.Windows.Forms.Button();
            this.lblUsedLibraries = new System.Windows.Forms.Label();
            this.lblAuthors = new System.Windows.Forms.Label();
            this.btnBugReport = new System.Windows.Forms.Button();
            this.btnCheckForUpdates = new System.Windows.Forms.Button();
            this.stsMain = new System.Windows.Forms.StatusStrip();
            this.tlsStatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.tlsProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.tabsMain = new System.Windows.Forms.TabControl();
            this.tabInfo = new System.Windows.Forms.TabPage();
            this.tblPanelInfo = new System.Windows.Forms.TableLayoutPanel();
            this.tabRating = new System.Windows.Forms.TabPage();
            this.tblPanelRating = new System.Windows.Forms.TableLayoutPanel();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.tblPanelSettings = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.tabAbout = new System.Windows.Forms.TabPage();
            this.tblPanelAbout = new System.Windows.Forms.TableLayoutPanel();
            this.lblMediaVersion = new System.Windows.Forms.Label();
            this.lblAssembly = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbAbout = new System.Windows.Forms.PictureBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.imageTabs = new System.Windows.Forms.ImageList(this.components);
            this.notifyIconMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showMainWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPlayerControlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shufflePlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prevTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playPauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitIPlaylistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerScreenUpdate = new System.Windows.Forms.Timer(this.components);
            this.bgwCreatePlaylist = new System.ComponentModel.BackgroundWorker();
            this.toolTipMainWindow = new System.Windows.Forms.ToolTip(this.components);
            this.lblOnlinePeriod = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bgwUpdateCheck = new System.ComponentModel.BackgroundWorker();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            groupBox3 = new System.Windows.Forms.GroupBox();
            groupBox4 = new System.Windows.Forms.GroupBox();
            tableLayoutControls = new System.Windows.Forms.TableLayoutPanel();
            groupBox5 = new System.Windows.Forms.GroupBox();
            tableLayoutProgressBar = new System.Windows.Forms.TableLayoutPanel();
            groupBox6 = new System.Windows.Forms.GroupBox();
            lblPlaylistAccuracyText = new System.Windows.Forms.Label();
            lblPlayerInfoText = new System.Windows.Forms.Label();
            lblPlaylistDataText = new System.Windows.Forms.Label();
            lblRatingInfoText = new System.Windows.Forms.Label();
            lblSrcPlaytime = new System.Windows.Forms.Label();
            lblOnlineText = new System.Windows.Forms.Label();
            groupBox7 = new System.Windows.Forms.GroupBox();
            groupBox8 = new System.Windows.Forms.GroupBox();
            lblPlayingPlaylsit = new System.Windows.Forms.Label();
            groupBox9 = new System.Windows.Forms.GroupBox();
            lblLanguage = new System.Windows.Forms.Label();
            groupBox10 = new System.Windows.Forms.GroupBox();
            groupBox11 = new System.Windows.Forms.GroupBox();
            panel2 = new System.Windows.Forms.Panel();
            lblVersionLabel = new System.Windows.Forms.Label();
            lblAssemblyLabel = new System.Windows.Forms.Label();
            lblMediaVersionLabel = new System.Windows.Forms.Label();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            tableLayoutControls.SuspendLayout();
            groupBox5.SuspendLayout();
            tableLayoutProgressBar.SuspendLayout();
            groupBox6.SuspendLayout();
            this.tblPaneliPlaylistInfo.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox8.SuspendLayout();
            groupBox9.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            groupBox10.SuspendLayout();
            this.tableLayoutPresets.SuspendLayout();
            groupBox11.SuspendLayout();
            panel2.SuspendLayout();
            this.stsMain.SuspendLayout();
            this.tabsMain.SuspendLayout();
            this.tabInfo.SuspendLayout();
            this.tblPanelInfo.SuspendLayout();
            this.tabRating.SuspendLayout();
            this.tblPanelRating.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.tblPanelSettings.SuspendLayout();
            this.tabAbout.SuspendLayout();
            this.tblPanelAbout.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAbout)).BeginInit();
            this.contextMenuIcon.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.tblPanelRating.SetColumnSpan(groupBox1, 2);
            groupBox1.Controls.Add(this.lbTopTracks);
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // lbTopTracks
            // 
            resources.ApplyResources(this.lbTopTracks, "lbTopTracks");
            this.lbTopTracks.FormattingEnabled = true;
            this.lbTopTracks.Name = "lbTopTracks";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(this.lbTopArtists);
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
            // 
            // lbTopArtists
            // 
            resources.ApplyResources(this.lbTopArtists, "lbTopArtists");
            this.lbTopArtists.FormattingEnabled = true;
            this.lbTopArtists.Name = "lbTopArtists";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(this.lbTopAlbums);
            resources.ApplyResources(groupBox3, "groupBox3");
            groupBox3.Name = "groupBox3";
            groupBox3.TabStop = false;
            // 
            // lbTopAlbums
            // 
            resources.ApplyResources(this.lbTopAlbums, "lbTopAlbums");
            this.lbTopAlbums.FormattingEnabled = true;
            this.lbTopAlbums.Name = "lbTopAlbums";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(tableLayoutControls);
            groupBox4.Controls.Add(this.btnShuffle);
            resources.ApplyResources(groupBox4, "groupBox4");
            groupBox4.Name = "groupBox4";
            groupBox4.TabStop = false;
            // 
            // tableLayoutControls
            // 
            resources.ApplyResources(tableLayoutControls, "tableLayoutControls");
            tableLayoutControls.Controls.Add(this.btnPlayPause, 1, 0);
            tableLayoutControls.Controls.Add(this.btnPrevTrack, 0, 0);
            tableLayoutControls.Controls.Add(this.btnNextTrack, 2, 0);
            tableLayoutControls.Name = "tableLayoutControls";
            // 
            // btnPlayPause
            // 
            resources.ApplyResources(this.btnPlayPause, "btnPlayPause");
            this.btnPlayPause.Image = global::iPlaylist.Properties.Resources.PlayIcon;
            this.btnPlayPause.Name = "btnPlayPause";
            this.btnPlayPause.UseVisualStyleBackColor = true;
            this.btnPlayPause.Click += new System.EventHandler(this.btnPlayPause_Click);
            // 
            // btnPrevTrack
            // 
            resources.ApplyResources(this.btnPrevTrack, "btnPrevTrack");
            this.btnPrevTrack.Image = global::iPlaylist.Properties.Resources.RewIcon;
            this.btnPrevTrack.Name = "btnPrevTrack";
            this.btnPrevTrack.UseVisualStyleBackColor = true;
            this.btnPrevTrack.Click += new System.EventHandler(this.btnPrevTrack_Click);
            // 
            // btnNextTrack
            // 
            resources.ApplyResources(this.btnNextTrack, "btnNextTrack");
            this.btnNextTrack.Image = global::iPlaylist.Properties.Resources.FwdIcon;
            this.btnNextTrack.Name = "btnNextTrack";
            this.btnNextTrack.UseVisualStyleBackColor = true;
            this.btnNextTrack.Click += new System.EventHandler(this.btnNextTrack_Click);
            // 
            // btnShuffle
            // 
            resources.ApplyResources(this.btnShuffle, "btnShuffle");
            this.btnShuffle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(97)))), ((int)(((byte)(150)))));
            this.btnShuffle.Image = global::iPlaylist.Properties.Resources.ShuffleIcon;
            this.btnShuffle.Name = "btnShuffle";
            this.btnShuffle.UseVisualStyleBackColor = true;
            this.btnShuffle.Click += new System.EventHandler(this.btnShuffle_Click);
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(this.lblAlbumInfo);
            groupBox5.Controls.Add(tableLayoutProgressBar);
            groupBox5.Controls.Add(this.lblNowPlaying);
            groupBox5.Controls.Add(this.lblNowPlayingLabel);
            resources.ApplyResources(groupBox5, "groupBox5");
            groupBox5.Name = "groupBox5";
            groupBox5.TabStop = false;
            // 
            // lblAlbumInfo
            // 
            resources.ApplyResources(this.lblAlbumInfo, "lblAlbumInfo");
            this.lblAlbumInfo.Name = "lblAlbumInfo";
            // 
            // tableLayoutProgressBar
            // 
            resources.ApplyResources(tableLayoutProgressBar, "tableLayoutProgressBar");
            tableLayoutProgressBar.Controls.Add(this.lblCurrentPosition, 0, 0);
            tableLayoutProgressBar.Controls.Add(this.pbNowPlaying, 1, 0);
            tableLayoutProgressBar.Controls.Add(this.lblCurrentLeft, 2, 0);
            tableLayoutProgressBar.Name = "tableLayoutProgressBar";
            // 
            // lblCurrentPosition
            // 
            resources.ApplyResources(this.lblCurrentPosition, "lblCurrentPosition");
            this.lblCurrentPosition.Name = "lblCurrentPosition";
            // 
            // pbNowPlaying
            // 
            resources.ApplyResources(this.pbNowPlaying, "pbNowPlaying");
            this.pbNowPlaying.Name = "pbNowPlaying";
            this.pbNowPlaying.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbNowPlaying_MouseMove);
            // 
            // lblCurrentLeft
            // 
            resources.ApplyResources(this.lblCurrentLeft, "lblCurrentLeft");
            this.lblCurrentLeft.Name = "lblCurrentLeft";
            // 
            // lblNowPlaying
            // 
            resources.ApplyResources(this.lblNowPlaying, "lblNowPlaying");
            this.lblNowPlaying.Name = "lblNowPlaying";
            this.lblNowPlaying.UseMnemonic = false;
            // 
            // lblNowPlayingLabel
            // 
            resources.ApplyResources(this.lblNowPlayingLabel, "lblNowPlayingLabel");
            this.lblNowPlayingLabel.Name = "lblNowPlayingLabel";
            // 
            // groupBox6
            // 
            this.tblPanelInfo.SetColumnSpan(groupBox6, 2);
            groupBox6.Controls.Add(this.tblPaneliPlaylistInfo);
            resources.ApplyResources(groupBox6, "groupBox6");
            groupBox6.Name = "groupBox6";
            groupBox6.TabStop = false;
            // 
            // tblPaneliPlaylistInfo
            // 
            resources.ApplyResources(this.tblPaneliPlaylistInfo, "tblPaneliPlaylistInfo");
            this.tblPaneliPlaylistInfo.Controls.Add(this.lblPlaylistAccuracy, 1, 3);
            this.tblPaneliPlaylistInfo.Controls.Add(lblPlaylistAccuracyText, 0, 3);
            this.tblPaneliPlaylistInfo.Controls.Add(this.lblTimeOnline, 1, 2);
            this.tblPaneliPlaylistInfo.Controls.Add(lblPlayerInfoText, 0, 0);
            this.tblPaneliPlaylistInfo.Controls.Add(this.lblMediaPlayerInfo, 1, 0);
            this.tblPaneliPlaylistInfo.Controls.Add(lblPlaylistDataText, 0, 5);
            this.tblPaneliPlaylistInfo.Controls.Add(this.lbliPlaylistDataStatus, 1, 5);
            this.tblPaneliPlaylistInfo.Controls.Add(lblRatingInfoText, 0, 4);
            this.tblPaneliPlaylistInfo.Controls.Add(this.lblRatingDatabaseInfo, 1, 4);
            this.tblPaneliPlaylistInfo.Controls.Add(lblSrcPlaytime, 0, 1);
            this.tblPaneliPlaylistInfo.Controls.Add(this.lblSourceMediaDuration, 1, 1);
            this.tblPaneliPlaylistInfo.Controls.Add(lblOnlineText, 0, 2);
            this.tblPaneliPlaylistInfo.Name = "tblPaneliPlaylistInfo";
            // 
            // lblPlaylistAccuracy
            // 
            resources.ApplyResources(this.lblPlaylistAccuracy, "lblPlaylistAccuracy");
            this.lblPlaylistAccuracy.Name = "lblPlaylistAccuracy";
            // 
            // lblPlaylistAccuracyText
            // 
            resources.ApplyResources(lblPlaylistAccuracyText, "lblPlaylistAccuracyText");
            lblPlaylistAccuracyText.Name = "lblPlaylistAccuracyText";
            // 
            // lblTimeOnline
            // 
            resources.ApplyResources(this.lblTimeOnline, "lblTimeOnline");
            this.lblTimeOnline.Name = "lblTimeOnline";
            // 
            // lblPlayerInfoText
            // 
            resources.ApplyResources(lblPlayerInfoText, "lblPlayerInfoText");
            lblPlayerInfoText.Name = "lblPlayerInfoText";
            // 
            // lblMediaPlayerInfo
            // 
            resources.ApplyResources(this.lblMediaPlayerInfo, "lblMediaPlayerInfo");
            this.lblMediaPlayerInfo.Name = "lblMediaPlayerInfo";
            // 
            // lblPlaylistDataText
            // 
            resources.ApplyResources(lblPlaylistDataText, "lblPlaylistDataText");
            lblPlaylistDataText.Name = "lblPlaylistDataText";
            // 
            // lbliPlaylistDataStatus
            // 
            resources.ApplyResources(this.lbliPlaylistDataStatus, "lbliPlaylistDataStatus");
            this.lbliPlaylistDataStatus.Name = "lbliPlaylistDataStatus";
            // 
            // lblRatingInfoText
            // 
            resources.ApplyResources(lblRatingInfoText, "lblRatingInfoText");
            lblRatingInfoText.Name = "lblRatingInfoText";
            // 
            // lblRatingDatabaseInfo
            // 
            resources.ApplyResources(this.lblRatingDatabaseInfo, "lblRatingDatabaseInfo");
            this.lblRatingDatabaseInfo.Name = "lblRatingDatabaseInfo";
            // 
            // lblSrcPlaytime
            // 
            resources.ApplyResources(lblSrcPlaytime, "lblSrcPlaytime");
            lblSrcPlaytime.Name = "lblSrcPlaytime";
            // 
            // lblSourceMediaDuration
            // 
            resources.ApplyResources(this.lblSourceMediaDuration, "lblSourceMediaDuration");
            this.lblSourceMediaDuration.Name = "lblSourceMediaDuration";
            // 
            // lblOnlineText
            // 
            resources.ApplyResources(lblOnlineText, "lblOnlineText");
            lblOnlineText.Name = "lblOnlineText";
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(this.cbCheckForUpdates);
            groupBox7.Controls.Add(this.cbAllowGlobalHotKeys);
            groupBox7.Controls.Add(this.cbStartMinimized);
            groupBox7.Controls.Add(this.cbShowPlayerControls);
            resources.ApplyResources(groupBox7, "groupBox7");
            groupBox7.Name = "groupBox7";
            groupBox7.TabStop = false;
            // 
            // cbCheckForUpdates
            // 
            resources.ApplyResources(this.cbCheckForUpdates, "cbCheckForUpdates");
            this.cbCheckForUpdates.Name = "cbCheckForUpdates";
            this.cbCheckForUpdates.UseVisualStyleBackColor = true;
            // 
            // cbAllowGlobalHotKeys
            // 
            resources.ApplyResources(this.cbAllowGlobalHotKeys, "cbAllowGlobalHotKeys");
            this.cbAllowGlobalHotKeys.Name = "cbAllowGlobalHotKeys";
            this.toolTipMainWindow.SetToolTip(this.cbAllowGlobalHotKeys, resources.GetString("cbAllowGlobalHotKeys.ToolTip"));
            this.cbAllowGlobalHotKeys.UseVisualStyleBackColor = true;
            // 
            // cbStartMinimized
            // 
            resources.ApplyResources(this.cbStartMinimized, "cbStartMinimized");
            this.cbStartMinimized.Name = "cbStartMinimized";
            this.cbStartMinimized.UseVisualStyleBackColor = true;
            // 
            // cbShowPlayerControls
            // 
            resources.ApplyResources(this.cbShowPlayerControls, "cbShowPlayerControls");
            this.cbShowPlayerControls.Name = "cbShowPlayerControls";
            this.toolTipMainWindow.SetToolTip(this.cbShowPlayerControls, resources.GetString("cbShowPlayerControls.ToolTip"));
            this.cbShowPlayerControls.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(this.cbListenMoreUnrated);
            groupBox8.Controls.Add(this.cbIgnoreWhenLocked);
            groupBox8.Controls.Add(this.cbPauseOnLock);
            groupBox8.Controls.Add(this.cbSkipNonAudio);
            groupBox8.Controls.Add(this.cbIncreaseRandomness);
            groupBox8.Controls.Add(this.cbSkipBestWorst);
            resources.ApplyResources(groupBox8, "groupBox8");
            groupBox8.Name = "groupBox8";
            groupBox8.TabStop = false;
            // 
            // cbListenMoreUnrated
            // 
            resources.ApplyResources(this.cbListenMoreUnrated, "cbListenMoreUnrated");
            this.cbListenMoreUnrated.Name = "cbListenMoreUnrated";
            this.toolTipMainWindow.SetToolTip(this.cbListenMoreUnrated, resources.GetString("cbListenMoreUnrated.ToolTip"));
            this.cbListenMoreUnrated.UseVisualStyleBackColor = true;
            // 
            // cbIgnoreWhenLocked
            // 
            resources.ApplyResources(this.cbIgnoreWhenLocked, "cbIgnoreWhenLocked");
            this.cbIgnoreWhenLocked.Name = "cbIgnoreWhenLocked";
            this.toolTipMainWindow.SetToolTip(this.cbIgnoreWhenLocked, resources.GetString("cbIgnoreWhenLocked.ToolTip"));
            this.cbIgnoreWhenLocked.UseVisualStyleBackColor = true;
            // 
            // cbPauseOnLock
            // 
            resources.ApplyResources(this.cbPauseOnLock, "cbPauseOnLock");
            this.cbPauseOnLock.Name = "cbPauseOnLock";
            this.toolTipMainWindow.SetToolTip(this.cbPauseOnLock, resources.GetString("cbPauseOnLock.ToolTip"));
            this.cbPauseOnLock.UseVisualStyleBackColor = true;
            this.cbPauseOnLock.CheckedChanged += new System.EventHandler(this.cbPauseOnLock_CheckedChanged);
            // 
            // cbSkipNonAudio
            // 
            resources.ApplyResources(this.cbSkipNonAudio, "cbSkipNonAudio");
            this.cbSkipNonAudio.Name = "cbSkipNonAudio";
            this.toolTipMainWindow.SetToolTip(this.cbSkipNonAudio, resources.GetString("cbSkipNonAudio.ToolTip"));
            this.cbSkipNonAudio.UseVisualStyleBackColor = true;
            // 
            // cbIncreaseRandomness
            // 
            resources.ApplyResources(this.cbIncreaseRandomness, "cbIncreaseRandomness");
            this.cbIncreaseRandomness.Name = "cbIncreaseRandomness";
            this.toolTipMainWindow.SetToolTip(this.cbIncreaseRandomness, resources.GetString("cbIncreaseRandomness.ToolTip"));
            this.cbIncreaseRandomness.UseVisualStyleBackColor = true;
            // 
            // cbSkipBestWorst
            // 
            resources.ApplyResources(this.cbSkipBestWorst, "cbSkipBestWorst");
            this.cbSkipBestWorst.Name = "cbSkipBestWorst";
            this.toolTipMainWindow.SetToolTip(this.cbSkipBestWorst, resources.GetString("cbSkipBestWorst.ToolTip"));
            this.cbSkipBestWorst.UseVisualStyleBackColor = true;
            // 
            // lblPlayingPlaylsit
            // 
            resources.ApplyResources(lblPlayingPlaylsit, "lblPlayingPlaylsit");
            lblPlayingPlaylsit.Name = "lblPlayingPlaylsit";
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(this.tableLayoutPanel1);
            resources.ApplyResources(groupBox9, "groupBox9");
            groupBox9.Name = "groupBox9";
            groupBox9.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.cmbLanguage, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbMediaPlayer, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbUseTwoPlaylists, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbPlayablePlaylist, 1, 4);
            this.tableLayoutPanel1.Controls.Add(lblPlayingPlaylsit, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.cmbSourcePlaylist, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblSourcePlaylist, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblMediaPlayer, 0, 0);
            this.tableLayoutPanel1.Controls.Add(lblLanguage, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbLanguage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            resources.ApplyResources(this.cmbLanguage, "cmbLanguage");
            this.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLanguage.Name = "cmbLanguage";
            this.toolTipMainWindow.SetToolTip(this.cmbLanguage, resources.GetString("cmbLanguage.ToolTip"));
            // 
            // cmbMediaPlayer
            // 
            this.cmbMediaPlayer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbMediaPlayer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            resources.ApplyResources(this.cmbMediaPlayer, "cmbMediaPlayer");
            this.cmbMediaPlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMediaPlayer.Name = "cmbMediaPlayer";
            // 
            // cbUseTwoPlaylists
            // 
            resources.ApplyResources(this.cbUseTwoPlaylists, "cbUseTwoPlaylists");
            this.tableLayoutPanel1.SetColumnSpan(this.cbUseTwoPlaylists, 2);
            this.cbUseTwoPlaylists.Name = "cbUseTwoPlaylists";
            this.toolTipMainWindow.SetToolTip(this.cbUseTwoPlaylists, resources.GetString("cbUseTwoPlaylists.ToolTip"));
            this.cbUseTwoPlaylists.UseVisualStyleBackColor = true;
            this.cbUseTwoPlaylists.CheckedChanged += new System.EventHandler(this.cbUseTwoPlaylists_CheckedChanged);
            // 
            // tbPlayablePlaylist
            // 
            resources.ApplyResources(this.tbPlayablePlaylist, "tbPlayablePlaylist");
            this.tbPlayablePlaylist.Name = "tbPlayablePlaylist";
            // 
            // cmbSourcePlaylist
            // 
            this.cmbSourcePlaylist.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSourcePlaylist.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            resources.ApplyResources(this.cmbSourcePlaylist, "cmbSourcePlaylist");
            this.cmbSourcePlaylist.Name = "cmbSourcePlaylist";
            // 
            // lblSourcePlaylist
            // 
            resources.ApplyResources(this.lblSourcePlaylist, "lblSourcePlaylist");
            this.lblSourcePlaylist.Name = "lblSourcePlaylist";
            // 
            // lblMediaPlayer
            // 
            resources.ApplyResources(this.lblMediaPlayer, "lblMediaPlayer");
            this.lblMediaPlayer.Name = "lblMediaPlayer";
            // 
            // lblLanguage
            // 
            resources.ApplyResources(lblLanguage, "lblLanguage");
            lblLanguage.Name = "lblLanguage";
            this.toolTipMainWindow.SetToolTip(lblLanguage, resources.GetString("lblLanguage.ToolTip"));
            // 
            // groupBox10
            // 
            groupBox10.Controls.Add(this.tableLayoutPresets);
            resources.ApplyResources(groupBox10, "groupBox10");
            groupBox10.Name = "groupBox10";
            groupBox10.TabStop = false;
            // 
            // tableLayoutPresets
            // 
            this.tableLayoutPresets.Controls.Add(this.btnAutodetectLibrary, 0, 1);
            this.tableLayoutPresets.Controls.Add(this.btnSmallLibrary, 0, 0);
            this.tableLayoutPresets.Controls.Add(this.btnBigLibrary, 1, 0);
            resources.ApplyResources(this.tableLayoutPresets, "tableLayoutPresets");
            this.tableLayoutPresets.Name = "tableLayoutPresets";
            // 
            // btnAutodetectLibrary
            // 
            this.tableLayoutPresets.SetColumnSpan(this.btnAutodetectLibrary, 2);
            resources.ApplyResources(this.btnAutodetectLibrary, "btnAutodetectLibrary");
            this.btnAutodetectLibrary.Name = "btnAutodetectLibrary";
            this.btnAutodetectLibrary.UseVisualStyleBackColor = true;
            this.btnAutodetectLibrary.Click += new System.EventHandler(this.btnAutodetectLibrary_Click);
            // 
            // btnSmallLibrary
            // 
            resources.ApplyResources(this.btnSmallLibrary, "btnSmallLibrary");
            this.btnSmallLibrary.Name = "btnSmallLibrary";
            this.btnSmallLibrary.UseVisualStyleBackColor = true;
            this.btnSmallLibrary.Click += new System.EventHandler(this.btnSmallLibrary_Click);
            // 
            // btnBigLibrary
            // 
            resources.ApplyResources(this.btnBigLibrary, "btnBigLibrary");
            this.btnBigLibrary.Name = "btnBigLibrary";
            this.btnBigLibrary.UseVisualStyleBackColor = true;
            this.btnBigLibrary.Click += new System.EventHandler(this.btnBigLibrary_Click);
            // 
            // groupBox11
            // 
            this.tblPanelAbout.SetColumnSpan(groupBox11, 2);
            groupBox11.Controls.Add(this.lblUsedLibraries);
            groupBox11.Controls.Add(this.lblAuthors);
            resources.ApplyResources(groupBox11, "groupBox11");
            groupBox11.Name = "groupBox11";
            groupBox11.TabStop = false;
            // 
            // lblUsedLibraries
            // 
            resources.ApplyResources(this.lblUsedLibraries, "lblUsedLibraries");
            this.lblUsedLibraries.Name = "lblUsedLibraries";
            // 
            // lblAuthors
            // 
            resources.ApplyResources(this.lblAuthors, "lblAuthors");
            this.lblAuthors.Name = "lblAuthors";
            // 
            // panel2
            // 
            this.tblPanelAbout.SetColumnSpan(panel2, 2);
            panel2.Controls.Add(this.btnBugReport);
            panel2.Controls.Add(this.btnCheckForUpdates);
            resources.ApplyResources(panel2, "panel2");
            panel2.Name = "panel2";
            // 
            // btnBugReport
            // 
            resources.ApplyResources(this.btnBugReport, "btnBugReport");
            this.btnBugReport.Name = "btnBugReport";
            this.toolTipMainWindow.SetToolTip(this.btnBugReport, resources.GetString("btnBugReport.ToolTip"));
            this.btnBugReport.UseVisualStyleBackColor = true;
            this.btnBugReport.Click += new System.EventHandler(this.btnBugReport_Click);
            // 
            // btnCheckForUpdates
            // 
            resources.ApplyResources(this.btnCheckForUpdates, "btnCheckForUpdates");
            this.btnCheckForUpdates.Name = "btnCheckForUpdates";
            this.btnCheckForUpdates.UseVisualStyleBackColor = true;
            this.btnCheckForUpdates.Click += new System.EventHandler(this.btnCheckForUpdates_Click);
            // 
            // lblVersionLabel
            // 
            resources.ApplyResources(lblVersionLabel, "lblVersionLabel");
            lblVersionLabel.Name = "lblVersionLabel";
            // 
            // lblAssemblyLabel
            // 
            resources.ApplyResources(lblAssemblyLabel, "lblAssemblyLabel");
            lblAssemblyLabel.Name = "lblAssemblyLabel";
            // 
            // lblMediaVersionLabel
            // 
            resources.ApplyResources(lblMediaVersionLabel, "lblMediaVersionLabel");
            lblMediaVersionLabel.Name = "lblMediaVersionLabel";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
            // 
            // stsMain
            // 
            this.stsMain.GripMargin = new System.Windows.Forms.Padding(0);
            this.stsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsStatusText,
            this.tlsProgress});
            resources.ApplyResources(this.stsMain, "stsMain");
            this.stsMain.Name = "stsMain";
            this.stsMain.SizingGrip = false;
            // 
            // tlsStatusText
            // 
            this.tlsStatusText.Name = "tlsStatusText";
            resources.ApplyResources(this.tlsStatusText, "tlsStatusText");
            this.tlsStatusText.Spring = true;
            // 
            // tlsProgress
            // 
            this.tlsProgress.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tlsProgress.Name = "tlsProgress";
            resources.ApplyResources(this.tlsProgress, "tlsProgress");
            // 
            // tabsMain
            // 
            this.tabsMain.Controls.Add(this.tabInfo);
            this.tabsMain.Controls.Add(this.tabRating);
            this.tabsMain.Controls.Add(this.tabSettings);
            this.tabsMain.Controls.Add(this.tabAbout);
            resources.ApplyResources(this.tabsMain, "tabsMain");
            this.tabsMain.ImageList = this.imageTabs;
            this.tabsMain.Name = "tabsMain";
            this.tabsMain.SelectedIndex = 0;
            // 
            // tabInfo
            // 
            this.tabInfo.Controls.Add(this.tblPanelInfo);
            resources.ApplyResources(this.tabInfo, "tabInfo");
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.UseVisualStyleBackColor = true;
            // 
            // tblPanelInfo
            // 
            resources.ApplyResources(this.tblPanelInfo, "tblPanelInfo");
            this.tblPanelInfo.Controls.Add(groupBox4, 1, 0);
            this.tblPanelInfo.Controls.Add(groupBox5, 0, 0);
            this.tblPanelInfo.Controls.Add(groupBox6, 0, 1);
            this.tblPanelInfo.Name = "tblPanelInfo";
            // 
            // tabRating
            // 
            this.tabRating.Controls.Add(this.tblPanelRating);
            resources.ApplyResources(this.tabRating, "tabRating");
            this.tabRating.Name = "tabRating";
            this.tabRating.UseVisualStyleBackColor = true;
            // 
            // tblPanelRating
            // 
            resources.ApplyResources(this.tblPanelRating, "tblPanelRating");
            this.tblPanelRating.Controls.Add(groupBox1, 0, 0);
            this.tblPanelRating.Controls.Add(groupBox2, 0, 1);
            this.tblPanelRating.Controls.Add(groupBox3, 1, 1);
            this.tblPanelRating.Name = "tblPanelRating";
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.tblPanelSettings);
            resources.ApplyResources(this.tabSettings, "tabSettings");
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // tblPanelSettings
            // 
            resources.ApplyResources(this.tblPanelSettings, "tblPanelSettings");
            this.tblPanelSettings.Controls.Add(this.btnSave, 0, 2);
            this.tblPanelSettings.Controls.Add(this.btnReset, 1, 2);
            this.tblPanelSettings.Controls.Add(groupBox7, 0, 1);
            this.tblPanelSettings.Controls.Add(groupBox9, 0, 0);
            this.tblPanelSettings.Controls.Add(groupBox8, 1, 0);
            this.tblPanelSettings.Controls.Add(groupBox10, 1, 1);
            this.tblPanelSettings.Name = "tblPanelSettings";
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReset
            // 
            resources.ApplyResources(this.btnReset, "btnReset");
            this.btnReset.Name = "btnReset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // tabAbout
            // 
            this.tabAbout.Controls.Add(this.tblPanelAbout);
            resources.ApplyResources(this.tabAbout, "tabAbout");
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.UseVisualStyleBackColor = true;
            // 
            // tblPanelAbout
            // 
            resources.ApplyResources(this.tblPanelAbout, "tblPanelAbout");
            this.tblPanelAbout.Controls.Add(this.lblMediaVersion, 2, 2);
            this.tblPanelAbout.Controls.Add(lblMediaVersionLabel, 1, 2);
            this.tblPanelAbout.Controls.Add(this.lblAssembly, 2, 1);
            this.tblPanelAbout.Controls.Add(this.panel1, 0, 0);
            this.tblPanelAbout.Controls.Add(groupBox11, 1, 3);
            this.tblPanelAbout.Controls.Add(panel2, 1, 4);
            this.tblPanelAbout.Controls.Add(lblVersionLabel, 1, 0);
            this.tblPanelAbout.Controls.Add(this.lblVersion, 2, 0);
            this.tblPanelAbout.Controls.Add(lblAssemblyLabel, 1, 1);
            this.tblPanelAbout.Name = "tblPanelAbout";
            // 
            // lblMediaVersion
            // 
            resources.ApplyResources(this.lblMediaVersion, "lblMediaVersion");
            this.lblMediaVersion.Name = "lblMediaVersion";
            // 
            // lblAssembly
            // 
            resources.ApplyResources(this.lblAssembly, "lblAssembly");
            this.lblAssembly.Name = "lblAssembly";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pbAbout);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            this.tblPanelAbout.SetRowSpan(this.panel1, 5);
            // 
            // pbAbout
            // 
            resources.ApplyResources(this.pbAbout, "pbAbout");
            this.pbAbout.Image = global::iPlaylist.Properties.Resources.AboutPage;
            this.pbAbout.Name = "pbAbout";
            this.pbAbout.TabStop = false;
            // 
            // lblVersion
            // 
            resources.ApplyResources(this.lblVersion, "lblVersion");
            this.lblVersion.Name = "lblVersion";
            // 
            // imageTabs
            // 
            this.imageTabs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageTabs.ImageStream")));
            this.imageTabs.TransparentColor = System.Drawing.Color.Transparent;
            this.imageTabs.Images.SetKeyName(0, "iPlaylist-settings.png");
            this.imageTabs.Images.SetKeyName(1, "iPlaylist-about.png");
            this.imageTabs.Images.SetKeyName(2, "iPlaylist-best.png");
            this.imageTabs.Images.SetKeyName(3, "iPlaylist-info.png");
            // 
            // notifyIconMain
            // 
            this.notifyIconMain.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            resources.ApplyResources(this.notifyIconMain, "notifyIconMain");
            this.notifyIconMain.ContextMenuStrip = this.contextMenuIcon;
            this.notifyIconMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIconMain_MouseClick);
            // 
            // contextMenuIcon
            // 
            this.contextMenuIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showMainWindowToolStripMenuItem,
            this.showPlayerControlsToolStripMenuItem,
            toolStripSeparator1,
            this.shufflePlaylistToolStripMenuItem,
            this.prevTrackToolStripMenuItem,
            this.playPauseToolStripMenuItem,
            this.nextTrackToolStripMenuItem,
            toolStripSeparator2,
            this.exitIPlaylistToolStripMenuItem});
            this.contextMenuIcon.Name = "contextMenuIcon";
            this.contextMenuIcon.ShowCheckMargin = true;
            this.contextMenuIcon.ShowImageMargin = false;
            resources.ApplyResources(this.contextMenuIcon, "contextMenuIcon");
            // 
            // showMainWindowToolStripMenuItem
            // 
            resources.ApplyResources(this.showMainWindowToolStripMenuItem, "showMainWindowToolStripMenuItem");
            this.showMainWindowToolStripMenuItem.Name = "showMainWindowToolStripMenuItem";
            this.showMainWindowToolStripMenuItem.Click += new System.EventHandler(this.showMainWindowToolStripMenuItem_Click);
            // 
            // showPlayerControlsToolStripMenuItem
            // 
            this.showPlayerControlsToolStripMenuItem.CheckOnClick = true;
            this.showPlayerControlsToolStripMenuItem.Name = "showPlayerControlsToolStripMenuItem";
            resources.ApplyResources(this.showPlayerControlsToolStripMenuItem, "showPlayerControlsToolStripMenuItem");
            this.showPlayerControlsToolStripMenuItem.CheckedChanged += new System.EventHandler(this.showPlayerControlsToolStripMenuItem_CheckedChanged);
            // 
            // shufflePlaylistToolStripMenuItem
            // 
            this.shufflePlaylistToolStripMenuItem.Name = "shufflePlaylistToolStripMenuItem";
            resources.ApplyResources(this.shufflePlaylistToolStripMenuItem, "shufflePlaylistToolStripMenuItem");
            this.shufflePlaylistToolStripMenuItem.Click += new System.EventHandler(this.shufflePlaylistToolStripMenuItem_Click);
            // 
            // prevTrackToolStripMenuItem
            // 
            this.prevTrackToolStripMenuItem.Name = "prevTrackToolStripMenuItem";
            resources.ApplyResources(this.prevTrackToolStripMenuItem, "prevTrackToolStripMenuItem");
            this.prevTrackToolStripMenuItem.Click += new System.EventHandler(this.prevTrackToolStripMenuItem_Click);
            // 
            // playPauseToolStripMenuItem
            // 
            this.playPauseToolStripMenuItem.Name = "playPauseToolStripMenuItem";
            resources.ApplyResources(this.playPauseToolStripMenuItem, "playPauseToolStripMenuItem");
            this.playPauseToolStripMenuItem.Click += new System.EventHandler(this.playPauseToolStripMenuItem_Click);
            // 
            // nextTrackToolStripMenuItem
            // 
            this.nextTrackToolStripMenuItem.Name = "nextTrackToolStripMenuItem";
            resources.ApplyResources(this.nextTrackToolStripMenuItem, "nextTrackToolStripMenuItem");
            this.nextTrackToolStripMenuItem.Click += new System.EventHandler(this.nextTrackToolStripMenuItem_Click);
            // 
            // exitIPlaylistToolStripMenuItem
            // 
            this.exitIPlaylistToolStripMenuItem.Name = "exitIPlaylistToolStripMenuItem";
            resources.ApplyResources(this.exitIPlaylistToolStripMenuItem, "exitIPlaylistToolStripMenuItem");
            this.exitIPlaylistToolStripMenuItem.Click += new System.EventHandler(this.exitIPlaylistToolStripMenuItem_Click);
            // 
            // timerScreenUpdate
            // 
            this.timerScreenUpdate.Interval = 500;
            this.timerScreenUpdate.Tick += new System.EventHandler(this.timerScreenUpdate_Tick);
            // 
            // bgwCreatePlaylist
            // 
            this.bgwCreatePlaylist.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwCreatePlaylist_DoWork);
            this.bgwCreatePlaylist.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwCreatePlaylist_RunWorkerCompleted);
            // 
            // lblOnlinePeriod
            // 
            resources.ApplyResources(this.lblOnlinePeriod, "lblOnlinePeriod");
            this.lblOnlinePeriod.Name = "lblOnlinePeriod";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // bgwUpdateCheck
            // 
            this.bgwUpdateCheck.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwUpdateCheck_DoWork);
            this.bgwUpdateCheck.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwUpdateCheck_RunWorkerCompleted);
            // 
            // frmMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabsMain);
            this.Controls.Add(this.stsMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            tableLayoutControls.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            tableLayoutProgressBar.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            this.tblPaneliPlaylistInfo.ResumeLayout(false);
            this.tblPaneliPlaylistInfo.PerformLayout();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            groupBox9.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            groupBox10.ResumeLayout(false);
            this.tableLayoutPresets.ResumeLayout(false);
            groupBox11.ResumeLayout(false);
            panel2.ResumeLayout(false);
            this.stsMain.ResumeLayout(false);
            this.stsMain.PerformLayout();
            this.tabsMain.ResumeLayout(false);
            this.tabInfo.ResumeLayout(false);
            this.tblPanelInfo.ResumeLayout(false);
            this.tabRating.ResumeLayout(false);
            this.tblPanelRating.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            this.tblPanelSettings.ResumeLayout(false);
            this.tabAbout.ResumeLayout(false);
            this.tblPanelAbout.ResumeLayout(false);
            this.tblPanelAbout.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbAbout)).EndInit();
            this.contextMenuIcon.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip stsMain;
        private System.Windows.Forms.ToolStripStatusLabel tlsStatusText;
        private System.Windows.Forms.ToolStripProgressBar tlsProgress;
        private System.Windows.Forms.TabControl tabsMain;
        private System.Windows.Forms.TabPage tabInfo;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.TabPage tabRating;
        private System.Windows.Forms.ListBox lbTopTracks;
        private System.Windows.Forms.ListBox lbTopArtists;
        private System.Windows.Forms.ListBox lbTopAlbums;
        private System.Windows.Forms.TableLayoutPanel tblPanelInfo;
        private System.Windows.Forms.TableLayoutPanel tblPanelRating;
        private System.Windows.Forms.Button btnShuffle;
        private System.Windows.Forms.Button btnNextTrack;
        private System.Windows.Forms.TableLayoutPanel tblPanelSettings;
        private System.Windows.Forms.CheckBox cbShowPlayerControls;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.CheckBox cbStartMinimized;
        private System.Windows.Forms.CheckBox cbSkipBestWorst;
        private System.Windows.Forms.CheckBox cbIncreaseRandomness;
        private System.Windows.Forms.TabPage tabAbout;
        private System.Windows.Forms.TextBox tbPlayablePlaylist;
        private System.Windows.Forms.Button btnSmallLibrary;
        private System.Windows.Forms.Button btnBigLibrary;
        private System.Windows.Forms.Button btnAutodetectLibrary;
        private System.Windows.Forms.CheckBox cbAllowGlobalHotKeys;
        private System.Windows.Forms.CheckBox cbUseTwoPlaylists;
        private System.Windows.Forms.TableLayoutPanel tblPanelAbout;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblUsedLibraries;
        private System.Windows.Forms.Label lblAuthors;
        private System.Windows.Forms.Button btnBugReport;
        private System.Windows.Forms.Button btnCheckForUpdates;
        private System.Windows.Forms.Label lblNowPlayingLabel;
        private System.Windows.Forms.TableLayoutPanel tblPaneliPlaylistInfo;
        private System.Windows.Forms.Label lblNowPlaying;
        private System.Windows.Forms.ProgressBar pbNowPlaying;
        private System.Windows.Forms.Label lbliPlaylistDataStatus;
        private System.Windows.Forms.Label lblRatingDatabaseInfo;
        private System.Windows.Forms.Label lblMediaPlayerInfo;
        private System.Windows.Forms.NotifyIcon notifyIconMain;
        private System.Windows.Forms.ContextMenuStrip contextMenuIcon;
        private System.Windows.Forms.Timer timerScreenUpdate;
        private System.Windows.Forms.ToolStripMenuItem showMainWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPlayerControlsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextTrackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shufflePlaylistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitIPlaylistToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker bgwCreatePlaylist;
        private System.Windows.Forms.Label lblSourcePlaylist;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblMediaVersion;
        private System.Windows.Forms.Label lblAssembly;
        private System.Windows.Forms.ToolTip toolTipMainWindow;
        private System.Windows.Forms.CheckBox cbCheckForUpdates;
        private System.ComponentModel.BackgroundWorker bgwUpdateCheck;
        private System.Windows.Forms.ComboBox cmbSourcePlaylist;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblMediaPlayer;
        private System.Windows.Forms.ComboBox cmbMediaPlayer;
        private System.Windows.Forms.ComboBox cmbLanguage;
        private System.Windows.Forms.CheckBox cbSkipNonAudio;
        private System.Windows.Forms.CheckBox cbIgnoreWhenLocked;
        private System.Windows.Forms.CheckBox cbPauseOnLock;
        private System.Windows.Forms.Label lblTimeOnline;
        private System.Windows.Forms.Label lblSourceMediaDuration;
        private System.Windows.Forms.Label lblOnlinePeriod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPlaylistAccuracy;
        private System.Windows.Forms.Label lblCurrentLeft;
        private System.Windows.Forms.Label lblCurrentPosition;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPresets;
        private System.Windows.Forms.CheckBox cbListenMoreUnrated;
        private System.Windows.Forms.Label lblAlbumInfo;
        private System.Windows.Forms.Button btnPlayPause;
        private System.Windows.Forms.Button btnPrevTrack;
        private System.Windows.Forms.ImageList imageTabs;
        private System.Windows.Forms.PictureBox pbAbout;
        private System.Windows.Forms.ToolStripMenuItem prevTrackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playPauseToolStripMenuItem;

    }
}

