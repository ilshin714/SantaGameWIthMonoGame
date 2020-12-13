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
/// It is All scene's parent class and will hide/show, enable, and update a scene
/// </summary>
namespace IJFinalProject
{
    /// <summary>
    /// Game scene class start from here
    /// </summary>
    public class GameScene : DrawableGameComponent
    {
        //variables
        private List<GameComponent> components;
        public List<GameComponent> Components { get => components; set => components = value; }

        /// <summary>
        /// Game scene constructor
        /// </summary>
        /// <param name="game">thie game</param>
        public GameScene(Game game) : base(game)
        {
            components = new List<GameComponent>();
            hide();
        }

        /// <summary>
        /// It will enable and make a scene visible on the game screen
        /// </summary>
        public virtual void show()
        {
            this.Enabled = true;
            this.Visible = true;
        }

        /// <summary>
        /// It will disable and make a scene invisible on the game screen 
        /// </summary>
        public virtual void hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }

        /// <summary>
        /// Game scene draw function
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent comp = null;
            foreach (GameComponent item in components)
            {
                if (item is DrawableGameComponent)
                {
                    comp = (DrawableGameComponent)item;
                    if (comp.Visible)
                    {
                        comp.Draw(gameTime);
                    }
                }
            }
            base.Draw(gameTime);
        }

        /// <summary>
        /// It will update the enabled scene
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent item in components)
            {
                if (item.Enabled)
                {
                    item.Update(gameTime);
                }
            }
            base.Update(gameTime);
        }
    }
}
