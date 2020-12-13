using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IJFinalProject
{
    class Score : DrawableGameComponent
    {
        protected SpriteBatch spriteBatch;

        private SpriteFont font;
        public SpriteFont Font { get => font; set => font = value; }

        private int point;
        public int Point { get => point; set => point = value; }

        private Vector2 position;
        public Vector2 Postion { get => position; set => position = value; }

        private Color color;
        public Color Color { get => color; set => color = value; }
        

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


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(font, point.ToString(), position, color);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
