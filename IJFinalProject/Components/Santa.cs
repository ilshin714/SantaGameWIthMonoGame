/*  Program: IJFinalProject
 *  Purpose: Game making for final project
 *  Revision History: 
 *      Created by Ilshin Ji December 1 2020
 *      Modified by Ilshin Ji December 13 2020
 */
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

/// <summary>
/// IJFinalProject
/// </summary>
namespace IJFinalProject
{
    /// <summary>
    /// Santa is the main actor for this game. 
    /// Santa object will collect presents and continue 
    /// </summary>
    class Santa : DrawableGameComponent
    {
        //Variables
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 dimension;
        private List<Rectangle> frames;
        private int frameIndex = -1;
        private int delay;
        private int delayCounter;
        private SoundEffect santaVoice;

        //Frame division for the santa frame
        private const int ROW = 4;
        private const int COL = 6;

        public Vector2 Position { get => position; set => position = value; }

        private Vector2 speed;
        private Vector2 stage;
        private Vector2 speedHorizontal;
        private Vector2 speedVertical;

        /// <summary>
        /// Santa class constructor
        /// </summary>
        /// <param name="game">this game</param>
        /// <param name="spriteBatch">this spriteBatch</param>
        /// <param name="tex">santa spritesheet for animation</param>
        /// <param name="position">santa initial postion</param>
        /// <param name="delay">it will set the time for changing one frame to another</param>
        /// <param name="speed">santa's moving speed for the game</param>
        /// <param name="stage">the game screen size</param>
        /// <param name="santaVoice">santa's greeting sound effect</param>
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

            //santa tex will be divided by ROW and COL values
            dimension = new Vector2(tex.Width / COL, tex.Height / ROW);

            //moving speed for sana
            speedHorizontal = new Vector2(speed.X, 0);
            speedVertical = new Vector2(0, speed.Y);

            //This will create santa frame
            CreateFrames();

            //When the game starts, santa will greet
            santaVoice.Play();
        }

        /// <summary>
        /// It will create animation effect for Santa
        /// </summary>
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

        /// <summary>
        /// It will draw the santa on the screen
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
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

        /// <summary>
        /// frame will be changed to make animation effect, 
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            //changing frames
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

            //Santa will move up, down, left, and right according to user's keyboard input
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
                if (position.X + tex.Width / COL > stage.X)
                {
                    position.X = stage.X - tex.Width / COL;
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
                if (position.Y + tex.Height / ROW > stage.Y)
                {
                    position.Y = stage.Y - tex.Height / ROW;
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// It will take the santa object's location position to make collision manager calculate
        /// if it intersects with present objects
        /// </summary>
        /// <returns>Location position of santa</returns>
        public Rectangle getBound()
        {
            return new Rectangle((int)position.X, (int)position.Y, (int)dimension.X, (int)dimension.Y);
        }
    }
}
