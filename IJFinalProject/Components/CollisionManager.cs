/*  Program: IJFinalProject
 *  Purpose: Game making for final project
 *  Revision History: 
 *      Created by Ilshin Ji December 1 2020
 *      Modified by Ilshin Ji December 13 2020
 */
using IJFinalProject.GameScenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// CollisionManager will be generated after a present object is generated
/// </summary>
namespace IJFinalProject
{
    /// <summary>
    /// Collision Manager class
    /// </summary>
    internal class CollisionManager : GameComponent
    {
        //Variables 
        private Game game;
        private Santa santa;
        private Present present;
        private SoundEffect presentSound;
        private SoundEffect faultySound;
        private int delayCounter = 0;
        private int delay = 600;
        private bool isItGift;
        private bool gotPresent;
        public bool GotPresent { get => gotPresent; set => gotPresent = value; }
        Score score;

        /// <summary>
        /// Collision manager constructor
        /// </summary>
        /// <param name="game">this game</param>
        /// <param name="santa">santa object of this game</param>
        /// <param name="present">newly created present object</param>
        /// <param name="presentgettingSound">positive sound for a good present</param>
        /// <param name="faultySound">negative sound for a bad present</param>
        /// <param name="score">game score</param>
        public CollisionManager(Game game,
            Santa santa,
            Present present,
            SoundEffect presentgettingSound,
            SoundEffect faultySound,
            Score score) : base(game)
        {
            this.game = game;
            this.santa = santa;
            this.present = present;
            this.presentSound = presentgettingSound;
            this.faultySound = faultySound;
            this.score = score;
            //Present objects has two types, one is good type the other is fualty type
            //It will check the type and store boolean variable
            isItGift = IsItGift();
        }

        /// <summary>
        /// It will check the passed present name property. 
        /// If the property is present, it will return true, otherwise return false  
        /// </summary>
        /// <returns>true for a present, false for a faulty object</returns>
        public bool IsItGift()
        {
            switch (present.PresentTexture.Name)
            {
                case "Images/present1":
                case "Images/present2":
                case "Images/present3":
                case "Images/present4":
                case "Images/candyCane1":
                    isItGift = true;
                    break;
                case "Images/cane1":
                case "Images/umbrella1":
                    isItGift = false;
                    break;
                default:
                    isItGift = false;
                    break;
            }
            return isItGift;
        }

        /// <summary>
        /// It will check if the present object is intersected with the santa object in the game, 
        /// and change its direction to downward and update the score.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            Rectangle santaRect = santa.getBound();
            Rectangle presentRect = present.getBound();
            if (santaRect.Intersects(presentRect))
            {
                //the sound will be played only onetime
                //it will play different sound effect according to the present's characteristics
                delayCounter++;
                if (delayCounter == 1)
                {
                    if (isItGift)
                    {
                        presentSound.Play();
                    }
                    else
                    {
                        faultySound.Play();
                    }
                }

                //It will change the direction
                present.PresentSpeed = new Vector2(present.PresentSpeed.X, -present.PresentSpeed.X * 5);
                
                //It updates the score point. A good present will increase the point and a bad present will decrease it 
                score.Point += present.Point;

                this.Enabled = false;
            }
            //If Santa misses a good present, it will reduce point by 50
            else if (presentRect.Left < 0 && isItGift)
            {
                faultySound.Play();
                score.Point -= 50;
                this.Enabled = false;
            }
            base.Update(gameTime);
        }
    }
}
