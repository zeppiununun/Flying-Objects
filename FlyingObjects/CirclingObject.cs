using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FlyingObjects
{
    class CirclingObject: MovingObject 
    {
        double x0;
        double y0;
        double v;
        public CirclingObject(Canvas c, double x0, double y0, double r, double alpha, double speed, String imageRes) : base(c, x0 + r * Math.Cos(alpha), y0 + r * Math.Sin(alpha), -speed*Math.Sin(alpha), speed*Math.Cos(alpha), imageRes)
         {
            this.x0 = x0;
            this.y0 = y0;
            v = speed;
        }
        override public void Animate(TimeSpan ts)
         {
           Vector w = new Vector(y0 - y, x - x0);
            double k = w.Length;
            vx = (v / k) * w.X;
            vy = (v / k) * w.Y;
            base.Animate(ts);

        }


    }
}
