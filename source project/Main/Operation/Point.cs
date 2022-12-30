using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main.Operation
{
    class Point
    {
        private double x;
        private double y;
        //set x,y while initializing
        public Point(double xCoor, double yCoor)
        {
            x = xCoor;
            y = yCoor;
        }
        public double GetX()
        {
            return x;
        }
        public double GetY()
        {
            return y;
        }
        public void SetX(double newX)
        {
            x = newX;
        }
        public void SetY(double newY)
        {
            y = newY;
        }
        public double Length(Point a)
        {
            return Math.Sqrt(Math.Pow(x - a.x, 2) + Math.Pow(y - a.y, 2));
        }
        //angle of line from one point to another point
        public double ReachAngle(Point a)
        {
            return ((a.x - x) < 0) ? (Math.Atan2((a.x - x), (a.y - y)) + Math.PI * 2) : (Math.Atan2((a.x - x), (a.y - y)));
        }
    }
}
