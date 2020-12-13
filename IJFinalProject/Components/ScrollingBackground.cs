/*  Program: IJFinalProject
 *  Purpose: Game making for final project
 *  Revision History: 
 *      Created by Ilshin Ji December 1 2020
 *      Modified by Ilshin Ji December 13 2020
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// IJFinalProject
/// </summary>
namespace IJFinalProject
{
    /// <summary>
    /// This class will instantiate moving background 
    /// </summary>
    public class ScrollingBackground : DrawableGameComponent
    {
        //Declaring variables
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        public Texture2D Tex { get => tex; set => tex = value; }
        private Rectangle srcRect;
        private Vector2 position1, position2;
        private Vector2 speed;
        
        /// <summary>
        /// Scrolling background constructor
        /// </summary>
        /// <param name="game">this game</param>
        /// <param name="spriteBatch">this spriteBatch</param>
        /// <param name="tex">background texture will differ according to the level of the game</param>
        /// <param name="position">position will move from right to left</param>
        /// <param name="srcRect">background textrue size</param>
        /// <param name="speed">moving speed</param>
        public ScrollingBackground(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            Rectangle srcRect,
            Vector2 speed) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.srcRect = srcRect;
            this.position1 = position;
            this.position2 = new Vector2(position.X + tex.Width, position.Y);
            this.speed = speed;
        }

        /// <summary>
        /// It will draw two same textrues and will make it repeatedly appear to the game screen
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(tex, position1, srcRect, Color.White);
            spriteBatch.Draw(tex, position2, srcRect, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// the position will be moved according to the speed variable
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            position1 -= speed;
            position2 -= speed;

            if (position1.X < -tex.Width)
            {
                position1.X = position2.X + tex.Width;
            }
            if (position2.X < -tex.Width)
            {
                position2.X = position1.X + tex.Width;
            }
            base.Update(gameTime);
        }
    }
}
