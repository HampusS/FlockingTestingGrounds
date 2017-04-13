using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlockingTestingGrounds.GameObjects
{
    class Leader : Boid
    {
        Color color;
        float timer = 0;
        float angle;
        Random rnd;
        Vector2 test;
        Vector2 newVelocity;

        public Vector2 FuturePos
        {
            get { return newVelocity; }
            private set { newVelocity = value; }
        }

        public Leader(Texture2D texture, Vector2 position, Color color)
            : base(texture, position, color)
        {
            this.texture = texture;
            this.position = position;
            this.color = color;
            direction = new Vector2(0, 1);
            rnd = new Random();
            speed = 200;
        }

        public override void Update(float time)
        {
            SetWander();
            timer += time;
            Bounds();
            position += direction * speed * time;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, 0, new Vector2(texture.Width / 2, texture.Height / 2), 5, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, test, null, Color.Blue, 0, new Vector2(texture.Width / 2, texture.Height / 2), 2, SpriteEffects.None, 1);
        }

        public void SetWander()
        {
            newVelocity = direction;
            newVelocity *= 150;
            newVelocity += position;

            if (timer > 1.5f)
            {
                angle = GetRandomClamped(-5, 5);
                timer = 0;
            }

            newVelocity.X += (float)Math.Cos(angle) * 15;
            newVelocity.Y += (float)Math.Sin(angle) * 15;
            direction = newVelocity - position;
            test = newVelocity;
            direction.Normalize();
        }

        public float GetRandomClamped(float min, float max)
        {
            return (float)(rnd.NextDouble() * (max - min) + min);
        }
    }
}
