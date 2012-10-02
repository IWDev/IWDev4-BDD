namespace Catte
{
    using System;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// A concrete implementation of the <see cref="IControlPad"/> interface that allows
    /// a human player to interact with the <see cref="GameWorld"/> by using a control pad
    /// or computer keyboard.
    /// </summary>
    public class ControlPad : IControlPad
    {
        private PlayerIndex _PlayerIndex;

        /// <summary>
        /// Constructs a new instance of the <see cref="ControlPad"/> class.
        /// </summary>
        /// <param name="playerIndex">The player index that will be monitored.</param>
        public ControlPad(PlayerIndex playerIndex)
        {
            this._PlayerIndex = playerIndex;
        }

        /// <summary>
        /// Gets a <see cref="Movement"/> value representing the currect (if any) movement
        /// control being used by the player.
        /// </summary>
        public Movement LeftStick
        {
            get
            {
                if (GamePad.GetState(this._PlayerIndex).ThumbSticks.Left.Y > 0.2f ||
                    Keyboard.GetState().IsKeyDown(Keys.Up)) 
                    return Movement.Up;

                if (GamePad.GetState(this._PlayerIndex).ThumbSticks.Left.Y < -0.2f ||
                    Keyboard.GetState().IsKeyDown(Keys.Down)) 
                    return Movement.Down;

                return Movement.None;
            }
        }
        
        /// <summary>
        /// Returns true if the Button A (or space key) is currently being pressed.
        /// </summary>
        public Boolean ButtonA
        {
            get
            {
                return ((GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed ||
                    Keyboard.GetState().IsKeyDown(Keys.Space)));
            }
        }
    }
}
