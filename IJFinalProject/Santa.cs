using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJFinalProject
{
    class Santa : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 dimension;
        private List<Rectangle> frames;
        private int frameIndex = -1;
        private int delay;
        private int delayCounter;
        private SoundEffect santaVoice;

        private const int ROW = 4;
        private const int COL = 6;

        public Vector2 Position { get => position; set => position = value; }

        private Vector2 speed;
        private Vector2 stage;
        private Vector2 speedHorizontal;
        private Vector2 speedVertical;
        public Santa(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex,
            Vector2 position,
            int delay,
            Vector2 speed,
            Vector2 stage,
            SoundEffect santaVoice) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.delay = delay;
            this.speed = speed;
            this.stage = stage;
            this.santaVoice = santaVoice;

            dimension = new Vector2(tex.Width / COL, tex.Height / ROW);
            speedHorizontal = new Vector2(speed.X, 0);
            speedVertical = new Vector2(0, speed.Y);
            CreateFrames();
            santaVoice.Play();
        }

        private void CreateFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COL; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;

                    Rectangle r = new Rectangle(x, y, (int)dimension.X, (int)dimension.Y);
                    frames.Add(r);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (frameIndex >= 0)
            {
                //Version #4
                spriteBatch.Draw(tex, Position, frames[frameIndex], Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if (delayCounter > delay)
            {
                frameIndex++;
                if (frameIndex > ROW * COL - 1)
                {
                    frameIndex = -1;
                }
                delayCounter = 0;
            }

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                position -= speedHorizontal;
                if (position.X < 0)
                {
                    position.X = 0;
                }
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                position += speedHorizontal;
                if (position.X + tex.Width/COL > stage.X)
                {
                    position.X = stage.X - tex.Width/COL;
                }
            }
            else if (keyboardState.IsKeyDown(Keys.Up))
            {
                position -= speedVertical;
                if (position.Y < 0)
                {
                    position.Y = 0;
                }
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                position += speedVertical;
                if (position.Y + tex.Height/ROW > stage.Y)
                {
                    position.Y = stage.Y - tex.Height/ROW;
                }
            }

            base.Update(gameTime);
        }
    }
}
