using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gingerbread_Dash
{
    class Carrot
    {
        Rectangle position;
        Texture2D text;
        int speed, bounce, startPos, spin;
        public Carrot(Rectangle rect, Texture2D t, int spd)
        {
            position = rect;
            text = t;
            speed = spd;
            bounce = -3;
            spin = 0;
            startPos = position.Y;
        }
        public void setSpeed(int spd)
        {
            speed = spd;
        }
        public void SetRectangle(Rectangle rect)
        {
            position = rect;
        }

        public void Move()
        {
            position.X -= speed;
            spin++;

            if (spin % 5 == 0)
            {
                position.Y += bounce;
                if (position.Y > startPos + 20)
                    bounce *= -1;
                if (position.Y < startPos)
                    bounce *= -1;
            }
        }


        public Rectangle GetRectangle()
        {
            return position;
        }

        public void Update()
        {
            Move();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(text, position, Color.White);
        }
    }
}
