using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UtilityLib;

namespace iPlaylist
{
    public partial class frmControls : Form
    {
        private bool isDragging = false;
        private bool lowerScreenPart = false;
        private Point dragStartPoint;
        private frmMain parent = null;
        private bool fullSize = true;
        private object lckResize = new object();
        private bool isPlaying = false;

        public frmControls(frmMain p_parent)
        {
            InitializeComponent();
            parent = p_parent;
            
            if ((Parameters.ControlsWindowLeft != -1) && (Parameters.ControlsWindowTop != -1) &&
                WindowHelper.PointInScreenBounds(Parameters.ControlsWindowLeft, Parameters.ControlsWindowTop))
            {
                this.Left = Parameters.ControlsWindowLeft;
                this.Top = Parameters.ControlsWindowTop;
            }
            else
            {
                this.Left = Screen.PrimaryScreen.WorkingArea.Right - this.Width;
                this.Top = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height;
            }
            resizeTimer.Start();
        }

        private void frmControls_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button | MouseButtons.Left) == MouseButtons.Left)
            {
                isDragging = true;
                dragStartPoint = e.Location;
            }
        }

        private void frmControls_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int newX = this.Left + (e.X - dragStartPoint.X);
                int newY = this.Top + (e.Y - dragStartPoint.Y);

                Point newPoint = WindowHelper.GetDockedPoint(newX, newY, this.Width, this.Height);
                this.Left = newPoint.X;
                this.Top = newPoint.Y;

                if (this.Location.Y > Screen.GetBounds(this).Height / 2)
                {
                    if (!(lowerScreenPart))
                    {
                        this.SuspendLayout();
                        moveMarqueeToBottom();
                        lowerScreenPart = true;
                        this.ResumeLayout();
                    }
                }
                else
                {
                    if (lowerScreenPart)
                    {
                        this.SuspendLayout();
                        moveMarqueeToTop();
                        lowerScreenPart = false;
                        this.ResumeLayout();
                    }
                }

            }
        }

        private void frmControls_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button | MouseButtons.Left) == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void imgNext_Click(object sender, EventArgs e)
        {
            parent.ActionNextTrack();
        }

        private void imgShuffle_Click(object sender, EventArgs e)
        {
            parent.ActionShuffle();
        }

        private void imgPrev_Click(object sender, EventArgs e)
        {
            parent.ActionPreviousTrack();
        }

        private void imgPlayPause_Click(object sender, EventArgs e)
        {
            parent.ActionPlayPause();
        }

        public String NowPlaying
        {
            get
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    //return lblNowPlaying.Text;
                    return mrqLblNowPlaying.Text;
                }
                else
                {
                    return String.Empty;
                }
            }
            set
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    //lblNowPlaying.Text = value;
                    mrqLblNowPlaying.Text = value;
                }
            }
        }

        public void SetPlaying(bool setPlaying)
        {
            if (setPlaying != isPlaying)
            {
                isPlaying = setPlaying;
                String title = setPlaying ? iPlaylist.Resources.PlayerControls.TitlePause : iPlaylist.Resources.PlayerControls.TitlePlay;

                if (setPlaying)
                {
                    imgPlayPause.HoverImage = iPlaylist.Properties.Resources.PauseHover;
                    imgPlayPause.NormalImage = iPlaylist.Properties.Resources.PauseNormal;
                    imgPlayPause.PressedImage = iPlaylist.Properties.Resources.PausePressed;
                }
                else
                {
                    imgPlayPause.HoverImage = iPlaylist.Properties.Resources.PlayHover;
                    imgPlayPause.NormalImage = iPlaylist.Properties.Resources.PlayNormal;
                    imgPlayPause.PressedImage = iPlaylist.Properties.Resources.PlayPressed;
                }
                imgPlayPause.UpdateImages();

                toolTipButtons.SetToolTip(imgPlayPause, 
                    setPlaying ? iPlaylist.Resources.PlayerControls.ToolTipPause : iPlaylist.Resources.PlayerControls.ToolTipPlay);
            }
        }

        public void SetProgress(int progress)
        {
            mrqLblNowPlaying.Progress = progress;
        }

        private void frmControls_FormClosing(object sender, FormClosingEventArgs e)
        {
            showFull();
            Parameters.ControlsWindowLeft = this.Left;
            Parameters.ControlsWindowTop = this.Top;
        }

        private void tlsExit_Click(object sender, EventArgs e)
        {
            parent.ActionExitPlayer();
        }

        private void tlsShowMainScreen_Click(object sender, EventArgs e)
        {
            parent.Show();
            parent.WindowState = FormWindowState.Normal;
            parent.Activate();
        }

        private void tlsHideControls_Click(object sender, EventArgs e)
        {
            parent.ActionHidePlayerControls();
        }

        public void Reposition()
        {
            this.Left = Screen.PrimaryScreen.WorkingArea.Right - this.Width;
            this.Top = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height;
        }

        private void frmControls_MouseEnter(object sender, EventArgs e)
        {
            resizeTimer.Start();
            resizeTimer_Tick(sender, e);
        }

        private void resizeTimer_Tick(object sender, EventArgs e)
        {
            if (isDragging) return;

            Point pos = this.PointToClient(Cursor.Position);

            if (this.ClientRectangle.Contains(pos))
            {
                showFull();
            }
            else
            {
                showCompact();
                resizeTimer.Stop();
            }
        }

        private void showCompact()
        {
            lock (lckResize)
            {
                if (fullSize)
                {
                    this.SuspendLayout();
                    imgNext.Visible = false;
                    imgPrev.Visible = false;
                    imgPlayPause.Visible = false;
                    imgShuffle.Visible = false;

                    if (this.Location.Y > Screen.GetBounds(this).Height / 2)
                    {
                        lowerScreenPart = true;
                        this.Bounds = new Rectangle(this.Bounds.Location.X, this.Bounds.Location.Y + 40, this.Bounds.Size.Width, 15);
                        //this.mrqLblNowPlaying.Dock = DockStyle.Bottom;
                    }
                    else
                    {
                        lowerScreenPart = false;
                        this.Bounds = new Rectangle(this.Bounds.Location.X, this.Bounds.Location.Y, this.Bounds.Size.Width, 15);
                        //this.mrqLblNowPlaying.Dock = DockStyle.Top;
                    }

                    //this.Size = new Size(this.Size.Width, 13);
                    //this.Location = new Point(this.Location.X, this.Location.Y + 40);
                    fullSize = false;
                    this.ResumeLayout();
                }
            }
        }

        private void showFull()
        {
            lock (lckResize)
            {
                if (!(fullSize))
                {
                    this.SuspendLayout();

                    //this.Size = new Size(this.Size.Width, 53);
                    if (lowerScreenPart)
                    {
                        this.Bounds = new Rectangle(this.Bounds.Location.X, this.Bounds.Location.Y - 40, this.Bounds.Size.Width, 55);
                        moveMarqueeToBottom();
                    }
                    else
                    {
                        this.Bounds = new Rectangle(this.Bounds.Location.X, this.Bounds.Location.Y, this.Bounds.Size.Width, 55);
                        moveMarqueeToTop();
                    }
                    imgNext.Visible = true;
                    imgPrev.Visible = true;
                    imgPlayPause.Visible = true;
                    imgShuffle.Visible = true;
                    //this.Location = new Point(this.Location.X, this.Location.Y - 40);
                    fullSize = true;
                    this.ResumeLayout();
                }
            }
        }

        private void moveMarqueeToTop()
        {
            this.BackgroundImage = iPlaylist.Properties.Resources.BackgroundBottom;
            imgNext.Location = new Point(imgNext.Location.X, 19);
            imgPrev.Location = new Point(imgPrev.Location.X, 19);
            imgShuffle.Location = new Point(imgShuffle.Location.X, 19);
            imgPlayPause.Location = new Point(imgPlayPause.Location.X, 19);

            this.mrqLblNowPlaying.Dock = DockStyle.Top;
        }

        private void moveMarqueeToBottom()
        {
            this.BackgroundImage = iPlaylist.Properties.Resources.BackgroundTop;
            imgNext.Location = new Point(imgNext.Location.X, 7);
            imgPrev.Location = new Point(imgPrev.Location.X, 7);
            imgShuffle.Location = new Point(imgShuffle.Location.X, 7);
            imgPlayPause.Location = new Point(imgPlayPause.Location.X, 7);
            this.mrqLblNowPlaying.Dock = DockStyle.Bottom;
        }
    }
}
