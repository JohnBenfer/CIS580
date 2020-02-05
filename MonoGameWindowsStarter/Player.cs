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
        TriangleHitBox hitBox;
        Vector2 location;
        Vector2 rotation;


        public Player (Game1 game)
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, location, null, null, rotation);
        }

        public void Update(GhostShip ghost)
        {
            if(CollidesWith(ghost.hitBox, this.hitBox))
            {
                Console.WriteLine("Hit");
            }
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("playerspaceship");
        }

        private bool CollidesWith(TriangleHitBox t1, TriangleHitBox t2)
        {

            float t2area = getAreaOfTraingle(t2.x1, t2.x2, t2.x3, t2.y1, t2.y2, t2.y3);
            int tempX = t1.x1;
            int tempY = t1.y1;
            for(int i = 1; i <= 3; i++)
            {
                float a1 = getAreaOfTraingle(tempX, t2.x1, t2.x3, tempY, t2.y1, t2.y3);
                float a2 = getAreaOfTraingle(tempX, t2.x2, t2.x3, tempY, t2.y2, t2.y3);
                float a3 = getAreaOfTraingle(tempX, t2.x1, t2.x2, tempY, t2.y1, t2.y2);
                if( (a1 + a2 + a3) - t2area < 0.0000001 && t2area - (a1 + a2 + a3) < 0.0000001)
                {
                    return true;
                }
                if (i == 1)
                {
                    tempX = t1.x2;
                    tempY = t1.y2;
                } else
                {
                    tempX = t1.x3;
                    tempY = t1.y3;
                }
            }

            return false;
        }

        private float getAreaOfTraingle(int x1, int x2, int x3, int y1, int y2, int y3)
        {
            return Math.Abs((x1*(y2-y3) + x2*(y3-y1) + x3*(y1-y2)) / 2);
        } 


    }
}
