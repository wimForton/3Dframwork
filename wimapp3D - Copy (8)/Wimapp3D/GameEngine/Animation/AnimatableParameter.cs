using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace GameEngine
{
    [JsonObject(MemberSerialization.OptIn)]
    public class AnimatableParameter
    {
        [JsonProperty]
        public SortedDictionary<int, KeyFrame> KeyFrames { get; set; } = new SortedDictionary<int, KeyFrame>();
        public List<int> indexList;
        private double ValueNotkeyed { get; set; } = 0;
        public AnimatableParameter(double inStartValue)
        {
            if(KeyFrames == null)
            {
                KeyFrames = new SortedDictionary<int, KeyFrame>();
                
            }
            if(indexList == null)
            {
                indexList = new List<int>();
            }
            KeyFrames.Add(0, new KeyFrame(inStartValue));
        }
        public double GetValueAtFrame(int inFrame)
        {
            double result = 0;
            if (KeyFrames.Count > 0)
            {
                indexList.Clear();
                indexList = new List<int>(KeyFrames.Keys);
                int number = inFrame;
                // 2 methodes om de dichtstbijzijnde index te vinden
                int findLowestAbove = indexList.Where(x => x > number).FirstOrDefault();
                int findHighestBelow = indexList.OrderByDescending(x => x).Where(x => x < number).FirstOrDefault();
                //var findLowestAbove = indexList.BinarySearch(number);
                //MessageBox.Show(Convert.ToString(findLowestAbove));
                //MessageBox.Show(Convert.ToString(findHighestBelow));
                if (KeyFrames.ContainsKey(findHighestBelow) || KeyFrames.ContainsKey(inFrame))
                {
                    if (!KeyFrames.ContainsKey(inFrame))
                    {
                        if (KeyFrames.ContainsKey(findLowestAbove) && findLowestAbove != findHighestBelow && findLowestAbove != 0)
                        {
                            result = MyMath.Lerp(KeyFrames[findHighestBelow].Value, KeyFrames[findLowestAbove].Value, (double)(inFrame - findHighestBelow) / (double)(findLowestAbove - findHighestBelow));
                            //MessageBox.Show(Convert.ToString(findLowestAbove));
                        }
                        else
                        {
                            result = KeyFrames[findHighestBelow].Value;
                        }
                    }
                    else
                    {
                        result = KeyFrames[inFrame].Value;
                    }

                    
                    
                }
            }
            else
            {
                result = ValueNotkeyed;
            }
            return result;
        }
        public void SetKeyAtFrame(double value, int inFrame)
        {
            KeyFrame myKeyFrame = new KeyFrame(value);

            KeyFrame val;
            if (KeyFrames.TryGetValue(inFrame, out val))
            {
                // yay, value exists!
                KeyFrames[inFrame] = myKeyFrame;
            }
            else
            {
                // darn, lets add the value
                KeyFrames.Add(inFrame, myKeyFrame);
            }
        }
       
    }
}

