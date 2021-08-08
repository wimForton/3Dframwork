using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class Particle
    {
        public List<int> ConstraintNeighbors { get; set; }
        public List<double> RestLengths { get; set; } = new List<double>();
        public int ParticleId { get; set; }
        public int ParticleInstance { get; set; }
        public Vector Pos { get; set; }
        public Vector PrevPos { get; set; }
        public Vector Rot { get; set; }
        public Vector Vel { get; set; }
        public Vector RGB { get; set; }
        public double Size { get; set; }
        public double Mass { get; set; }
        public Vector Drag { get; set; }
        public double Age { get; set; }
        public double Lifespan { get; set; }
        public bool Fix { get; set; }
        public Particle(int inParticleId = 0, int inParticleInstance = 0, Vector inPos = null, Vector inRot = null, Vector inVel = null, Vector inRGB = null, double inSize = 1, double inMass = 1.0, double inDrag = 0.999, double inSpan = 2.0, List<int> inConstraintNB = null, List<double> inRestLengths = null)
        {
            ParticleId = inParticleId;
            ParticleInstance = inParticleInstance;
            ConstraintNeighbors = inConstraintNB != null ? inConstraintNB : new List<int>();
            Pos = inPos != null ? inPos : new Vector(0, 0, 0);
            PrevPos = inPos != null ? inPos : new Vector(0, 0, 0);
            Rot = inRot != null ? inRot : new Vector(0, 0, 0);
            Vel = inVel != null ? inVel : new Vector(0, 0, 0);
            RGB = inRGB != null ? inRGB : new Vector(1, 1, 1);
            Size = inSize;
            Mass = inMass;
            Drag = new Vector(inDrag, inDrag, inDrag);
            Age = 0.0;
            Lifespan = inSpan;
            Fix = false;
        }
    }
}
