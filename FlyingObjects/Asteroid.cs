using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FlyingObjects
{
    public class Asteroid : MovingObject
    {
        static Random rnd = new Random();
        static Point getRandomSpeedVector(double minspeed, double maxspeed)
        {
            double alpha = 2 * Math.PI * rnd.NextDouble();
            double r = minspeed + (maxspeed - minspeed) * rnd.NextDouble();

            return new Point(r * Math.Cos(alpha), r * Math.Sin(alpha));
        }

        public Asteroid(Canvas c, double minspeed, double maxspeed, String imageRes) : base(c, new Point(c.Width*rnd.NextDouble(), c.Height*rnd.NextDouble()), getRandomSpeedVector(minspeed, maxspeed), imageRes)
        {
        
        }
    }
}