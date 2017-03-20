using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlockingTestingGrounds.Utilities
{
    class Emitter
    {
        List<Particle> particles;
        Texture2D texture;
        public Vector2 position;
        Color color;

        public Emitter(Texture2D texture, Vector2 position, Color color)
        {
            this.texture = texture;
            this.position = position;
            this.color = color;
            particles = new List<Particle>();
        }

        public void Update(float time)
        {
            EmitParticles();
            foreach (Particle p in particles)
            {
                p.Update(time);
            }
            RemoveParticles();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particle p in particles)
            {
                p.Draw(spriteBatch);
            }
        }

        protected virtual Particle GenerateParticle()
        {
            return new Particle(texture, position, color, 0.5f);
        }

        protected virtual void EmitParticles()
        {
            particles.Add(GenerateParticle());
        }

        protected void RemoveParticles()
        {
            for (int i = particles.Count - 1; i >= 0; i--)
            {
                if (!particles[i].IsAlive)
                    particles.RemoveAt(i);
            }
        }
    }
}
