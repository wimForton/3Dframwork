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
        public double Rows { get; set; } = 3;
        public double Columns { get; set; } = 100;
        public double Pi { get; set; } = 3.14159265358979323846;
        public double WrapStart { get; set; } = 0;
        public double WrapEnd { get; set; } = 1.0;
        public double RowWrapStart { get; set; } = 0.0;
        public double RowWrapEnd { get; set; } = 0.5;
        public double Middle { get; set; } = 0.0;
        public double Roll { get; set; } = 0.0;
        public double SphereRadius { get; set; } = 1;

        private MultiPrimitiveControls ProportiesWindow;

        public MultiPrimitive(int inRows, int inCols, string inName)
        {
            Name = inName;
            ProportiesWindow = new MultiPrimitiveControls(this);
            ProportiesWindow.TextboxName = Name;
            ProportiesWindow.Rows = Rows;
            ProportiesWindow.Columns = Columns;
            ProportiesWindow.WrapStart = WrapStart;
            ProportiesWindow.WrapEnd = WrapEnd;
            ProportiesWindow.RowWrapStart = RowWrapStart;
            ProportiesWindow.RowWrapEnd = RowWrapEnd;
            ProportiesWindow.Middle = Middle;
            ProportiesWindow.Roll = Roll;
            ProportiesWindow.SphereRadius = SphereRadius;
            //Wimapp3D.MainWindow.AppWindow.ProportieWindowStack.Children.Add(ProportiesWindow);
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
            ProportiesWindow.Show();
            ProportiesWindow.Focus();
        }
        public override void CheckProportiesWindow()
        {
            if (ProportiesWindow.NeedsUpdate)
            {
                Rows = ProportiesWindow.Rows;
                Columns = ProportiesWindow.Columns;
                WrapStart = ProportiesWindow.WrapStart;
                WrapEnd = ProportiesWindow.WrapEnd;
                RowWrapStart = ProportiesWindow.RowWrapStart;
                RowWrapEnd = ProportiesWindow.RowWrapEnd;
                Middle = ProportiesWindow.Middle;
                Roll = ProportiesWindow.Roll;
                SphereRadius = ProportiesWindow.SphereRadius;
                ProportiesWindow.NeedsUpdate = false;
                NeedsUpdate = true;
            }
        }
        public override void Update()
        {
            //NeedsUpdate = false;
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
            MakeVaoList();
        }


        //public override void Update()
        //{
        //}
    }
}
