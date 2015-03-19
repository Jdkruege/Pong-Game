using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong_Game
{
    class Info
    {
        public int goal, p1Score, p2Score;
        public bool p1Power = false, p2Power = false;

        public Texture2D arrowImage;
        public Texture2D powerUpImage;
        public SpriteFont font;

        public int windAngle;

        private Game game;

        public Info(Game game,Texture2D arrow, int goal, SpriteFont font)
        {
            this.goal = goal;
            this.font = font;
            arrowImage = arrow;
            p1Score = 0;
            p2Score = 0;
            windAngle = -180;

            this.game = game;
        }

        public void ChangeWind(String direction)
        {
            switch(direction)
            {
                case "N":
                    windAngle = 0;
                    break;
                case "E":
                    windAngle = 90;
                    break;
                case "S":
                    windAngle = 180;
                    break;
                case "W":
                    windAngle = 270;
                    break;
                case "NE":
                    windAngle = 45;
                    break;
                case "SE":
                    windAngle = 135;
                    break;
                case "SW":
                    windAngle = 225;
                    break;
                case "NW":
                    windAngle = 315;
                    break;
                case "None":
                    windAngle = -180;
                    break;
            }
        }

        public void Draw()
        {
            SpriteBatch spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));

            spriteBatch.Draw(arrowImage, new Vector2(320, 455), null, Color.White, MathHelper.ToRadians(windAngle), new Vector2(25, 25), 0.5f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "" + goal, new Vector2(315, 14), Color.Crimson);
            spriteBatch.DrawString(font, "" + p1Score, new Vector2(20, 14), Color.Crimson);
            spriteBatch.DrawString(font, "" + p2Score, new Vector2(610, 14), Color.Crimson);

            if(p1Power)
            {
                spriteBatch.Draw(arrowImage, new Vector2(25, 455), null, Color.White, 0f, new Vector2(25, 25), 0.5f, SpriteEffects.None, 0);
            }
            if (p2Power)
            {
                spriteBatch.Draw(arrowImage, new Vector2(615, 455), null, Color.White, 0f, new Vector2(25, 25), 0.5f, SpriteEffects.None, 0);
            }

        }
    }
}
