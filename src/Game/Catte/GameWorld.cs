namespace Catte
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Defines a class that represents this game's "game world" - i.e. the playable world.
    /// </summary>
    public class GameWorld
    {
        private IControlPad _PlayerControlPad;
        private Rectangle _CatRectangle;
        private Texture2D _CatTexture;
        private Rectangle _AlienRectangle;
        private Texture2D _AlienTexture;
        private Point _AlienMoveTarget;
        private Random _AlienMoveRandomizer;

        private Rectangle _FurballRectangle;
        private Texture2D _FurballTexture;
        private Rectangle _AcidRectangle;
        private Texture2D _AcidTexture;

        /// <summary>
        /// Constructs a new instance of the <see cref="GameWorld"/> class, provided all
        /// of it's required dependencies.
        /// </summary>
        /// <param name="controlPad">The human player's controller.</param>
        /// <param name="catRectangle">The rectangle that will be used to represent the Cat.</param>
        /// <param name="catTexture">The texture that will be drawn for the Cat.</param>
        /// <param name="furballRectangle">The rectangle that will be used to represent the Cat's projectile Furball.</param>
        /// <param name="furballTexture">The texture that will be drawn for the Furball.</param>
        /// <param name="alienRectangle">The rectangle that will be used to represent the Alien.</param>
        /// <param name="alienTexture">The texture that will be drawn for the Alien.</param>
        /// <param name="acidRectangle">The rectangle that will be used to represent the Alien's projectile Acid.</param>
        /// <param name="acidTexture">The texture that will be drawn for the Acid.</param>
        public GameWorld(IControlPad controlPad, 
            Rectangle catRectangle, Texture2D catTexture, 
            Rectangle furballRectangle, Texture2D furballTexture, 
            Rectangle alienRectangle, Texture2D alienTexture, 
            Rectangle acidRectangle, Texture2D acidTexture)
        {
            // Setup dependencies
            this._PlayerControlPad  = controlPad;
            this._CatRectangle      = catRectangle;
            this._CatTexture        = catTexture;
            this._FurballRectangle  = furballRectangle;
            this._FurballTexture    = furballTexture;
            this._AlienRectangle    = alienRectangle;
            this._AlienTexture      = alienTexture;
            this._AcidRectangle     = acidRectangle;
            this._AcidTexture       = acidTexture;

            // Initialize the random instance used to move the Alien.
            this._AlienMoveRandomizer = new Random();
        }

        #region API members

        /// <summary>
        /// Returns true if the Alien is currently active (i.e. moving)
        /// </summary>
        public Boolean AlienIsActive { get; private set; }

        /// <summary>
        /// Gets the current location of the Cat within the game world.
        /// </summary>
        public Point CatLocation
        {
            get { return new Point(this._CatRectangle.Location.X, this._CatRectangle.Location.Y); }
        }

        /// <summary>
        /// Gets the current location of the Alien within the game world.
        /// </summary>
        public Point AlienLocation
        {
            get { return new Point(this._AlienRectangle.Location.X, this._AlienRectangle.Location.Y); }
        }

        /// <summary>
        /// Gets the current location of the Furball within in the game world.
        /// </summary>
        public Point FurballLocation
        {
            get { return new Point(this._FurballRectangle.Location.X, this._FurballRectangle.Location.Y); }
        }

        /// <summary>
        /// Gets the current location of the Acid blob within in the game world.
        /// </summary>
        public Point AcidLocation
        {
            get { return new Point(this._AcidRectangle.Location.X, this._AcidRectangle.Location.Y); }
        }

        /// <summary>
        /// Gets the current number of Furballs that are in the game world and are "in-shot".
        /// </summary>
        public Int32 FurballCount
        {
            get
            {
                if (this._FurballRectangle.Location.X <= 1280)
                    return 1;

                return 0;
            }
        }

        /// <summary>
        /// Gets the current number of Acid blobs that are in the game world and are "in-shot".
        /// </summary>
        public Int32 AcidCount
        {
            get
            {
                if (this._AcidRectangle.Location.X >= 0)
                    return 1;

                return 0;
            }
        }

        /// <summary>
        /// Gets the number of times that the Cat has killed the Alien.
        /// </summary>
        public Int32 AlienKillCount { get; private set; }

        /// <summary>
        /// Begins the Alien activity (i.e random movement).
        /// </summary>
        public void ActivateAlien()
        {
            if (!this.AlienIsActive)
            {
                this.RandomizeNewAlienMoveTarget();
                this.AlienIsActive = true;
            }
        }

        /// <summary>
        /// Begins the Alien activity, with a non-random movement destination seed.
        /// </summary>
        /// <param name="targetDestination"></param>
        public void ActivateAlien(Point targetDestination)
        {
            if (!this.AlienIsActive)
            {
                this._AlienMoveTarget = targetDestination;
                this.AlienIsActive = true;
            }
        }

        /// <summary>
        /// Calls the <see cref="GameWorld.Update"/> method a specified number of times.
        /// </summary>
        /// <remarks>
        /// This is largely just a method used for testing.
        /// </remarks>
        public void Update(Int32 count)
        {
            for (var i = 0; i < count; i++)
                this.Update();
        }

        /// <summary>
        /// Updates the game world and the contents within it.
        /// </summary>
        public void Update()
        {
            // Handle player movement
            if (this._PlayerControlPad.LeftStick == Movement.Up)
                this._CatRectangle.Offset(0, -4);
            if (this._PlayerControlPad.LeftStick == Movement.Down)
                this._CatRectangle.Offset(0, 4);

            // Handle player shooting
            if (this._PlayerControlPad.ButtonA)
                this._FurballRectangle.Location = new Point(this._CatRectangle.X + 100, this._CatRectangle.Y + 50);
            

            // Handle alien moving
            if (this.AlienIsActive)
            {
                if (this._AlienRectangle.Top > this._AlienMoveTarget.Y + 5)
                    this._AlienRectangle.Offset(0, -4);
                else if (this._AlienRectangle.Top < this._AlienMoveTarget.Y - 5)
                    this._AlienRectangle.Offset(0, 4);
                else
                {
                    // We've reached the destination - shoot acid!
                    this._AcidRectangle.Location = new Point(this._AlienRectangle.X - 100, this._AlienRectangle.Y + 50);

                    this.RandomizeNewAlienMoveTarget();
                }
            }

            // Handle furball moving
            this._FurballRectangle.Offset(10, 0);

            // Handle acid moving
            this._AcidRectangle.Offset(-10, 0);

            // Handle furball-alien collision detection
            if (this._FurballRectangle.Intersects(this._AlienRectangle))
            {
                this._FurballRectangle.X = 2000;
                this._AlienRectangle.Y = -this._AlienRectangle.Height;
                this.AlienKillCount++;
            }
        }

        /// <summary>
        /// Draws the game world and the contents within it.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this._CatTexture, this._CatRectangle, Color.White);
            spriteBatch.Draw(this._FurballTexture, this._FurballRectangle, Color.White);
            spriteBatch.Draw(this._AlienTexture, this._AlienRectangle, Color.White);
            spriteBatch.Draw(this._AcidTexture, this._AcidRectangle, Color.White);
        }

        #endregion

        /// <summary>
        /// Uses the Random instance to generate a new "target" location for the alien to move towards.
        /// </summary>
        private void RandomizeNewAlienMoveTarget()
        {
            this._AlienMoveTarget = new Point(0, this._AlienMoveRandomizer.Next(720 - this._AlienRectangle.Height));
        }
    }
}
