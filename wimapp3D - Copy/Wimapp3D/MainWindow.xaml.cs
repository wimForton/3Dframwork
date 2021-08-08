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

namespace Wimapp3D
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RenderOpenGlTemplate gameEngine;
        public MainWindow()
        {
            InitializeComponent();


        }
        private void StartGame(object sender, MouseButtonEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");

            List<IRenderableGeo> myObjects = new List<IRenderableGeo>();

            //myObjects.Add(new PolyObjectLoader("PionA.obj"));
            //myObjects.Add(new PolyObjectLoader("PionB.obj"));
            //myObjects.Add(new PolyObjectLoader("PionC.obj"));
            //myObjects.Add(new PolyObjectLoader("PionD.obj"));
            //myObjects.Add(new PolyObjectLoader("GanzenBord.obj"));
            //SpriteText myText = new SpriteText("TT");

            myObjects.Add(new MultiPrimitive(20, 20));

            gameEngine = new RenderOpenGlTemplate(myObjects, 30f, 1280, 720, "Ganzenbord 3D");
            gameEngine.Start();
            gameEngine.Run();
        }
        private void AdvanceGame(object sender, MouseButtonEventArgs e)
        {
            //gameEngine.AdvanceGame();
            //gameEngine.Run();
        }
        private void EndGame(object sender, MouseButtonEventArgs e)
        {
            gameEngine.End();
        }
    }
}
