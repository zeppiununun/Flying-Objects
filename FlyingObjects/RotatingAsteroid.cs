using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FlyingObjects
{
    class RotatingAsteroid: Asteroid
    {
        protected double rotSpeed;
        protected double angle;
        static Random rndrot=new Random();
        public  RotatingAsteroid(Canvas c, double minspeed, double maxspeed, double minrspeed, double maxrspeed, String imageRes): base(c, minspeed, maxspeed, imageRes)
        {
            rotSpeed = minrspeed + (maxrspeed - minrspeed) * rndrot.NextDouble();
            angle = 360 * rndrot.NextDouble();
        }

        override public void Animate(TimeSpan ts)
        {
            RotateTransform rotateTransform = new RotateTransform(angle, centroid.X, centroid.Y);
            img.RenderTransform = rotateTransform;
            shape.RenderTransform = rotateTransform;

            for(int i=0; i< shape.Points.Count; i++)
                shape.Points[i] = rotateTransform.Transform(shape.Points[i]);
            // shape.Points.ToList().ForEach(p => rotateTransform.Transform(p));

            angle += rotSpeed;
            angle = angle % 360;
            base.Animate(ts);
        }
    }
}
