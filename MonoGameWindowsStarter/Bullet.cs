using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter
{
    class Bullet
    {
        double X;
        double Y;
        public CircleHitBox hitBox;
        Texture2D texture;

        public bool Killed = false;

        int screenWidth;
        int screenHeight;

        int bulletWidth;
        int bulletHeight;

        double speed;

        Vector2 origin;

        double rotation;

        public Bullet(Game1 game, ContentManager content, double playerX, double playerY, double playerRotation)
        {
            LoadContent(content);
            screenHeight = game.graphics.PreferredBackBufferHeight;
            screenWidth = game.graphics.PreferredBackBufferWidth;

            rotation = playerRotation;
            bulletWidth = texture.Width;
            bulletHeight = texture.Height;
            origin = new Vector2(bulletWidth / 2, bulletHeight / 2);
            speed = 18;
            X = playerX;
            Y = playerY;

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
