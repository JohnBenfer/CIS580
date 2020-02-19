﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter
{
    class Bullet : BulletModel
    {
        public bool isActive;
        double X;
        double Y;
        public CircleHitBox hitBox;
        

        public bool Killed = false;

        double rotation;

        public Bullet(ContentManager content, Game1 game, double playerX, double playerY, double playerRotation)
        {
            BulletModel bm = new BulletModel(content, game);

            isActive = false;
        }

        /// <summary>
        /// activates the bullet and will start updating and drawing
        /// </summary>
        /// <param name="playerX"></param>
        /// <param name="PlayerY"></param>
        /// <param name="playerRotation"></param>
        public void SpawnBullet(double playerX, double PlayerY, double playerRotation)
        {
            isActive = true;
            rotation = playerRotation;
            X = playerX;
            Y = PlayerY;
            hitBox = new CircleHitBox(10, X, Y);
        }

        public void Update()
        {
            X += Math.Sin(ConvertToRadians(rotation)) * speed;
            Y -= Math.Cos(ConvertToRadians(rotation)) * speed;

            hitBox.X = X;
            hitBox.Y = Y;

            if (X > screenWidth + 10 || X < -10 || Y > screenHeight + 10 || Y < -10)
            {
                Killed = true;
            }
        }

        private float ConvertToRadians(double degrees)
        {
            return (float)(degrees * Math.PI / 180);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)X, (int)Y, 70, 70), null, Color.White, (float)ConvertToRadians(rotation), origin, SpriteEffects.None, 0);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Bullet");
        }
    }
}
