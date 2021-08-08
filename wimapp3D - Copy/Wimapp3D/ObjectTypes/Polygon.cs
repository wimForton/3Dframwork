using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class Polygon
    {
        public List<int> Vertices { get; set; } = new List<int>();
        public List<int> UVs { get; set; } = new List<int>();
        public List<Vector> Colors { get; set; } = new List<Vector>();
        public List<int> Normals { get; set; } = new List<int>();
    }
}
