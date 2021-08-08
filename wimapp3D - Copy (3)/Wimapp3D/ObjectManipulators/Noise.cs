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
        private int Iterations = 3;
        private Vector Frequency = new Vector(2.0,2.0,2.0);

        //public List<Vector> inputPoints { get; set; } = new List<Vector>();

        public Noise(IRenderableGeo inObject)
        {
            NeedsUpdate = true;
            inputObject = inObject;
            //Points = inObject.Points;
            //for (int i = 0; i < inObject.Points.Count; i++)
            //{
                Points = inputObject.Points.ToList();
            //}


            UVs = inObject.UVs;
            Normals = inObject.Normals;
            Polygons = inObject.Polygons;
        }
        public override float[] MakeVaoArray()
        {
            if (inputObject.OutputNeedsUpdate)
            {
                Update();
                VaoArray = myVaoList.ToArray();
                //MessageBox.Show("MakeVaoArray");
                //inputObject.OutputNeedsUpdate = false;
            }
            
            return VaoArray;
        }


        public override void OpenProportiesWindow()
        {
            throw new NotImplementedException();
        }
        public override void CheckProportiesWindow()
        {
            //MessageBox.Show("check");
        }
        public override void Update()
        {
            Points = inputObject.Points.ToList();
            UVs = inputObject.UVs;
            Normals = inputObject.Normals;
            Polygons = inputObject.Polygons;
            Random myRandom = new Random(123);
            for (int i = 0; i < Points.Count; i++)
            {
                Points[i] = Points[i] * (1 + MakeNoise(Points[i]));
            }
            //MessageBox.Show(Convert.ToString(Points[0].GetHashCode()) + "   " + Convert.ToString(Points[0].GetHashCode()));
            MakeVaoList();
        }

        private double MakeNoise(Vector vector)
        {
            return MyMath.PerlinNoise(vector, Frequency, Iterations); 
        }
    }
}
