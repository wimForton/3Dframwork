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
        public double TwistAmount { get; set; } = 0.5;
        TwistControlsGrid PropertyGrid;
        public Twist(IRenderableGeo inObject = null)
        {
            IRenderableGeo.HighestId++;
            Id = IRenderableGeo.HighestId;

            isRootGeoNode = false;
            Name = "Twist";
            PropertyGrid = new TwistControlsGrid(this);
            GuiNode = new NodeGuiElement(this);
            Wimapp3D.MainWindow.AppWindow.MainWindowCanvas.Children.Add(GuiNode);
            PropertyGrid.SliderTwist.mySlider.ValueChanged += Sliders_ValueChanged;

            NeedsInputObject = true;
            NeedsUpdate = true;
            if(inObject != null)
            {
                InputObject = inObject;
                InputObject.ChildGeoNodes.Add(this);
                Points = InputObject.Points.ToList();//make local
                UVs = inObject.UVs;
                Normals = inObject.Normals;
                Polygons = inObject.Polygons;
                NeedsUpdate = true;
            }
            else
            {
                NeedsUpdate = false;
            }
        }

        private void Sliders_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TwistAmount = PropertyGrid.SliderTwist.mySlider.Value;
            NeedsUpdate = true;
        }

        public override void OpenProportiesWindow()
        {
            if(Wimapp3D.MainWindow.AppWindow.ProportieWindowStack.Children.IndexOf(PropertyGrid) < 0)
            {
                Wimapp3D.MainWindow.AppWindow.ProportieWindowStack.Children.Add(PropertyGrid);
            }
        }

        public override void Update()
        {
            //CheckProportiesWindow();
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
