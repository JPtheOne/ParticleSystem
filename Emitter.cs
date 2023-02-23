using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Pelotas
{
    public class Emitter
    {
        private Random rand;
        public List<Particle> particles;
        private Size space;
        private int maxParticles;
        private float emissionRate;
        private float timer;
        public float x;
        public float y;



        public Emitter(Random rand, Size space, int maxParticles, float emissionRate)
        {
            this.rand = rand;
            this.particles = new List<Particle>();
            this.space = space;
            this.maxParticles = maxParticles;
            this.emissionRate = emissionRate;
            this.timer = 0;
            this.x = 0;
            this.y = 0;
        }

         
        public void Update(float deltaTime)
        {
            if (particles.Count < maxParticles)
            {
                timer += deltaTime;

                if (timer >= 1 / emissionRate)
                {
                    Particle newParticle = new Particle(rand, space, particles.Count);
                    //particles.Add(newParticle);
                    timer = 0;
                }
            }
        }


    }
}
