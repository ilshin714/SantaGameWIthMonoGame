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
/// It will display my name, course and short description
/// </summary>
namespace IJFinalProject.GameScenes
{
    /// <summary>
    /// AboutScene class
    /// </summary>
    class AboutScene : GameScene
    {
        SpriteBatch spriteBatch;
        private Texture2D tex;

        /// <summary>
        /// About scene constructor
        /// </summary>
        /// <param name="game">this game</param>
        /// <param name="spriteBatch">this spriteBatch</param>
        public AboutScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            tex = game.Content.Load<Texture2D>("Images/AboutScene");
        }

        /// <summary>
        /// About scene draw method
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
