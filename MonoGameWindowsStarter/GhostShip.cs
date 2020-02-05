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
        public TriangleHitBox hitBox;
        Texture2D  texture;
        Vector2 location;
        Vector2 rotation;

        public GhostShip()
        {
            location = new Vector2(100, 100);
            rotation = new Vector2(0, 0);
        }
        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(500, 200, 200, 150), Color.Black);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("ghostship");
        }

    }
}
