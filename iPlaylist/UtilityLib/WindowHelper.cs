using System.Drawing;
using System.Windows.Forms;

namespace UtilityLib
{
    public static class WindowHelper
    {
        private const int DockingInterval = 15;

        static WindowHelper()
        {
        }


        /// <summary>
        /// Gets coordinates of point, allowing to "dock" window to screen borders.
        /// Not very good implementation, could behave improperly if screens have different resolutions
        /// (needs to be checked in more details).
        /// </summary>
        /// <param name="X">Top left corner X</param>
        /// <param name="Y">Top left corner Y</param>
        /// <param name="width">Window width</param>
        /// <param name="height">Window height</param>
        /// <returns></returns>
        public static Point GetDockedPoint(int X, int Y, int width, int height)
        {
            Point newPoint = new Point(X, Y);

            // checking all: left, top, right and bottom for closest boundaries...
            foreach (Screen scr in Screen.AllScreens)
            {
                if (scr.Bounds.Contains(X, Y))
                {
                    newPoint = dockToRectangle(new Rectangle(newPoint.X, newPoint.Y, width, height), scr.WorkingArea);
                    newPoint = dockToRectangle(new Rectangle(newPoint.X, newPoint.Y, width, height), scr.Bounds);
                }
            }

            return newPoint;
        }

        private static Point dockToRectangle(Rectangle window, Rectangle screen)
        {
            Point newPoint = new Point(window.Left, window.Top);

            // checking left boundary of screen
            if (near(newPoint.X, screen.Left))
            {
                newPoint.X = screen.Left;
            }

            // checking right
            if (near(newPoint.X + window.Width, screen.Right))
            {
                newPoint.X = screen.Right - window.Width;
            }

            // checking top
            if (near(newPoint.Y, screen.Top))
            {
                newPoint.Y = screen.Top;
            }
            // checking bottom
            if (near(newPoint.Y + window.Height, screen.Bottom))
            {
                newPoint.Y = screen.Bottom - window.Height;
            }

            return newPoint;
        }

        public static bool PointInScreenBounds(int X, int Y)
        {
            foreach (Screen scr in Screen.AllScreens)
            {
                if (scr.Bounds.Contains(X, Y))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool near(int point, int boundary)
        {
            return ((point > (boundary - DockingInterval)) && (point < (boundary + DockingInterval)));
        }
    }
}
