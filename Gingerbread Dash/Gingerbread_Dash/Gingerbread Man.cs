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
    class Gingerbread_Man
    {
        Color color;
        Rectangle manRect;
        Rectangle[] arrayOfSprites;
        Texture2D manTexture;

        int currentFrame = 0;
        float timer = 0f;
        float frameInterval = 100f;
        int totalFrames;

        Rectangle collisionRect;

        public Gingerbread_Man(Rectangle[] v, Texture2D t, Color c)
        {
            manRect = new Rectangle(250, 370, 60, 80);
            arrayOfSprites = v;
            manTexture = t;
            color = c;
            collisionRect = new Rectangle(270, 370, 40, 60);
        }

        public Texture2D GetTexture()
        {
            return manTexture;
        }

        public void changeTexture(Texture2D t)
        {
            manTexture = t;
        }

        public Rectangle GetRect()
        {
            return manRect;
        }

        public Rectangle CollisionGetRect()
        {
            return collisionRect;
        }

        public Rectangle GetSourceRect(int l)
        {
            return arrayOfSprites[l];
        }

        public Color GetColor()
        {
            return color;
        }

        public void ChangeColor(Color c)
        {
            color = c;
        }

        public void ChangeY(int y)
        {
            manRect.Y += y;
        }
        public void SetHeight(int h)
        {
            manRect.Height = h;
        }
        public void SetY(int y)
        {
            manRect.Y = y;
        }
        public void SetX(int x)
        {
            manRect.X = x;
        }

        public void SetLandingTexture()
        {
            currentFrame = 5;
        }

        public void SetJumpingTexture()
        {
            currentFrame = 3;
        }

        public void SetDuckingTexture()
        {
            currentFrame = 7;
        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer >= frameInterval)
            {
                currentFrame = (currentFrame + 1) % arrayOfSprites.Length;
                timer = 0f;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(manTexture, manRect, arrayOfSprites[currentFrame], color);
        }
    }
}
