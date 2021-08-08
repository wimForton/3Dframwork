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
        public static void UpdateConnectionLines(List<IRenderableGeo> myRenderGeoList)
        {
        

            Wimapp3D.MainWindow.AppWindow.MainWindowCanvasConnections.Children.Clear();
            for (int i = 0; i < myRenderGeoList.Count; i++)
            {
                if (myRenderGeoList[i].InputObject != null)
                {
                    double startX = myRenderGeoList[i].GuiNode.myTranslate.X;
                    double startY = myRenderGeoList[i].GuiNode.myTranslate.Y;
                    double endX = myRenderGeoList[i].InputObject.GuiNode.myTranslate.X;
                    double endY = myRenderGeoList[i].InputObject.GuiNode.myTranslate.Y;
                    Path myPath = DrawConnection.DrawCurve(0.3, startX + 35, startY, endX + 35, endY + 90);
                    Wimapp3D.MainWindow.AppWindow.MainWindowCanvasConnections.Children.Add(myPath);
                }

            }
        }
    }
}
