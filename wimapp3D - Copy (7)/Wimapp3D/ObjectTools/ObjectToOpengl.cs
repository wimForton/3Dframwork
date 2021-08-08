using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class ObjectToOpengl
    {
        //List<>
        public ObjectToOpengl()
        {

        }
        public static List<float> MakeVaoList(IRenderableGeo inObject)
        {
            List<float> myVaoList = new List<float>();
            foreach (var poly in inObject.Polygons)//We use only triangles in our engine 
            {
                myVaoList.AddRange(VertexToVao(0, poly, inObject));
                myVaoList.AddRange(VertexToVao(1, poly, inObject));
                myVaoList.AddRange(VertexToVao(2, poly, inObject));
                if (poly.Vertices.Count == 4)//create 2 triangles per quad in the right order (or 1triangle per triangle)
                {
                    myVaoList.AddRange(VertexToVao(2, poly, inObject));
                    myVaoList.AddRange(VertexToVao(3, poly, inObject));
                    myVaoList.AddRange(VertexToVao(0, poly, inObject));
                }
            }
            return myVaoList;
        }

        public static List<float> VertexToVao(int inVertexIndex, Polygon poly, IRenderableGeo inObject)
        {
            List<float> myVaoList = new List<float>();
            Vector myPoint = inObject.Points[poly.Vertices[inVertexIndex]] + inObject.Position;
            myVaoList.Add((float)myPoint.X);
            myVaoList.Add((float)myPoint.Y);
            myVaoList.Add((float)myPoint.Z);
            Vector myUV = inObject.UVs[poly.UVs[inVertexIndex]];
            myVaoList.Add((float)myUV.X);//OpenGL takes only 2 UV coordinates
            myVaoList.Add((float)myUV.Y);
            Vector myNormal = inObject.Normals[poly.Normals[inVertexIndex]];
            myVaoList.Add((float)myNormal.X);
            myVaoList.Add((float)myNormal.Y);
            myVaoList.Add((float)myNormal.Z);
            //Vector myColor = poly.Colors[inVertexIndex];
            //myVaoList.Add((float)myColor.X);
            //myVaoList.Add((float)myColor.Y);
            //myVaoList.Add((float)myColor.Z);
            return myVaoList;
        }
    }
}
