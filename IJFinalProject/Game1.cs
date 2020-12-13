/*  Program: IJFinalProject
 *  Purpose: Game making for final project
 *  Revision History: 
 *      Created by Ilshin Ji December 1 2020
 *      Modified by Ilshin Ji December 13 2020
 */

using IJFinalProject.GameScenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

/// <summary>
/// IJFinalProject
/// </summary>
namespace IJFinalProject
{
    /// <summary>
    /// Game1 Class start from here
    /// </summary>
    public class Game1 : Game
    {
        //Declaring variables 
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //declare all scene references 
        private ActionScene actionScene;
        private MenuScene menuScene;
        private HelpScene helpScene;
        private HighScoreScene highScoreScene;
        private AboutScene aboutScene;
        //Menu music variable
        private Song menuMusic;

        /// <summary>
        /// Contructor here
        /// </summary>
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
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

        /// <summary>
        /// It will hide all scenes at once so that only one scene can be displayed
        /// </summary>
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

            Shared.stage = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            menuMusic = this.Content.Load<Song>("Sounds/JazzJingleBell");
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

            aboutScene = new AboutScene(this, spriteBatch);
            this.Components.Add(aboutScene);

            highScoreScene = new HighScoreScene(this, spriteBatch);
            this.Components.Add(highScoreScene);

            //show only menutScene
            menuScene.show();
            MediaPlayer.Play(menuMusic);
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

            //index 0 = actionScene, 1 = helpScene, 3 = aboutScene, 4 = exit function 
            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();
            
            //This will make each scene visible/invisible and enabled/disabled
            if (menuScene.Enabled)
            {
                selectedIndex = menuScene.Menu.SelectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    //other scenes will be here
                    actionScene = new ActionScene(this, spriteBatch);
                    this.Components.Add(actionScene);
                    MediaPlayer.Stop();
                    actionScene.show();
                    actionScene.StartGame();
                }
                if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    helpScene.show();
                }
                if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    highScoreScene.show();
                }
                if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    aboutScene.show();
                }

                if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    this.Exit();
                }
            }
            //When a user entered one scene, if they press esc button, it will comeback to menu scene
            if (helpScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    hideAllScenes();
                    menuScene.show();
                }
            }
            if (aboutScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    hideAllScenes();
                    menuScene.show();
                }
            }
            if (highScoreScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    hideAllScenes();
                    menuScene.show();
                }
            }
            if (actionScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    actionScene.StopGame();
                    hideAllScenes();
                    menuScene.show();
                    MediaPlayer.Play(menuMusic);
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
            base.Draw(gameTime);
        }
    }
}
