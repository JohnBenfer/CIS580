using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter
{
    class Asteroid
    {
        double X;
        double Y;
        double XDelta;
        double YDelta;
        Texture2D texture;

        int screenWidth;
        int screenHeight;

        public bool Killed = false;

        int ghostWidth;
        int ghostHeight;

        double speed;

        public CircleHitBox hitBox;

        public Color color;

        Vector2 origin;

        double rotation;

        public Asteroid(Game1 game, ContentManager content, double playerX, double playerY)
        {
            LoadContent(content);
            screenHeight = game.graphics.PreferredBackBufferHeight;
            screenWidth = game.graphics.PreferredBackBufferWidth;

            ghostWidth = texture.Width;
            ghostHeight = texture.Height;

            Random random = new Random();

            X = random.Next(0, 3);
            //Console.WriteLine(X);
            if(X == 0)
            {
                X = random.Next(-100, -1);
                Y = random.Next(-100, screenHeight + 100);
            } else if(X == 1)
            {
                X = random.Next(0, screenWidth);
                
                if(random.Next(0,2) == 1)
                {
                    Y = random.Next(-100, -1);
                } else
                {
                    Y = random.Next(screenHeight + 1, screenHeight + 100);
                }
            } else
            {
                X = random.Next(screenWidth + 1, screenWidth + 100);
                Y = random.Next(-100, screenHeight + 100);
            }
            

            color = Color.White;

            origin = new Vector2(ghostWidth / 2, ghostHeight / 2);

            hitBox = new CircleHitBox(45, X, Y);
            speed = 50;
            double slope = (playerY - Y) / (playerX - X);
            XDelta = Math.Sqrt(speed / ((slope * slope) + 1));
            YDelta = slope * XDelta;

            if(playerX < X)
            {
                XDelta *= -1;
            }
            if(playerY < Y)
            {
                YDelta *= -1;
            }
            

        }

        

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Asteroid1");
        }

        public void Update()
        {
            rotation += 0.1;

            X += XDelta;
            Y += YDelta;
            
            

            hitBox.X = X;
            hitBox.Y = Y;

            if(X > screenWidth + 130 || X < -130 || Y > screenHeight + 130 || Y < -130)
            {
                Killed = true;
            }

            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)X, (int)Y, 100, 100), null, color, (float)rotation, origin, SpriteEffects.None, 0);
        }
    }
}
