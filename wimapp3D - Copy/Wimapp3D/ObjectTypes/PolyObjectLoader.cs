﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class PolyObjectLoader : RenderableGeo, IRenderableGeo
    {

        List<float> myVaoList { get; set; } = new List<float>();
        Vector RGBColor { get; set; } = new Vector(0,0.5,1);
        private string FilePath { get; set; }
        //public Vector Position { get; set; } = new Vector(0, 0, -30);
        public PolyObjectLoader(string inFilePath)
        {
            FilePath = inFilePath;
            //Position.Z = -30;
            LoadFromFile();
            MakeVaoList();
        }

        public override void Update() { }
        public void LoadFromFile()
        {
            using (StreamReader streamReader = new StreamReader(FilePath))
            {
                
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    string[] values = line.Split(' ');
                    

                    if(values[0] == "v")
                    {
                        Vector myPoint = new Vector(Convert.ToDouble(values[1]), Convert.ToDouble(values[2]), Convert.ToDouble(values[3]));

                        Points.Add(myPoint);
                    }
                    if (values[0] == "vt")
                    {
                        Vector myUV = new Vector(Convert.ToDouble(values[1]), Convert.ToDouble(values[2]), Convert.ToDouble(values[3]));
                        myUV.Y *= -1;
                        UVs.Add(myUV);
                    }
                    if (values[0] == "vn")
                    {
                        Vector myNormal = new Vector(Convert.ToDouble(values[1]), Convert.ToDouble(values[2]), Convert.ToDouble(values[3]));
                        Normals.Add(myNormal);
                    }
                    if (values[0] == "f")
                    {

                        Random myRandom = new Random();
                        Polygon mypoly = new Polygon();
                        Vector randomColor = new Vector(myRandom.NextDouble(), myRandom.NextDouble(), myRandom.NextDouble());
                        randomColor *= RGBColor;
                        for (int i = 1; i < values.Length; i++)
                        {
                            int vertice, verticeTex, verticeNorm;
                            string[] v_vt_vn = values[i].Split('/');
                            vertice = Convert.ToInt32(v_vt_vn[0]) - 1;//Convert 1 based (obj) to 0 based
                            mypoly.Vertices.Add(vertice);
                            if (v_vt_vn.Length > 1) 
                            { 
                                verticeTex = Convert.ToInt32(v_vt_vn[1]) - 1;
                                mypoly.UVs.Add(verticeTex);
                            }
                            if (v_vt_vn.Length > 2)
                            {
                                verticeNorm = Convert.ToInt32(v_vt_vn[2]) - 1;
                                mypoly.Normals.Add(verticeNorm);
                            }
                            
                            mypoly.Colors.Add(randomColor);
                        }
                        Polygons.Add(mypoly);
                    }
                }
            }
        }

    }
}
