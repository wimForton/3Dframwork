using GLFW;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Drawing;
using System.Drawing.Imaging;
using static GameEngine.OpenGL.GL;
using Ganzenbord;
using System.Windows;

namespace GameEngine
{
    class RenderOpenGlTemplate : RenderOpenGL
    {
        uint vao;
        uint vbo;
        private KeyStrokes myKeystrokes = new KeyStrokes();
        private MouseButtons myMouseButtons = new MouseButtons();
        WindowInput myWindowInput = new WindowInput();
        double mouseXpos, mouseYpos;
        double mousePrevXpos, mousePrevYpos;
        private Matrix4x4 sceneRotation = Matrix4x4.CreateFromYawPitchRoll(0.0f, 0.0f, 0.0f);
        private Matrix4x4 scenePosition = Matrix4x4.CreateTranslation(0.0f, 0.0f, 0.0f);
        uint texID;
        uint CursorObject;
        int SelectedObject;
        float sceneRotationX = 0.0f;
        float sceneRotationY = 0.0f;
        float sceneRotationZ = 0.0f;





        public List<IRenderableGeo> myRendergeo = new List<IRenderableGeo>();
        //List<float> myVaoList = new List<float>();
        float[] vertices;
        ShaderPhong shader;

        Camera3D cam;

        public RenderOpenGlTemplate(List<IRenderableGeo> inGeoList, float inFps, int initialWindowWidth, int initialWindowHeight, string initialWindowTitle) : base(initialWindowWidth, initialWindowHeight, initialWindowTitle)
        {
            myRendergeo.Clear();
            myRendergeo = inGeoList;
            fps = inFps;
            sceneRotation = Matrix4x4.CreateFromYawPitchRoll(0.0f, 0.5f, 0.0f);
            scenePosition = Matrix4x4.CreateTranslation(0.0f, 0.0f, -7.0f);
            for (int i = 0; i < myRendergeo.Count; i++)
            {
                myRendergeo[0].MakeVaoArray();
            }
            //myRendergeo[0].OpenProportiesWindow();
            myRendergeo[0].Scale.Y = 2;
            //vertices = myRendergeo[0].MakeVaoArray();
        }
        uint loadImage(string filepath)
        {
            Bitmap myBitmap = new Bitmap(filepath);
            texID = glGenTexture();
            glBindTexture(GL_TEXTURE_2D, texID);
            BitmapData data = myBitmap.LockBits(new Rectangle(0, 0, myBitmap.Width, myBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, myBitmap.Width, myBitmap.Height, 0, GL_BGR, GL_UNSIGNED_BYTE, data.Scan0);
            myBitmap.UnlockBits(data);
            //quality settings
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
            glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR_MIPMAP_LINEAR);
            glGenerateMipmap(GL_TEXTURE_2D);
            //quality settings
            return texID;
        }

        protected override void Initialize()
        {
            loadImage("Uvcoordinates.bmp");
            shader = new ShaderPhong();
            shader.Load();
            glEnable(GL_DEPTH_TEST);
            vao = glGenVertexArray();
            vbo = glGenBuffer();
            glBindVertexArray(vao);
            glBindBuffer(GL_ARRAY_BUFFER, vao);
            shader.SetVec3("objectColor", new Vector3(1.0f, 1.0f, 1.0f));
        }
        protected unsafe override void LoadContent()
        {            

        }

