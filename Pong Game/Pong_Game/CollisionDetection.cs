using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong_Game
{
    class CollisionDetection
    {
        private Rectangle topBarrier, botBarrier;
        private Game game;

        public CollisionDetection(Game game, Rectangle top, Rectangle bot)
        {
            topBarrier = top;
            botBarrier = bot;
            this.game = game;
        }

        public void CheckCollisions(Ball ball, Player player1, Player player2)
        {
            Powerup power = (Powerup)game.Services.GetService(typeof(Powerup));
            Info info = (Info)game.Services.GetService(typeof(Info));
            SoundEffect hit = (SoundEffect)game.Services.GetService(typeof(SoundEffect));

            Rectangle ballRect = ball.CreateRectangle();
            Rectangle p1Rect = player1.CreateRectangle();
            Rectangle p2Rect = player2.CreateRectangle();
            Rectangle powerUpRect = power.CreateRectangle(); 

            if (ballRect.Intersects(topBarrier))
            {
                ball.y = topBarrier.Bottom + 10;
                ball.Bounce();

                hit.Play();

                return;
            }
            else if (ballRect.Intersects(botBarrier))
            {
                ball.y = botBarrier.Top - 12;
                ball.Bounce();

                hit.Play();

                return;
            }
            else if(ballRect.Intersects(p1Rect))
            {
                ball.x = p1Rect.Right + 10;

                ball.lastVolley = 1;

                if (info.p1Power)
                {
                    ball.PowerShot();
                    info.p1Power = false;
                    player1.powered = false;
                }
                else
                {
                    double impactPortion = (ballRect.Center.Y - p1Rect.Center.Y) / (p1Rect.Height / 6);
                    ball.Volley(impactPortion);
                    ball.ApplySpin(player1.spin);
                    player1.spin = 0;
                }

                hit.Play();
                return;

            }
            else if (ballRect.Intersects(p2Rect))
            {
                ball.x = p2Rect.Left - 10;

                ball.lastVolley = 2;

                if (info.p2Power)
                {
                    ball.PowerShot();
                    info.p2Power = false;
                    player2.powered = false;
                }
                else
                {
                    int impactPortion = (ballRect.Center.Y - p2Rect.Center.Y) / (p2Rect.Height / 6);

                    ball.Volley(impactPortion);
                    ball.ApplySpin(player2.spin);

                    player2.spin = 0;
                }

                hit.Play();
                return;
            }
            else if(ballRect.Intersects(powerUpRect))
            {
                if(ball.lastVolley == 1)
                {
                    info.p1Power = true;
                }
                else if (ball.lastVolley == 2)
                {
                    info.p2Power = true;
                }
                power.CollidedWith();
            }
        }
    }
}
