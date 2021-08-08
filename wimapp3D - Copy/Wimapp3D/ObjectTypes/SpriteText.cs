using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class SpriteText : RenderableGeo, IRenderableGeo
    {
        public string TextString { get; set; }
        public SpriteText(string inString)
        {
            TextString = inString;
            StringToPolys(inString);
            MakeVaoList();

        }
        private void StringToPolys(string inString)
        {
            TextString = inString;
            double myScale = 0.5;
            double Upos1, Upos2, Vpos1, Vpos2;
            for (int i = 0; i < TextString.Length; i++)
            {
                char myChar = TextString[i];
                int myAscii = (int)myChar;
                double spriteNum = 40;
                if (MyMath.InRange(48, 57, myAscii)) spriteNum = myAscii - 22;
                if (MyMath.InRange(65, 90, myAscii)) spriteNum = myAscii - 65;
                //spriteNum = i * 0.05;
                //Clockwise!
                Points.Add(Vector.setNew(i, 1, 0) * myScale + Position);
                Points.Add(Vector.setNew(i + 1, 1, 0) * myScale + Position);
                Points.Add(Vector.setNew(i + 1, 0, 0) * myScale + Position);
                Points.Add(Vector.setNew(i, 0, 0) * myScale + Position);

                if(spriteNum < 20)
                {
                    Upos1 = spriteNum * 0.05 - 0.00;
                    Upos2 = spriteNum * 0.05 + 0.05;
                    Vpos1 = 0.0;
                    Vpos2 = 0.05;
                }
                else
                {
                    Upos1 = (spriteNum * 0.05 - 0.00) - 1;
                    Upos2 = (spriteNum * 0.05 + 0.05) - 1;
                    Vpos1 = 0.05;
                    Vpos2 = 0.1;
                }
                UVs.Add(Vector.setNew(Upos1, Vpos1, 0));
                UVs.Add(Vector.setNew(Upos2, Vpos1, 0));
                UVs.Add(Vector.setNew(Upos2, Vpos2, 0));
                UVs.Add(Vector.setNew(Upos1, Vpos2, 0));
                //myUVs.Add(Vector.setNew(0, 1, 0));
                //myUVs.Add(Vector.setNew(1, 1, 0));
                //myUVs.Add(Vector.setNew(1, 0, 0));
                //myUVs.Add(Vector.setNew(0, 0, 0));
                Normals.Add(Vector.setNew(0, 0, -1));
                Normals.Add(Vector.setNew(0, 0, -1));
                Normals.Add(Vector.setNew(0, 0, -1));
                Normals.Add(Vector.setNew(0, 0, -1));
                Polygon myPoly = new Polygon();
                for (int j = 0; j < 4; j++)
                {
                    int offset = i * 4;
                    myPoly.Vertices.Add(j + offset);
                    myPoly.UVs.Add(j + offset);
                    myPoly.Normals.Add(j + offset);
                }

                Polygons.Add(myPoly);
            }
        }

        public override void Update() 
        {
        }
    }
}
