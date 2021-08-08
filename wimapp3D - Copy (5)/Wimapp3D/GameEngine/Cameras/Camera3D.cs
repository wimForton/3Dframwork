using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class Camera3D
    {
        public Vector3 FocusPosition { get; set; }
        public float Zoom { get; set; }
        public Camera3D(Vector3 focusposition, float zoom)
        {
            FocusPosition = focusposition;
            Zoom = zoom;
        }
        public Matrix4x4 GetProjectionMatrix()
        {
            Matrix4x4 perspectiveMatrix = Matrix4x4.CreatePerspectiveFieldOfView(0.5f, 1.77f, 0.1f, 100f);
            return perspectiveMatrix;
        }

    }
}
