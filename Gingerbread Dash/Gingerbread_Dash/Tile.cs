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
    class Tile
    {
        Texture2D text;
        Rectangle rect, source;
        int src;
        public Tile(Rectangle r, int s, Texture2D t)
        {
            text = t;
            rect = r;
            src = s;
            source = SetSource(s);
        }

        public Rectangle GetSource()
        {
            return source;
        }

        public Rectangle SetSource(int s)
        {
            switch (s)
            {
                case 0:
                    return new Rectangle(10, 43, 160, 77);
                case 1:
                    return new Rectangle(187, 43, 160, 77);
                case 2:
                    return new Rectangle(363, 43, 160, 77);
                case 3:
                    return new Rectangle(538, 43, 160, 77);
                case 4:
                    return new Rectangle(10, 219, 160, 77);
                case 5:
                    return new Rectangle(187, 219, 160, 77);
                case 6:
                    return new Rectangle(363, 219, 160, 77);

            }

            return new Rectangle(10, 43, 160, 77);
        }

        public int GetSrc()
        {
            return src;
        }
        public Rectangle GetRect()
        {
            return rect;
        }
        public void SetPos(Rectangle r)
        {
            rect = r;
        }

        public Texture2D GetTexture2D()
        {
            return text;
        }
        public void Move(int s)
        {
            rect.X -= s;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(text, rect, source, Color.White);
        }
    }
}
