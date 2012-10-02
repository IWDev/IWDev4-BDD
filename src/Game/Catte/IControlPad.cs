namespace Catte
{
    using System;

    /// <summary>
    /// Defines an interface that human player controllers should implement
    /// in order to interact with the <see cref="GameWorld"/>.
    /// </summary>
    public interface IControlPad
    {
        /// <summary>
        /// Returns the current <see cref="Movement"/> being used (if any).
        /// </summary>
        Movement LeftStick { get; }

        /// <summary>
        /// Returns true if the button A (shoot) button is pressed.
        /// </summary>
        Boolean ButtonA { get; }
    }
}
