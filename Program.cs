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
            HORZRES = 8,
            VERTRES = 10,
            DESKTOPVERTRES = 117
        }


        static double _x, _y;
        static int _fistZone = 2;
        static int _secindtZone = 2;
        static int _thirdZone = 20;
        static int _fourthZone = 20;
        static int _fifthZone = 10;
        static int _sixthZone = 10;


        static void Main(string[] args)
        {
            List<string> Points = new List<string>();
            //float sc = GetScalingFactor();
            //IsAvaiblePosition(new POINT {X=1,Y=1});




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

        static Resolution GetScreenResolution() /*не проверял с несколькими экранами*/
        {
            Graphics graphics = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = graphics.GetHdc();

            Resolution screenResolution = new Resolution();
            screenResolution.Widh = GetDeviceCaps(desktop, (int)DeviceCap.HORZRES);
            screenResolution.Height = GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
            return screenResolution;
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



        //static bool IsAvaiblePosition(POINT point) 
        //{
        //    Resolution screenResolution = GetScreenResolution();
        //    Zone firstZone = new Zone(
        //        new POINT 
        //        {X= screenResolution.Widh * 0.25});


        //    return true;
        //}
    }
}
