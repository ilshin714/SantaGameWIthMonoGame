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
        private Vector2 presentPositionOri;
        private Vector2 presentSpeed;
        private SoundEffect gettingSound;
        private Vector2 stage;
        private int positionX;
        private int positionY;
        private int delayCounter;
        private int delay = 300;
        private Game game;

        public Present(Game game,
            SpriteBatch spriteBatch,
            Texture2D presentTexture,
            Vector2 presentPosition,
            Vector2 presentSpeed,
            Vector2 stage
            //SoundEffect gettingSound
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
            //this.gettingSound = gettingSound;

        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(presentTexture, presentPositionOri, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            //delayCounter++;
            //if (delayCounter > delay)
            //{
               
            //    Random random = new Random();

            //    positionY = random.Next(0, positionY + presentTexture.Height);
            //    Vector2 presentPosition = new Vector2(positionX, positionY);
            //    Present present = new Present(game, spriteBatch, presentTexture, presentPosition, new Vector2((int)presentSpeed.X, 0), stage);
            //    game.Components.Add(present);
            //    delayCounter = 0;
            //}


            presentPositionOri -= presentSpeed;
            //if ((int)presentPositionOri.X <0)
            //{
            //    presentPositionOri = new Vector2(-200, 0);
            //}
            base.Update(gameTime);
        }
    }
}
