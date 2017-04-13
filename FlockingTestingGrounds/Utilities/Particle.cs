using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlockingTestingGrounds.Utilities
{
    class Particle
    {
        Texture2D texture;
        Vector2 position;
        Color color;

        Vector2 origin;
        float lifeTime;

        public bool IsAlive
        {
            get;
            private set;
        }

        public Particle(Texture2D texture, Vector2 position, Color color, float lifeTime)
        {
            this.texture = texture;
            this.position = position;
            this.color = color;
            this.lifeTime = lifeTime;
            IsAlive = true;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public void Update(float time)
        {
            lifeTime -= time;

            if (lifeTime < 0)
                IsAlive = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, 0, origin, 1 * lifeTime, SpriteEffects.None, 1);
        }

        public void KillMe(int amount)
        {
            lifeTime = amount;
        }
    }
}
