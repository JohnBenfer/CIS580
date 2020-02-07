using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        int asteroidCount;
        int level;
        List<Asteroid> asteroids;
        int maxAsteroids = 8;
        Texture2D background;
        Texture2D SpaceBackground;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            //graphics.PreferredBackBufferWidth = 3840;
            //graphics.PreferredBackBufferHeight = 2050;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();

            level = 1;
            asteroidCount = 0;
            asteroids = new List<Asteroid>();


            base.Initialize();
        }

        

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            
            background = Content.Load<Texture2D>("Space");
            //SpaceBackground = Content.Load<Texture2D>("SpaceSpriteSheet");
            player = new Player(this, Content);
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //player.LoadContent(Content);
            ///ghostShip.LoadContent(Content);
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update();
            List<Asteroid> temp = new List<Asteroid>();
            List<Bullet> tempBullets = new List<Bullet>();
            foreach(Asteroid a in asteroids)
            {
                a.Update();
                if(a.hitBox.CollidesWith(player.hitBox))
                {
                    temp.Add(a);
                    
                    Restart();
                }

                foreach (Bullet b in player.bullets)
                {
                    if(a.hitBox.CollidesWith(b.hitBox))
                    {
                        temp.Add(a);
                        tempBullets.Add(b);
                        level++;
                    }
                }
                foreach (Bullet b in tempBullets) 
                {
                    player.bullets.Remove(b);
                }

                if(a.Killed)
                {
                    temp.Add(a);

                }
            }
            foreach(Asteroid a in temp)
            {
                asteroids.Remove(a);
                asteroidCount--;
            }

            if(asteroidCount < level && asteroidCount < maxAsteroids)
            {
                asteroids.Add(new Asteroid(this, Content, player.X, player.Y));
                asteroidCount++;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            Rectangle r = new Rectangle(new Point(0, 0), new Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            spriteBatch.Draw(background, r, Color.White);

            /*spriteBatch.Draw(
                SpaceBackground, 
                new Vector2(0,0), 
                new Rectangle(new Point(0,0), new Point(SpaceBackground.Width/3, SpaceBackground.Height)), 
                Color.White);
*/

            player.Draw(spriteBatch);
            foreach(Asteroid a in asteroids)
            {
                a.Draw(spriteBatch);
            }

            spriteBatch.End();
            
            base.Draw(gameTime);
        }

        private bool CollidesWith(TriangleHitBox t1, TriangleHitBox t2)
        {

            double t2area = getAreaOfTraingle(t2.x1, t2.x2, t2.x3, t2.y1, t2.y2, t2.y3);
            double tempX = t1.x1;
            double tempY = t1.y1;
            for (int i = 1; i <= 3; i++)
            {
                double a1 = getAreaOfTraingle(tempX, t2.x1, t2.x3, tempY, t2.y1, t2.y3);
                double a2 = getAreaOfTraingle(tempX, t2.x2, t2.x3, tempY, t2.y2, t2.y3);
                double a3 = getAreaOfTraingle(tempX, t2.x1, t2.x2, tempY, t2.y1, t2.y2);
                if ((a1 + a2 + a3) - t2area < 0.0000001 && t2area - (a1 + a2 + a3) < 0.0000001)
                {
                    return true;
                }
                if (i == 1)
                {
                    tempX = t1.x2;
                    tempY = t1.y2;
                }
                else
                {
                    tempX = t1.x3;
                    tempY = t1.y3;
                }
            }

            return false;
        }

        private double getAreaOfTraingle(double x1, double x2, double x3, double y1, double y2, double y3)
        {
            return Math.Abs((x1 * (y2 - y3) + x2 * (y3 - y1) + x3 * (y1 - y2)) / 2);
        }


        private void Restart()
        {
            Initialize();

        }

    }
}
