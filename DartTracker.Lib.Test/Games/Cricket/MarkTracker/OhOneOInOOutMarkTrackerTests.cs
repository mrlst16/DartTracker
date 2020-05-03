using DartTracker.Lib.Games.MarkTrackers;
using DartTracker.Model.Shooting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DartTracker.Lib.Test.Games.Cricket.MarkTracker
{
    [TestClass]
    public class OhOneOInOOutMarkTrackerTests
    {
        [TestMethod]
        public async Task TenPointStart9Hit()
        {
            OhOneOInOOutMarkTracker tracker = new OhOneOInOOutMarkTracker(Guid.Empty, 10);
            var shot = new Shot()
            {
                Contact = Model.Enum.ContactType.Single,
                NumberHit = 9
            };

            bool notBusted = tracker.MarkShot(shot);
            Assert.IsTrue(notBusted);
            Assert.AreEqual(1, tracker.Score);
        }

        [TestMethod]
        public async Task TenPointStart10Hit()
        {
            OhOneOInOOutMarkTracker tracker = new OhOneOInOOutMarkTracker(Guid.Empty, 10);
            var shot = new Shot()
            {
                Contact = Model.Enum.ContactType.Single,
                NumberHit = 10
            };

            bool notBusted = tracker.MarkShot(shot);
            Assert.IsTrue(notBusted);
            Assert.AreEqual(0, tracker.Score);
        }

        [TestMethod]
        public async Task TenPointStart20Hit()
        {
            OhOneOInOOutMarkTracker tracker = new OhOneOInOOutMarkTracker(Guid.Empty, 10);
            var shot = new Shot()
            {
                Contact = Model.Enum.ContactType.Single,
                NumberHit = 20
            };

            bool notBusted = tracker.MarkShot(shot);
            Assert.IsFalse(notBusted);
            Assert.AreEqual(10, tracker.Score);
        }
    }
}
