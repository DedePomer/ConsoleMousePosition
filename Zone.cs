using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMousePosition
{
    public class Zone
    {
        public POINT firstPoint;
        public POINT secondPoint;

        public Zone(POINT firstPoint, POINT secondPoint) 
        {
            this.firstPoint = firstPoint;
            this.secondPoint = secondPoint; 
        }   
    }
}
