using DartTracker.Lib.Extensions;
using DartTracker.Lib.Test.Data;
using DartTracker.Model.Games;
using DartTracker.Model.Shooting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTracker.Lib.Test.Games.OhOne
{
    [TestClass]
    public class OhOneExtensionsTests
    {
        [TestMethod]
        public async Task OnePlayerTenPointLimit11Hit()
        {
            Game game = GamesData.OnePlayer();
            var shots = new List<Shot>() {
                new Shot() {
                    Contact = Model.Enum.ContactType.Single,
                    NumberHit = 11
                }
            };

            var shotboard = game.Players.CalculateFor01OpenInOpenOut(shots, 10);

            var tracker = shotboard.ElementAt(0).Value;

            Assert.AreEqual(10, tracker.Score);
        }

        [TestMethod]
        public async Task TwoPlayerTenPointLimit()
        {
            Game game = GamesData.OnePlayer();
            var shots = new List<Shot>() {
                new Shot() {
                    Contact = Model.Enum.ContactType.Single,
                    NumberHit = 11
                }
            };

            var shotboard = game.Players.CalculateFor01OpenInOpenOut(shots, 10);

            var tracker = shotboard.ElementAt(0).Value;

            Assert.AreEqual(10, tracker.Score);
        }

    }
}
