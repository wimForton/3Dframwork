using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    static class GameTime
    {
        public static float DeltaTime { get; set; }
        public static float TotalElapsedSeconds { get; set; }
        public static float PrevFrame { get; set; }
        public static float Frame { get; set; }
        public static bool isNewFrame { get; set; } = true;
    }
}
