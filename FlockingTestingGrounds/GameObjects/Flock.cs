﻿using FlockingTestingGrounds.Constants;
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

        Leader leader;
        Predator pred;
        float timer;

        public Flock(Texture2D texture, int flockSize)
        {
            this.texture = texture;
            this.flockSize = flockSize;
            Initialize();
        }

        public void Initialize()
        {
            flock = new List<Boid>();
            rnd = new Random();
            color = Color.White;
            leader = new Leader(texture, new Vector2(200, 200), Color.Pink);
            pred = new Predator(texture, new Vector2(400, 400), Color.Red);
            for (int i = 0; i < flockSize; i++)
            {
                flock.Add(new Boid(texture, new Vector2(300 + rnd.Next(-100, 100), 300 + rnd.Next(-100, 100)), color));
            }
        }

        public void Update(float time)
        {
            timer += time;
            oldKbd = kbd;
            kbd = Keyboard.GetState();
            oldMouse = mouse;
            mouse = Mouse.GetState();
            if (kbd.IsKeyDown(Keys.A) && oldKbd.IsKeyUp(Keys.A))
                GlobalData.AssembleFlock();
            else if (kbd.IsKeyDown(Keys.S) && oldKbd.IsKeyUp(Keys.S))
                GlobalData.ScatterFlock();
            else if (kbd.IsKeyDown(Keys.D))
                Initialize();

            MoveAsFlock();
            leader.Update(time);
            pred.Update(time);
            foreach (Boid boid in flock)
            {
                boid.Update(time);
            }

            if (pred.Hungering())
            {
                Vector2 temp = Vector2.Zero;
                foreach (Boid b in flock)
                {
                    if (temp == Vector2.Zero)
                        temp = b.myPosition;
                    if (Vector2.Distance(temp, pred.myPosition) < Vector2.Distance(b.myPosition, pred.myPosition))
                        temp = b.myPosition;
                }

                pred.myDirection = Vector2.Normalize((temp - pred.myPosition));
                EatBoids(pred.myPosition);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            leader.Draw(spriteBatch);
            foreach (Boid boid in flock)
            {
                boid.Draw(spriteBatch);
            }
            pred.Draw(spriteBatch);
            //spriteBatch.Draw(texture, new Vector2(mouse.Position.X, mouse.Position.Y), null, Color.Red, 0, new Vector2(texture.Width / 2, texture.Height / 2), 3, SpriteEffects.None, 1);
        }

        public void MoveAsFlock()
        {
            Vector2 separation, cohesion, alignment, destination;

            foreach (Boid boid in flock)
            {
                separation = GlobalData.m1 * ComputeSeparation(boid);
                cohesion = GlobalData.m2 * ComputeCohesion(boid);
                alignment = GlobalData.m3 * ComputeAlignment(boid);
                destination = GlobalData.m4 * ComputeDestination(leader.FuturePos);

                if (ScareFlock(pred.myPosition))
                {
                    alignment *= -1;
                }

                boid.myDirection += separation + cohesion + alignment + destination;
            }

        }

        public void EatBoids(Vector2 other)
        {
            for (int i = flock.Count - 1; i > 0; --i)
            {
                if (Vector2.Distance(flock[i].myPosition, other) < GlobalData.SEPARATIONDISTANCE * 2)
                {
                    //flock.RemoveAt(i);
                    pred.Hunger--;
                }
            }
        }

        public bool ScareFlock(Vector2 mouse)
        {
            foreach (Boid boid in flock)
            {
                if (Vector2.Distance(boid.myPosition, mouse) < GlobalData.SEPARATIONDISTANCE * 3)
                    return true;
            }
            return false;
        }

        Vector2 ComputeDestination(Vector2 target)
        {
            Vector2 newPos = Vector2.Zero;
            foreach (Boid b in flock)
            {
                newPos = target - b.myPosition;
                newPos.Normalize();
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
            averagePos += leader.myPosition;
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
                    averageVelocity += b.myDirection;
                }
            }
            averageVelocity = averageVelocity / (flockSize - 1);

            return (averageVelocity - boid.myDirection) / GlobalData.ALIGNMENTMODIFIER;
        }

        public void AddBoid(Vector2 position)
        {
            flock.Add(new Boid(texture, position, Color.Red));
        }

        public void ResetFlock()
        {
            foreach (Boid b in flock)
            {
                b.myDirection = Vector2.Zero;
            }
        }
    }
}
