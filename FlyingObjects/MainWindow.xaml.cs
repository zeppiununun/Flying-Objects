using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FlyingObjects
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer dt;
        int counter = 0;
        Dictionary<int, MovingObject> flyingobjects =
            new Dictionary<int, MovingObject>();
        List<int> toBeRemoved = new List<int>();
        PointCollection asteroid2PC = new PointCollection();


        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine(BaseUriHelper.GetBaseUri(this));
            this.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Resources/bg_space.jpg")));
            drawingArea.Height = 410;
            drawingArea.Width = 790;

            asteroid2PC.Add(new Point(1,55));
            asteroid2PC.Add(new Point(2,45));
            asteroid2PC.Add(new Point(20,16));
            asteroid2PC.Add(new Point(50, 1));
            asteroid2PC.Add(new Point(58, 2));
            asteroid2PC.Add(new Point(92, 29));
            asteroid2PC.Add(new Point(99, 41));
            asteroid2PC.Add(new Point(98, 62));
            asteroid2PC.Add(new Point(91, 79));
            asteroid2PC.Add(new Point(80, 89));
            asteroid2PC.Add(new Point(47, 100));
            asteroid2PC.Add(new Point(21, 86));

            String fnpref = "Resources/asteroid2";
            String fn;
            for (int i = 0; i < 10; i++)
            {
                flyingobjects.Add(i, null);
                fn = fnpref +i.ToString() +".png";
                flyingobjects[i] = new RotatingAsteroid(drawingArea, 50, 200, -40, 40, fn);
                flyingobjects[i].ID = i;
                flyingobjects[i].setPolygon(asteroid2PC);
                flyingobjects[i].OutOfBound += MovingObject_OutOfBound;
            }

            flyingobjects.Add(10, null);
            flyingobjects[10] = new CirclingObject(drawingArea, 400, 200, 10, 0, 100, "Resources/ufo.png");
            flyingobjects[10].ID = 10;
            flyingobjects[10].OutOfBound += MovingObject_OutOfBound;

            //flyingobjects.Add(11, null);
            //flyingobjects[11] = new RotatingObject(drawingArea, 50, 50, 30, 30, -45, 5, "Resources/asteroid2.png");
            //flyingobjects[11].ID = 11;
            //flyingobjects[11].OutOfBound += MovingObject_OutOfBound;


            dt = new DispatcherTimer();
            dt.Tick += new EventHandler(dispatcherTimer_Tick);

            dt.Interval = new TimeSpan(0, 0, 0, 0, 100);
            dt.Start();
                      
          }

     public void MovingObject_OutOfBound(object sender, EventArgs e)
        {
            if  (!(toBeRemoved.Contains(((MovingObject)sender).ID)))
                toBeRemoved.Add(((MovingObject)sender).ID);
                
        }
        
    public void dispatcherTimer_Tick(object sender, EventArgs e)
        {        
              foreach (KeyValuePair<int, MovingObject> keymo in flyingobjects)
                    if (keymo.Value != null)
                        keymo.Value.Animate(dt.Interval);

            foreach (KeyValuePair<int, MovingObject> keymo1 in flyingobjects)
                foreach (KeyValuePair<int, MovingObject> keymo2 in flyingobjects)
                    if ((keymo1.Value != null) && (keymo2.Value != null) && (keymo1.Key < keymo2.Key))
                        if (SkyManager.isCollision(keymo1.Value, keymo2.Value))
                            Console.WriteLine("Collision of {0} and {1}", keymo1.Key, keymo2.Key);

            String fnpref = "Resources/asteroid2";
            String fn;
            foreach (int id in toBeRemoved)
            {
                flyingobjects.Remove(id);
                flyingobjects.Add(id, null);
                fn = fnpref + id.ToString() + ".png";
                flyingobjects[id] = new RotatingAsteroid(drawingArea, 50, 200,-40, 40, fn);
                flyingobjects[id].ID = id;
                flyingobjects[id].OutOfBound += MovingObject_OutOfBound;
            }
            toBeRemoved.Clear();

            counter++;

            if (counter % 100 ==0)
            {
                counter = 0;
                drawingArea.InvalidateVisual();
            }

    }
}}