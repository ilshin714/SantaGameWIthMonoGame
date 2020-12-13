using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJFinalProject
{
    class CandyCane : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private Vector2 position;
        private Vector2 speed;
        private Vector2 stage;
        private SoundEffect hitSound;
        private SoundEffect applause;
        private Rectangle sourceRectangle;
        private Vector2 origin;
        private float rotationChange = 0.03f;
        public float Rotation { get; set; }

        public CandyCane(Game game,
            SpriteBatch spriteBatch,
            Texture2D texture,
            Vector2 position,
            Vector2 speed,
            Vector2 stage
            // later soundEffect
            ) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            this.position = position;
            this.speed = speed;
            this.stage = stage;
            //lateer soundEffect

            sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            origin = new Vector2(texture.Width / 2, texture.Height / 2);

        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            //spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.Draw(texture, position, sourceRectangle, Color.White, Rotation, origin, 0, SpriteEffects.FlipVertically, 0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            Rotation += rotationChange;
            position += speed;
            if (position.Y < 0)
            {
                speed.Y = -speed.Y;
                hitSound.Play();
            }
            else if (position.X < 0)
            {
                speed.X = -speed.X;
                hitSound.Play();
            }
            else if (position.X + texture.Width > stage.X)
            {
                speed.X = -Math.Abs(speed.X);
                hitSound.Play();
            }
            else if (position.Y + texture.Height > stage.Y)
            {
                //speed.Y = -Math.Abs(speed.Y);
                applause.Play();
                this.Enabled = false;
                this.Visible = false;
            }

            base.Update(gameTime);
        }
    }
}
