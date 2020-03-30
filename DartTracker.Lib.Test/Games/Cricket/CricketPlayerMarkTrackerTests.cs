using DartTracker.Lib.Games.Cricket;
using DartTracker.Model.Shooting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DartTracker.Lib.Test
{
    [TestClass]
    public class CricketPlayerMarkTrackerTests
    {
        [TestMethod]
        public void Add1Single20()
        {
            Guid playerid = Guid.NewGuid();
            CricketPlayerMarkTracker tracker
                = new CricketPlayerMarkTracker(playerid);

            var shot = tracker.MarkShot(new Shot()
            {
                Contact = Model.Enum.ContactType.Single,
                NumberHit = 20
            });

            Assert.IsTrue(tracker.Marks[20] == 1);
            Assert.IsTrue(tracker.Score == 0);
        }

        [TestMethod]
        public void Add1Double20()
        {
            Guid playerid = Guid.NewGuid();
            CricketPlayerMarkTracker tracker
                = new CricketPlayerMarkTracker(playerid);

            var shot = tracker.MarkShot(new Shot()
            {
                Contact = Model.Enum.ContactType.Double,
                NumberHit = 20
            });

            Assert.IsTrue(tracker.Marks[20] == 2);
            Assert.IsTrue(tracker.Score == 0);
        }

        [TestMethod]
        public void AddTripple20()
        {
            Guid playerid = Guid.NewGuid();
            CricketPlayerMarkTracker tracker
                = new CricketPlayerMarkTracker(playerid);

            var shot = tracker.MarkShot(new Shot()
            {
                Contact = Model.Enum.ContactType.Triple,
                NumberHit = 20
            });

            Assert.IsTrue(tracker.Marks[20] == 3);
            Assert.IsTrue(tracker.Score == 0);
        }

        [TestMethod]
        public void Add5Bulls()
        {
            Guid playerid = Guid.NewGuid();
            CricketPlayerMarkTracker tracker
                = new CricketPlayerMarkTracker(playerid);

            var shot1 = tracker.MarkShot(new Shot()
            {
                Contact = Model.Enum.ContactType.DoubleBullsEye,
                NumberHit = 25
            });

            Assert.IsTrue(tracker.Marks[25] == 2);
            Assert.IsTrue(tracker.Score == 0);

            var shot2 = tracker.MarkShot(new Shot()
            {
                Contact = Model.Enum.ContactType.DoubleBullsEye,
                NumberHit = 25
            });

            Assert.IsTrue(tracker.Marks[25] == 4);
            Assert.IsTrue(tracker.Score == 25);

            var shot3 = tracker.MarkShot(new Shot()
            {
                Contact = Model.Enum.ContactType.BullsEye,
                NumberHit = 25
            });

            Assert.IsTrue(tracker.Marks[25] == 5);
            Assert.IsTrue(tracker.Score == 50);
        }


    }
}
