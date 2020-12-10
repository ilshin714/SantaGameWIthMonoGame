using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace IJFinalProject.GameScenes
{   
    /// <summary>
    /// This is action scene for the game.
    /// </summary>
    public class ActionScene : GameScene
    {
        private Game game;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Santa santa;
        private Random randomPosition = new Random();
        private int delayCounter = 0;
        private int delay = 300;
        private Present present;
        private int positionX;
        private int positionY;
        private Vector2 presentPosition;
        private int presentSpeed = 4;
        private Texture2D presentTexture;
        private Song gettingSound;
        Vector2 stage;
        
        public ActionScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.game = game;
            
            game.IsMouseVisible = true;
            game.Content.RootDirectory = "Content";
            this.spriteBatch = spriteBatch;
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
            //Background
            Texture2D level1 = game.Content.Load<Texture2D>("Images/BG_02");
            Texture2D level2 = game.Content.Load<Texture2D>("Images/BG_03");
            Texture2D startImage = game.Content.Load<Texture2D>("Images/startImage");
            Texture2D village = game.Content.Load<Texture2D>("Images/houses3");
            stage = Shared.stage;
            Rectangle srcRec = new Rectangle(0, 0, level1.Width, level1.Height);
            Vector2 pos = new Vector2(0, 0);
            Vector2 speed = new Vector2(2, 0);

            //SoundEffect menuMusic = this.Content.Load<SoundEffect>("Sounds/JazzJingleBell");
            //SoundEffect level1Music = this.Content.Load<SoundEffect>("Sounds/Jingle-Bell-Rock-Bobby-Helms");

            Song menuMusic = game.Content.Load<Song>("Sounds/JazzJingleBell");
            Song level1Music = game.Content.Load<Song>("Sounds/Jingle-Bell-Rock-Bobby-Helms");
            Song level2Music = game.Content.Load<Song>("Sounds/Rudolph-The-Red-Nosed-Reindeer-Gene-Autry");
            MediaPlayer.IsRepeating = true;
            ScrollingBackground scrollingBackground = new ScrollingBackground(game, spriteBatch, level2, pos, srcRec, speed);
            ScrollingBackground houses = new ScrollingBackground(game, spriteBatch, village, pos, srcRec, speed);
            this.Components.Add(scrollingBackground);
            this.Components.Add(houses);

            //Santa 
            //Texture2D santaTexture = Content.Load<Texture2D>("Images/modify_Santa_with_sledgh");
            Texture2D santaTextureBig = game.Content.Load<Texture2D>("Images/santaBig2");
            SoundEffect santaVoice = game.Content.Load<SoundEffect>("Sounds/SantaVoice");
            Vector2 santaInitialPosition = new Vector2(0, stage.Y / 2);
            Vector2 santaSpeed = new Vector2(4, 4);
            int santaDelay = 3;
            santa = new Santa(game, spriteBatch, santaTextureBig, santaInitialPosition, santaDelay, santaSpeed, stage, santaVoice);
            this.Components.Add(santa);
            MediaPlayer.Play(level1Music);

            //CandyCane 
            Texture2D candyCaneTexture = game.Content.Load<Texture2D>("Images/candyCane1");
            Vector2 candyCanePosition = new Vector2(stage.X, randomPosition.Next((int)stage.Y));

            //Present 
            presentTexture = game.Content.Load<Texture2D>("Images/present2");
            Texture2D present2 = game.Content.Load<Texture2D>("Images/present3");
            Texture2D present3 = game.Content.Load<Texture2D>("Images/present4");
            gettingSound = game.Content.Load<Song>("Sounds/zapsplat_foley_present_gift_wrapped_pick_up_grab_001_42924");
            positionX = graphics.PreferredBackBufferWidth;
            Random random = new Random();

            positionY = random.Next(0, graphics.PreferredBackBufferHeight - presentTexture.Height);
            presentPosition = new Vector2(positionX, positionY);

            present = new Present(game, spriteBatch, presentTexture, presentPosition, new Vector2(presentSpeed, 0), stage);
            this.Components.Add(present);
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
        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                game.Exit();

            // TODO: Add your update logic here
            delayCounter++;
            if (delayCounter > delay)
            {
                positionX = (int)Shared.stage.X;
                Random random = new Random();

                positionY = random.Next(0, (int)Shared.stage.Y - presentTexture.Height);
                presentPosition = new Vector2(positionX, positionY);
                present = new Present(game, spriteBatch, presentTexture, presentPosition, new Vector2(presentSpeed, 0), stage);
                this.Components.Add(present);
                delayCounter = 0;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

    }
}
