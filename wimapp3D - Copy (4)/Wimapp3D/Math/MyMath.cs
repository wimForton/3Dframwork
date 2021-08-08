using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class MyMath
    {
        public static double Lerp(double a, double b, double weight)
        {
            double result = a * (1 - weight) + b * weight;

            return result;
        }
        public static bool InRange(int min, int max, int input)
        {
            bool result = false;
            if(input >= min && input <= max) result = true;
            return result;
        }
        public static double Fit(double inValue, double oldMin, double oldMax, double newMin, double newMax)
        {

            double oldRange = (oldMax - oldMin);
            double newValue = 0;
            if (oldRange == 0)
            {
                newValue = newMin;
            }
                
            else
            {
                double NewRange = (newMax - newMin);
                newValue = ((inValue - oldMin) * NewRange / oldRange) + newMin;
            }
            return newValue;
        }
        public static double PerlinNoise(Vector inVector, Vector frequency, int iterations)
        {
            double result = 0;
            inVector += new Vector(12345, 456789, 321);
            if (iterations == 0) iterations = 1;//devide by zero safety

            for (int i = 0; i < iterations; i++)
            {
                double X = inVector.X * i * frequency.X;
                double Y = inVector.Y * i * frequency.Y;
                double Z = inVector.Z * i * frequency.Z;
                double fX = Math.Floor(X);
                double cX = Math.Ceiling(X);
                double fY = Math.Floor(Y);
                double cY = Math.Ceiling(Y);
                double fZ = Math.Floor(Z);
                double cZ = Math.Ceiling(Z);

                Vector posFrontTopLeft = new Vector(fX, cY, fZ);
                Vector posFrontTopRight = new Vector(cX, cY, fZ);
                Vector posFrontBottomLeft = new Vector(fX, fY, fZ);
                Vector posFrontBottomRight = new Vector(cX, fY, fZ);
                Vector posBackTopLeft = new Vector(fX, cY, cZ);
                Vector posBackTopRight = new Vector(cX, cY, cZ);
                Vector posBackBottomLeft = new Vector(fX, fY, cZ);
                Vector posBackBottomRight = new Vector(cX, fY, cZ);

                double valueFrontTopLeft = new Random((int)posFrontTopLeft.X + (int)posFrontTopLeft.Y + (int)posFrontTopLeft.Z).NextDouble();
                double valueFrontTopRight = new Random((int)posFrontTopRight.X + (int)posFrontTopRight.Y + (int)posFrontTopRight.Z).NextDouble();
                double valueFrontBottomLeft = new Random((int)posFrontBottomLeft.X + (int)posFrontBottomLeft.Y + (int)posFrontBottomLeft.Z).NextDouble();
                double valueFrontBottomRight = new Random((int)posFrontBottomRight.X + (int)posFrontBottomRight.Y + (int)posFrontBottomRight.Z).NextDouble();
                double valueBackTopLeft = new Random((int)posBackTopLeft.X + (int)posBackTopLeft.Y + (int)posBackTopLeft.Z).NextDouble();
                double valueBackTopRight = new Random((int)posBackTopRight.X + (int)posBackTopRight.Y + (int)posBackTopRight.Z).NextDouble();
                double valueBackBottomLeft = new Random((int)posBackBottomLeft.X + (int)posBackBottomLeft.Y + (int)posBackBottomLeft.Z).NextDouble();
                double valueBackBottomRight = new Random((int)posBackBottomRight.X + (int)posBackBottomRight.Y + (int)posBackBottomRight.Z).NextDouble();

                double xLerpFrontTop = MyMath.Lerp(valueFrontTopLeft, valueFrontTopRight, X - fX);
                double xLerpFrontBottom = MyMath.Lerp(valueFrontBottomLeft, valueFrontBottomRight, X - fX);
                double xLerpBackTop = MyMath.Lerp(valueBackTopLeft, valueBackTopRight, X - fX);
                double xLerpBackBottom = MyMath.Lerp(valueBackBottomLeft, valueBackBottomRight, X - fX);

                double yLerpFront = MyMath.Lerp(xLerpFrontTop, xLerpFrontBottom, Y - fY);
                double yLerpBack = MyMath.Lerp(xLerpBackTop, xLerpBackBottom, Y - fY);

                double zLerp = MyMath.Lerp(yLerpFront, yLerpBack, Z - fZ);
                result += zLerp;
            }
            result = result / iterations;
            return result;
        }
    }
}
