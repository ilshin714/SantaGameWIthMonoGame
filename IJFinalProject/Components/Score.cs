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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/// <summary>
/// IJFinalProject
/// </summary>
namespace IJFinalProject
{
    /// <summary>
    /// Score class
    /// </summary>
    class Score : DrawableGameComponent
    {
        //Variables
        protected SpriteBatch spriteBatch;

        private SpriteFont font;
        public SpriteFont Font { get => font; set => font = value; }

        private int point;
        public int Point { get => point; set => point = value; }

        private Vector2 position;
        public Vector2 Postion { get => position; set => position = value; }

        private Color color;
        public Color Color { get => color; set => color = value; }

        /// <summary>
        /// Score constructor
        /// </summary>
        /// <param name="game">this game</param>
        /// <param name="spriteBatch">this spriteBatch</param>
        /// <param name="font">font texture to display score on the game screen</param>
        /// <param name="position">it will decide the location of the score</param>
        /// <param name="point">updated point to be displayed on the game screen</param>
        /// <param name="color">font color of the score</param>
        public Score(Game game,
            SpriteBatch spriteBatch,
            SpriteFont font,
            Vector2 position,
            int point,
            Color color
            ) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.font = font;
            this.position = position;
            this.point = point;
            this.color = color;
        }

        /// <summary>
        /// It will draw the score on the game screen
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, point.ToString(), position, color);
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
