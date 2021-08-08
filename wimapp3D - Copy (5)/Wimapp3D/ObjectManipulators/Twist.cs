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
    class Twist : RenderableGeo, IRenderableGeo
    {
        public double TwistAmount { get; set; } = 1;
        TwistControls ProportiesWindow;
        TwistControlsGrid ProportiesGrid;
        public Twist(IRenderableGeo inObject)
        {
            inObject.ChildGeoNodes.Add(this);
            isRootGeoNode = false;
            Name = "Twist";
            ProportiesWindow = new TwistControls(this);
            ProportiesGrid = new TwistControlsGrid(this);
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
            //ProportiesWindow.Show();
            //ProportiesWindow.Focus();
            if(Wimapp3D.MainWindow.AppWindow.ProportieWindowStack.Children.IndexOf(ProportiesGrid) < 0)
            {
                Wimapp3D.MainWindow.AppWindow.ProportieWindowStack.Children.Add(ProportiesGrid);
            }
        }
        public override void CheckProportiesWindow()
        {
            if (ProportiesGrid.NeedsUpdate)
            {
                TwistAmount = ProportiesGrid.sliderTwist.Value;
                ProportiesGrid.NeedsUpdate = false;
                NeedsUpdate = true;
                //MessageBox.Show(Convert.ToString(TwistAmount));
            }
        }
        public override void Update()
        {
            CheckProportiesWindow();
            if (InputObject.OutputNeedsUpdate || NeedsUpdate)
            {
                BuildObject();
                MakeVaoList();
                OutputNeedsUpdate = true;
                NeedsUpdate = false;
            }
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
                Points[i] = MakeTwist(Points[i]);
            }
        }

        private Vector MakeTwist(Vector vector)
        {
            Vector result = new Vector(0,0,0);
            result = Vector.GetEulerRotation(vector, 0, 0, vector.Z * TwistAmount, "zxy");
            return result;
        }
    }
}
