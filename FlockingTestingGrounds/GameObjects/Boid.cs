using FlockingTestingGrounds.Constants;
using FlockingTestingGrounds.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlockingTestingGrounds.GameObjects
{
    class Boid
    {
        protected Vector2 position, direction;
        protected Texture2D texture;
        protected float speed;
        Emitter emitter;

        int width, height;

        public Vector2 myPosition
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 myDirection
        {
            get { return direction; }
            set { direction = value; }
        }

        public Boid(Texture2D texture, Vector2 position, Color color)
        {
            this.texture = texture;
            this.position = position;
            emitter = new Emitter(texture, position, color);
            speed = GlobalData.BOIDSPEED;
            width = GlobalData.screenWidth;
            height = GlobalData.screenHeight;
        }

        public virtual void Update(float time)
        {
            Bounds();
            position += direction * speed * time;
            emitter.position = position;
            emitter.Update(time);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            emitter.Draw(spriteBatch);
        }

        public void Bounds()
        {
            if (position.X < -5)
                position.X = width + 5;
            else if (position.X > width + 5)
                position.X = -5;

            if (position.Y < -5)
                position.Y = height + 5;
            else if (position.Y > height + 5)
                position.Y = -5;

            int vLimit = 150;
            if (direction.Length() > vLimit)
                direction = (direction / direction.Length()) * vLimit;
        }
    }
}
