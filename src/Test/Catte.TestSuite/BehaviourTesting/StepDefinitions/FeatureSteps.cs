namespace Catte.TestSuite.BehaviourTesting.StepDefinitions
{
    using TechTalk.SpecFlow;
    using NUnit.Framework;
    using Microsoft.Xna.Framework;
    using Catte.TestSuite.Mocks;

    [Binding]
    public class FeatureSteps
    {
        [Given(@"there are no Furballs")]
        [Given(@"the Cat starts in one location")]
        [Given(@"the Alien starts in one location")]
        [Given(@"I am lined up with the Alien")]
        [Given(@"there are no Acid blobs")]
        public void GivenTheCatStartsInOneLocation()
        {
            this.CatStartLocation = new Point(0, 0);
            this.AlienStartLocation = new Point(750, 0);
            this.FurballStartLocation = new Point(2000, 0);
            this.AcidStartLocation = new Point(-2000, 0);

            this.MockControlPad = new MockControlPad();

            Rectangle catRectangle = new Rectangle(this.CatStartLocation.X, this.CatStartLocation.Y, 250, 245);
            Rectangle furballRectangle = new Rectangle(this.FurballStartLocation.X, this.FurballStartLocation.Y, 45, 45);
            Rectangle alienRectangle = new Rectangle(this.AlienStartLocation.X, this.AlienStartLocation.Y, 165, 250);
            Rectangle acidRectangle = new Rectangle(this.AcidStartLocation.X, this.AcidStartLocation.Y, 45, 30);

            this.GameWorld = new GameWorld(this.MockControlPad, catRectangle, null, furballRectangle, null, alienRectangle, null, acidRectangle, null);
        }

        [When(@"the Alien moves in a different direction")]
        [When(@"I launch an Acid blob")]
        public void WhenTheAlienMovesInADifferentDirection()
        {
            var changeDirectionDestination = new Point(750, 20);
            this.GameWorld.ActivateAlien(changeDirectionDestination);
            this.GameWorld.Update(10);
        }

        [When(@"the Furball collides with the Alien")]
        public void WhenTheFurballCollidesWithTheAlien()
        {
            this.GameWorld.Update(100);
        }

        #region out of scope

        [When(@"I move the Cat up and down")]
        public void WhenIMoveTheCatUpAndDown()
        {
            this.MockControlPad.MoveUp();
            this.GameWorld.Update(10);

            this.MockControlPad.MoveDown();
            this.GameWorld.Update(20);
        }

        [When(@"I launch a Furball")]
        [When(@"I make the Cat shoot a Furball")]
        public void WhenIMakeTheCatShootAFurball()
        {
            this.MockControlPad.Shoot();
            this.GameWorld.Update();

            this.MockControlPad.StopShooting();
            this.GameWorld.Update(9);
        }

        [When(@"the computer moves the Alien up and down")]
        public void WhenTheComputerMovesTheAlienUpAndDown()
        {
            this.GameWorld.ActivateAlien();
            this.GameWorld.Update(10);
        }

        [Then(@"the Cat will be in a different location")]
        public void ThenTheCatWillBeInADifferentLocation()
        {
            var previousCatLocation = this.CatStartLocation;
            var currentCatLocation = this.GameWorld.CatLocation;

            Assert.AreNotEqual(previousCatLocation.Y, currentCatLocation.Y);
        }

        [Then(@"the Alien will be in a different location")]
        public void ThenTheAlienWillBeInADifferentLocation()
        {
            var previousAlienLocation = this.AlienStartLocation;
            var currentAlienLocation = this.GameWorld.AlienLocation;

            Assert.AreNotEqual(previousAlienLocation.Y, currentAlienLocation.Y);
        }

        [Then(@"there will be a Furball")]
        public void ThenThereWillBeAFurball()
        {
            Assert.Greater(this.GameWorld.FurballCount, 0);
        }

        [Then(@"there will be an Acid blob")]
        public void ThenThereWillBeAnAcidBlob()
        {
            Assert.Greater(this.GameWorld.AcidCount, 0);
        }

        [Then(@"a Furball will fly accross the screen")]
        public void ThenAFurballWillFlyAccrossTheScreen()
        {
            var previousFurballLocation = this.GameWorld.FurballLocation;
            this.GameWorld.Update(5);
            var currentFurballLocation = this.GameWorld.FurballLocation;

            Assert.Greater(currentFurballLocation.X, previousFurballLocation.X);
        }

        [Then(@"an Acid blob will fly accross the screen")]
        public void ThenAnAcidBlobWillFlyAccrossTheScreen()
        {
            var previousAcidLocation = this.GameWorld.AcidLocation;
            this.GameWorld.Update(5);
            var currentAcidLocation = this.GameWorld.AcidLocation;

            Assert.Less(currentAcidLocation.X, previousAcidLocation.X);
        }

        [Then(@"the Alien will be killed")]
        public void ThenTheAlienWillBeKilled()
        {
            Assert.Greater(this.GameWorld.AlienKillCount, 0);
        }

        #endregion

        #region Fields & Properties

        public MockControlPad MockControlPad
        {
            get
            {
                if (ScenarioContext.Current.ContainsKey("MockControlPad"))
                    return ScenarioContext.Current["MockControlPad"] as MockControlPad;

                return null;
            }
            set { ScenarioContext.Current["MockControlPad"] = value; }
        }

        public GameWorld GameWorld
        {
            get
            {
                if (ScenarioContext.Current.ContainsKey("GameWorld"))
                    return ScenarioContext.Current["GameWorld"] as GameWorld;

                return null;
            }
            set { ScenarioContext.Current["GameWorld"] = value; }
        }

        public Point CatStartLocation
        {
            get
            {
                if (ScenarioContext.Current.ContainsKey("CatStartLocation"))
                    return (Point)ScenarioContext.Current["CatStartLocation"];

                return new Point(0, 0);
            }
            set { ScenarioContext.Current["CatStartLocation"] = value; }
        }

        public Point AlienStartLocation
        {
            get
            {
                if (ScenarioContext.Current.ContainsKey("AlienStartLocation"))
                    return (Point)ScenarioContext.Current["AlienStartLocation"];

                return new Point(0, 0);
            }
            set { ScenarioContext.Current["AlienStartLocation"] = value; }
        }

        public Point FurballStartLocation
        {
            get
            {
                if (ScenarioContext.Current.ContainsKey("FurballStartLocation"))
                    return (Point)ScenarioContext.Current["FurballStartLocation"];

                return new Point(0, 0);
            }
            set { ScenarioContext.Current["FurballStartLocation"] = value; }
        }

        public Point AcidStartLocation
        {
            get
            {
                if (ScenarioContext.Current.ContainsKey("AcidStartLocation"))
                    return (Point)ScenarioContext.Current["AcidStartLocation"];

                return new Point(0, 0);
            }
            set { ScenarioContext.Current["AcidStartLocation"] = value; }
        }

        #endregion
    }
}
