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
    public class CricketCutthroatMarkTrackerTest
    {

        [TestMethod]
        public async Task SixBullsEyes()
        {
            CricketCutthroatPlayerMarkTracker tracker
                = new CricketCutthroatPlayerMarkTracker(Guid.NewGuid());

            Shot shot = new Shot()
            {
                NumberHit = 25,
                Contact = Model.Enum.ContactType.DoubleBullsEye
            };
            tracker.MarkShotAgainst(shot);
            Assert.AreEqual(0, tracker.Score);
            tracker.MarkShotAgainst(shot);
            Assert.AreEqual(25, tracker.Score);
            tracker.MarkShotAgainst(shot);
            Assert.AreEqual(75, tracker.Score);
        }
    }
}
