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
    class MultiPrimitive : RenderableGeo, IRenderableGeo
    {
        private double Rows { get; set; } = 3;
        private double Columns { get; set; } = 100;
        private double Pi { get; set; } = 3.14159265358979323846;
        private double WrapStart { get; set; } = 0;
        private double WrapEnd { get; set; } = 1.0;
        private double RowWrapStart { get; set; } = 0.0;
        private double RowWrapEnd { get; set; } = 0.5;
        private double Middle { get; set; } = 0.0;
        private double Roll { get; set; } = 0.0;
        private double SphereRadius { get; set; } = 1;

        PropertyControllerGrid PropertyGrid;


        public MultiPrimitive(int inRows, int inCols, string inName)
        {
            isRootGeoNode = true;
            Name = inName;
            Rows = inRows;
            Columns = inCols;
            
            PropertyGrid = new PropertyControllerGrid(Name);
            AnimationControls = new List<IAnimationControl>
            {
                new KeyFrameSlider("Rows", Rows, 2, 100, 1),
                new KeyFrameSlider("Cols", Columns, 2, 100, 1),
                new KeyFrameSlider("Ystart", WrapStart, 0, 1, 0.01),
                new KeyFrameSlider("Yend", WrapEnd, 0, 1, 0.01),
                new KeyFrameSlider("Xstart", RowWrapStart, 0, 1, 0.01),
                new KeyFrameSlider("Xend", RowWrapEnd, 0, 1, 0.01),
                new KeyFrameSlider("MiddHole", Middle, 0, 5, 0.01),
                new KeyFrameSlider("Roll", Roll, -3, 3, 0.01),
                new KeyFrameSlider("Radius", SphereRadius, 0, 3, 0.01)
            };
            for (int i = 0; i < AnimationControls.Count; i++)
            {
                PropertyGrid.ControlsStackPanel.Children.Add(AnimationControls[i].AnimCtrlGrid);
                AnimationControls[i].mySlider.ValueChanged += Sliders_ValueChanged;
            }
            GuiNode = new NodeGuiElement(this);
            Wimapp3D.MainWindow.AppWindow.MainWindowCanvas.Children.Add(GuiNode);
        }

        private void Sliders_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Rows = (int)AnimationControls[0].mySlider.Value;
            Columns = (int)AnimationControls[1].mySlider.Value;
            WrapStart = AnimationControls[2].mySlider.Value;
            WrapEnd = AnimationControls[3].mySlider.Value;
            RowWrapStart = AnimationControls[4].mySlider.Value;
            RowWrapEnd = AnimationControls[5].mySlider.Value;
            Middle = AnimationControls[6].mySlider.Value;
            Roll = AnimationControls[7].mySlider.Value;
            SphereRadius = AnimationControls[8].mySlider.Value;

            NeedsUpdate = true;
        }

        public override void OpenProportiesWindow()
        {
            if (Wimapp3D.MainWindow.AppWindow.ProportieWindowStack.Children.IndexOf(PropertyGrid) < 0)
            {
                Wimapp3D.MainWindow.AppWindow.ProportieWindowStack.Children.Add(PropertyGrid);
            }
        }

        public override void Update()
        {
            //CheckProportiesWindow();
            if (NeedsUpdate)
            {
                BuildObject();
                MakeVaoList();
                OutputNeedsUpdate = true;
            }
            NeedsUpdate = false;
        }
        private void BuildObject()
        {
            Points.Clear();
            UVs.Clear();
            Normals.Clear();
            Polygons.Clear();

            for (int row = 0; row <= Rows; row++)
            {
                for (int col = 0; col <= Columns; col++)
                {
                    Vector pos = new Vector(0.0, 0.0, 0.0);
                    Vector norm = new Vector(0.0, 0.0, 0.0);
                    //cylinder
                    double wrapPos = MyMath.Fit(col / Columns, 0, 1, WrapStart, WrapEnd) * 2 * Pi;
                    double sphereWrapPos = MyMath.Fit(row / Rows, 0, 1, RowWrapStart, RowWrapEnd) * 2 * Pi;
                    pos.X = Math.Sin(wrapPos);//Math.Sin
                    pos.Z = Math.Cos(wrapPos);

                    //deform to sphereshape
                    pos.X *= Math.Sin(sphereWrapPos + Roll) * SphereRadius;
                    pos.Z *= Math.Sin(sphereWrapPos + Roll) * SphereRadius;
                    pos.Y = Math.Cos(sphereWrapPos + Roll) * SphereRadius;
                    norm.X = pos.X;//simple normals based on offset
                    norm.Z = pos.Z;
                    norm.Y = pos.Y;
                    Vector.Normalize(norm);
                    //offset
                    pos.X += Math.Sin(wrapPos) * Middle;
                    pos.Z += Math.Cos(wrapPos) * Middle;

                    Points.Add(pos);
                    Vector myUV = new Vector(row / Rows, col / Columns, 0);// set(row / rows, col / cols, 0);
                    UVs.Add(myUV);
                    //Vector myNormal = new Vector(0.0, 1.0, 0.0);
                    Normals.Add(norm);
                }
            }

            int startIndex = 0;
            for (int row = 0; row <= Rows; row++)
            {
                for (int col = 0; col <= Columns; col++)
                {
                    if (row < Rows && col < Columns)
                    {
                        Polygon myPoly = new Polygon();

                        myPoly.Vertices.Add(startIndex);
                        myPoly.Vertices.Add(startIndex + 1);
                        myPoly.Vertices.Add(startIndex + (int)Columns + 2);
                        myPoly.Vertices.Add(startIndex + (int)Columns + 1);
                        myPoly.UVs.Add(startIndex);
                        myPoly.UVs.Add(startIndex + 1);
                        myPoly.UVs.Add(startIndex + (int)Columns + 2);
                        myPoly.UVs.Add(startIndex + (int)Columns + 1);
                        myPoly.Normals.Add(startIndex);
                        myPoly.Normals.Add(startIndex + 1);
                        myPoly.Normals.Add(startIndex + (int)Columns + 2);
                        myPoly.Normals.Add(startIndex + (int)Columns + 1);
                        Polygons.Add(myPoly);
                    }
                    startIndex++;
                }
            }
        }
    }
}
