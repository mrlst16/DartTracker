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
        public async Task TwoPlayerTenPointLimitAN11AndA0()
        {
            Game game = GamesData.TwoPlayers();
            var shots = new List<Shot>() {
                new Shot() {
                    Contact = Model.Enum.ContactType.Single,
                    NumberHit = 11
                },
                new Shot() {
                    Contact = Model.Enum.ContactType.Single,
                    NumberHit = 10
                }
            };

            var shotboard = game.Players.CalculateFor01OpenInOpenOut(shots, 10);

            var tracker1 = shotboard.ElementAt(0).Value;
            var tracker2 = shotboard.ElementAt(1).Value;

            Assert.AreEqual(10, tracker1.Score);
            Assert.AreEqual(0, tracker2.Score);
        }

        [TestMethod]
        public async Task TwoPlayer100PointLimit3ShotsSingle20sEach()
        {
            Game game = GamesData.TwoPlayers();
            var shots = new List<Shot>() {
                new Shot() {
                    Contact = Model.Enum.ContactType.Single,
                    NumberHit = 20
                },
                new Shot() {
                    Contact = Model.Enum.ContactType.Single,
                    NumberHit = 20
                },
                new Shot() {
                    Contact = Model.Enum.ContactType.Single,
                    NumberHit = 20
                },
                new Shot() {
                    Contact = Model.Enum.ContactType.Single,
                    NumberHit = 20
                },
                new Shot() {
                    Contact = Model.Enum.ContactType.Single,
                    NumberHit = 20
                },
                new Shot() {
                    Contact = Model.Enum.ContactType.Single,
                    NumberHit = 20
                }
            };

            var shotboard = game.Players.CalculateFor01OpenInOpenOut(shots, 100);

            var tracker1 = shotboard.ElementAt(0).Value;
            var tracker2 = shotboard.ElementAt(1).Value;

            Assert.AreEqual(40, tracker1.Score);
            Assert.AreEqual(40, tracker2.Score);
        }

    }
}
