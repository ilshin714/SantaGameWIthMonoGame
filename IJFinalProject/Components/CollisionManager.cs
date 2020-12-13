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

namespace IJFinalProject
{
    internal class CollisionManager : GameComponent
    {
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
        ActionScene actionScene;
        ScrollingBackground background;
        ScrollingBackground village;

        public CollisionManager(Game game,
            Santa santa,
            Present present,
            SoundEffect presentgettingSound,
            SoundEffect faultySound,
            Score score,
            ActionScene actionScene,
            ScrollingBackground background,
            ScrollingBackground village) : base(game)
        {
            this.game = game;
            this.santa = santa;
            this.present = present;
            this.presentSound = presentgettingSound;
            this.faultySound = faultySound;
            this.score = score;
            this.actionScene = actionScene;
            this.background = background;
            this.village = village;
            isItGift = IsItGift();
        }
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

        public override void Update(GameTime gameTime)
        {
            Rectangle santaRect = santa.getBound();
            Rectangle presentRect = present.getBound();
            if (santaRect.Intersects(presentRect))
            {
                delayCounter++;
                if (delayCounter == 1)
                {
                    switch (present.PresentTexture.Name)
                    {

                        case "Images/present1":
                        case "Images/present2":
                        case "Images/present3":
                        case "Images/present4":
                        case "Images/candyCane1":
                            presentSound.Play();
                            isItGift = true;
                            break;
                        case "Images/cane1":
                        case "Images/umbrella1":
                            faultySound.Play();
                            isItGift = false;
                            break;
                        default:
                            break;

                    }
                    
                }

                present.PresentSpeed = new Vector2(present.PresentSpeed.X, -present.PresentSpeed.X * 5);
                gotPresent = true;
                if (gotPresent)
                {
                   score.Point += present.Point;
                }
                this.Enabled = false;
            }
            else if(presentRect.Left < 0 && isItGift)
            {
                faultySound.Play();
                score.Point -= 50;
                this.Enabled = false;
            }
            base.Update(gameTime);
        }
    }
}
