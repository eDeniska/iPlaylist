using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace iPlaylistControls
{
    public partial class ImageButton : UserControl
    {
        private Image current = null;

        public ImageButton()
        {
            NormalImage = null;
            HoverImage = null;
            PressedImage = null;
            
            InitializeComponent();

            // have to do this due to very specific behavior of context menu when shown on other display
            // if context menu is shown on another display, right mouse button click produces a Click event
            // we have to avoid this, so we'll launch Click events ourselves
            SetStyle(ControlStyles.StandardClick, false);
        }

        /// <summary>
        /// Normal button image displayed on form
        /// </summary>
        public Image NormalImage { get; set; }

        /// <summary>
        /// Image displayed when mouse is over the button
        /// </summary>
        public Image HoverImage { get; set; }

        /// <summary>
        /// Image displayed when user presses left mouse button
        /// </summary>
        public Image PressedImage { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (current == null)
            {
                if (NormalImage != null)
                {
                    current = NormalImage;
                }
                else
                {
                    return;
                }
            }
            e.Graphics.DrawImage(current, 0, 0, current.Width, current.Height);
        }

        private void ImageButton_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button | MouseButtons.Left) == MouseButtons.Left)
            {

                current = PressedImage;
                Invalidate();
            }
        }

        private void ImageButton_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button | MouseButtons.Left) == MouseButtons.Left)
            {
                if (this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
                {
                    this.OnClick(e);
                }

                current = HoverImage;
                Invalidate();
            }
        }

        private void ImageButton_MouseEnter(object sender, EventArgs e)
        {
            current = HoverImage;
            Invalidate();
        }

        private void ImageButton_MouseLeave(object sender, EventArgs e)
        {
            current = NormalImage;
            Invalidate();
        }

        public void UpdateImages()
        {
            if (this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
            {
                current = HoverImage;
            }
            else
            {
                current = NormalImage;
            }
            Invalidate();
        }
    }
}
