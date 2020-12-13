/*  Program: IJFinalProject
 *  Purpose: Game making for final project
 *  Revision History: 
 *      Created by Ilshin Ji December 1 2020
 *      Modified by Ilshin Ji December 13 2020
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// It will share the stage witdth, height, and high score of this game
/// </summary>
namespace IJFinalProject
{
    /// <summary>
    /// Static shared variable class
    /// </summary>
    public class Shared
    {
        public static Vector2 stage;
        public static int highScore;
    }
}
