using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class ParticleEmitter: RenderableGeo, IRenderableGeo
    {
        private double Gravity { get; set; } = 0;
        private double LifeSpan { get; set; }
        private bool RecycleParticles { get; set; } = false;
        private double Explosion { get; set; } = 0;
        private double Drag { get; set; } = 0.999;
        private Random myRandom = new Random();

        public ParticleEmitter(int input = 0)
        {
            Console.WriteLine("Bingo");
        }
        public ParticleEmitter(string inName, int inParticleMaxAmount = 20, Vector inEmitPos = null, double inExplosion = 1.0, double inGravity = 1.0, double friction = 0.999, bool recycle = false, double inLifeSpan = 2, double turbulenceStrength = 0, double turbulenceSize = 0)
        {
            Name = inName;
            Gravity = inGravity;
            Position = inEmitPos;
            Explosion = inExplosion;
            Drag = friction;
            RecycleParticles = recycle;
            LifeSpan = inLifeSpan;
            for (int i = 0; i < inParticleMaxAmount; i++)
            {
                myParticles.Add(new Particle());
                StartParticle(myParticles[i]);
            }  
        }
        public void StartParticle(Particle inParticle) {
            double velocityX = (myRandom.NextDouble() - 0.5) * Explosion;
            double velocityY = (myRandom.NextDouble() - 0.5) * Explosion;
            double velocityZ = (myRandom.NextDouble() - 0.5) * Explosion;
            inParticle.Pos = Position;
            inParticle.Mass = myRandom.NextDouble() * 0.5;
            inParticle.Vel = new Vector(velocityX, velocityY, velocityZ);
            inParticle.Age = 0.0;
            inParticle.Drag  = Vector.setNew(Drag, Drag, Drag);
            inParticle.ParticleInstance = myRandom.Next(33, 122);
            inParticle.RGB = new Vector(myRandom.NextDouble(), myRandom.NextDouble(), myRandom.NextDouble());
            inParticle.Size = 0.4;
            inParticle.Lifespan = LifeSpan + myRandom.NextDouble();
        }
        public override void Update()
        {
            for (int i = 0; i < myParticles.Count; i++)
            {
                if (i < myParticles.Count)
                {
                    myParticles[i].PrevPos = myParticles[i].Pos;
                    myParticles[i].Age += 0.01;

                    if (myParticles[i].Age >= myParticles[i].Lifespan || (double)myParticles[i].Vel < 0.03)
                    {
                        if (RecycleParticles)
                        {
                            StartParticle(myParticles[i]);
                        }
                        else
                        {
                            myParticles.RemoveAt(i);
                            break;
                        }

                    }

                    CollideEdges(myParticles[i]);
                    myParticles[i].Vel *= myParticles[i].Drag;
                    myParticles[i].Vel.Y += myParticles[i].Mass * Gravity;
                    myParticles[i].Pos += myParticles[i].Vel;
                }
            }
        }
        public static void CollideEdges(Particle inParticle)
        {
            if (inParticle.Pos.Y < -5) { 
                inParticle.Vel.Y *= -0.9;
                inParticle.Pos.Y = -4.95;
            }

        }

    }
}
