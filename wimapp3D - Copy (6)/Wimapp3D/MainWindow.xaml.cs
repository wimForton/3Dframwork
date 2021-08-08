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
    //<Grid>
    //    <Button Content = "StartGame" PreviewMouseLeftButtonDown="StartGame" HorizontalAlignment="Left" Margin="10,10,0,0" VerticalAlignment="Top" Height="100" Width="189"/>
    //    <Button Content = "add multiprim" PreviewMouseLeftButtonDown="AddMultiPrim" HorizontalAlignment="Left" Margin="10,128,0,0" VerticalAlignment="Top" Height="66" Width="129" Click="Button_Click"/>
    //</Grid>
    public partial class MainWindow : Window
    {
        public static MainWindow AppWindow;
        RenderOpenGlTemplate GameEngine { get; set; }
        double OffsetX;
        double OffsetY;

        //RenderOpenGlTemplate gameEngine;
        public MainWindow()
        {
            InitializeComponent();
            AppWindow = this;
            OffsetX = 0;
            OffsetY = 0;
        }

        public void StartGame()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");

            List<IRenderableGeo> myObjects = new List<IRenderableGeo>();
            IRenderableGeo object3D = new MultiPrimitive(20, 20, "primitive1");
            object3D.Position.X = -6;
            myObjects.Add(object3D);

            IRenderableGeo object3D2 = new MultiPrimitive(20, 20, "primitive2");
            object3D2.Position.X = -4;
            myObjects.Add(object3D2);

            IRenderableGeo Twist = new Twist(myObjects[0]);
            Twist.Position.X = -2;
            myObjects.Add(Twist);

            IRenderableGeo Twist0 = new Twist(myObjects[0]);
            Twist0.Position.X = 0;
            myObjects.Add(Twist0);

            IRenderableGeo Twist1 = new Twist(myObjects[1]);
            Twist1.Position.X = 2;
            myObjects.Add(Twist1);

            IRenderableGeo Twist2 = new Twist(myObjects[4]);
            Twist2.Position.X = 4;
            myObjects.Add(Twist2);


            GameEngine = new RenderOpenGlTemplate(myObjects, 30f, 1280, 720, "Mixer");
            GameEngine.Start();
            GameEngine.Run();
        }
        private void AddMultiPrim(object sender, MouseButtonEventArgs e)
        {
            IRenderableGeo myMultiPrim = new MultiPrimitive(10, 10, "primitive" + Convert.ToString(GameEngine.myRendergeo.Count + 1));
            myMultiPrim.GuiNode.myTranslate.X = 130;
            myMultiPrim.GuiNode.myTranslate.Y = 130;
            myMultiPrim.GuiNodePosition.X = 130;
            myMultiPrim.GuiNodePosition.Y = 130;
            GameEngine.myRendergeo.Add(myMultiPrim);
        }
        private void LayoutNodes(object sender, MouseButtonEventArgs e)
        {
            IterateGeoTree.LayoutNodes(GameEngine.myRendergeo);
            UpdateCanvas();
        }
        private void ClearProportieStack(object sender, MouseButtonEventArgs e)
        {
            ProportieWindowStack.Children.Clear();
        }
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.MiddleButton == MouseButtonState.Pressed)
            {

                Point mousePos = e.GetPosition(MainWindowCanvasConnections);

                for (int i = 0; i < GameEngine.myRendergeo.Count; i++)
                {
                    GameEngine.myRendergeo[i].GuiNodePosition.X += OffsetX - mousePos.X;
                    GameEngine.myRendergeo[i].GuiNodePosition.Y += OffsetX - mousePos.Y;
                    GameEngine.myRendergeo[i].GuiNode.myTranslate.X -= OffsetX - mousePos.X;
                    GameEngine.myRendergeo[i].GuiNode.myTranslate.Y -= OffsetY - mousePos.Y;
                    
                }
                UpdateCanvas();

                double pos = e.GetPosition(MainWindowCanvasConnections).X;
                
                OffsetX = mousePos.X;
                OffsetY = mousePos.Y;
            }
            else if (e.MiddleButton == MouseButtonState.Released)
            {
                Point mousePos = e.GetPosition(MainWindowCanvasConnections);
                OffsetX = mousePos.X;
                OffsetY = mousePos.Y;
                Title = Convert.ToString(OffsetX) + "_" + Convert.ToString(mousePos.X);
            }
        }
        public void UpdateCanvas()
        {
            if(GameEngine != null)
            {
                DrawConnectionlines.UpdateConnectionLines(GameEngine.myRendergeo);
            }

        }
    }
}
