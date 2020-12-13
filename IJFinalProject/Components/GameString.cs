
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IJFinalProject.GameScenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IJFinalProject
{
    public class GameString : DrawableGameComponent
    {
        Game game;
        protected SpriteBatch spriteBatch;

        private SpriteFont font;
        public SpriteFont Font { get => font; set => font = value; }

        private string message;
        public string Message { get => message; set => message = value; }

        private Vector2 position;
        public Vector2 Postion { get => position; set => position = value; }

        private Color color;

        private int delay = 200;
        private int delayCounter;

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

        public Color Color { get => color; set => color = value; }

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

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(font, message, position, color);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
