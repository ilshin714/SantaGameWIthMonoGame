using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

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
        private int presentSpeed = 5;
        Texture2D level1;
        Texture2D level2;

        SpriteFont status;

        private Texture2D effect;
        private SoundEffect presentGettingSound;
        private SoundEffect faultyObjectSound;
        Vector2 stage;
        private List<Texture2D> presentList = new List<Texture2D>();
        private int presentIndex;

        private Score score;
        public int newScore;
        ScrollingBackground scrollingBackground;
        ScrollingBackground houses;
        public bool isLevelChanged;
        bool isMusicChanged;

        public ActionScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.game = game;

            game.IsMouseVisible = false;
            game.Content.RootDirectory = "Content";
            this.spriteBatch = spriteBatch;

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void StartGame()
        {
           
            isLevelChanged = false;
            isMusicChanged = false;
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //Background
            level1 = game.Content.Load<Texture2D>("Images/BG_02");
            level2 = game.Content.Load<Texture2D>("Images/BG_03");

            Texture2D village = game.Content.Load<Texture2D>("Images/houses3");
            stage = Shared.stage;
            Rectangle srcRec = new Rectangle(0, 0, level1.Width, level1.Height);
            Vector2 pos = new Vector2(0, 0);
            Vector2 speed = new Vector2(2, 0);

            Song level1Music = game.Content.Load<Song>("Sounds/Jingle-Bell-Rock-Bobby-Helms");
            Song level2Music = game.Content.Load<Song>("Sounds/Rudolph-The-Red-Nosed-Reindeer-Gene-Autry");
            MediaPlayer.IsRepeating = true;
            scrollingBackground = new ScrollingBackground(game, spriteBatch, level1, pos, srcRec, speed);
            houses = new ScrollingBackground(game, spriteBatch, village, pos, srcRec, speed);
            this.Components.Add(scrollingBackground);
            this.Components.Add(houses);

            //Santa 
            Texture2D santaTexture = game.Content.Load<Texture2D>("Images/santaBig");
            Texture2D santaTextureBig = game.Content.Load<Texture2D>("Images/santaBig2");
            SoundEffect santaVoice = game.Content.Load<SoundEffect>("Sounds/SantaVoice");
            Vector2 santaInitialPosition = new Vector2(0, stage.Y / 2);
            Vector2 santaSpeed = new Vector2(4, 4);
            int santaDelay = 3;
            santa = new Santa(game, spriteBatch, santaTexture, santaInitialPosition, santaDelay, santaSpeed, stage, santaVoice);
            this.Components.Add(santa);
            MediaPlayer.Play(level1Music);

            //Present 
            Texture2D present0 = game.Content.Load<Texture2D>("Images/present2");
            Texture2D present1 = game.Content.Load<Texture2D>("Images/present1");
            Texture2D present2 = game.Content.Load<Texture2D>("Images/present3");
            Texture2D present3 = game.Content.Load<Texture2D>("Images/present4");
            effect = game.Content.Load<Texture2D>("Images/effect");

            presentList.Add(present0);
            presentList.Add(present1);
            presentList.Add(present2);
            presentList.Add(present3);

            presentGettingSound = game.Content.Load<SoundEffect>("Sounds/present_pick");
            faultyObjectSound = game.Content.Load<SoundEffect>("Sounds/gettingSound");
            positionX = (int)Shared.stage.X;
            Random random = new Random();

            positionY = random.Next(0, random.Next(0, (int)Shared.stage.Y - present0.Height));
            presentPosition = new Vector2(positionX, positionY);

            //Score
            SpriteFont scoreFont = game.Content.Load<SpriteFont>("Fonts/HighlightFont");
            score = new Score(game, spriteBatch, scoreFont, new Vector2(stage.X - 200, 50), 0, Color.RosyBrown);
            this.Components.Add(score);
            GameString point = new GameString(game, spriteBatch, scoreFont, "Score Here: ", new Vector2(stage.X-500, 50), Color.OrangeRed);
            this.Components.Add(point);
            //status
            status = game.Content.Load<SpriteFont>("Fonts/Status");
            GameString start = new GameString(game, spriteBatch, status, "LEVEL 1 START", new Vector2(stage.X /5, stage.Y / 3), Color.OrangeRed);

            this.Components.Add(start);

        }

        public void StopGame()
        {
          
            MediaPlayer.Stop();
            Shared.highScore = score.Point;

        }

        public void AddObject()
        {
            //Texture2D[] presents2 = new Texture2D[7];
            Texture2D candyCane = game.Content.Load<Texture2D>("Images/candyCane1");
            Texture2D trap1 = game.Content.Load<Texture2D>("Images/cane1");
            Texture2D trap2 = game.Content.Load<Texture2D>("Images/umbrella1");
            presentList.Add(candyCane);
            presentList.Add(trap1);
            presentList.Add(trap2);

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {

            // TODO: Add your update logic here

            delayCounter++;
            if (delayCounter > delay)
            {
                if (presentIndex > presentList.Count - 1)
                {
                    presentIndex = 0;
                }
                positionX = (int)Shared.stage.X;
                Random random = new Random();

                positionY = random.Next(0, (int)Shared.stage.Y - presentList.ElementAt(presentIndex).Height);
                presentPosition = new Vector2(positionX, positionY);
                present = new Present(game, spriteBatch, presentList.ElementAt(presentIndex), presentPosition, new Vector2(presentSpeed, 0), stage, effect);
                this.Components.Add(present);
                delayCounter = 0;
                presentIndex++;

                //CollisionManager 
                CollisionManager collision = new CollisionManager(game, santa, present,presentGettingSound,faultyObjectSound, score, this, scrollingBackground, houses);
                this.Components.Add(collision);
                newScore = score.Point;
            }

            if (newScore > 300)
            {
                
                Texture2D level2 = game.Content.Load<Texture2D>("Images/BG_03");
                scrollingBackground.Tex = level2;
                isLevelChanged = true;
                if (isLevelChanged && isMusicChanged == false)
                {
                    Song level2Music = game.Content.Load<Song>("Sounds/Rudolph-The-Red-Nosed-Reindeer-Gene-Autry");
                    MediaPlayer.Play(level2Music);
                    GameString levelUp = new GameString(game, spriteBatch, status, "LEVEL 2 START", new Vector2(stage.X / 5, stage.Y / 3), Color.OrangeRed);
                    this.Components.Add(levelUp);
                    isMusicChanged = true;
                }
                if(presentSpeed == 5)
                {
                    presentSpeed = presentSpeed * 2;
                }
                if (presentList.Count != 7)
                {
                    AddObject();
                }
                if(delay == 300)
                {
                    delay = delay / 2;
                }

            }
            else if(newScore < 0)
            {
                MediaPlayer.Stop();
                santa.Enabled = false;
                presentSpeed = 0;
                
                GameString gameOver = new GameString(game, spriteBatch, status, "Game Over \nPlease press ESC", new Vector2(stage.X / 5, stage.Y / 3), Color.OrangeRed);
                this.Components.Add(gameOver);
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

        public override void Initialize()
        {
            base.Initialize();
        }
    }
}
