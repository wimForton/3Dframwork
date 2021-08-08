using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace GameEngine
{
    /*interface Vector
    {
        public void draw(string c, ConsoleColor colr);
    }*/
    class Vector
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public int Dimensions { get; set; }
        public Vector(double inX, double inY, double inZ)
        {
            X = inX; Y = inY; Z = inZ;
            Dimensions = 3;
        }
        public Vector(double inX, double inY)
        {
            X = inX; Y = inY;
            Dimensions = 2;
        }
        public void Set(double inX, double inY, double inZ)
        {
            X = inX; Y = inY; Z = inZ;
            Dimensions = 3;
        }
        public static Vector setNew(double inX, double inY, double inZ)
        {
            return new Vector(inX, inY, inZ);
        }
        public static Vector operator +(Vector d1, Vector d2)
        {
            //vector2D result = new vector2D();
            //T v1 = 0.0;
            Vector result = new Vector(0.0, 0.0);
            if (d1.Dimensions == 3 || d2.Dimensions == 3) 
            {
                result = new Vector(0.0, 0.0, 0.0);
                result.X = d1.X + d2.X;
                result.Y = d1.Y + d2.Y;
                result.Z = d1.Z + d2.Z;
            }
            else
            {
                result = new Vector(0.0, 0.0);
                result.X = d1.X + d2.X;
                result.Y = d1.Y + d2.Y;
            }

            return result;
        }
        public static Vector operator -(Vector d1, Vector d2)
        {
            //vector2D result = new vector2D();
            //T v1 = 0.0;
            Vector result = new Vector(0.0, 0.0);
            if (d1.Dimensions == 3 || d2.Dimensions == 3)
            {
                result = new Vector(0.0, 0.0, 0.0);
                result.X = d1.X - d2.X;
                result.Y = d1.Y - d2.Y;
                result.Z = d1.Z - d2.Z;
            }
            else
            {
                result = new Vector(0.0, 0.0);
                result.X = d1.X - d2.X;
                result.Y = d1.Y - d2.Y;
            }

            return result;
        }
        public static Vector operator *(Vector d1, Vector d2)
        {
            //vector2D result = new vector2D();
            //T v1 = 0.0;
            Vector result = new Vector(0.0, 0.0);
            if (d1.Dimensions == 3 || d2.Dimensions == 3)
            {
                result = new Vector(0.0, 0.0, 0.0);
                result.X = d1.X * d2.X;
                result.Y = d1.Y * d2.Y;
                result.Z = d1.Z * d2.Z;
            }
            else
            {
                result = new Vector(0.0, 0.0);
                result.X = d1.X * d2.X;
                result.Y = d1.Y * d2.Y;
            }

            return result;
        }
        public static Vector operator /(Vector d1, Vector d2)
        {
            //vector2D result = new vector2D();
            //T v1 = 0.0;
            Vector result = new Vector(0.0, 0.0);
            if (d1.Dimensions == 3 || d2.Dimensions == 3)
            {
                result = new Vector(0.0, 0.0, 0.0);
                result.X = d1.X / d2.X;
                result.Y = d1.Y / d2.Y;
                result.Z = d1.Z / d2.Z;
            }
            else
            {
                result = new Vector(0.0, 0.0);
                result.X = d1.X / d2.X;
                result.Y = d1.Y / d2.Y;
            }

            return result;
        }
        public static Vector operator *(Vector d1, double d2)
        {
            //vector2D result = new vector2D();
            //T v1 = 0.0;
            Vector result = new Vector(0.0, 0.0);
            if (d1.Dimensions == 3)
            {
                result = new Vector(0.0, 0.0, 0.0);
                result.X = d1.X * d2;
                result.Y = d1.Y * d2;
                result.Z = d1.Z * d2;
            }
            else
            {
                result = new Vector(0.0, 0.0);
                result.X = d1.X * d2;
                result.Y = d1.Y * d2;
            }

            return result;
        }
        public static Vector operator /(Vector d1, double d2)
        {
            //vector2D result = new vector2D();
            //T v1 = 0.0;
            Vector result = new Vector(0.0, 0.0);
            if (d1.Dimensions == 3)
            {
                result = new Vector(0.0, 0.0, 0.0);
                result.X = d1.X / d2;
                result.Y = d1.Y / d2;
                result.Z = d1.Z / d2;
            }
            else
            {
                result = new Vector(0.0, 0.0);
                result.X = d1.X / d2;
                result.Y = d1.Y / d2;
            }

            return result;
        }
        /*
        public static bool operator ==(Vector v1, Vector v2)
        {
            return (double)v1 == (double)v2;
        }
        public static bool operator ==(Vector v1, null)
        {
            return v1 == null;
        }
        public static bool operator !=(Vector v1, Vector v2)
        {
            return (double)v1 != (double)v2;
        }
        public static bool operator !=(Vector v1, null)
        {
            return true;
        }
        */
        public static bool operator <(Vector v1, Vector v2)
        {
            return (double)v1 < (double)v2;
        }
        public static bool operator >(Vector v1, Vector v2)
        {
            return (double)v1 > (double)v2;
        }
        public static bool operator >=(Vector v1, Vector v2)
        {
            return (double)v1 >= (double)v2;
        }
        public static bool operator <=(Vector v1, Vector v2)
        {
            return (double)v1 <= (double)v2;
        }
        
        public static double length(Vector v1)
        {
            double result = Math.Sqrt(Math.Pow(v1.X, 2.0) + Math.Pow(v1.Y, 2.0) + Math.Pow(v1.Z, 2.0));
            return result;
        }
        public static explicit operator double(Vector v1)//does the same as length
        {
            double result = Math.Sqrt(Math.Pow(v1.X, 2.0) + Math.Pow(v1.Y, 2.0) + Math.Pow(v1.Z, 2.0));
            return result;
        }
        public static Vector Lerp(Vector v1, Vector v2, double weight)
        {
            Vector result = v1 * (1 - weight) + v2 * weight;
            return result;
        }
        public static Vector Normalize(Vector v1)
        {
            Vector result = new Vector(0.0,0.0,0.0);
            if((double)v1 != 0)
            {
                result.X = v1.X / (double)v1;
                result.Y = v1.Y / (double)v1;
                result.Z = v1.Z / (double)v1;
            }
            else
            {
                result.Z = 1.0;
            }
            return result;
        }
        public static Vector CrossProduct(Vector v1, Vector v2)
        {
            Vector result = Vector.setNew(0, 0, 0);
            
            result.X = v1.Y * v2.Z - v1.Z * v2.Y;
            result.Y = v1.Z * v2.X - v1.X * v2.Z;
            result.Z = v1.X * v2.Y - v1.Y * v2.X;

            result = Vector.Normalize(result);
            return result;
        }
        public static Vector GetNormal(Vector v1, Vector v2, Vector v3)
        {
            Vector result = Vector.setNew(0, 0, 0);
            v2 -= v1;
            v3 -= v1;
            result = Vector.CrossProduct(v2, v3);
            return result;
        }
        public static Vector Pow(Vector v1, double pow)
        {
            Vector result = v1;
            result.X = Math.Pow(result.X, pow);
            result.Y = Math.Pow(result.Y, pow);
            result.Z = Math.Pow(result.Z, pow);
            return result;
        }
        public static Vector RotateEuler(Vector inVector, double angle)
        {
            Vector result;
            double resultX = inVector.X * Math.Cos(angle) - inVector.Y * Math.Sin(angle);
            double resultY = inVector.X * Math.Sin(angle) + inVector.Y * Math.Cos(angle);
            result = new Vector(resultX, resultY);
            return result;
        }
        public void RotateEuler(double angle)
        {
            double rad = angle * Math.PI / 180;
            double tempX = X;
            X = X * Math.Cos(rad) - Z * Math.Sin(rad);
            Z = tempX * Math.Sin(rad) + Z * Math.Cos(rad);
        }
        public static Vector GetEulerRotation(Vector inVector, double angleX, double angleY, double angleZ, string rotationOrder)
        {
            Vector result;
            double resultX = inVector.X;
            double resultY = inVector.Y;
            double resultZ = inVector.Z;
            for (int i = 0; i < 3; i++) {
                char axis = rotationOrder[i];
                double calcX = 0.0;
                double calcY = 0.0;
                double calcAngle = 0.0;

                if (axis == 'x') { calcX = resultY; calcY = resultZ; calcAngle = angleX; }
                if (axis == 'y') { calcX = resultX; calcY = resultZ; calcAngle = angleY; }
                if (axis == 'z') { calcX = resultX; calcY = resultY; calcAngle = angleZ; }

                if (axis == 'x')
                {
                    resultY = calcX * Math.Cos(calcAngle) - calcY * Math.Sin(calcAngle);
                    resultZ = calcX * Math.Sin(calcAngle) + calcY * Math.Cos(calcAngle);
                }
                if (axis == 'y')
                {
                    resultX = calcX * Math.Cos(calcAngle) - calcY * Math.Sin(calcAngle);
                    resultZ = calcX * Math.Sin(calcAngle) + calcY * Math.Cos(calcAngle);
                }
                if (axis == 'z')
                {
                    resultX = calcX * Math.Cos(calcAngle) - calcY * Math.Sin(calcAngle);
                    resultY = calcX * Math.Sin(calcAngle) + calcY * Math.Cos(calcAngle);
                }

            }
            result = new Vector(resultX, resultY, resultZ);
            return result;
        }
        public bool TestRange(double minX, double maxX, double minY, double maxY)
        {
            bool result = false;
            if (X >= minX && X <= maxX && Y >= minY && Y <= maxY)
            {
                result = true;
            }
            return result;
        }
        public void  SetRange(double minX, double maxX, double minY, double maxY, double minZ, double maxZ)
        {
            X = Math.Clamp(X, minX, maxX);
            Y = Math.Clamp(Y, minY, maxY);
            Z = Math.Clamp(Z, minZ, maxZ);
        }
        public override bool Equals(object o)
        {
            bool gelijk = false;
            Console.WriteLine(o.GetType());
            if (GetType() != o.GetType())
            {
                gelijk = false;
            }
            else
            {
                Vector temp = o as Vector;
                if (X == temp.X && Y == temp.Y && Z == temp.Z)
                    gelijk = true;
                else gelijk = false;
            }
            return gelijk;
        }
        public override string ToString()
        {
            string vector = $"{X},{Y},{Z}";
            return vector;
        }
    }
}
