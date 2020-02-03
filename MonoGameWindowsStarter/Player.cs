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


        public Player (Game1 game)
        {

        }
        public void Draw()
        {

        }

        public void Update()
        {

        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("playerspaceship");
        }

    }
}
