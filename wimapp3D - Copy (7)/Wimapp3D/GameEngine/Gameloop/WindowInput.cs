using GLFW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class WindowInput
    {
        public double mouseXpos, mouseYpos;
        public double mousePrevXpos, mousePrevYpos;
        public double mouseXmove;
        public double mouseYmove;
        public int mouseInput = -1;
        public int keyBoardInput = -1;

        public void UpdateInputs()
        {
            List<InputState> mouseButtonStateList = new List<InputState>();
            mouseButtonStateList.Add(Glfw.GetMouseButton(DisplayManager.Window, MouseButton.Left));
            mouseButtonStateList.Add(Glfw.GetMouseButton(DisplayManager.Window, MouseButton.Middle));
            mouseButtonStateList.Add(Glfw.GetMouseButton(DisplayManager.Window, MouseButton.Right));
            mouseInput = TestMouse(mouseButtonStateList, true);

            List<InputState> keyBoardStateList = new List<InputState>();
            keyBoardStateList.Add(Glfw.GetKey(DisplayManager.Window, Keys.LeftAlt));//0
            keyBoardStateList.Add(Glfw.GetKey(DisplayManager.Window, Keys.Enter));//1
            keyBoardStateList.Add(Glfw.GetKey(DisplayManager.Window, Keys.NumpadEnter));//2
            keyBoardStateList.Add(Glfw.GetKey(DisplayManager.Window, Keys.E));//3
            keyBoardStateList.Add(Glfw.GetKey(DisplayManager.Window, Keys.R));//4
            keyBoardStateList.Add(Glfw.GetKey(DisplayManager.Window, Keys.T));//5
            keyBoardInput = TestKeys(keyBoardStateList, true);

            /////////////keep track of the cursormovement
            Glfw.GetCursorPosition(DisplayManager.Window, out mouseXpos, out mouseYpos);
            mouseXmove = mouseXpos - mousePrevXpos;
            mouseYmove = mouseYpos - mousePrevYpos;
            mousePrevXpos = mouseXpos;
            mousePrevYpos = mouseYpos;
        }
        public int TestMouse(List<InputState> StateList, bool repeat)
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
