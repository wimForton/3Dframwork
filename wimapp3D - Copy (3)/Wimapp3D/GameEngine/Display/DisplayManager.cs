using GLFW;
using System;
using System.Numerics;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameEngine.OpenGL.GL;

namespace GameEngine
{
    class DisplayManager
    {
        public static Window Window { get; set; }
        public static Vector2 WindowSize { get; set; }
        public static void CreateWindow(int width, int height, string title)
        {
            WindowSize = new Vector2(width, height );
            Glfw.Init();
            // opengl 3.3 core profile
            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
            Glfw.WindowHint(Hint.Focused, true);
            Glfw.WindowHint(Hint.Resizable, false);

            //Create the window
            Window = Glfw.CreateWindow(width, height, title, Monitor.None, Window.None);
            if(Window == Window.None)
            {
                //something went wrong
            }
            //Glfw.SetWindowOpacity(Window, 0.2f);///Zbrush style :-)
            /*
            Rectangle screen = Glfw.PrimaryMonitor.WorkArea;//Rectangle - using System.Drawing
            int x = (screen.Width - width) / 2;
            int y = (screen.Height - height) / 2;
            Glfw.SetWindowPosition(Window, x, y);
            */
            Glfw.MakeContextCurrent(Window);
            Import(Glfw.GetProcAddress);
            glViewport(0,0,width,height);
            Glfw.SwapInterval(1); //VSync is off 1 is on
        }
        public static void CloseWindow()
        {
            Glfw.Terminate();
        }

    }
}
