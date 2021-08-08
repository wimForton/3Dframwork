using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace GameEngine
{
    class Noise : RenderableGeo, IRenderableGeo
    {
        IRenderableGeo inputObject;
        public Noise(IRenderableGeo inObject)
        {
            inputObject = inObject;
            Points = inObject.Points;
            UVs = inObject.UVs;
            Normals = inObject.Normals;
            Polygons = inObject.Polygons;

        }

        public override void OpenProportiesWindow()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            Points = inputObject.Points;
            UVs = inputObject.UVs;
            Normals = inputObject.Normals;
            Polygons = inputObject.Polygons;
            Random myRandom = new Random();
            for (int i = 0; i < Points.Count; i++)
            {
                Points[i] *= myRandom.NextDouble() * 0.2;

            }
            MakeVaoList();
        }
    }
}
