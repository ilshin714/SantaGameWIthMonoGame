/* Program: SantaGame
 * Purpose: Game Programming Final Project
 * 
 * Revision History: 
 *      Created by Ilshi Ji December 2020
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

namespace IJFinalProject
{
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
        

        public Present(Game game,
            SpriteBatch spriteBatch,
            Texture2D presentTexture,
            Vector2 presentPosition,
            Vector2 presentSpeed,
            Vector2 stage,
            Texture2D effect
            //more to come
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
            CreateFrames();
            SetPoint();


        }
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
                    this.point = 50 ;
                    break;
                case "Images/present4":
                    this.point = 40;
                    break;
                case "Images/candyCane1":
                    this.point = 50;
                    break;
                case "Images/cane1":
                    this.point = -30;
                    break;
                case "Images/umbrella1":
                    this.point = -40;
                    break;
                default:
                    this.point = 0;
                    break;

            }
        }

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

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(presentTexture, presentPositionOri, Color.White);
            
            if (frameIndex >= 0)
            {
                //Version #4
                spriteBatch.Draw(effect, presentPositionOri, frames[frameIndex], Color.White);
            }
            

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {

            presentPositionOri -= presentSpeed;
            positionX = (int)presentPositionOri.X;
            positionY = (int)presentPositionOri.Y;


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

        public Rectangle getBound()
        {
            return new Rectangle(positionX, positionY, presentTexture.Width, presentTexture.Height);
        }
    }
}
