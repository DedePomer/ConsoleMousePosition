using System.Drawing;
using System.Runtime.InteropServices;


namespace ConsoleMousePosition
{
    internal class Program
    {
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);
        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117
        }


        static int _x, _y;
        

        static void Main(string[] args)
        {
            List<string> Points = new List<string>();
            float sc = GetScalingFactor();

            Console.CursorVisible = false;
            while (Points.Count != 20)
            {
                POINT point = ShowMousePosition();
                if (point.X != _x && point.Y != _y)
                {
                    Points.Add(point.X + "" + point.Y);
                    _x = point.X;
                    _y = point.Y;
                }
                Thread.Sleep(250);
            }
            Console.CursorVisible = true;
        }


        static POINT ShowMousePosition()
        {
            POINT point;
            if (GetCursorPos(out point) && point.X != _x && point.Y != _y)
            {
                Console.WriteLine("({0},{1})", point.X, point.Y);
            }
            return point;
        }

        static float GetScalingFactor()
        {
            using Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            int LogicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
            int PhysicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);

            g.ReleaseHdc(desktop);

            float ScreenScalingFactor = (float)PhysicalScreenHeight / (float)LogicalScreenHeight;
            g.Dispose();

            return ScreenScalingFactor; // 1.25 = 125%
        }


        //static bool PositionChek() /*можно попробовать virtual screen*/
        //{
        //    string screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth.ToString();

        //    return true;
        //}
    }
}
