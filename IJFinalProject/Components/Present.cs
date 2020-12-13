/*  Program: IJFinalProject
 *  Purpose: Game making for final project
 *  Revision History: 
 *      Created by Ilshin Ji December 1 2020
 *      Modified by Ilshin Ji December 13 2020
 */

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using IJFinalProject.GameScenes;

/// <summary>
/// IJFIanl Project
/// </summary>
namespace IJFinalProject
{
    /// <summary>
    /// Present class is the main class for getting score
    /// </summary>
    class Present : DrawableGameComponent
    {
        //Variable Declaration 
        private SpriteBatch spriteBatch;
        private Texture2D presentTexture;
        public Texture2D PresentTexture { get => presentTexture; set => presentTexture = value; }
        private Vector2 presentPositionOri;
        private Vector2 presentSpeed;
        private SoundEffect gettingSound;
        private Vector2 stage;
        private int positionX;
        private int positionY;
        private int delayCounter;
        private int delay = 3;
        private Game game;
        private int point;
        public int Point { get => point; set => point = value; }

        private Texture2D effect;
        private Vector2 dimension;
        private List<Rectangle> frames;
        private int frameIndex = -1;
        private const int ROW = 4;
        private const int COL = 6;

        public Vector2 PresentSpeed
        {
            get => presentSpeed;
            set => presentSpeed = value;
        }

        /// <summary>
        /// Present Construction
        /// </summary>
        /// <param name="game">this game</param>
        /// <param name="spriteBatch">this spriteBatch</param>
        /// <param name="presentTexture">It diterminds a present's shape</param>
        /// <param name="presentPosition">This position is randomly created from the actionScene</param>
        /// <param name="presentSpeed">Default speed is 5, level 2 will be the twice of the speed</param>
        /// <param name="stage">the game creen size</param>
        /// <param name="effect">this will be realized next step</param>
        public Present(Game game,
            SpriteBatch spriteBatch,
            Texture2D presentTexture,
            Vector2 presentPosition,
            Vector2 presentSpeed,
            Vector2 stage,
            Texture2D effect
            ) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.presentTexture = presentTexture;
            this.presentPositionOri = presentPosition;
            this.presentSpeed = presentSpeed;
            positionX = (int)stage.X;
            positionY = (int)stage.Y;
            this.effect = effect;

            dimension = new Vector2(effect.Width / COL, effect.Height / ROW);
            //I was going to make the effect for the present, but it will be realized next version
            CreateFrames();
            //This will set point according to each present texture
            SetPoint();
        }

        /// <summary>
        /// The point of this present will vary according to its texture
        /// </summary>
        private void SetPoint()
        {
            switch (presentTexture.Name)
            {
                case "Images/present1":
                    this.point = 30;
                    break;
                case "Images/present2":
                    this.point = 40;
                    break;
                case "Images/present3":
                    this.point = 50;
                    break;
                case "Images/present4":
                    this.point = 40;
                    break;
                case "Images/candyCane1":
                    this.point = 50;
                    break;
                case "Images/cane1":
                    this.point = -50;
                    break;
                case "Images/umbrella1":
                    this.point = -60;
                    break;
                default:
                    this.point = 0;
                    break;
            }
        }

        /// <summary>
        /// This will be realized for the next version
        /// </summary>
        private void CreateFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COL; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;

                    Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                    frames.Add(r);
                }
            }
        }

        /// <summary>
        /// Draw function for present 
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(presentTexture, presentPositionOri, Color.White);
            //This will be realized for the next version
            if (frameIndex >= 0)
            {
                //Version #4
                spriteBatch.Draw(effect, presentPositionOri, frames[frameIndex], Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// It will update the position of the moving present
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {

            presentPositionOri -= presentSpeed;
            positionX = (int)presentPositionOri.X;
            positionY = (int)presentPositionOri.Y;

            //This will be realized for the next version
            if (positionY >= Shared.stage.Y - presentTexture.Height)
            {
                delayCounter++;
                if (delayCounter > delay)
                {
                    frameIndex++;
                    if (frameIndex > ROW * COL - 1)
                    {
                        frameIndex = -1;
                    }
                    delayCounter = 0;
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// It will take the object's size so that collision manager can calculate if the santa and 
        /// this object intersect or not 
        /// </summary>
        /// <returns>a rectangle position of the object</returns>
        public Rectangle getBound()
        {
            return new Rectangle(positionX, positionY, presentTexture.Width, presentTexture.Height);
        }
    }
}
