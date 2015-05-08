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

        public float windAngle;
        public double windDelta = 0.025;

        private Random rand;

        private Game game;

        public Info(Game game,Texture2D arrow, Texture2D power, int goal, SpriteFont font)
        {
            this.goal = goal;
            this.font = font;
            arrowImage = arrow;
            powerUpImage = power;
            p1Score = 0;
            p2Score = 0;
            windAngle = -180;

            rand = new Random(System.DateTime.Now.Millisecond + System.DateTime.Now.Second + System.DateTime.Now.Minute + System.DateTime.Now.Hour + System.DateTime.Now.Day + System.DateTime.Now.Month + System.DateTime.Now.Year); ;

            this.game = game;
        }

        // Starts the wind going either north or south
        public void StartWind(int direction)
        {
            switch(direction)
            {
                case 0:
                    windAngle = 0;
                    break;
                case 1:
                    windAngle = 180;
                    break;
                default:
                    windAngle = -180;
                    break;
            }
        }

        // Slowly modifies the wind direction
        public void ChangeWind()
        {
            int magnitude = rand.Next(-100, 100);

            windAngle += (float)(magnitude * windDelta);

            if(windAngle < 0)
            {
                windAngle += 360;
            }
            else if(windAngle > 360)
            {
                windAngle -= 360;
            }
        }

        // Stops wind for any user of the class
        public void StopWind()
        {
            windAngle = -180;
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
                spriteBatch.Draw(powerUpImage, new Vector2(25, 455), null, Color.White, 0f, new Vector2(25, 25), 0.5f, SpriteEffects.None, 0);
            }
            if (p2Power)
            {
                spriteBatch.Draw(powerUpImage, new Vector2(615, 455), null, Color.White, 0f, new Vector2(25, 25), 0.5f, SpriteEffects.None, 0);
            }

        }
    }
}
