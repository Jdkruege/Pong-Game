using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong_Game
{
    class Boards
    {
        private Texture2D scoreboardImage;
        private Texture2D infoboardImage;

        private Game game;

        public Boards(Game game, Texture2D scoreboard, Texture2D infoboard)
        {
            scoreboardImage = scoreboard;
            infoboardImage = infoboard;

            this.game = game;
        }

        public void Draw()
        {
            SpriteBatch spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));

            spriteBatch.Draw(scoreboardImage, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(infoboardImage, new Vector2(0, 430), Color.White);
        }
    }
}
