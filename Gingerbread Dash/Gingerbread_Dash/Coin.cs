using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace Gingerbread_Dash
{
    class Coin
    {
        Rectangle position, orginPos;
        Rectangle[] sourceRects;
        Texture2D text;
        int speed, pos, spin, startPos, bounce;
        Color col;
        public bool isCollected = false;
        Boolean up;

        public Coin(Rectangle rect, Texture2D t, int spd)
        {
            position = rect;
            text = t;
            speed = spd;
            orginPos = rect;
            up = true;
            pos = 0;
            spin = 0;
            startPos = position.Y;
            bounce = -3;
            sourceRects = new Rectangle[4];
            sourceRects[0] = new Rectangle(0, 3, 183, 180);
            sourceRects[1] = new Rectangle(209, 3, 140, 180);
            sourceRects[2] = new Rectangle(418, 3, 91, 180);
            sourceRects[3] = new Rectangle(608, 3, 66, 180);
            col = Color.White;
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
            if(spin % 5 == 0)
            {
                pos++;
                if (pos > 3)
                    pos = 0;
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
            spriteBatch.Draw(text, position, sourceRects[pos], Color.White);
        }

        public int Disappear(int currScore)
        {
            col = Color.Transparent;
            return currScore + 1;
        }
    }
}
