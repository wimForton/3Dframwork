using GameEngine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;



namespace Wimapp3D
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //RenderOpenGlTemplate gameEngine;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow wnd = new MainWindow();           
            wnd.Title = "Mixer 1.0";
            wnd.Show();
            wnd.StartGame();
        }
    }
}
