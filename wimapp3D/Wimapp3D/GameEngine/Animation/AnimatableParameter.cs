using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class AnimatableParameter
    {
        public SortedDictionary<double, KeyFrame> KeyFrames { get; set; } = new SortedDictionary<double, KeyFrame>();
        public List<double> indexList;
        private double ValueNotkeyed { get; set; } = 0;
        public AnimatableParameter()
        {
            KeyFrames = new SortedDictionary<double, KeyFrame>();
            KeyFrames.Add(0,new KeyFrame(0));
            indexList = new List<double>();

        }
        public double GetValueAtTime(double inTime)
        {
            double result = 0;
            if (KeyFrames.Count > 0)
            {
                indexList.Clear();
                indexList = new List<double>(KeyFrames.Keys);
                double number = inTime;
                // 2 methodes om de dichtstbijzijnde index te vinden
                //double findLowestAbove = indexList.Where(x => x > number).FirstOrDefault();
                //double findHighestBelow = indexList.OrderByDescending(x => x).Where(x => x < number).FirstOrDefault();
                var findLowestAbove = indexList.BinarySearch(number);
                if (KeyFrames.ContainsKey(findLowestAbove))
                {
                    //if (KeyFrames.ContainsKey(findLowestAbove))
                    //{
                    //    result = MyMath.Lerp(KeyFrames[findHighestBelow].Value, KeyFrames[findLowestAbove].Value, inTime - findHighestBelow / findLowestAbove - findHighestBelow);
                    //}
                    result = KeyFrames[findLowestAbove].Value;
                }
            }
            else
            {
                result = ValueNotkeyed;
            }
            return result;
        }
        public void SetKeyAtTime(double value, double inTime)
        {
            KeyFrame myKeyFrame = new KeyFrame(value);

            KeyFrame val;
            if (KeyFrames.TryGetValue(inTime, out val))
            {
                // yay, value exists!
                KeyFrames[inTime] = myKeyFrame;
            }
            else
            {
                // darn, lets add the value
                KeyFrames.Add(inTime, myKeyFrame);
            }
        }
       
    }
}

