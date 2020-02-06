using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter
{
    class Player
    {
        Texture2D texture;
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

        List<Bullet> bullets;
        ContentManager content;
        Game1 game;


        public Player (Game1 game, ContentManager content)
        {
            screenHeight = game.graphics.PreferredBackBufferHeight;
            screenWidth = game.graphics.PreferredBackBufferWidth;
            this.content = content;
            bullets = new List<Bullet>();
            LoadContent(content);
            this.game = game;

            X = screenWidth / 2;
            Y = screenHeight / 2;
            rotation = 0;

            playerWidth = texture.Width;
            playerHeight = texture.Height;

            origin = new Vector2(playerWidth / 2, playerHeight / 2);

            speed = 8;

            hitBox = new CircleHitBox(70, X, Y);

            Console.WriteLine(hitBox.X);
            Console.WriteLine(hitBox.Y);
            Console.WriteLine(hitBox.radius);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)X, (int)Y, 262, 175), null, Color.White, ConvertToRadians(rotation + 180), origin, SpriteEffects.None, 0);
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
                rotation-= speed;
                
            } 
            if(keyboardState.IsKeyDown(Keys.Right))
            {
                rotation+= speed;

            }
            if(keyboardState.IsKeyDown(Keys.W))
            {
                X += Math.Sin(ConvertToRadians(rotation)) * speed;
                Y -= Math.Cos(ConvertToRadians(rotation)) * speed;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                X -= Math.Sin(ConvertToRadians(rotation)) * speed;
                Y += Math.Cos(ConvertToRadians(rotation)) * speed;
            }

            if(keyboardState.IsKeyDown(Keys.Space))
            {
                Shoot();
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

            

        }

        private void Shoot()
        {
            bullets.Add(new Bullet(game, content, X, Y, rotation));
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("playerspaceship");
        }

       


    }
}
