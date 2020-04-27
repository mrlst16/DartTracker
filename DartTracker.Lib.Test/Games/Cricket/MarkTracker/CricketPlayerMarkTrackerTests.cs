using DartTracker.Lib.Games.Cricket;
using DartTracker.Lib.Games.Cricket.MarkTracker;
using DartTracker.Model.Shooting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DartTracker.Lib.Test.Games.Cricket.MarkTracker
{
    [TestClass]
    public class CricketPlayerMarkTrackerTests
    {
        [TestMethod]
        public void Add1Single20()
        {
            Guid playerid = Guid.NewGuid();
            Cricket200PlayerMarkTracker tracker
                = new Cricket200PlayerMarkTracker(playerid);

            tracker.MarkShotFor(new Shot()
            {
                Contact = Model.Enum.ContactType.Single,
                NumberHit = 20
            }, false);

            Assert.IsTrue(tracker.MarksFor[20] == 1);
            Assert.IsTrue(tracker.Score == 0);
        }

        [TestMethod]
        public void Add1Double20()
        {
            Guid playerid = Guid.NewGuid();
            Cricket200PlayerMarkTracker tracker
                = new Cricket200PlayerMarkTracker(playerid);

            tracker.MarkShotFor(new Shot()
            {
                Contact = Model.Enum.ContactType.Double,
                NumberHit = 20
            }, false);

            Assert.IsTrue(tracker.MarksFor[20] == 2);
            Assert.IsTrue(tracker.Score == 0);
        }

        [TestMethod]
        public void AddTripple20()
        {
            Guid playerid = Guid.NewGuid();
            Cricket200PlayerMarkTracker tracker
                = new Cricket200PlayerMarkTracker(playerid);

            tracker.MarkShotFor(new Shot()
            {
                Contact = Model.Enum.ContactType.Triple,
                NumberHit = 20
            }, false);

            Assert.IsTrue(tracker.MarksFor[20] == 3);
            Assert.IsTrue(tracker.Score == 0);
        }

        [TestMethod]
        public void Add5Bulls()
        {
            Guid playerid = Guid.NewGuid();
            Cricket200PlayerMarkTracker tracker
                = new Cricket200PlayerMarkTracker(playerid);

            tracker.MarkShotFor(new Shot()
            {
                Contact = Model.Enum.ContactType.DoubleBullsEye,
                NumberHit = 25
            }, false);

            Assert.IsTrue(tracker.MarksFor[25] == 2);
            Assert.IsTrue(tracker.Score == 0);

            tracker.MarkShotFor(new Shot()
            {
                Contact = Model.Enum.ContactType.DoubleBullsEye,
                NumberHit = 25
            }, false);

            Assert.IsTrue(tracker.MarksFor[25] == 4);
            Assert.IsTrue(tracker.Score == 25);

            tracker.MarkShotFor(new Shot()
            {
                Contact = Model.Enum.ContactType.BullsEye,
                NumberHit = 25
            }, false);

            Assert.IsTrue(tracker.MarksFor[25] == 5);
            Assert.IsTrue(tracker.Score == 50);
        }
    }
}
