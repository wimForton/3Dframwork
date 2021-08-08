using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    
    class ParticleTensionLine : RenderableGeo, IRenderableGeo
    {
        //public List<Particle> myParticles = new List<Particle>();
        private double Drag { get; set; } = 0.999;
        private double Gravity { get; set; } = 0;
        private Vector StartPos { get; set; }
        private Vector EndPos { get; set; }
        private int ParticleMaxAmount { get; set; }
        private int SamplesPerFrame { get; set; }
        private Random myRandom = new Random();
        public ParticleTensionLine(string inName, int inParticleMaxAmount = 10, int inSamples = 1, Vector inStartPos = null, Vector inEndPos = null, double inExplosion = 1, double inGravity = 1, double friction = 0.999, bool recycle = false, double turbulenceStrength = 0, double turbulenceSize = 0)
        {
            Name = inName;
            ParticleMaxAmount = inParticleMaxAmount;
            SamplesPerFrame = inSamples;
            StartPos = inStartPos;
            EndPos = inEndPos;
            Gravity = inGravity;
            Drag = friction;
            CreateParticles();
        }
        public void CreateParticles()
        {
            for (int i = 0; i < ParticleMaxAmount; i++)
            {
                myParticles.Add(new Particle(i));//i is ParticleId
            }
            for (int i = 0; i < ParticleMaxAmount; i++)
            {
                StartParticle(myParticles[i]);
            }
            for (int i = 0; i < ParticleMaxAmount; i++)
            {
                SetNeighbourProporties(myParticles[i]);
            }
        }
        public void StartParticle(Particle inParticle)
        {
            int id = inParticle.ParticleId;
            inParticle.ParticleInstance = 50;
            inParticle.Mass = 0.2 + myRandom.NextDouble() * 0.8;

            double interpolation = (double)id / (double)ParticleMaxAmount;
            inParticle.Pos = Vector.Lerp(StartPos, EndPos, interpolation);
            inParticle.PrevPos = inParticle.Pos;
            inParticle.RGB = new Vector(myRandom.NextDouble(), myRandom.NextDouble(), myRandom.NextDouble());
            inParticle.Size = 0.6;
            if (id == 0 || id == ParticleMaxAmount - 1)
            {
                inParticle.Fix = true;
            }
            else
            {
                inParticle.ConstraintNeighbors.Add(id - 1);
                inParticle.ConstraintNeighbors.Add(id + 1);
            }
        }
        public void SetNeighbourProporties(Particle inParticle)
        {
            int id = inParticle.ParticleId;
            if (id == 0 || id == ParticleMaxAmount - 1)
            {
                inParticle.Fix = true;
            }
            else
            {
                Vector neighbourAPos = myParticles[id - 1].Pos;
                Vector neighbourBPos = myParticles[id + 1].Pos;
                Vector thisPos = inParticle.Pos;
                double neighbourALength = (double)(neighbourAPos - thisPos) * 0.8;
                double neighbourBLength = (double)(neighbourBPos - thisPos) * 0.8;
                inParticle.RestLengths.Add(neighbourALength);
                inParticle.RestLengths.Add(neighbourBLength);
            }
        }
        public override void Update()
        {
            for (int s = 0; s < SamplesPerFrame; s++)
            {
                for (int i = 0; i < myParticles.Count; i++)
                {
                    if (!myParticles[i].Fix)
                    {
                        myParticles[i].PrevPos = myParticles[i].Pos;
                        Vector neighbourLocalPosA = myParticles[i - 1].Pos - myParticles[i].Pos;
                        Vector neighbourLocalPosB = myParticles[i + 1].Pos - myParticles[i].Pos;
                        Vector neighboursAverage = (neighbourLocalPosA + neighbourLocalPosB) * 0.5;
                        //double neighbourWeightA = Math.Max(Vector.length(neighbourLocalPosA) - myParticles[i].RestLengths[0], 0);
                        //double neighbourWeightB = Math.Max(Vector.length(neighbourLocalPosB) - myParticles[i].RestLengths[1], 0);
                        double neighbourWeightA = (double)neighbourLocalPosA - myParticles[i].RestLengths[0];
                        double neighbourWeightB = (double)neighbourLocalPosB - myParticles[i].RestLengths[1];
                        double neighbourWeightAverage = (neighbourWeightA + neighbourWeightB) * 0.5;

                        //myParticles[i].Vel += Vector.Pow(neighbourLocalPosA, 2) * 0.02 * neighbourWeightA;
                        //myParticles[i].Vel += Vector.Pow(neighbourLocalPosB, 2) * 0.02 * neighbourWeightB;
                        myParticles[i].Vel += neighboursAverage * 0.008 * neighbourWeightAverage;
                        myParticles[i].Vel += neighbourLocalPosA * 0.008 * neighbourWeightA;
                        myParticles[i].Vel += neighbourLocalPosB * 0.008 * neighbourWeightB;
                        myParticles[i].Vel *= Drag - Math.Pow((double)myParticles[i].Vel, 2) * 0.4;
                        myParticles[i].Vel.Y += myParticles[i].Mass * Gravity;
                        //if (myParticles[i].Pos.Y > 40) myParticles[i].Vel.Y *= -1;

                        myParticles[i].Pos += myParticles[i].Vel;


                    }
                }
            }
        }
    }
}
