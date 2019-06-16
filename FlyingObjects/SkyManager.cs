using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace FlyingObjects
{
    class SkyManager
    {
        static public bool isCollision(MovingObject o1, MovingObject o2)
        {


            Vector C1 = new Vector(o1.C.X + o1.X, o1.C.Y +o1.Y);
            Vector C2 = new Vector(o2.C.X +o2.X, o2.C.Y + o2.Y);
            

            double l = (C1 - C2).Length;

            if (l > o1.R + o2.R) return false;
            else
            {
                PointCollection pol1 = o1.getPolygon().Clone();
                PointCollection pol2 = o2.getPolygon().Clone();

                for (int i = 0; i < pol1.Count; i++)
                    pol1[i] = new Point(o1.X + pol1[i].X, o1.Y + pol1[i].Y);

                for (int i = 0; i < pol2.Count; i++)
                    pol2[i] = new Point(o2.X + pol2[i].X, o2.Y + pol2[i].Y);

                return isCollisionExact(pol1, pol2);
            }
        }

        static protected double linear(double slope, Point p, Point q)
        {
            return q.Y - p.Y - slope * (q.X -p.X);
        }

        static public bool isCollisionTest(MovingObject o1, MovingObject o2)
        {
            PointCollection pol1 = o1.getPolygon().Clone();
            PointCollection pol2 = o2.getPolygon().Clone();

            for (int i = 0; i < pol1.Count; i++)
                pol1[i]= new Point(o1.X + pol1[i].X, o1.Y+ pol1[i].Y);

            for (int i = 0; i < pol2.Count; i++)
                pol2[i]= new Point(o2.X + pol2[i].X, o2.Y + pol2[i].Y);

            return isCollisionExact(pol1, pol2);
        }

        static protected bool isCollisionExact(PointCollection pc1, PointCollection pc2)
        {
            for (int i = 0; i < pc1.Count; i++)
            {
                double m = (pc1[(i + 1) % pc1.Count].Y - pc1[i].Y) / (pc1[(i + 1) % pc1.Count].X - pc1[i].X);
                double posit = linear(m, pc1[i], pc1[(i + 2) % pc1.Count]);

                int j;
                for (j= 0; j < pc2.Count; j++)
                    if (linear(m, pc1[i], pc2[j]) * posit >= 0) break;

                if (j == pc2.Count) return false;

            }
                       
                return true;
        }

    }
}
