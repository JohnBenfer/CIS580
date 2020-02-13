using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;


namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        
        // Window dimensions
        int WINDOW_WIDTH = 3000;
        int WINDOW_HEIGHT = 2000;

        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        int asteroidCount;
        int level;
        List<Asteroid> asteroids;
        int maxAsteroids;
        Texture2D background;
        Texture2D SpaceBackground;
        SoundEffect gameOver;
        public float soundEffectVolume;
        SoundEffect asteroidDestroyed;
        Song backgroundMusic;
        float musicVolume;
        public int FRAME_RATE = 60;
        int score;
        SpriteFont scoreFont;
        bool isGameOver;
        int width;
        int height;
        




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
            //graphics.PreferredBackBufferWidth = 1920;
            //graphics.PreferredBackBufferHeight = 1080;
            //graphics.PreferredBackBufferWidth = 3000;
            //graphics.PreferredBackBufferHeight = 2000;
            //graphics.ApplyChanges();

            SetGraphics();

            soundEffectVolume = 0.15f;
            musicVolume = 0.2f;
            level = 1;
            asteroidCount = 0;
            asteroids = new List<Asteroid>();
            width = graphics.PreferredBackBufferWidth;
            height = graphics.PreferredBackBufferHeight;
            SetMaxAsteroids();

            base.Initialize();
            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.Volume = musicVolume;
            score = 0;
            isGameOver = false;
        }

        private void SetMaxAsteroids()
        {
            if(WINDOW_WIDTH <= 2000)
            {
                maxAsteroids = 8;

            } else if (WINDOW_WIDTH <= 2500)
            {
                maxAsteroids = 10;
            } else if (WINDOW_WIDTH <= 3000)
            {
                maxAsteroids = 12;
            } else
            {
                maxAsteroids = 14;
            }
        }

        private void SetGraphics()
        {
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            graphics.ApplyChanges();
        }
        

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            
            background = Content.Load<Texture2D>("Space");
            //SpaceBackground = Content.Load<Texture2D>("AsteroidSpriteSheet");
            player = new Player(this, Content);
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            gameOver = Content.Load<SoundEffect>("Player hit");
            asteroidDestroyed = Content.Load<SoundEffect>("Asteroid Destroyed");
            backgroundMusic = Content.Load<Song>("Background Song");
            scoreFont = Content.Load<SpriteFont>("ScoreFont");
            
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
            if (!isGameOver)
            {
                
                player.Update();
                List<Asteroid> AsteroidsDestroyedByPlayer = new List<Asteroid>();
                List<Asteroid> AsteroidsOffScreen = new List<Asteroid>();
                List<Bullet> BulletsHitAsteroid = new List<Bullet>();
                foreach (Asteroid asteroid in asteroids)
                {
                    asteroid.Update(gameTime);
                    if (asteroid.hitBox.CollidesWith(player.hitBox) && !asteroid.Exploding) // game over
                    {
                        GameOver();
                        return;
                    }

                    foreach (Bullet b in player.bullets)
                    {
                          if (asteroid.hitBox.CollidesWith(b.hitBox) && !asteroid.Hit)
                        {
                            AsteroidsDestroyedByPlayer.Add(asteroid);
                            BulletsHitAsteroid.Add(b);
                            asteroid.Exploding = true;
                            asteroid.Hit = true;
                            // play asteroid destroyed noise
                            asteroidDestroyed.Play(soundEffectVolume, 0, 0);
                            level++;
                            score += 10;
                        }
                    }
                    foreach (Bullet b in BulletsHitAsteroid)
                    {
                        player.bullets.Remove(b);
                    }

                    if (asteroid.OffScreen)
                    {
                        AsteroidsOffScreen.Add(asteroid);

                    }
                }

                RemoveAsteroids(AsteroidsDestroyedByPlayer);

                RemoveAsteroids(AsteroidsOffScreen);

                if (asteroids.Count < level && asteroids.Count < maxAsteroids)
                {
                    asteroids.Add(new Asteroid(this, Content, player.X, player.Y));

                }

                base.Update(gameTime);
            } else
            {
                // game is over here..

                var k = Keyboard.GetState();
                if(k.IsKeyDown(Keys.R))
                {
                    Initialize();
                }
                SuppressDraw(); // suppress draw to lower cpu consumption

            }

        }


        private void RemoveAsteroids(List<Asteroid> ToDeleteAsteroids)
        {
            foreach (Asteroid a in ToDeleteAsteroids)
            {
                if (!a.Exploding)
                {
                    asteroids.Remove(a);
                }
            }
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
            

            if (!isGameOver)
            {
                player.Draw(spriteBatch);
                foreach (Asteroid a in asteroids)
                {
                    a.Draw(spriteBatch);
                }
                spriteBatch.DrawString(scoreFont, "Score: " + score, new Vector2(graphics.PreferredBackBufferWidth / 2, 10), Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1);
            } else
            {
                spriteBatch.DrawString(scoreFont, "Score: " + score, new Vector2((graphics.PreferredBackBufferWidth / 2) - 200, (graphics.PreferredBackBufferHeight / 2) - 100), Color.White, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 1);
                spriteBatch.DrawString(scoreFont, "Press R to play again or ESC to quit", new Vector2((graphics.PreferredBackBufferWidth / 2) - 530, (graphics.PreferredBackBufferHeight / 2)), Color.White, 0f, new Vector2(0, 0), 1.5f, SpriteEffects.None, 1);
            }
            spriteBatch.End();
            
            base.Draw(gameTime);
        }


        private void GameOver()
        {
            gameOver.Play(soundEffectVolume, 0, 0);
            asteroids = null;
            player = null;
            MediaPlayer.Stop();
            isGameOver = true;
        }

    }
}
