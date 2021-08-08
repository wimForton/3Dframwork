using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    interface IRenderableGeo
    {
        public string Name { get; set; }
        public Vector Position { get; set; }
        public Vector Rotation { get; set; }
        public Vector Scale { get; set; }
        public List<Polygon> Polygons { get; set; }
        public List<Vector> Points { get; set; }
        public List<Vector> UVs { get; set; }
        public List<Vector> Normals { get; set; }
        public List<Particle> myParticles { get; set; }
        public bool NeedsUpdate { get; set; }
        public bool OutputNeedsUpdate { get; set; }
        public List<float> MakeVaoList();
        public float[] MakeVaoArray();
        public abstract float[] GetVaoArray();
        public virtual void UpdateVAO() { }
        public virtual void OpenProportiesButton() { }
        //public abstract void StartParticle(Particle inparticle);
        public abstract void Update();
        public abstract void OpenProportiesWindow();
        public abstract void CheckProportiesWindow();
        public abstract string ToString();
        
    }
}
