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
    }
}