        protected unsafe override void Update()
        {

            for (int i = 0; i < myRendergeo.Count; i++)
            {
                myRendergeo[i].CheckProportiesWindow();
                if (myRendergeo[i].NeedsUpdate)//only for changed geometry
                {
                    //GeometryNeedsUpdate = true;
                    myRendergeo[i].MakeVaoArray();
                }
            }


            if (GameTime.isNewFrame) {

                //List<InputState> mouseButtonStateList = new List<InputState>();
                //mouseButtonStateList.Add(Glfw.GetMouseButton(DisplayManager.Window, MouseButton.Left));
                //mouseButtonStateList.Add(Glfw.GetMouseButton(DisplayManager.Window, MouseButton.Middle));
                //mouseButtonStateList.Add(Glfw.GetMouseButton(DisplayManager.Window, MouseButton.Right));
                //int mouseButton = myMouseButtons.TestKeys(mouseButtonStateList, true);
                myWindowInput.UpdateInputs();
                int mouseButton = myWindowInput.mouseInput;
                int keyStroke = myWindowInput.keyBoardInput;
                if (keyStroke == 0)
                {
                    if (mouseButton == 0)//mouseButtonLeft
                    {
                        sceneRotationX += (float)myWindowInput.mouseYmove * 0.01f;
                        sceneRotationY += (float)myWindowInput.mouseXmove * 0.01f;
                        sceneRotation = Matrix4x4.CreateFromYawPitchRoll(0.0f, sceneRotationX, 0.0f);
                        sceneRotation = Matrix4x4.CreateFromYawPitchRoll(sceneRotationY, 0.0f, 0.0f) * sceneRotation;
                    }
                    else if (mouseButton == 1)//mouseButtonMiddle
                    {
                        scenePosition = Matrix4x4.CreateTranslation((float)myWindowInput.mouseXmove * 0.006f, 0.0f, 0.0f) * scenePosition;
                        scenePosition = Matrix4x4.CreateTranslation(0.0f, 0.0f, (float)myWindowInput.mouseYmove * 0.01f) * scenePosition;
                    }
                    else if (mouseButton == 2)//mouseButtonRight
                    {
                        scenePosition = Matrix4x4.CreateTranslation(0.0f, (float)myWindowInput.mouseYmove * -0.005f, 0.0f) * scenePosition;
                    }
                }else if (keyStroke == 1 || keyStroke == 2)
                {
                    if (SelectedObject >= 0 && SelectedObject < myRendergeo.Count) myRendergeo[SelectedObject].OpenProportiesWindow();
                }
                else if (keyStroke == 3)
                {
                    if (mouseButton == 0)
                    {
                        myRendergeo[SelectedObject].Scale.X += myWindowInput.mouseXmove * 0.01;
                        myRendergeo[SelectedObject].Scale.Z += myWindowInput.mouseYmove * -0.01;
                    }
                    if (mouseButton == 1) myRendergeo[SelectedObject].Scale.Z += myWindowInput.mouseYmove * -0.01;
                    if (mouseButton == 2) myRendergeo[SelectedObject].Scale.Y += myWindowInput.mouseYmove * -0.01;
                }
                else if (keyStroke == 4)
                {
                    if (mouseButton == 0)
                    {
                        myRendergeo[SelectedObject].Rotation.X += myWindowInput.mouseYmove * 0.01;
                        myRendergeo[SelectedObject].Rotation.Z += myWindowInput.mouseXmove * -0.01;
                    }
                    if (mouseButton == 1) myRendergeo[SelectedObject].Rotation.Z += myWindowInput.mouseXmove * -0.01;
                    if (mouseButton == 2) myRendergeo[SelectedObject].Rotation.Y += myWindowInput.mouseXmove * -0.01;
                }
                else if (keyStroke == 5)
                {
                    if (mouseButton == 0)
                    {
                        myRendergeo[SelectedObject].Position.X += myWindowInput.mouseXmove * 0.01;
                        myRendergeo[SelectedObject].Position.Z += myWindowInput.mouseYmove * 0.01;
                    }
                    if (mouseButton == 1) myRendergeo[SelectedObject].Position.Z += myWindowInput.mouseYmove * 0.01;
                    if (mouseButton == 2) myRendergeo[SelectedObject].Position.Y += myWindowInput.mouseYmove * -0.01;
                }
                else
                {
                    if (mouseButton == 0)//mouseButtonLeft
                    {
                        if (CursorObject > 0) SelectedObject = (int)CursorObject - 1;
                        //if (CursorObject == 1) SelectedObject = myRendergeo.Count;
                    }
                }
                Glfw.SetWindowTitle(DisplayManager.Window, myRendergeo[SelectedObject].Name);
            }

            Vector3 cameraposition = new Vector3(DisplayManager.WindowSize.X, DisplayManager.WindowSize.Y, 0);
            cam = new Camera3D(cameraposition / 2f, 1f);

            for (int i = 0; i < myRendergeo.Count; i++)
            {
                Matrix4x4 modelPos = Matrix4x4.CreateTranslation((float)myRendergeo[i].Position.X, (float)myRendergeo[i].Position.Y, (float)myRendergeo[i].Position.Z);//MathF.Sin(GameTime.TotalElapsedSeconds) * (float)i);
                Matrix4x4 modelScale = Matrix4x4.CreateScale((float)myRendergeo[i].Scale.X, (float)myRendergeo[i].Scale.Y, (float)myRendergeo[i].Scale.Z);
                Matrix4x4 modelRotation = Matrix4x4.CreateFromYawPitchRoll((float)myRendergeo[i].Rotation.Y, (float)myRendergeo[i].Rotation.X, (float)myRendergeo[i].Rotation.Z);
                Matrix4x4 modelCombined = modelScale * modelRotation * modelPos;
                Matrix4x4 inverseModelRotation;
                Matrix4x4 sceneRot = Matrix4x4.CreateRotationY(MathF.PI * 2);//rotate the scene 180
                Vector3 lightPos = new Vector3(5.0f, 5.0f, 5.0f);
                Matrix4x4.Invert(modelCombined, out inverseModelRotation);
                lightPos = Vector3.Transform(lightPos, inverseModelRotation);
                
                Vector3 viewPos = new Vector3(0.0f, 5.0f, 5.0f);
                viewPos = Vector3.Transform(viewPos, sceneRotation * scenePosition);//invert?
                Vector3 lightColor = new Vector3(1.0f, 1.0f, 1.0f);
                Vector3 objectColor = new Vector3(1.0f, 1.0f, 1.0f);
                if (SelectedObject == i)
                {
                    objectColor = new Vector3(1.0f, 1.0f, 1.0f);
                }
                else
                {
                    objectColor = new Vector3(0.0f, 1.0f, 1.0f);
                }

                shader.SetVec3("lightPos", lightPos);
                shader.SetVec3("viewPos", viewPos);
                shader.SetVec3("lightColor", lightColor);
                shader.SetVec3("objectColor", objectColor);
                shader.SetMatrix4x4("model", modelCombined);
                shader.SetMatrix4x4("view", sceneRotation * scenePosition);
                shader.SetMatrix4x4("projection", sceneRot * cam.GetProjectionMatrix());
                shader.Use();

                vertices = myRendergeo[i].GetVaoArray();
                glBindTexture(GL_TEXTURE_2D, texID);
                glBindBuffer(GL_ARRAY_BUFFER, vao);

                glStencilFunc(GL_ALWAYS, i + 1, 0);
                fixed (float* v = &vertices[0])//we can make a pointer in the object class something like myRendergeo[i].getvaopointer
                {
                    glBufferData(GL_ARRAY_BUFFER, sizeof(float) * vertices.Length, v, GL_DYNAMIC_DRAW);
                }
                //pointer location of Vertices
                glVertexAttribPointer(0, 3, GL_FLOAT, false, 8 * sizeof(float), (void*)0);// index, 3D positions (x and y), type, not normalized, stride(aantal bytes tot de volgende vertex): 5 X float, vertexes start on: 0(casted to some type of pointer)
                glEnableVertexAttribArray(0);
                //pointer location of UVs
                glVertexAttribPointer(1, 2, GL_FLOAT, false, 8 * sizeof(float), (void*)(3 * sizeof(float))); //index 1, size 3 (3 colorvalues), floats, not normalized, stride(bytes tot volgende lijn), 2 size of float (cast to pointer): first colorvalue starts at...
                glEnableVertexAttribArray(1);
                //pointer location of Normals
                glVertexAttribPointer(2, 3, GL_FLOAT, false, 8 * sizeof(float), (void*)(5 * sizeof(float))); //index 1, size 3 (3 colorvalues), floats, not normalized, stride(bytes tot volgende lijn), 2 size of float (cast to pointer): first colorvalue starts at...
                glEnableVertexAttribArray(2);
                glBindVertexArray(vao);
                glDrawArrays(GL_TRIANGLES, 0, vertices.Length * 6); //GL_TRIANGLES



                //unbind vertex arrays
                glBindBuffer(GL_ARRAY_BUFFER, 0);
                glBindVertexArray(0);
            }

        }

