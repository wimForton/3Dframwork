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

        private double Rows { get; set; } = 3;//////////////////////////////////////hier "AnimatableParameter IPV double"
        private double Columns { get; set; } = 100;
        private double WrapStart { get; set; } = 0;
        private double WrapEnd { get; set; } = 1.0;
        private double RowWrapStart { get; set; } = 0.0;
        private double RowWrapEnd { get; set; } = 0.5;
        private double Middle { get; set; } = 0.0;
        private double Roll { get; set; } = 0.0;
        private double SphereRadius { get; set; } = 1;

        private AnimatableParameter ApRows { get; set; }//////////////////////////////////////hier "AnimatableParameter IPV double"
        private AnimatableParameter ApColumns { get; set; }
        private AnimatableParameter ApWrapStart { get; set; }
        private AnimatableParameter ApWrapEnd { get; set; }
        private AnimatableParameter ApRowWrapStart { get; set; }
        private AnimatableParameter ApRowWrapEnd { get; set; }
        private AnimatableParameter ApMiddle { get; set; }
        private AnimatableParameter ApRoll { get; set; }
        private AnimatableParameter ApSphereRadius { get; set; }



        public MultiPrimitive() : base()
        {
            isRootGeoNode = true;
            Name = "multiprim";
            Rows = 10;
            Columns = 10;
            PropertyGrid.GridName = Name;
            if (AnimatableParameters == null)
            {
                AnimatableParameters = new List<AnimatableParameter>()
                {
                    new AnimatableParameter(3),
                    new AnimatableParameter(10),
                    new AnimatableParameter(0),
                    new AnimatableParameter(1),
                    new AnimatableParameter(0),
                    new AnimatableParameter(0.5),
                    new AnimatableParameter(0),
                    new AnimatableParameter(0),
                    new AnimatableParameter(1),
                };
            }
            if(AnimationControls == null)
            {
                AnimationControls = new List<IAnimationControl>
                {
                    new KeyFrameSlider(0, "Rows", Rows, 2, 100, 1),
                    new KeyFrameSlider(1, "Cols", Columns, 2, 100, 1),
                    new KeyFrameSlider(2, "Ystart", WrapStart, 0, 1, 0.01),
                    new KeyFrameSlider(3, "Yend", WrapEnd, 0, 1, 0.01),
                    new KeyFrameSlider(4, "Xstart", RowWrapStart, 0, 1, 0.01),
                    new KeyFrameSlider(5, "Xend", RowWrapEnd, 0, 1, 0.01),
                    new KeyFrameSlider(6, "MiddHole", Middle, 0, 5, 0.01),
                    new KeyFrameSlider(7, "Roll", Roll, -6, 6, 0.01),
                    new KeyFrameSlider(8, "Radius", SphereRadius, 0, 3, 0.01)
                };
                for (int i = 0; i < AnimationControls.Count; i++)
                {
                    PropertyGrid.ControlsStackPanel.Children.Add(AnimationControls[i].AnimCtrlGrid);
                    AnimationControls[i].mySlider.ValueChanged += Sliders_ValueChanged;
                    AnimationControls[i].SetKeyButton.Click += SetKeyButton_Click;
                }
            }
            AnimationTime.Instance.PropertyChanged += Instance_PropertyChanged;
            if (GuiNode == null)
            {
                GuiNode = new NodeGuiElement(this);
                Wimapp3D.MainWindow.AppWindow.MainWindowCanvas.Children.Add(GuiNode);
            }

        }
        //private void SetKeyButton_Click(object sender, RoutedEventArgs e)
        //{
        //    int myValue = (int)((Button)sender).Tag;
        //    AnimatableParameters[myValue].SetKeyAtFrame(AnimationControls[myValue].Value, AnimationTime.Instance.Frame);
        //}

        private void Sliders_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Rows = (int)AnimationControls[0].Value;
            Columns = (int)AnimationControls[1].Value;
            WrapStart = AnimationControls[2].Value;
            WrapEnd = AnimationControls[3].Value;
            RowWrapStart = AnimationControls[4].Value;
            RowWrapEnd = AnimationControls[5].Value;
            Middle = AnimationControls[6].Value;
            Roll = AnimationControls[7].Value;
            SphereRadius = AnimationControls[8].Value;

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

            if (Rows < 2) Rows = 2;
            if (Columns < 2) Columns = 2;

            for (int row = 0; row <= Rows; row++)
            {
                for (int col = 0; col <= Columns; col++)
                {
                    Vector pos = new Vector(0.0, 0.0, 0.0);
                    Vector norm = new Vector(0.0, 0.0, 0.0);
                    //cylinder
                    double wrapPos = MyMath.Fit(col / Columns, 0, 1, WrapStart, WrapEnd) * 2 * Math.PI;
                    double sphereWrapPos = MyMath.Fit(row / Rows, 0, 1, RowWrapStart, RowWrapEnd) * 2 * Math.PI;
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
