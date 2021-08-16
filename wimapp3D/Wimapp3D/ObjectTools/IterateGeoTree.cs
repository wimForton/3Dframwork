using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class IterateGeoTree
    {
        public static void OffsetChildIds(List<IRenderableGeo> inGeoList)
        {
            
            if (inGeoList != null)
            {
                for (int i = 0; i < inGeoList.Count; i++)
                {
                    if(inGeoList[i].Id > IRenderableGeo.OffsetId - 1)
                    {
                        for (int j = 0; j < inGeoList[i].ChildGeoNodeIds.Count; j++)
                        {
                            inGeoList[j].ChildGeoNodeIds[j] += IRenderableGeo.OffsetId;
                        }
                    }
                }
            }
        }
        public static void RefreshNodes(List<IRenderableGeo> inGeoList) 
        {
            if(inGeoList != null)
            {
                for (int i = 0; i < inGeoList.Count; i++)
                {
                    inGeoList[i].GuiNode.myTranslate.X = inGeoList[i].GuiNodePosition.X;
                    inGeoList[i].GuiNode.myTranslate.Y = inGeoList[i].GuiNodePosition.Y;
                    if (inGeoList[i].isRootGeoNode)
                    {
                        inGeoList[i].NeedsUpdate = true;
                    }
                }
            }
        }
        public static void ReconnectNodes(List<IRenderableGeo> inGeoList)
        {
            foreach (var renderableGeo in inGeoList)
            {
                foreach (var childNodeId in renderableGeo.ChildGeoNodeIds)
                {
                    renderableGeo.ChildGeoNodes.Add(inGeoList[childNodeId]);
                    inGeoList[childNodeId].InputObject = renderableGeo;
                }
            }
        }
        public static void ParentChildIterate(List<IRenderableGeo> inGeoList)
        {



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

                    startPoints[i].Update();//indien needsupdate == false gebeurt er niets

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
                if (inGeoList[i].isRootGeoNode || inGeoList[i].InputObject == null)
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
