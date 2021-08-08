using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class MultiPrimitive : RenderableGeo, IRenderableGeo
    {
        private int Rows { get; set; } = 50;
        private int Columns { get; set; } = 50;
        public MultiPrimitive(int inRows, int inCols)
        {
            //BuildGeo();
            BuildPrimitive();
            MakeVaoList();

        }

        private void BuildPrimitive()
        {
            double pi = 3.14159265358979323846;
            double Rows = 50;
            double Columns = 50;
            double wrapStart = 0;
            double wrapEnd = 1;
            double sphereWrapStart = 0;
            double sphereWrapEnd = 1;
            double middle = 2;
            double roll = 1 * pi * 2;
            double sphereRadius = 1;


            for (int row = 0; row <= Rows; row++)
            {
                for (int col = 0; col <= Columns; col++)
                {
                    Vector pos = new Vector(0.0, 0.0, 0.0);
                    Vector norm = new Vector(0.0, 0.0, 0.0);
                    //cylinder
                    double wrapPos = MyMath.Fit(col / Columns, 0, 1, wrapStart, wrapEnd) * 2 * pi;
                    double sphereWrapPos = MyMath.Fit(row / Rows, 0, 1, sphereWrapStart, sphereWrapEnd) * 2 * pi;
                    pos.X = Math.Sin(wrapPos);//Math.Sin
                    pos.Z = Math.Cos(wrapPos);

                    //deform to sphereshape
                    pos.X *= Math.Sin(sphereWrapPos + roll) * sphereRadius;
                    pos.Z *= Math.Sin(sphereWrapPos + roll) * sphereRadius;
                    pos.Y = Math.Cos(sphereWrapPos + roll) * sphereRadius;
                    norm.X = pos.X;//simple normals based on offset
                    norm.Z = pos.Z;
                    norm.Y = pos.Y;
                    Vector.Normalize(norm);
                    //offset
                    pos.X += Math.Sin(wrapPos) * middle;
                    pos.Z += Math.Cos(wrapPos) * middle;

                    Points.Add(pos);
                    Vector myUV = new Vector(row / Rows, col / Columns, 0);// set(row / rows, col / cols, 0);
                    UVs.Add(myUV);
                    Vector myNormal = new Vector(0.0, 1.0, 0.0);
                    Normals.Add(myNormal);
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
                        myPoly.Vertices.Add(startIndex + this.Columns + 2);
                        myPoly.Vertices.Add(startIndex + this.Columns + 1);
                        myPoly.UVs.Add(startIndex);
                        myPoly.UVs.Add(startIndex + 1);
                        myPoly.UVs.Add(startIndex + this.Columns + 2);
                        myPoly.UVs.Add(startIndex + this.Columns + 1);
                        myPoly.Normals.Add(startIndex);
                        myPoly.Normals.Add(startIndex + 1);
                        myPoly.Normals.Add(startIndex + this.Columns + 2);
                        myPoly.Normals.Add(startIndex + this.Columns + 1);
                        Polygons.Add(myPoly);
                        /*
                        int points[];
                        points[0] = startIndex;
                        points[1] = startIndex + 1;
                        points[2] = startIndex + (int)cols + 2;
                        points[3] = startIndex + (int)cols + 1;

                        addprim(0, "poly", points);
                        */
                    }
                    startIndex++;
                }
            }
        }

        private void BuildGeo()
        {
            //points
            for (double row = 0; row <= Rows; row++)
            {
                for (double column = 0; column <= Columns; column++)
                {

                    Points.Add(new Vector(column / (double)Columns, row / (double)Rows, 0) * 2);
                    UVs.Add(new Vector(column / (double)Columns, row / (double)Rows, 0));
                    Normals.Add(new Vector(0, 0, -1));

                }
            }
            /*
              0  1  2  3  4  5
              6  7  8  9  10 11
              12 13 14 15 16 17
              18 19 20 21 22 23
              24 25 26 27 28 29
              30 31 32 33 34 35
            */
            /*
              0  1  2  
              3  4  5  
              6  7  8 
            */
            int startIndex = 0;
            for (int row = 0; row <= Rows; row++)
            {
                for (int column = 0; column <= Columns; column++)
                {
                    
                    if (row < Rows && column < Columns)
                    {
                        Polygon myPoly = new Polygon();

                        myPoly.Vertices.Add(startIndex);
                        myPoly.Vertices.Add(startIndex + 1);
                        myPoly.Vertices.Add(startIndex + Columns + 2);
                        myPoly.Vertices.Add(startIndex + Columns + 1);
                        myPoly.UVs.Add(startIndex);
                        myPoly.UVs.Add(startIndex + 1);
                        myPoly.UVs.Add(startIndex + Columns + 2);
                        myPoly.UVs.Add(startIndex + Columns + 1);
                        myPoly.Normals.Add(startIndex);
                        myPoly.Normals.Add(startIndex + 1);
                        myPoly.Normals.Add(startIndex + Columns + 2);
                        myPoly.Normals.Add(startIndex + Columns + 1);
                        Polygons.Add(myPoly);
                    }
                    startIndex++;

                }
            }
            
        }

        public override void Update() 
        {
        }
    }
}
