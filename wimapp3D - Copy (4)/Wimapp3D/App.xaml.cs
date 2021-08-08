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
        RenderOpenGlTemplate gameEngine;

        protected override void OnStartup(StartupEventArgs e)
        {

            base.OnStartup(e);
            // here you take control

            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");

            List<IRenderableGeo> myObjects = new List<IRenderableGeo>();
            for (int i = 0; i < 1; i++)
            {
                IRenderableGeo object3D = new MultiPrimitive(20, 20, "object" + Convert.ToString(i));
                object3D.Position.X = i;
                object3D.Scale.Y = (double)i / 6;
                myObjects.Add(object3D);
            }



            gameEngine = new RenderOpenGlTemplate(myObjects, 30f, 1280, 720, "Ganzenbord 3D");


            gameEngine.Start();
            gameEngine.Run();

        }
    }
}
