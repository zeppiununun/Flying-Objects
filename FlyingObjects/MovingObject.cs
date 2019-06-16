using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FlyingObjects
{
    public class MovingObject : DependencyObject
    {
       protected int id;
       public int ID { get { return id; } set { id = value;} }
      
       protected double x;
       protected double y;
       public double  X { get { return x; } }
       public double Y { get { return y; } }

       protected double vx;
       public double VX { get { return vx; } }
       protected double vy;
       public double VY { get { return vy; } }
       protected Point centroid;
       public Point C { get { return centroid; } }
       protected double radius;
       public double R { get { return radius; } }
       protected Image img;
       protected Canvas c;
       protected Polygon shape =new Polygon();
       public event EventHandler<OutOfBoundArgs> OutOfBound;
       public readonly int horizontalTop = 1;
       public readonly int horizontalLow = 2;
       public readonly int verticalLeft = 3;
       public readonly int verticalRight = 4;

        private void initialize(Canvas  c, double x, double y, double vx, double vy, String imageRes)
        {
            this.x = x;
            this.y = y;
            this.vx = vx;
            this.vy = vy;
            this.c = c;

            BitmapImage bitimg = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), imageRes));

            img = new Image();
            img.Height = bitimg.PixelHeight;
            img.Width = bitimg.PixelWidth;

            shape.Points.Add(new Point(0, 0));
            shape.Points.Add(new Point(0, img.Height));
            shape.Points.Add(new Point(img.Width, img.Height));
            shape.Points.Add(new Point(img.Width, 0));

            img.Source = bitimg;
            Canvas.SetLeft(img, x);
            Canvas.SetTop(img, y);
            c.Children.Add(img);
            centroid = calcCentroid();
            radius = getRadius();

        }

        public Point calcCentroid()
        {
            
            Point center= new Point(0,0);
            
            foreach (Vector p in shape.Points)
            {
                center = Point.Add(center, p);
            }
            return (new Point(center.X/shape.Points.Count, center.Y / shape.Points.Count));
          

        }

        public double getRadius()
        {
            double r= Point.Subtract(centroid, shape.Points[0]).Length;
            foreach (Point p in shape.Points)
                if (r > Point.Subtract(centroid, p).Length)
                    r = Point.Subtract(centroid, p).Length;
            return (r);
        }

        public MovingObject(Canvas  c, double x, double y, double vx, double vy, String imageRes)
        {
            initialize(c, x, y, vx, vy, imageRes);
           

        }
        public MovingObject(Canvas c, Point pos, Point speedvec, String imageRes)
        {
            initialize(c, pos.X, pos.Y, speedvec.X, speedvec.Y, imageRes);
            
        }

        public void setPolygon(PointCollection pc)
        {
            shape.Points = pc.Clone();
            centroid = calcCentroid();
            radius = getRadius();

        }

        public PointCollection getPolygon()
        {
            return shape.Points;
        }   


        virtual public void Animate(TimeSpan ts)
        {
            x+=vx* ts.TotalSeconds;
            y+= vy * ts.TotalSeconds;
            

            if (x>=c.ActualWidth)
            {
                OutOfBoundArgs e = new OutOfBoundArgs();
                e.code = verticalRight;
                e.coordinate = y;
                c.Children.Remove(img);
                OnOutofBound(e);
                return;
            }

            if (x <-img.Width)
            {
                OutOfBoundArgs e = new OutOfBoundArgs();
                e.code = verticalLeft;
                e.coordinate = y;
                c.Children.Remove(img);
                OnOutofBound(e);
                return;
            }
            
            if (y >= c.ActualHeight)
            {
                OutOfBoundArgs e = new OutOfBoundArgs();
                e.code = horizontalLow;
                e.coordinate = x;
                c.Children.Remove(img);
                OnOutofBound(e);
                return;
            }

            if (y < -img.Height)
            {
                OutOfBoundArgs e = new OutOfBoundArgs();
                e.code = horizontalTop;
                e.coordinate = x;
                c.Children.Remove(img);
                OnOutofBound(e);
                return;
            }

            Canvas.SetLeft(img, x);
            Canvas.SetTop(img, y);

        }

        protected virtual void OnOutofBound(OutOfBoundArgs e)
        {
            EventHandler<OutOfBoundArgs> handler = OutOfBound;
;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public class OutOfBoundArgs : EventArgs
        {
            public int code{ get; set; }
            public double coordinate { get; set; }
        }


    }
}
