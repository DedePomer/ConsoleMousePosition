using System.Runtime.InteropServices;

namespace ConsoleMousePosition
{
    internal class Program
    {
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out POINT lpPoint);

        static int _x, _y;
        

        static void Main(string[] args)
        {
            List<string> Points = new List<string>();

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


        static bool PositionChek() /*можно попробовать virtual screen*/
        {


        }
    }
}
