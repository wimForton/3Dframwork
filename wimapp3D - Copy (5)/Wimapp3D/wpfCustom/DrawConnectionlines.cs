using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace GameEngine
{
    class DrawConnectionlines
    {
        public static void UpdateConnectionLines(IRenderableGeo myRenderGeo)
        {
        //Wimapp3D.MainWindow.AppWindow.MainWindowCanvasConnections.Children.Clear();
        List<IRenderableGeo> listStart = myRenderGeo.getConnectionsStart();
        List<IRenderableGeo> listEnd = myRenderGeo.getConnectionsEnd();

            Wimapp3D.MainWindow.AppWindow.MainWindowCanvasConnections.Children.Clear();
            for (int i = 0; i < myRenderGeo.getConnectionsStart().Count; i++)
            {
                Vector startPos = listStart[i].GuiNodePosition;
                Vector endPos = listEnd[i].GuiNodePosition;
                Path myPath = DrawConnection.DrawCurve(0.3, startPos.X + 35, startPos.Y, endPos.X + 35, endPos.Y + 90);
                Wimapp3D.MainWindow.AppWindow.MainWindowCanvasConnections.Children.Add(myPath);
            }
        }
    }
}
