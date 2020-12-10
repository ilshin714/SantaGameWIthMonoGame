using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IJFinalProject.GameScenes
{
    class MenuScene : GameScene
    {
        private MenuComponent menu;
        private SpriteBatch spriteBatch;
        private string[] menuItems = { "Start game", "Help", "High Score", "About", "Quit" };
        private Texture2D tex;

        public MenuComponent Menu { get => menu; set => menu = value; }

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

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, Vector2.Zero, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
