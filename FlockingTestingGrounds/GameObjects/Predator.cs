using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlockingTestingGrounds.GameObjects
{
    class Predator : Boid
    {
        Color color;
        Random rnd;
        int hunger;

        public int Hunger
        {
            get { return hunger; }
            set { hunger = value; }
        }

        public Predator(Texture2D texture, Vector2 position, Color color)
            : base(texture, position, color)
        {
            this.m_texture = texture;
            this.m_vPosition = position;
            this.color = color;
            m_vDirection = new Vector2(0, 1);
            rnd = new Random();
            m_fSpeed = 200;
        }

        public override void Update(float time)
        {
            if (rnd.Next(50) == 1)
            {
                ++hunger;
                Console.WriteLine(hunger);
            }
            if (hunger < 0)
                hunger = 0;
            Bounds();
            m_vPosition += m_vDirection * m_fSpeed * time;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_texture, m_vPosition, null, color, 0, new Vector2(m_texture.Width / 2, m_texture.Height / 2), 3, SpriteEffects.None, 1);
        }

        public bool Hungering()
        {
            if (hunger > 5)
                return true;
            return false;
        }
    }
}
