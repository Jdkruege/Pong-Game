using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong_Game
{
    class Background
    {
        private Texture2D backgroundImage;
        private Texture2D bleacherImage;

        private Game game;

        public Background(Game game, Texture2D background, Texture2D bleacher)
        {
            backgroundImage = background;
            bleacherImage = bleacher;

            this.game = game;
        }

        public void Draw()
        {
            SpriteBatch spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));

            spriteBatch.Draw(backgroundImage, new Rectangle(0, 0, 640, 480), Color.White);
            spriteBatch.Draw(bleacherImage, new Vector2(0, -15), Color.White);
            spriteBatch.Draw(bleacherImage, new Vector2(0, 395), null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.FlipVertically, 0f);
        }

    }
}
