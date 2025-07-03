using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Gingerbread_Dash
{
    class Platform
    {
        int width = 80;
        int height = 30;
        Rectangle rect, source;
        Texture2D text;
        int speed;
        public Platform(Texture2D t, Rectangle r, int spd)
        {
            text = t;
            rect = new Rectangle(r.X, r.Y, width, height);
            source = new Rectangle(10, 43, 160, 77);
            speed = spd;
        }
        public void setSpeed(int spd)
        {
            speed = spd;
        }

        public Rectangle GetRectangle()
        {
            return rect;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(text, rect, source, Color.White);
        }
        public void Move()
        {
            rect.X -= speed;
        }

    }
}
