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
/// It will contain menu component to display the image and menu strings 
/// </summary>
namespace IJFinalProject.GameScenes
{
    /// <summary>
    /// Menu scene class
    /// </summary>
    class MenuScene : GameScene
    {
        //menu variables
        private MenuComponent menu;
        private SpriteBatch spriteBatch;
        private string[] menuItems = { "Start game", "Help", "High Score", "About", "Quit" };
        private Texture2D tex;
        public MenuComponent Menu { get => menu; set => menu = value; }

        /// <summary>
        /// MenuScene constructor
        /// </summary>
        /// <param name="game">this game</param>
        /// <param name="spriteBatch">spriteBatch</param>
        public MenuScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            menu = new MenuComponent(game,
                spriteBatch,
                game.Content.Load<SpriteFont>("Fonts/Regular"),
                game.Content.Load<SpriteFont>("Fonts/HighlightFont"),
                menuItems);
            tex = game.Content.Load<Texture2D>("Images/MenuScene");
            this.Components.Add(menu);
        }

        /// <summary>
        /// MeneScene draw function
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, Vector2.Zero, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
