﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong_Game
{
    class Powerup
    {
        public int x { get; set; }
        public int y { get; set; }

        private Texture2D myImage;
        private Game game;

        public Powerup(Game game, Texture2D image)
        {
            myImage = image;
            this.game = game;

            x = -100;
            y = -100;
        }

        // Moves the object off the screen when it is hit 
        public void CollidedWith()
        {
            x = -100;
            y = -100;
        }

        // Creates a rectangle in the powerup location for collisions
        public Rectangle CreateRectangle()
        {
            return new Rectangle((int)(x - 12.5), (int)(y - 12.5), 25, 25);
        }

        public void Draw()
        {
            SpriteBatch spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));

            spriteBatch.Draw(myImage, new Vector2(x, y), null, Color.White, 0f, new Vector2(25, 25), 0.5f, SpriteEffects.None, 0);
        }
    }
}
