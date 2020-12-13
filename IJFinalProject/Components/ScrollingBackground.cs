using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJFinalProject
{
    public class ScrollingBackground : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        public Texture2D Tex { get => tex; set => tex = value; }
        private Rectangle srcRect;
        private Vector2 position1, position2;
        private Vector2 speed;
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

        

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(tex, position1, srcRect, Color.White);
            spriteBatch.Draw(tex, position2, srcRect, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            position1 -= speed;
            position2 -= speed;

            if (position1.X < -tex.Width)
            {
                position1.X = position2.X + tex.Width;
            }
            if(position2.X < -tex.Width)
            {
                position2.X = position1.X + tex.Width;
            }
            base.Update(gameTime);
        }
    }
}
