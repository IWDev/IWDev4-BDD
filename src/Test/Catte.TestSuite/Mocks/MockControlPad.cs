namespace Catte.TestSuite.Mocks
{
    using System;

    /// <summary>
    /// A mock object implementing the <see cref="IControlPad"/> interface so that we
    /// can easily run tests on the <see cref="GameWorld"/> class.
    /// </summary>
    public class MockControlPad : IControlPad
    {
        private Movement _LeftStick;
        private Boolean _ButtonA;

        /// <summary>
        /// Default constructor initializes the controller mock with no current movement.
        /// </summary>
        public MockControlPad()
        {
            this._LeftStick = Movement.None;
        }

        #region IControlPad members

        public Movement LeftStick { get { return this._LeftStick; } }

        public Boolean ButtonA { get { return this._ButtonA; } }

        #endregion

        public void MoveUp()
        {
            this._LeftStick = Movement.Up;    
        }

        public void MoveDown()
        {
            this._LeftStick = Movement.Down;
        }

        public void Shoot()
        {
            this._ButtonA = true;
        }

        public void StopShooting()
        {
            this._ButtonA = false;  
        }
    }
}
