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
        }
        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, location, null, null, rotation);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("ghostship");
        }

    }
}
