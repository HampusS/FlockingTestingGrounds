using FlockingTestingGrounds.Constants;
using FlockingTestingGrounds.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlockingTestingGrounds
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Flock flock;
        Texture2D texture;
        KeyboardState kbd, oldKbd = Keyboard.GetState();

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
            // TODO: Add your initialization logic here
            //IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GlobalData.screenWidth = Window.ClientBounds.Width;
            GlobalData.screenHeight = Window.ClientBounds.Height;
            texture = Content.Load<Texture2D>("circle");
            flock = new Flock(texture, GlobalData.FLOCKSIZE);
            // TODO: use this.Content to load your game content here
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
            oldKbd = kbd;
            kbd = Keyboard.GetState();
            if (kbd.IsKeyDown(Keys.D1) && oldKbd.IsKeyUp(Keys.D1))
            {
                flock.ResetFlock();
                GlobalData.Formation();
            }
            else if (kbd.IsKeyDown(Keys.D2) && oldKbd.IsKeyUp(Keys.D2))
            {
                flock.ResetFlock();
                GlobalData.Swarm();
            }
            else if (kbd.IsKeyDown(Keys.D3) && oldKbd.IsKeyUp(Keys.D4))
            {
                flock.ResetFlock();
                GlobalData.Chaotic();
            }
            flock.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            flock.Draw(spriteBatch);
            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
