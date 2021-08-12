using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameEngine
{
    interface IRenderableGeo
    {
        //public static List<IRenderableGeo> ConnectionsStart { get; set; }
        //public static List<IRenderableGeo> ConnectionsEnd { get; set; }
        //public List<IRenderableGeo> getConnectionsStart();
        //public List<IRenderableGeo> getConnectionsEnd();
        public IRenderableGeo InputObject { get; set; }
        public bool isRootGeoNode { get; set; }
        public List<IRenderableGeo> ChildGeoNodes { get; set; }
        public static IRenderableGeo ChildLookingForGeoParent { get; set; }
        public List<IAnimationControl> AnimationControls { get; set; }
        public NodeGuiElement GuiNode { get; set; }
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
        public bool NeedsInputObject { get; set; }
        public Vector GuiNodePosition { get; set; }
        public void MakeVaoList();
        public void MakeVaoArray();
        public abstract float[] GetVaoArray();
        public virtual void UpdateVAO() { }
        public abstract void Update();
        public abstract void OpenProportiesWindow();
        public abstract void CheckProportiesWindow();
        public abstract string ToString();
        
    }
}
