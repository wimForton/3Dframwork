using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using GLFW;


namespace GameEngine
{
    abstract class RenderOpenGL
    {
        protected float fps = 25;
        int InitialWindowWidth { get; set;  }
        int InitialWindowHeight { get; set; }
        string InitialWindowTitle { get; set; }
        public RenderOpenGL(int initialWindowWidth, int initialWindowHeight, string initialWindowTitle)
        {
            InitialWindowWidth = initialWindowWidth;
            InitialWindowHeight = initialWindowHeight;
            InitialWindowTitle = initialWindowTitle;
        }
        public void Start()
        {
            DisplayManager.CreateWindow(InitialWindowWidth, InitialWindowHeight, InitialWindowTitle);
            Initialize();

            LoadContent();
        }
        public void Run()
        {
            //DisplayManager.CreateWindow(InitialWindowWidth, InitialWindowHeight, InitialWindowTitle);
            //Initialize();

            //LoadContent();
            while (!Glfw.WindowShouldClose(DisplayManager.Window))
            {
                GameTime.DeltaTime = (float)Glfw.Time - GameTime.TotalElapsedSeconds;
                GameTime.TotalElapsedSeconds = (float)Glfw.Time;
                GameTime.Frame = MathF.Floor(GameTime.TotalElapsedSeconds * fps);
                if (GameTime.Frame > GameTime.PrevFrame)
                {
                    GameTime.PrevFrame = GameTime.Frame;
                    GameTime.isNewFrame = true;
                }

                Update();
                Glfw.PollEvents();
                Render();
                GameTime.isNewFrame = false;
            }
            //for(int i = 0; i<100; i++)
            //{
            //    GameTime.DeltaTime = (float)Glfw.Time - GameTime.TotalElapsedSeconds;
            //    GameTime.TotalElapsedSeconds = (float)Glfw.Time;
            //    GameTime.Frame = MathF.Floor(GameTime.TotalElapsedSeconds * fps);
            //    if (GameTime.Frame > GameTime.PrevFrame)
            //    {
            //        GameTime.PrevFrame = GameTime.Frame;
            //        GameTime.isNewFrame = true;
            //    }

            //    Update();
            //    Glfw.PollEvents();
            //    Render();
            //    GameTime.isNewFrame = false;
            //}
            //DisplayManager.CloseWindow();
        }
        public void End()
        {
            DisplayManager.CloseWindow();
        }
        protected abstract void Initialize();
        protected abstract void LoadContent();
        protected abstract void Update();
        protected abstract void Render();


    }
}
