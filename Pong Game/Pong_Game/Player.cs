using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong_Game
{
    class Player
    {
        public int x { get; set; }
        public int y { get; set; }

        private const int lowestPoint = 345, highestPoint = 120;

        private Texture2D myImage;

        public int spin = 0;
        public bool powered = false;

        private Game game;

        public Player(Game game, int x, int y, Texture2D image)
        {
            this.x = x;
            this.y = y;
            myImage = image;

            this.game = game;
        }

        public void Move(int deltaY)
        {
           if(y + deltaY > lowestPoint)
           {
               deltaY = lowestPoint - y;
           }
           if (y + deltaY < highestPoint)
           {
               deltaY = y - highestPoint;
           }

           y += deltaY;
        }

        public void spinLow()
        {
            spin = 1;
        }

        public void spinHigh()
        {
            spin = -1;
        }

        public void Draw()
        {
            SpriteBatch spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));

            spriteBatch.Draw(myImage, new Vector2(x, y + 45), null, Color.White, 0f, new Vector2(20, 550), .15f, SpriteEffects.None, 0);
        }

        public Rectangle CreateRectangle()
        {
            return new Rectangle((int)x - 3, (int)(y - 37.5), 6, 90);            
        }
    }
}
