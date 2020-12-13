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
/// It will display the description of how to play the game
/// </summary>
namespace IJFinalProject.GameScenes
{
    /// <summary>
    /// Help Scene class starts from here
    /// </summary>
    class HelpScene : GameScene
    {
        //Variable Declaration
        SpriteBatch spriteBatch;
        private Texture2D tex;

        /// <summary>
        /// Help Scene constructor
        /// </summary>
        /// <param name="game">this game</param>
        /// <param name="spriteBatch">this game's spriteBatch</param>
        public HelpScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            tex = game.Content.Load<Texture2D>("Images/HelpScene");
        }

        /// <summary>
        /// Help Scene draw method
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
