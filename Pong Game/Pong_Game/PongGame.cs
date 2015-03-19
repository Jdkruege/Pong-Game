using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pong_Game
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class PongGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Texture2D background;
        Player player1;
        Player player2;
        Ball ball;
        Inputs inputs;
        Background background;
        Boards boards;
        Info info;
        Powerup powerUp;

        SoundEffect serve;
        SoundEffectInstance backgroundMusic;

        Random rand;

        double matchTime = 0;
        bool gameStarted = false;
        bool gameWon = false;

        public PongGame()
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
            // TODO: Add your initialization logic here

            base.Initialize();

            this.Window.Title = "Tennis Aces";

            rand = new Random(System.DateTime.Now.Millisecond + System.DateTime.Now.Second + System.DateTime.Now.Minute + System.DateTime.Now.Hour + System.DateTime.Now.Day + System.DateTime.Now.Month + System.DateTime.Now.Year);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 480;
            graphics.ApplyChanges();

            // TODO: use this.Content to load your game content here
            background = new Background(this, this.Content.Load<Texture2D>("Tennis Court"), this.Content.Load<Texture2D>("Bleachers"));
            boards = new Boards(this, this.Content.Load<Texture2D>("Scoreboard"), this.Content.Load<Texture2D>("Infoboard"));
            
            player1 = new Player(this, 25, 240, this.Content.Load<Texture2D>("Player Images/Blue Racket"));
            player2 = new Player(this, 615, 240, this.Content.Load<Texture2D>("Player Images/Yellow Racket"));
            ball = new Ball(this, 320, 240, this.Content.Load<Texture2D>("Tennis Ball"));
            info = new Info(this, this.Content.Load<Texture2D>("Wind Direction"), 7, this.Content.Load<SpriteFont>("Basic"));
            powerUp = new Powerup(this, this.Content.Load<Texture2D>("Wind Direction"));
            
            inputs = new Inputs(player1, player2);

            serve = this.Content.Load<SoundEffect>("Serve");
            backgroundMusic = this.Content.Load<SoundEffect>("Background").CreateInstance();

            Services.AddService(typeof(Info), info);
            Services.AddService(typeof(Powerup), powerUp);
            Services.AddService(typeof(SpriteBatch), spriteBatch);
            Services.AddService(typeof(SoundEffect), this.Content.Load<SoundEffect>("Hit"));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            this.Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            inputs.CheckInputs();
            
            int winner = ball.Update(gameTime, player1, player2);

            if(matchTime > 5 && !gameStarted && !gameWon)
            {
                ball.yVel = rand.NextDouble() + .15;
                ball.xVel = rand.Next(0, 1) - .5;

                powerUp.x = rand.Next(160, 520);
                powerUp.y = rand.Next(120, 360);

                ChooseWind();

                serve.Play();

                gameStarted = true;
            }

            if(winner == 1)
            {
                info.p1Score++;

                ball.x = 320;
                ball.y = 240;
                ball.xVel = 0;
                ball.yVel = 0;
                info.ChangeWind("None");

                if(info.p1Score == info.goal)
                {
                    gameWon = true;
                }
                else
                {
                    matchTime = 0;
                    gameStarted = false;
                }
            }
            else if(winner == 2)
            {
                info.p2Score++;

                ball.x = 320;
                ball.y = 240;
                ball.yVel = 0;
                ball.xVel = 0;
                info.ChangeWind("None");

                if (info.p2Score == info.goal)
                {
                    gameWon = true;
                }
                else
                {
                    matchTime = 0;
                    gameStarted = false;
                }
            }

            ball.ApplyWind(info.windAngle);

            matchTime += gameTime.ElapsedGameTime.TotalSeconds;

            if(backgroundMusic.State == SoundState.Stopped || backgroundMusic.State == SoundState.Paused)
            {
                backgroundMusic.IsLooped = true;
                backgroundMusic.Volume = .75f;
                backgroundMusic.Play();
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

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            background.Draw();
            boards.Draw();
            info.Draw();

            player1.Draw();
            player2.Draw();
            ball.Draw();

            powerUp.Draw();

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void ChooseWind()
        {
            switch(rand.Next(0,7))
            {
                case 0:
                    info.ChangeWind("N");
                    break;
                case 1:
                    info.ChangeWind("E");
                    break;
                case 2:
                    info.ChangeWind("S");
                    break;
                case 3:
                    info.ChangeWind("W");
                    break;
                case 4:
                    info.ChangeWind("NE");
                    break;
                case 5:
                    info.ChangeWind("SE");
                    break;
                case 6:
                    info.ChangeWind("SW");
                    break;
                case 7:
                    info.ChangeWind("NW");
                    break;
            }
        }
    }
}
