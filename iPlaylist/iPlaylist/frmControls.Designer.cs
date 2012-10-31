namespace iPlaylist
{
    partial class frmControls
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
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmControls));
            this.ctxMenuControls = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tlsShowMainScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.tlsHideControls = new System.Windows.Forms.ToolStripMenuItem();
            this.tlsExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTipButtons = new System.Windows.Forms.ToolTip(this.components);
            this.imgNext = new iPlaylistControls.ImageButton();
            this.imgPlayPause = new iPlaylistControls.ImageButton();
            this.imgPrev = new iPlaylistControls.ImageButton();
            this.imgShuffle = new iPlaylistControls.ImageButton();
            this.resizeTimer = new System.Windows.Forms.Timer(this.components);
            this.mrqLblNowPlaying = new iPlaylistControls.MarqueeLabel();
            this.imageButton1 = new iPlaylistControls.ImageButton();
            this.imageButton3 = new iPlaylistControls.ImageButton();
            this.imageButton2 = new iPlaylistControls.ImageButton();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxMenuControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // ctxMenuControls
            // 
            this.ctxMenuControls.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlsShowMainScreen,
            this.tlsHideControls,
            toolStripSeparator1,
            this.tlsExit});
            this.ctxMenuControls.Name = "ctxMenuControls";
            this.ctxMenuControls.ShowCheckMargin = true;
            this.ctxMenuControls.ShowImageMargin = false;
            resources.ApplyResources(this.ctxMenuControls, "ctxMenuControls");
            // 
            // tlsShowMainScreen
            // 
            this.tlsShowMainScreen.Name = "tlsShowMainScreen";
            resources.ApplyResources(this.tlsShowMainScreen, "tlsShowMainScreen");
            this.tlsShowMainScreen.Click += new System.EventHandler(this.tlsShowMainScreen_Click);
            // 
            // tlsHideControls
            // 
            this.tlsHideControls.Name = "tlsHideControls";
            resources.ApplyResources(this.tlsHideControls, "tlsHideControls");
            this.tlsHideControls.Click += new System.EventHandler(this.tlsHideControls_Click);
            // 
            // tlsExit
            // 
            this.tlsExit.Name = "tlsExit";
            resources.ApplyResources(this.tlsExit, "tlsExit");
            this.tlsExit.Click += new System.EventHandler(this.tlsExit_Click);
            // 
            // toolTipButtons
            // 
            this.toolTipButtons.ShowAlways = true;
            // 
            // imgNext
            // 
            this.imgNext.BackColor = System.Drawing.Color.Transparent;
            this.imgNext.HoverImage = global::iPlaylist.Properties.Resources.NextHover;
            resources.ApplyResources(this.imgNext, "imgNext");
            this.imgNext.Name = "imgNext";
            this.imgNext.NormalImage = global::iPlaylist.Properties.Resources.NextNormal;
            this.imgNext.PressedImage = global::iPlaylist.Properties.Resources.NextPressed;
            this.toolTipButtons.SetToolTip(this.imgNext, resources.GetString("imgNext.ToolTip"));
            this.imgNext.Click += new System.EventHandler(this.imgNext_Click);
            // 
            // imgPlayPause
            // 
            this.imgPlayPause.BackColor = System.Drawing.Color.Transparent;
            this.imgPlayPause.HoverImage = global::iPlaylist.Properties.Resources.PlayHover;
            resources.ApplyResources(this.imgPlayPause, "imgPlayPause");
            this.imgPlayPause.Name = "imgPlayPause";
            this.imgPlayPause.NormalImage = global::iPlaylist.Properties.Resources.PlayNormal;
            this.imgPlayPause.PressedImage = global::iPlaylist.Properties.Resources.PlayPressed;
            this.toolTipButtons.SetToolTip(this.imgPlayPause, resources.GetString("imgPlayPause.ToolTip"));
            this.imgPlayPause.Click += new System.EventHandler(this.imgPlayPause_Click);
            // 
            // imgPrev
            // 
            this.imgPrev.BackColor = System.Drawing.Color.Transparent;
            this.imgPrev.HoverImage = global::iPlaylist.Properties.Resources.PrevHover;
            resources.ApplyResources(this.imgPrev, "imgPrev");
            this.imgPrev.Name = "imgPrev";
            this.imgPrev.NormalImage = global::iPlaylist.Properties.Resources.PrevNormal;
            this.imgPrev.PressedImage = global::iPlaylist.Properties.Resources.PrevPressed;
            this.toolTipButtons.SetToolTip(this.imgPrev, resources.GetString("imgPrev.ToolTip"));
            this.imgPrev.Click += new System.EventHandler(this.imgPrev_Click);
            // 
            // imgShuffle
            // 
            this.imgShuffle.BackColor = System.Drawing.Color.Transparent;
            this.imgShuffle.HoverImage = global::iPlaylist.Properties.Resources.ShuffleHover;
            resources.ApplyResources(this.imgShuffle, "imgShuffle");
            this.imgShuffle.Name = "imgShuffle";
            this.imgShuffle.NormalImage = global::iPlaylist.Properties.Resources.ShuffleNormal;
            this.imgShuffle.PressedImage = global::iPlaylist.Properties.Resources.ShufflePressed;
            this.toolTipButtons.SetToolTip(this.imgShuffle, resources.GetString("imgShuffle.ToolTip"));
            this.imgShuffle.Click += new System.EventHandler(this.imgShuffle_Click);
            // 
            // resizeTimer
            // 
            this.resizeTimer.Interval = 500;
            this.resizeTimer.Tick += new System.EventHandler(this.resizeTimer_Tick);
            // 
            // mrqLblNowPlaying
            // 
            this.mrqLblNowPlaying.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.mrqLblNowPlaying.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(97)))), ((int)(((byte)(150)))));
            this.mrqLblNowPlaying.CausesValidation = false;
            resources.ApplyResources(this.mrqLblNowPlaying, "mrqLblNowPlaying");
            this.mrqLblNowPlaying.ForeColor = System.Drawing.Color.White;
            this.mrqLblNowPlaying.MaximumSize = new System.Drawing.Size(1024, 50);
            this.mrqLblNowPlaying.Name = "mrqLblNowPlaying";
            // 
            // imageButton1
            // 
            this.imageButton1.BackColor = System.Drawing.Color.Transparent;
            this.imageButton1.HoverImage = null;
            resources.ApplyResources(this.imageButton1, "imageButton1");
            this.imageButton1.Name = "imageButton1";
            this.imageButton1.NormalImage = null;
            this.imageButton1.PressedImage = null;
            // 
            // imageButton3
            // 
            this.imageButton3.BackColor = System.Drawing.Color.Transparent;
            this.imageButton3.HoverImage = null;
            resources.ApplyResources(this.imageButton3, "imageButton3");
            this.imageButton3.Name = "imageButton3";
            this.imageButton3.NormalImage = null;
            this.imageButton3.PressedImage = null;
            // 
            // imageButton2
            // 
            this.imageButton2.BackColor = System.Drawing.Color.Transparent;
            this.imageButton2.HoverImage = null;
            resources.ApplyResources(this.imageButton2, "imageButton2");
            this.imageButton2.Name = "imageButton2";
            this.imageButton2.NormalImage = null;
            this.imageButton2.PressedImage = null;
            // 
            // frmControls
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::iPlaylist.Properties.Resources.BackgroundTop;
            this.ContextMenuStrip = this.ctxMenuControls;
            this.ControlBox = false;
            this.Controls.Add(this.imgNext);
            this.Controls.Add(this.imgPlayPause);
            this.Controls.Add(this.imgPrev);
            this.Controls.Add(this.imgShuffle);
            this.Controls.Add(this.mrqLblNowPlaying);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmControls";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.TopMost = true;
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmControls_MouseUp);
            this.MouseEnter += new System.EventHandler(this.frmControls_MouseEnter);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmControls_MouseDown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmControls_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmControls_MouseMove);
            this.ctxMenuControls.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip ctxMenuControls;
        private System.Windows.Forms.ToolStripMenuItem tlsShowMainScreen;
        private System.Windows.Forms.ToolStripMenuItem tlsHideControls;
        private System.Windows.Forms.ToolStripMenuItem tlsExit;
        private System.Windows.Forms.ToolTip toolTipButtons;
        private iPlaylistControls.MarqueeLabel mrqLblNowPlaying;
        private System.Windows.Forms.Timer resizeTimer;
        private iPlaylistControls.ImageButton imgShuffle;
        private iPlaylistControls.ImageButton imgPrev;
        private iPlaylistControls.ImageButton imgPlayPause;
        private iPlaylistControls.ImageButton imgNext;
        private iPlaylistControls.ImageButton imageButton1;
        private iPlaylistControls.ImageButton imageButton3;
        private iPlaylistControls.ImageButton imageButton2;

    }
}