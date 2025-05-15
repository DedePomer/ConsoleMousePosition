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


        static int _x, _y;

        static int _fistZone = 2;
        static int _secondZone = 2;
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
                POINT point;
                if (GetCursorPos(out point) && point.X != _x && point.Y != _y 
                    && IsAvaiblePosition(new Dot2D() { X = point.X, Y = point.Y }))
                {
                    Console.WriteLine("({0},{1})", point.X, point.Y);
                    Points.Add(point.X + "" + point.Y);
                    _x = point.X;
                    _y = point.Y;
                }
                Thread.Sleep(250); //возможно не спит поток
            }
            Console.CursorVisible = true;
        }


        //static POINT ShowMousePosition()
        //{
        //    POINT point;
        //    if (GetCursorPos(out point) && point.X != _x 
        //        && point.Y != _y /*&& IsAvaiblePosition(new Dot2D() { X = point.X, Y = point.Y })*/)
        //    {
        //        Console.WriteLine("({0},{1})", point.X, point.Y);
        //    }
        //    return point;
        //}

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



        static bool IsAvaiblePosition(Dot2D dot)
        {
            Resolution screenResolution = GetScreenResolution();
            //Zone firstZone = new Zone(
            //    new Dot2D
            //    { X = screenResolution.Widh * 0.25, Y = screenResolution.Height * 0.33 },
            //    new Dot2D
            //    { X = screenResolution.Widh * 0.5, Y = screenResolution.Height * 0.67 });

            //Zone secondZone = new Zone(
            //    new Dot2D
            //    { X = screenResolution.Widh * 0.5, Y = screenResolution.Height * 0.33 },
            //    new Dot2D
            //    { X = screenResolution.Widh * 0.75, Y = screenResolution.Height * 0.67 });

            //Zone thirdZone = new Zone(
            //    new Dot2D
            //    { X = screenResolution.Widh * 0.75, Y = screenResolution.Height * 0 },
            //    new Dot2D
            //    { X = screenResolution.Widh * 1, Y = screenResolution.Height * 1 });

            //Zone fourthZone = new Zone(
            //    new Dot2D
            //    { X = screenResolution.Widh * 0, Y = screenResolution.Height * 0 },
            //    new Dot2D
            //    { X = screenResolution.Widh * 0.25, Y = screenResolution.Height * 1 });

            //Zone fifthZone = new Zone(
            //    new Dot2D
            //    { X = screenResolution.Widh * 0.25, Y = screenResolution.Height * 0 },
            //    new Dot2D
            //    { X = screenResolution.Widh * 0.75, Y = screenResolution.Height * 0.33 });

            //Zone sixthZone = new Zone(
            //    new Dot2D
            //    { X = screenResolution.Widh * 0.25, Y = screenResolution.Height * 0.67 },
            //    new Dot2D
            //    { X = screenResolution.Widh * 0.75, Y = screenResolution.Height * 1 });

            //if (IsDotInZone(dot, firstZone) && _fistZone > 0)
            //{
            //    _fistZone--;
            //    return true;
            //}

            //if (IsDotInZone(dot, secondZone) && _secondZone > 0)
            //{
            //    _secondZone--;
            //    return true;
            //}

            //if (IsDotInZone(dot, thirdZone) && _thirdZone > 0)
            //{
            //    _thirdZone--;
            //    return true;
            //}

            //if (IsDotInZone(dot, fourthZone) && _fourthZone > 0)
            //{
            //    _fourthZone--;
            //    return true;
            //}

            //if (IsDotInZone(dot, fifthZone) && _fifthZone > 0)
            //{
            //    _fifthZone--;
            //    return true;
            //}

            //if (IsDotInZone(dot, sixthZone) && _sixthZone > 0)
            //{
            //    _sixthZone--;
            //    return true;
            //}

            return false;
        }

        static bool IsDotInZone(Dot2D dot, Zone zone)
        {
            if ((dot.X > zone.firstPoint.X && dot.Y > zone.firstPoint.Y) && (dot.X < zone.secondPoint.X && dot.Y < zone.secondPoint.Y))
            {
                return true;
            }
            return false;
        }
    }
}
