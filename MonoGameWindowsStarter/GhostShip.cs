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
    class GhostShip
    {
        public CircleHitBox hitBox;
        Texture2D  texture;

        double X;
        double Y;

        int screenWidth;
        int screenHeight;

        int ghostWidth;
        int ghostHeight;

        public Color color;

        Vector2 origin;

        public GhostShip(Game1 game, ContentManager content)
        {
            LoadContent(content);
            screenHeight = game.graphics.PreferredBackBufferHeight;
            screenWidth = game.graphics.PreferredBackBufferWidth;

            ghostWidth = texture.Width;
            ghostHeight = texture.Height;

            Random random = new Random();

            X = random.Next(ghostWidth, screenWidth);
            Y = random.Next(ghostHeight, screenHeight);

            color = Color.Black;

            origin = new Vector2(ghostWidth / 2, ghostHeight / 2);

            hitBox = new CircleHitBox(70, X, Y);
            Console.WriteLine(hitBox.X);
            Console.WriteLine(hitBox.Y);
            Console.WriteLine(hitBox.radius);

        }
        public void Update()
        {

            hitBox.X = X;
            hitBox.Y = Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)X, (int)Y, 262, 175), null, color, (float)Math.PI, origin, SpriteEffects.None, 0);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("ghostship");
        }

        public void Kill()
        {

        }

    }
}
