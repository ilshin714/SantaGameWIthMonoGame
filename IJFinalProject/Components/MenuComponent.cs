using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJFinalProject
{
    class MenuComponent : DrawableGameComponent
    {
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
        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPostion = position;
            spriteBatch.Begin();

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
