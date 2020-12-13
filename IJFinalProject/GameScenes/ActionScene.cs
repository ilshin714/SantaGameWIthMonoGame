/*  Program: IJFinalProject
 *  Purpose: Game making for final project
 *  Revision History: 
 *      Created by Ilshin Ji December 1 2020
 *      Modified by Ilshin Ji December 13 2020
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// It plays the main game of this project contains object classes
/// </summary>
namespace IJFinalProject.GameScenes
{
    /// <summary>
    /// This is action scene for the game.
    /// </summary>
    public class ActionScene : GameScene
    {
        //Global Variables 
        private Game game;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        
        //santa 
        private Santa santa;
        
        //about object variable
        //object's random position when it is created on the screen
        private Random randomPosition = new Random();        
        private int delayCounter = 0;
        private int delay = 300;
        private Present present;       
        private int positionX;
        private int positionY;
        private Vector2 presentPosition;
        private int presentSpeed = 5;
        private Texture2D effect;
        private SoundEffect presentGettingSound;
        private SoundEffect faultyObjectSound;
        private List<Texture2D> presentList = new List<Texture2D>();
        private int presentIndex;

        //about level variables
        Texture2D level1;
        Texture2D level2;
        public bool isLevelChanged;
        bool isMusicChanged;
        //About background varibles
        ScrollingBackground scrollingBackground;
        ScrollingBackground houses;

        //Global font for GameString
        SpriteFont status;

        //Shared stage 
        Vector2 stage;
        
        //About score varibles
        private Score score;
        public int newScore;

        /// <summary>
        /// ActionScene constructor
        /// </summary>
        /// <param name="game">this game</param>
        /// <param name="spriteBatch">this spriteBatch</param>
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
            //default is false of two boolean variables
            isLevelChanged = false;
            isMusicChanged = false;

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Background loading
            level1 = game.Content.Load<Texture2D>("Images/BG_02");
            level2 = game.Content.Load<Texture2D>("Images/BG_03");
            Texture2D village = game.Content.Load<Texture2D>("Images/houses3");
            stage = Shared.stage;
            Rectangle srcRec = new Rectangle(0, 0, level1.Width, level1.Height);
            Vector2 pos = new Vector2(0, 0);
            Vector2 speed = new Vector2(2, 0);
            scrollingBackground = new ScrollingBackground(game, spriteBatch, level1, pos, srcRec, speed);
            houses = new ScrollingBackground(game, spriteBatch, village, pos, srcRec, speed);
            this.Components.Add(scrollingBackground);
            this.Components.Add(houses);

            //Background music loading
            Song level1Music = game.Content.Load<Song>("Sounds/Jingle-Bell-Rock-Bobby-Helms");
            Song level2Music = game.Content.Load<Song>("Sounds/Rudolph-The-Red-Nosed-Reindeer-Gene-Autry");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(level1Music);

            //Santa object loading
            Texture2D santaTexture = game.Content.Load<Texture2D>("Images/santaBig");
            Texture2D santaTextureBig = game.Content.Load<Texture2D>("Images/santaBig2");
            SoundEffect santaVoice = game.Content.Load<SoundEffect>("Sounds/SantaVoice");
            Vector2 santaInitialPosition = new Vector2(0, stage.Y / 2);
            Vector2 santaSpeed = new Vector2(4, 4);
            int santaDelay = 3;
            santa = new Santa(game, spriteBatch, santaTexture, santaInitialPosition, santaDelay, santaSpeed, stage, santaVoice);
            this.Components.Add(santa);

            //Present loading 
            Texture2D present0 = game.Content.Load<Texture2D>("Images/present2");
            Texture2D present1 = game.Content.Load<Texture2D>("Images/present1");
            Texture2D present2 = game.Content.Load<Texture2D>("Images/present3");
            Texture2D present3 = game.Content.Load<Texture2D>("Images/present4");
            Texture2D candyCane = game.Content.Load<Texture2D>("Images/candyCane1");
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

            //Score loading
            SpriteFont scoreFont = game.Content.Load<SpriteFont>("Fonts/HighlightFont");
            score = new Score(game, spriteBatch, scoreFont, new Vector2(stage.X - 200, 50), 0, Color.RosyBrown);
            this.Components.Add(score);
            GameString point = new GameString(game, spriteBatch, scoreFont, "Score Here: ", new Vector2(stage.X - 500, 50), Color.OrangeRed);
            this.Components.Add(point);
            
            //Game string status
            status = game.Content.Load<SpriteFont>("Fonts/Status");
            GameString start = new GameString(game, spriteBatch, status, "LEVEL 1 START", new Vector2(stage.X / 5, stage.Y / 3), Color.OrangeRed);
            this.Components.Add(start);

        }

        /// <summary>
        /// When user push esc button, it will stop the game
        /// </summary>
        public void StopGame()
        {
            MediaPlayer.Stop();
            Shared.highScore = score.Point;
        }

        /// <summary>
        /// After levelup, it will add one more positive and two more negative object textures in to the list
        /// </summary>
        public void AddObject()
        {
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
            //Present object will be shon every 5 second
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

                //CollisionManager will be added to the newly made present object 
                CollisionManager collision = new CollisionManager(game, santa, present, presentGettingSound, faultyObjectSound, score);
                this.Components.Add(collision);

                //it will update score point
                newScore = score.Point;
            }
            //if the score is over 300, it will level up
            if (newScore > 300)
            {
                scrollingBackground.Tex = level2;
                isLevelChanged = true;
                
                //It will update music and show the levelup GameString
                if (isLevelChanged && isMusicChanged == false)
                {
                    Song level2Music = game.Content.Load<Song>("Sounds/Rudolph-The-Red-Nosed-Reindeer-Gene-Autry");
                    MediaPlayer.Play(level2Music);
                    GameString levelUp = new GameString(game, spriteBatch, status, "LEVEL 2 START", new Vector2(stage.X / 5, stage.Y / 3), Color.OrangeRed);
                    this.Components.Add(levelUp);
                    isMusicChanged = true;
                }

                //Change the object speed twice faster
                if (presentSpeed == 5)
                {
                    presentSpeed = presentSpeed * 2;
                }

                //Add three more objects
                if (presentList.Count != 7)
                {
                    AddObject();
                }

                //Objects will appear more often
                if (delay == 300)
                {
                    delay = delay / 2;
                }
            }
            // If new score is less than 0, game will be over
            else if (newScore < 0)
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
            base.Draw(gameTime);
        }

        /// <summary>
        /// Initialize game
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }
    }
}
