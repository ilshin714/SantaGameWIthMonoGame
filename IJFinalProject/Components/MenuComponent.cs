/*  Program: IJFinalProject
 *  Purpose: Game making for final project
 *  Revision History: 
 *      Created by Ilshin Ji December 1 2020
 *      Modified by Ilshin Ji December 13 2020
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
    /// Menu Component class
    /// </summary>
    class MenuComponent : DrawableGameComponent
    {
        //Variables
        Game game;
        private SpriteBatch spriteBatch;
        private SpriteFont regularFont, highlightFont;
        private List<string> menuItems;
        private int selectedIndex = 0;
        public int SelectedIndex { get => selectedIndex; set => selectedIndex = value; }

        private Vector2 position;
        private Color regularColor = Color.Black;
        private Color highlightColor = Color.Red;

        KeyboardState oldState;

        /// <summary>
        /// It will define menucomponents' characteristics and draw them on the menu scene
        /// </summary>
        /// <param name="game">this game</param>
        /// <param name="spriteBatch">this spriteBatch</param>
        /// <param name="regularFont">font for regular status</param>
        /// <param name="highlightFont">font for highlitened status</param>
        /// <param name="menus">it has strings to display</param>
        public MenuComponent(Game game,
            SpriteBatch spriteBatch,
            SpriteFont regularFont,
            SpriteFont highlightFont,
            string[] menus) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.regularFont = regularFont;
            this.highlightFont = highlightFont;
            menuItems = menus.ToList<string>();
            position = new Vector2(Shared.stage.X / 3, Shared.stage.Y / 3);

        }

        /// <summary>
        /// It will draw menu component
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPostion = position;
            spriteBatch.Begin();

            //It wil loop through the menu array and display all
            //and it will get the selectedIndex and change the menu component to be highlighted
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (selectedIndex == i)
                {
                    spriteBatch.DrawString(highlightFont, menuItems[i], tempPostion, highlightColor);
                    tempPostion.Y += highlightFont.LineSpacing;//height of the highlightFont with some spaces
                }
                else
                {
                    spriteBatch.DrawString(regularFont, menuItems[i], tempPostion, regularColor);
                    tempPostion.Y += regularFont.LineSpacing;// height of the regularFont with some spaces
                }

            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// According to the keyboard up and down event, it will change the selected index
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                selectedIndex++;
                if (selectedIndex == menuItems.Count)
                {
                    selectedIndex = 0;
                }
            }

            if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex == -1)
                {
                    selectedIndex = menuItems.Count - 1;
                }
            }

            oldState = ks;

            base.Update(gameTime);
        }
    }
}
