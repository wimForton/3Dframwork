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
        RenderOpenGlTemplate gameEngine;
        //RenderOpenGlTemplate gameEngine;
        public MainWindow()
        {
            InitializeComponent();
            AppWindow = this;
        }

        private void StartGame(object sender, MouseButtonEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");

            List<IRenderableGeo> myObjects = new List<IRenderableGeo>();
            for (int i = 0; i < 1; i++)
            {
                IRenderableGeo object3D = new MultiPrimitive(20, 20, "object" + Convert.ToString(i));
                object3D.Position.X = i;
                object3D.Scale.Y = (double)i/6;
                myObjects.Add(object3D);
            }



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
            //Button newBtn = new Button();
            //newBtn.Content = "A New Button";
            //MainWindowStack.Children.Add(newBtn);
        }
    }
}
