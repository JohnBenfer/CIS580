using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace MonoGameWindowsStarter
{
    class BulletModel
    {
        public Texture2D texture;
        public int screenWidth;
        public int screenHeight;
        int bulletWidth;
        int bulletHeight;
        public double speed;
        public Vector2 origin;
        public List<Bullet> bullets;

        public BulletModel(ContentManager content, Game1 game)
        {
            LoadContent(content);
            screenHeight = game.graphics.PreferredBackBufferHeight;
            screenWidth = game.graphics.PreferredBackBufferWidth;
            bulletWidth = texture.Width;
            bulletHeight = texture.Height;
            speed = 18;
            origin = new Vector2(bulletWidth / 2, bulletHeight / 2);
        }

        public void Update()
        {
            foreach (Bullet bullet in bullets)
            {
                bullet.X += Math.Sin(ConvertToRadians(bullet.rotation)) * speed;
                bullet.Y -= Math.Cos(ConvertToRadians(bullet.rotation)) * speed;

                bullet.hitBox.X = bullet.X;
                bullet.hitBox.Y = bullet.Y;

                if (bullet.X > screenWidth + 10 || bullet.X < -10 || bullet.Y > screenHeight + 10 || bullet.Y < -10)
                {
                    bullet.Killed = true;
                }
            }
        }

        private float ConvertToRadians(double degrees)
        {
            return (float)(degrees * Math.PI / 180);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet bullet in bullets)
            {
                spriteBatch.Draw(texture, new Rectangle((int)bullet.X, (int)bullet.Y, 70, 70), null, Color.White, (float)ConvertToRadians(bullet.rotation), origin, SpriteEffects.None, 0);
            }
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Bullet");
        }
    }
}
