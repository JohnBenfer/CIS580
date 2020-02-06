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
        CircleHitBox hitBox;
        Texture2D texture;

        int screenWidth;
        int screenHeight;

        double speed;

        Vector2 origin;

        double rotation;

        public Bullet(Game1 game, ContentManager content, double playerX, double playerY, double playerRotation)
        {
            LoadContent(content);
            screenHeight = game.graphics.PreferredBackBufferHeight;
            screenWidth = game.graphics.PreferredBackBufferWidth;



        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Bullet");
        }
    }
}
