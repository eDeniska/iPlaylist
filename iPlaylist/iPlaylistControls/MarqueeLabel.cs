using System;
using System.Drawing;
using System.Windows.Forms;

namespace iPlaylistControls
{
    public partial class MarqueeLabel : UserControl
    {
        private const int marqueeDivider = 10;

        private String marqueeText = String.Empty;
        private int position = 0;
        private Bitmap bmp = null;
        private bool easterEggShown = false;

        public MarqueeLabel()
        {
            Progress = 0;
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            marqueeTimer.Start();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (bmp == null)
            {
                bmp = new Bitmap(this.Width - 2, this.Height - 2);
            }
            Graphics g = Graphics.FromImage(bmp);
            Color progress = ControlPaint.Light (this.BackColor);
            

                        
            SizeF size = g.MeasureString(marqueeText, this.Font);
            g.Clear(this.BackColor);
            g.FillRectangle(new SolidBrush(progress), 0, 0, (this.Width - 2) * Progress / 100, this.Height - 2);

            int pos2 = position + (int)size.Width + marqueeDivider;
            int posN = pos2;
            while (posN < this.Width)
            {
                g.DrawString(marqueeText, this.Font, new SolidBrush(this.ForeColor), posN, 0);
                posN += (int)size.Width + marqueeDivider;
            }

            if (-position > size.Width)
            {
                position = pos2;
            }
            else
            {
                g.DrawString(marqueeText, this.Font, new SolidBrush(this.ForeColor), position, 0);
            }

            e.Graphics.DrawImageUnscaled(bmp, 1, 1);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // do nothing to speed up repainting process
        }

        private void marqueeTimer_Tick(object sender, EventArgs e)
        {
            position -= 1;
            Invalidate();
        }

        public override String Text
        {
            get
            {
                return marqueeText;
            }
            set
            {
                marqueeText = value;
                Progress = 0;

                // small easter egg; don't be afraid :)
                if ((DateTime.Now.Day == 6) && (DateTime.Now.Month == 6))
                {
                    if (!(easterEggShown))
                    {
                        Random rnd = new Random();
                        if (rnd.Next() % 3 == 0)
                        {
                            marqueeText += " [Happy Birthday, Danis! :)]";
                            easterEggShown = true;
                        }
                    }
                }
                position = 0;
                Invalidate();
            }
        }

        public int Progress { get; set; }
    }
}
