using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong_Game
{
    class Inputs
    {
        private Player player1, player2;
        private const int speed = 5;

        public Inputs(Player p1, Player p2)
        {
            player1 = p1;
            player2 = p2;
        }

        public void CheckInputs()
        {
            KeyboardState state = Keyboard.GetState();
            GamePadState pad1State = GamePad.GetState(PlayerIndex.One);
            GamePadState pad2State = GamePad.GetState(PlayerIndex.Two);

            if(state.IsKeyDown(Keys.W) || pad1State.IsButtonDown(Buttons.DPadUp))
            {
                player1.Move(-1 * speed);
            }
            if (state.IsKeyDown(Keys.S) || pad1State.IsButtonDown(Buttons.DPadDown))
            {
                player1.Move(speed);
            }
            if (state.IsKeyDown(Keys.A) || pad1State.IsButtonDown(Buttons.LeftTrigger))
            {
                player1.spinHigh();
            }
            if (state.IsKeyDown(Keys.D) || pad1State.IsButtonDown(Buttons.RightTrigger))
            {
                player1.spinLow();
            }
            if (state.IsKeyDown(Keys.LeftControl) || pad1State.IsButtonDown(Buttons.X))
            {
                player1.powered = true;
            }



            if (state.IsKeyDown(Keys.Up) || pad2State.IsButtonDown(Buttons.DPadUp))
            {
                player2.Move(-1 * speed);
            }
            if (state.IsKeyDown(Keys.Down) || pad2State.IsButtonDown(Buttons.DPadDown))
            {
                player2.Move(speed);
            }
            if (state.IsKeyDown(Keys.Left) || pad2State.IsButtonDown(Buttons.LeftTrigger))
            {
                player2.spinLow();
            }
            if (state.IsKeyDown(Keys.Right) || pad2State.IsButtonDown(Buttons.RightTrigger))
            {
                player2.spinHigh();
            }
            if (state.IsKeyDown(Keys.RightControl) || pad2State.IsButtonDown(Buttons.X))
            {
                player2.powered = true;
            }

        }
    }
}
