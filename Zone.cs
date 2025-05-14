using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMousePosition
{
    public class Zone
    {
        public Dot2D firstPoint;
        public Dot2D secondPoint;

        public Zone(Dot2D firstPoint, Dot2D secondPoint) 
        {
            this.firstPoint = firstPoint;
            this.secondPoint = secondPoint; 
        }   
    }
}
