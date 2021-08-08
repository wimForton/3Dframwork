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

        private int Iterations = 2;
        private Vector Frequency = new Vector(2.0,2.0,2.0);

        //public List<Vector> inputPoints { get; set; } = new List<Vector>();

        public Noise(IRenderableGeo inObject)
        {
            inObject.ChildGeoNodes.Add(this);
            isRootGeoNode = false;
            InputObject = inObject;
            ConnectionsStart.Add(this);
            ConnectionsEnd.Add(InputObject);
            NeedsInputObject = true;
            NeedsUpdate = true;

            Points = InputObject.Points.ToList();//make local
            UVs = inObject.UVs;
            Normals = inObject.Normals;
            Polygons = inObject.Polygons;
            AddNodeToCanvas();
        }
        private void AddNodeToCanvas()
        {
            NodeGuiElement myNode = new NodeGuiElement(this);
            Wimapp3D.MainWindow.AppWindow.MainWindowCanvas.Children.Add(myNode);
        }
        public override void OpenProportiesButton(object sender, RoutedEventArgs e)
        {
            OpenProportiesWindow();
        }
        public override void OpenProportiesWindow()
        {
            MessageBox.Show("OpenProportiesWindow");
        }
        public override void CheckProportiesWindow()
        {
            MessageBox.Show("check");
        }
        public override void Update()
        {
            if (InputObject.OutputNeedsUpdate)
            {
                BuildObject();
                MakeVaoList();
                OutputNeedsUpdate = true;
            }
            //InputObject.OutputNeedsUpdate = false;
        }

        private void BuildObject()
        {
            Points = InputObject.Points.ToList();
            UVs = InputObject.UVs;
            Normals = InputObject.Normals;
            Polygons = InputObject.Polygons;
            Random myRandom = new Random(123);
            for (int i = 0; i < Points.Count; i++)
            {
                Points[i] = Points[i] * MakeNoise(Points[i]);
            }
        }

        private double MakeNoise(Vector vector)
        {
            return MyMath.PerlinNoise(vector, Frequency, Iterations); 
        }
    }
}
