using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong_Game
{
    class Ball
    {
        private Texture2D myImage;

        public double x { get; set; }
        public double y { get; set; }

        public double xVel { get; set; }
        public double yVel { get; set; }

        private float angle;
        private CollisionDetection collider;

        private const double MAX_SPEED = 10;

        public int spin = 0;
        public int lastVolley;

        private Game game;

        public Ball(Game game, int x, int y, Texture2D image)
        {
            this.x = x;
            this.y = y;
            myImage = image;
            angle = 0;
            xVel = 0;
            yVel = 0;

            this.game = game;

            collider = new CollisionDetection(game, new Rectangle(0, 0, 640, 90), new Rectangle(0, 395, 640, 100));
        }

        public void Draw()
        {
            SpriteBatch spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            angle += .05f;

            spriteBatch.Draw(myImage, new Vector2((int)x, (int)y), null, Color.White, angle, new Vector2(1000, 1000), .01f, SpriteEffects.None, 0);
        }

        public Rectangle CreateRectangle()
        {
            return new Rectangle((int)x - 10, (int)y - 10, 20, 20);
        }

        public void ApplyWind(int windAngle)
        {
            if (windAngle < 0) return;
            yVel += Math.Cos(MathHelper.ToRadians(windAngle)) * -.0075;
            xVel += Math.Sin(MathHelper.ToRadians(windAngle)) * .0075;
        }

        public int Update(GameTime gameTime, Player player1, Player player2)
        {
            double seconds = gameTime.ElapsedGameTime.TotalMilliseconds / 10;

            double xNew = x + xVel * seconds;
            double yNew = y + yVel * seconds;

            Rectangle p1Rect = player1.CreateRectangle();
            Rectangle p2Rect = player2.CreateRectangle();

            if ((x > p1Rect.Left && xNew < p1Rect.Right) && (y > p1Rect.Top && y < p1Rect.Bottom))
            {
                xNew = p1Rect.Right;
            }
            else if ((x < p2Rect.Right && xNew > p2Rect.Left) && (y > p2Rect.Top && y < p2Rect.Bottom))
            {
                xNew = p2Rect.Left;
            }

            if (xNew < 0)
            {
                lastVolley = 0;
                return 2;
            }
            if (xNew > 640)
            {
                lastVolley = 0;
                return 1;
            }

            y = yNew;
            x = xNew;

            collider.CheckCollisions(this, player1, player2);

            return 0;
        }

        public void ApplySpin(int spinType)
        {
            spin = spinType;
        }

        public void Bounce()
        {
            yVel *= (-1 + .5 * spin);
            xVel *= (1 + .5 * spin);

            spin = 0;         
        }

        public void Volley(double yVelocity)
        {
            xVel *= -1.1;

            if(Math.Abs(xVel) > MAX_SPEED)
            {
                xVel = MAX_SPEED * (xVel / Math.Abs(xVel));
            }

            yVel = yVelocity;
        }

        public void PowerShot()
        {
            yVel = 0;
            if(xVel > 0)
            {
                xVel = MAX_SPEED * -1;
            }
            else 
            {
                xVel = MAX_SPEED;
            }
        }
    }
}
