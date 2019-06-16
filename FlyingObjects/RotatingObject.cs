using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace FlyingObjects
  {
    
    class RotatingObject:MovingObject
    {
        protected double rotSpeed;
        protected double angle;

        public RotatingObject(Canvas c, double x, double y, double vx, double vy, double angle, double w, String imageRes): base(c,x,y,vx, vy, imageRes)
        {
            this.angle = angle;
            rotSpeed = w;
        }

        override public void Animate(TimeSpan ts)
        {
            RotateTransform rotateTransform = new RotateTransform(angle, centroid.X, centroid.Y);
            img.RenderTransform = rotateTransform;
            shape.RenderTransform = rotateTransform;
            for (int i = 0; i < shape.Points.Count; i++)
                shape.Points[i] = rotateTransform.Transform(shape.Points[i]);
            angle +=rotSpeed;
            angle = angle % 360;
            base.Animate(ts);
        }
    }
}
