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

        public BulletModel(ContentManager content, Game1 game)
        {
            texture = content.Load<Texture2D>("Bullet");
            screenHeight = game.graphics.PreferredBackBufferHeight;
            screenWidth = game.graphics.PreferredBackBufferWidth;
            bulletWidth = texture.Width;
            bulletHeight = texture.Height;
            speed = 18;
            origin = new Vector2(bulletWidth / 2, bulletHeight / 2);
        }
    }
}
