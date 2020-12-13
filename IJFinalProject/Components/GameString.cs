/*  Program: IJFinalProject
 *  Purpose: Game making for final project
 *  Revision History: 
 *      Created by Ilshin Ji December 1 2020
 *      Modified by Ilshin Ji December 13 2020
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IJFinalProject.GameScenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// GameString will be appear to inform users
/// </summary>
namespace IJFinalProject
{
    /// <summary>
    /// GameString class
    /// </summary>
    public class GameString : DrawableGameComponent
    {
        //Variables
        Game game;
        protected SpriteBatch spriteBatch;

        private SpriteFont font;
        public SpriteFont Font { get => font; set => font = value; }

        private string message;
        public string Message { get => message; set => message = value; }

        private Vector2 position;
        public Vector2 Postion { get => position; set => position = value; }

        private Color color;
        public Color Color { get => color; set => color = value; }

        //It will disappear after a short amount of time
        private int delay = 200;
        private int delayCounter;

        /// <summary>
        /// GameString constructor
        /// </summary>
        /// <param name="game">this game</param>
        /// <param name="spriteBatch">this spriteBatch</param>
        /// <param name="font">font for the game string</param>
        /// <param name="message">string message for this game string</param>
        /// <param name="position">it set the position to appear in the game screen</param>
        /// <param name="color">font color</param>
        public GameString(Game game,
            SpriteBatch spriteBatch,
            SpriteFont font,
            string message,
            Vector2 position,
            Color color) : base(game)
        {
            game = this.game;
            this.spriteBatch = spriteBatch;
            Font = font;
            Message = message;
            Message = message;
            this.position = position;
            Color = color;

        }

        /// <summary>
        /// Game string will disappear in short amount of time
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if (delayCounter > delay)
            {
                this.Dispose();
                this.Visible = false;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Game string draw method
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, message, position, color);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
