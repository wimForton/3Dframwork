using GameEngine;
using System;
using System.Collections.Generic;
using System.Globalization;
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
//using Wimapp3D.ObjectTypes;

namespace Wimapp3D
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RenderOpenGlTemplate gameEngine;
        //RenderOpenGlTemplate gameEngine;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartGame(object sender, MouseButtonEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");

            List<IRenderableGeo> myObjects = new List<IRenderableGeo>();
            for (int i = 0; i < 2; i++)
            {
                IRenderableGeo object3D = new MultiPrimitive(20, 20, "object" + Convert.ToString(i));
                object3D.Position.X = i;
                object3D.Scale.Y = (double)i/6;
                myObjects.Add(object3D);
            }
            IRenderableGeo objectLoader = new PolyObjectLoader("PionA.obj");
            objectLoader.Position.X = -2;
            myObjects.Add(objectLoader);
            IRenderableGeo noise = new Noise(myObjects[0]);
            noise.Position.Z = 0.5;
            myObjects.Add(noise);


            gameEngine = new RenderOpenGlTemplate(myObjects, 30f, 1280, 720, "Ganzenbord 3D");


            gameEngine.Start();
            gameEngine.Run();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void AddMultiPrim(object sender, MouseButtonEventArgs e)
        {
            gameEngine.myRendergeo.Add(new MultiPrimitive(10,10,"myprim"));
        }
    }
}
