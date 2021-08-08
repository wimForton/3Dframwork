using GLFW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class MouseButtons
    {
        public int TestKeys(List<InputState> StateList, bool repeat)
        {
            int result = -1;
            for (int i = 0; i < StateList.Count; i++)
            {
                InputState state = StateList[i];
                if (state == InputState.Press)
                {
                    result = i;
                }
            }

            return result;
        }
    }
}
