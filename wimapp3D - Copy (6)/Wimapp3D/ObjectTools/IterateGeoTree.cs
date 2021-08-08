using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class IterateGeoTree
    {
        public static void ParentChildIterate(List<IRenderableGeo> inGeoList)
        {

            //for (int i = 0; i < inGeoList.Count; i++)
            //{
            //    inGeoList[i].Update();
            //}

            List<IRenderableGeo> startPoints = new List<IRenderableGeo>();
            for (int i = 0; i < inGeoList.Count; i++)//lijst start met ouders
            {
                if (inGeoList[i].isRootGeoNode)
                {
                    startPoints.Add(inGeoList[i]);
                }
            }
            while (startPoints.Count > 0)
            {
                List<IRenderableGeo> childpoints = new List<IRenderableGeo>();
                for (int i = 0; i < startPoints.Count; i++)//voor alle ouders
                {

                    //if (startPoints[i].NeedsUpdate)
                    //{
                    startPoints[i].Update();
                    //startPoints[i].NeedsUpdate = false;//moet in object gebeuren anders kan object niets beslissen
                    //    startPoints[i].OutputNeedsUpdate = true;
                    //}
                    for (int child = 0; child < startPoints[i].ChildGeoNodes.Count; child++)//voor alle kinderen
                    {
                        if (startPoints[i].OutputNeedsUpdate)//ouders zijn updated, volgende while de kinderen
                        {
                            startPoints[i].ChildGeoNodes[child].NeedsUpdate = true;//alle kinderen krijgen update commando
                        }
                        childpoints.Add(startPoints[i].ChildGeoNodes[child]);
                    }
                    startPoints[i].OutputNeedsUpdate = false;//alle kinderen hebben update commando gekregen dus mag uit

                }
                startPoints.Clear();
                startPoints.AddRange(childpoints);//nieuwe ouders
            }

        }

        public static void LayoutNodes(List<IRenderableGeo> inGeoList)
        {
            int treeDepth = 0;

            List<IRenderableGeo> startPoints = new List<IRenderableGeo>();
            for (int i = 0; i < inGeoList.Count; i++)//lijst start met ouders
            {
                if (inGeoList[i].isRootGeoNode)
                {
                    startPoints.Add(inGeoList[i]);
                }
            }
            while (startPoints.Count > 0)
            {
                
                List<IRenderableGeo> childpoints = new List<IRenderableGeo>();
                for (int i = 0; i < startPoints.Count; i++)//voor alle ouders
                {
                    startPoints[i].GuiNode.myTranslate.X = i * 100 + 15;
                    startPoints[i].GuiNode.myTranslate.Y = treeDepth * 110 + 15;

                    for (int child = 0; child < startPoints[i].ChildGeoNodes.Count; child++)//voor alle kinderen
                    {
                        childpoints.Add(startPoints[i].ChildGeoNodes[child]);
                    }
                }
                startPoints.Clear();
                startPoints.AddRange(childpoints);//nieuwe ouders
                treeDepth++;
            } 

        }
    }
}
