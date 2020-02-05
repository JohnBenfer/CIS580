using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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
        GhostShip ghostShip;

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

            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            player = new Player(this, Content);
            ghostShip = new GhostShip(this, Content);
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

            player.Update(ghostShip);
            ghostShip.Update();


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

            player.Draw(spriteBatch);
            ghostShip.Draw(spriteBatch);

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


    }
}
