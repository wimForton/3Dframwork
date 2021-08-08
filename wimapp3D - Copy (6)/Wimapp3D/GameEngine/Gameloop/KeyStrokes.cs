using GLFW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class KeyStrokes
    {
        private int keyDown = -1;
        public int TestKeys(List<InputState> stateList, bool repeat)
        {
            int result = -1;
            for (int i = 0; i < stateList.Count; i++)
            {
                InputState state = stateList[i];
                if (state == InputState.Press)
                {
                    if (keyDown == -1 || repeat)
                    {
                        result = i;
                        keyDown = i;
                    }
                }
                if (state == InputState.Release && keyDown == i)
                {
                    result = -1;
                    keyDown = -1;
                }
            }

            return result;
        }
    }
}
