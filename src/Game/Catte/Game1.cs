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

namespace Catte
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //// Textures and rectangles for our game entities
        Texture2D texCat;
        Texture2D texFurball;
        Texture2D texAlien;
        Texture2D texAcid;

        GameWorld _GameWorld;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Set our preferred screen size
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            // Load our game sprites, and set up rectangles for each
            texCat      = Content.Load<Texture2D>("cat");
            texFurball  = Content.Load<Texture2D>("furball");
            texAlien    = Content.Load<Texture2D>("alien");
            texAcid     = Content.Load<Texture2D>("acid");

            var rectCat     = new Rectangle(50, 300, texCat.Width, texCat.Height);
            var rectFurball = new Rectangle(2000, 0, texFurball.Width, texFurball.Height);
            var rectAlien   = new Rectangle(1000, 500, texAlien.Width, texAlien.Height);
            var rectAcid    = new Rectangle(-2000, 0, texAcid.Width, texAcid.Height);

            var controlPad = new ControlPad(PlayerIndex.One);

            _GameWorld = new GameWorld(controlPad, rectCat, texCat, rectFurball, texFurball, rectAlien, texAlien, rectAcid, texAcid);
            _GameWorld.ActivateAlien();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            _GameWorld.Update();

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

            // Begin drawing!
            spriteBatch.Begin();

            _GameWorld.Draw(spriteBatch);

            // End drawing!
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
