using FlockingTestingGrounds.Constants;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlockingTestingGrounds.GameObjects
{
    class Flock
    {
        KeyboardState kbd, oldKbd = Keyboard.GetState();
        MouseState mouse, oldMouse = Mouse.GetState();
        Texture2D texture;
        List<Boid> flock;
        int flockSize;

        Random rnd;
        Color color;

        public Flock(Texture2D texture, int flockSize)
        {
            this.texture = texture;
            this.flockSize = flockSize;
            flock = new List<Boid>();
            rnd = new Random();
            color = Color.White;
            for (int i = 0; i < flockSize; i++)
            {
                flock.Add(new Boid(texture, new Vector2(300 + rnd.Next(-100, 100), 300 + rnd.Next(-100, 100)), color));
            }
        }

        public void Update(float time)
        {
            oldKbd = kbd;
            kbd = Keyboard.GetState();
            oldMouse = mouse;
            mouse = Mouse.GetState();

            if (kbd.IsKeyDown(Keys.A) && oldKbd.IsKeyUp(Keys.A))
                GlobalData.AssembleFlock();
            else if (kbd.IsKeyDown(Keys.S) && oldKbd.IsKeyUp(Keys.S))
                GlobalData.ScatterFlock();

            MoveAsFlock();
            foreach (Boid boid in flock)
            {
                boid.Update(time);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Boid boid in flock)
            {
                boid.Draw(spriteBatch);
            }
            spriteBatch.Draw(texture, new Vector2(mouse.Position.X, mouse.Position.Y), null, Color.Pink, 0, new Vector2(texture.Width / 2, texture.Height / 2), 3, SpriteEffects.None, 1);
        }

        public void MoveAsFlock()
        {
            Vector2 separation, cohesion, alignment, destination;

            foreach (Boid boid in flock)
            {
                separation = GlobalData.m1 * ComputeSeparation(boid);
                cohesion = GlobalData.m2 * ComputeCohesion(boid);
                alignment = GlobalData.m3 * ComputeAlignment(boid);
                destination = GlobalData.m4 * ComputeDestination(boid);

                boid.myVelocity = boid.myVelocity + separation + cohesion + alignment + destination;
            }
        }



        Vector2 ComputeDestination(Boid boid)
        {
            Vector2 newPos = Vector2.Zero;
            foreach (Boid b in flock)
            {
                if (b != boid)
                {
                    newPos = new Vector2(mouse.Position.X, mouse.Position.Y) - b.myPosition;
                    newPos.Normalize();
                }
            }
            return newPos / GlobalData.DESTINATIONMODIFIER;
        }

        Vector2 ComputeCohesion(Boid boid)
        {
            Vector2 averagePos = Vector2.Zero;

            foreach (Boid b in flock)
            {
                if (b != boid)
                    averagePos += b.myPosition;
            }

            averagePos /= (flockSize - 1);

            return ((averagePos - boid.myPosition) / GlobalData.COHESIONMODIFIER);
        }

        Vector2 ComputeSeparation(Boid boid)
        {
            Vector2 displacement = Vector2.Zero;
            foreach (Boid b in flock)
            {
                if (b != boid)
                {
                    if (Vector2.Distance(boid.myPosition, b.myPosition) < GlobalData.SEPARATIONDISTANCE)
                        displacement = displacement - (b.myPosition - boid.myPosition);
                }
            }

            return displacement * GlobalData.SEPARATIONMODIFIER;
        }

        Vector2 ComputeAlignment(Boid boid)
        {
            Vector2 averageVelocity = Vector2.Zero;

            foreach (Boid b in flock)
            {
                if (b != boid)
                {
                    averageVelocity += b.myVelocity;
                }
            }

            averageVelocity = averageVelocity / (flockSize - 1);

            return (averageVelocity - boid.myVelocity) / GlobalData.ALIGNMENTMODIFIER;
        }

        public void AddBoid(Vector2 position)
        {
            flock.Add(new Boid(texture, position, Color.Red));
            flockSize++;
        }

        public void ResetFlock()
        {
            foreach (Boid b in flock)
            {
                b.myVelocity = Vector2.Zero;
            }
        }

        public void RemoveBoid()
        {
            flock.RemoveAt(flockSize);
            flockSize--;
        }
    }
}
