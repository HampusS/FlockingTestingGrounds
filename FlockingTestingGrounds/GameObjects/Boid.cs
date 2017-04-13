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
        protected Vector2 m_vPosition, m_vDirection;
        protected Texture2D m_texture;
        protected float m_fSpeed;
        Emitter m_emitter;

        int m_iScreenWidth, m_iScreenHeight;

        public Vector2 myPosition
        {
            get { return m_vPosition; }
            set { m_vPosition = value; }
        }

        public Vector2 myDirection
        {
            get { return m_vDirection; }
            set { m_vDirection = value; }
        }

        public Boid(Texture2D texture, Vector2 position, Color color)
        {
            this.m_texture = texture;
            this.m_vPosition = position;
            m_emitter = new Emitter(texture, position, color);
            m_fSpeed = GlobalData.BOIDSPEED;
            m_iScreenWidth = GlobalData.screenWidth;
            m_iScreenHeight = GlobalData.screenHeight;
        }

        public virtual void Update(float time)
        {
            Bounds();
            m_vPosition += m_vDirection * m_fSpeed * time;
            m_emitter.position = m_vPosition;
            m_emitter.Update(time);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            m_emitter.Draw(spriteBatch);
        }

        public void Bounds()
        {
            // Area bounds
            if (m_vPosition.X < -5)
                m_vPosition.X = m_iScreenWidth + 5;
            else if (m_vPosition.X > m_iScreenWidth + 5)
                m_vPosition.X = -5;

            if (m_vPosition.Y < -5)
                m_vPosition.Y = m_iScreenHeight + 5;
            else if (m_vPosition.Y > m_iScreenHeight + 5)
                m_vPosition.Y = -5;

            // Speed bounds
            int vLimit = 150;
            if (m_vDirection.Length() > vLimit)
                m_vDirection = (m_vDirection / m_vDirection.Length()) * vLimit;
        }
    }
}
