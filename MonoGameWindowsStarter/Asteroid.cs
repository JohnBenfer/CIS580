﻿using Microsoft.Xna.Framework;
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
        Texture2D Explosion1;
        Texture2D Explosion2;
        Texture2D Explosion3;
        Texture2D Explosion4;
        Texture2D currentTexture;
        Texture2D[] sprites;

        int frame;

        TimeSpan timer;

        int screenWidth;
        int screenHeight;

        public bool OffScreen = false;
        public bool Exploding = false;

        int ghostWidth;
        int ghostHeight;

        double speed;

        public CircleHitBox hitBox;

        public Color color;

        Vector2 origin;

        double rotation;

        Game1 game;

        public Asteroid(Game1 game, ContentManager content, double playerX, double playerY)
        {
            LoadContent(content);
            
            timer = new TimeSpan(0);

           

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

            hitBox = new CircleHitBox(60, X, Y);
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

            this.game = game;
            sprites = new Texture2D[5];
            sprites[0] = texture;
            sprites[1] = Explosion1;
            sprites[2] = Explosion2;
            sprites[3] = Explosion3;
            sprites[4] = Explosion4;

            currentTexture = sprites[0];
        }

        

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Asteroid2");
            Explosion1 = content.Load<Texture2D>("Explosion1");
            Explosion2 = content.Load<Texture2D>("Explosion2");
            Explosion3 = content.Load<Texture2D>("Explosion1");
            Explosion4 = content.Load<Texture2D>("Explosion3");
        }

        public void Update(GameTime gameTime)
        {
            if (!Exploding)
            {
                rotation += 0.1;

                X += XDelta;
                Y += YDelta;



                hitBox.X = X;
                hitBox.Y = Y;

                if (X > screenWidth + 130 || X < -130 || Y > screenHeight + 130 || Y < -130)
                {
                    OffScreen = true;
                }

                currentTexture = texture;

            }
            else
            {

                timer += gameTime.ElapsedGameTime;
                Explode();
            }
            
        }

        private void Explode()
        {
            Console.WriteLine(frame);
            
                
            if ((timer.TotalMilliseconds * 7) > (game.FRAME_RATE - 58))
            {
                    
                frame++;
                currentTexture = sprites[frame];
                Console.WriteLine(frame);
                timer -= new TimeSpan(0, 0, 0, 0, game.FRAME_RATE);
            }



            if (frame == 4)
            {
                //Exploding = false;
                OffScreen = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentTexture, new Rectangle((int)X, (int)Y, 120, 120), null, color, (float)rotation, origin, SpriteEffects.None, 0);
        }
    }
}
