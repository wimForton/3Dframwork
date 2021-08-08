using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    abstract class RenderableGeo : IRenderableGeo
    {
        public Vector Position { get; set; } = new Vector(0, 0, 0);
        public List<Polygon> Polygons { get; set; } = new List<Polygon>();
        public List<Vector> Points { get; set; } = new List<Vector>();
        public List<Vector> UVs { get; set; } = new List<Vector>();
        public List<Vector> Normals { get; set; } = new List<Vector>();
        public List<Particle> myParticles { get; set; } = new List<Particle>();
        public string Name { get; set; } = "Unnamed";
        private List<float> myVaoList = new List<float>();
        public abstract void Update();
        public virtual void UpdateVAO() { }

        public List<float> MakeVaoList()
        {
            myVaoList.Clear();
            foreach (var poly in Polygons)//We use only triangles in our engine 
            {
                VertexToVao(0, poly);
                VertexToVao(1, poly);
                VertexToVao(2, poly);
                if (poly.Vertices.Count == 4)//create 2 triangles per quad in the right order (or 1triangle per triangle)
                {
                    VertexToVao(2, poly);
                    VertexToVao(3, poly);
                    VertexToVao(0, poly);
                }
            }
            return myVaoList;
        }

        public void VertexToVao(int inVertexIndex, Polygon poly)
        {
            //List<float> myVaoList = new List<float>();
            Vector myPoint = Points[poly.Vertices[inVertexIndex]] + Position;
            myVaoList.Add((float)myPoint.X);
            myVaoList.Add((float)myPoint.Y);
            myVaoList.Add((float)myPoint.Z);
            Vector myUV = UVs[poly.UVs[inVertexIndex]];
            myVaoList.Add((float)myUV.X);//OpenGL takes only 2 UV coordinates
            myVaoList.Add((float)myUV.Y);
            Vector myNormal = Normals[poly.Normals[inVertexIndex]];
            myVaoList.Add((float)myNormal.X);
            myVaoList.Add((float)myNormal.Y);
            myVaoList.Add((float)myNormal.Z);
            //Vector myColor = poly.Colors[inVertexIndex];
            //myVaoList.Add((float)myColor.X);
            //myVaoList.Add((float)myColor.Y);
            //myVaoList.Add((float)myColor.Z);
        }
        public List<float> GetVAO()
        {//We don't update each frame, just return the list
            return myVaoList;
        }
        public override string ToString()
        {
            string particleString = "";
            foreach (var item in myParticles)
            {
                particleString += $"{item.Pos},";
                particleString += $"{item.RGB},";
                particleString += $"{item.Vel}";
                particleString += "\n";
            }
            return particleString;
        }
    }
}
