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
        private KeyboardState oldState;
        private GamePadState oldPad1State, oldPad2State;

        public Inputs(Player p1, Player p2)
        {
            player1 = p1;
            player2 = p2;

            oldState = Keyboard.GetState();
            oldPad1State = GamePad.GetState(PlayerIndex.One);
            oldPad2State = GamePad.GetState(PlayerIndex.Two);
        }

        public void CheckInputs(bool p1Power, bool p2Power)
        {
            // Get new state of keyboard
            KeyboardState newState = Keyboard.GetState();
            GamePadState newPad1State = GamePad.GetState(PlayerIndex.One);
            GamePadState newPad2State = GamePad.GetState(PlayerIndex.Two);


            /*
             * Player 1's Inputs
             */
            if(newState.IsKeyDown(Keys.W) || newPad1State.IsButtonDown(Buttons.DPadUp))
            {
                player1.Move(-1 * speed);
            }
            if (newState.IsKeyDown(Keys.S) || newPad1State.IsButtonDown(Buttons.DPadDown))
            {
                player1.Move(speed);
            }
            if ((newState.IsKeyDown(Keys.A) && !oldState.IsKeyDown(Keys.A)) || (newPad1State.IsButtonDown(Buttons.LeftTrigger) && !oldPad1State.IsButtonDown(Buttons.LeftTrigger)))
            {
                player1.spinHigh();
            }
            if ((newState.IsKeyDown(Keys.D) && !oldState.IsKeyDown(Keys.D)) || (newPad1State.IsButtonDown(Buttons.RightTrigger) && !oldPad1State.IsButtonDown(Buttons.RightTrigger)))
            {
                player1.spinLow();
            }
            if ((newState.IsKeyDown(Keys.LeftControl) && !oldState.IsKeyDown(Keys.LeftControl)) || (newPad1State.IsButtonDown(Buttons.A) && !oldPad1State.IsButtonDown(Buttons.A)))
            {
                if(p1Power)
                {
                    player1.powered = !player1.powered;
                }
       
            }


            /*
             * Player 2's Inputs
             */
            if (newState.IsKeyDown(Keys.Up) || newPad2State.IsButtonDown(Buttons.DPadUp))
            {
                player2.Move(-1 * speed);
            }
            if (newState.IsKeyDown(Keys.Down) || newPad2State.IsButtonDown(Buttons.DPadDown))
            {
                player2.Move(speed);
            }
            if ((newState.IsKeyDown(Keys.Left) && !oldState.IsKeyDown(Keys.Left)) || (newPad2State.IsButtonDown(Buttons.LeftTrigger) && !oldPad2State.IsButtonDown(Buttons.LeftTrigger)))
            {
                player2.spinLow();
            }
            if ((newState.IsKeyDown(Keys.Right) && !oldState.IsKeyDown(Keys.Right)) || (newPad2State.IsButtonDown(Buttons.RightTrigger) && !oldPad2State.IsButtonDown(Buttons.RightTrigger)))
            {
                player2.spinHigh();
            }
            if ((newState.IsKeyDown(Keys.RightControl) && !oldState.IsKeyDown(Keys.RightControl)) || (newPad2State.IsButtonDown(Buttons.A) && !oldPad2State.IsButtonDown(Buttons.A)))
            {
                if (p2Power)
                {
                    player2.powered = !player2.powered;
                }
            }


            // New state becomes the old state
            oldState = newState;
            oldPad1State = newPad1State;
            oldPad2State = newPad2State;
        }
    }
}
