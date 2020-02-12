using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;

namespace MonoGameWindowsStarter
{
    class Player
    {
        Texture2D texture;
        SoundEffect shooting;
        float soundEffectVolume;
        public CircleHitBox hitBox;
        double rotation;
        Vector2 origin;

        public double X;
        public double Y;

        int screenWidth;
        int screenHeight;

        int playerWidth;
        int playerHeight;

        double speed;

        public List<Bullet> bullets;
        ContentManager content;
        Game1 game;

        bool shootLock = false;

        public Player (Game1 game, ContentManager content)
        {
            screenHeight = game.graphics.PreferredBackBufferHeight;
            screenWidth = game.graphics.PreferredBackBufferWidth;
            this.content = content;
            bullets = new List<Bullet>();
            LoadContent(content);
            this.game = game;
            this.soundEffectVolume = game.soundEffectVolume;

            X = screenWidth / 2;
            Y = screenHeight / 2;
            rotation = 0;

            playerWidth = texture.Width;
            playerHeight = texture.Height;

            origin = new Vector2(playerWidth / 2, playerHeight / 2);

            speed = 8;

            hitBox = new CircleHitBox(25, X, Y);

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)X, (int)Y, 200, 133), null, Color.White, ConvertToRadians(rotation + 180), origin, SpriteEffects.None, 0);
            foreach(Bullet b in bullets)
            {
                b.Draw(spriteBatch);
            }
        }

        private float ConvertToRadians(double degrees)
        {
            return (float)(degrees * Math.PI / 180);
        }

        private double ConvertToDegrees(double degrees)
        {
            return -1;
        }

        public void Update()
        {

            var keyboardState = Keyboard.GetState();

            if(keyboardState.IsKeyDown(Keys.Left))
            {
                rotation-= speed * 0.7;
                
            } 
            if(keyboardState.IsKeyDown(Keys.Right))
            {
                rotation+= speed * 0.7;

            }
            if(keyboardState.IsKeyDown(Keys.Up))
            {
                X += Math.Sin(ConvertToRadians(rotation)) * speed;
                Y -= Math.Cos(ConvertToRadians(rotation)) * speed;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                X -= Math.Sin(ConvertToRadians(rotation)) * speed;
                Y += Math.Cos(ConvertToRadians(rotation)) * speed;
            }

            if(keyboardState.IsKeyDown(Keys.Space))
            {
                if(!shootLock)
                {
                    Shoot();
                    shootLock = true;
                }
                
            } else
            {
                shootLock = false;
            }

            if(X < 0)
            {
                X = 0;
            }
            if(X > screenWidth)
            {
                X = screenWidth;
            }
            if(Y < 0)
            {
                Y = 0;
            }
            if(Y > screenHeight)
            {
                Y = screenHeight;
            }

            hitBox.X = X;
            hitBox.Y = Y;

            List<Bullet> temp = new List<Bullet>();
            foreach(Bullet b in bullets)
            {
                b.Update();
                if(b.Killed)
                {
                    temp.Add(b);
                }
            }
            foreach (Bullet b in temp)
            {
                bullets.Remove(b);
            }

        }

        private void Shoot()
        {
            bullets.Add(new Bullet(game, content, X, Y, rotation));
            shooting.Play(soundEffectVolume * (float)0.4, 0, 0);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("playerspaceship");
            shooting = content.Load<SoundEffect>("Gun shot");
        }

       


    }
}