        protected unsafe override void Render()
        {
            glClearColor(0.2f, 0.2f, 0.3f, 1);
            glClearStencil(0);
            glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT | GL_STENCIL_BUFFER_BIT);
            //glBindTexture(GL_TEXTURE_2D, texID);
            glEnable(GL_STENCIL_TEST);
            glStencilOp(GL_KEEP, GL_KEEP, GL_REPLACE);
            //glStencilFunc(GL_ALWAYS, 1, 0);
            Update();


            
            Glfw.SwapBuffers(DisplayManager.Window);
            glFlush();
            glFinish();

            glPixelStorei(GL_UNPACK_ALIGNMENT, 1);

            Glfw.GetCursorPosition(DisplayManager.Window, out mouseXpos, out mouseYpos);
            int windowWidth;
            int windowHeight;
            Glfw.GetFramebufferSize(DisplayManager.Window, out windowWidth, out windowHeight);
            //byte[] data = new byte[4];
            //glReadPixels(1024 / 2, 768 / 2, 1, 1, GL_RGBA, GL_UNSIGNED_BYTE, data);
            uint index;
            glReadPixels((int)mouseXpos, windowHeight - (int)mouseYpos, 1, 1, GL_STENCIL_INDEX, GL_UNSIGNED_INT, &index);
            //Glfw.SetWindowTitle(DisplayManager.Window, Convert.ToString(index));
            CursorObject = index;
        }
    }
}
