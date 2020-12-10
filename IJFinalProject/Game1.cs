using IJFinalProject.GameScenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;


namespace IJFinalProject
{
    
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //declare all scene references 
        private ActionScene actionScene;
        private MenuScene menuScene;
        private HelpScene helpScene;
        private HighScoreScene highScoreScene;
        private AboutScene aboutScene;

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
            
            
            base.Initialize();
        }

        private void hideAllScenes()
        {
            foreach (GameScene item in Components)
            {
                item.hide();
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            Shared.stage = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            menuScene = new MenuScene(this, spriteBatch);
            this.Components.Add(menuScene);

            //other scenes will be here
            actionScene = new ActionScene(this, spriteBatch);
            this.Components.Add(actionScene);

            helpScene = new HelpScene(this, spriteBatch);
            this.Components.Add(helpScene);

            //show only startScene
            menuScene.show();
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

            // TODO: Add your update logic here
            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();

            if (menuScene.Enabled)
            {
                selectedIndex = menuScene.Menu.SelectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    //or startScene.hide();
                    actionScene.show();
                }
                if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    //or startScene.hide();
                    helpScene.show();
                }

                if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    this.Exit();
                }
            }
            if (helpScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    hideAllScenes();
                    //or helpScene.hide();
                    menuScene.show();
                }

            }
            if (actionScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    hideAllScenes();
                    //or actionScene.hide();
                    menuScene.show();
                }
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

            base.Draw(gameTime);
        }
    }
}
