using DartTracker.Lib.Test.Data.Cricket;
using DartTracker.Model.Players;
using DartTracker.Model.Shooting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DartTracker.Lib.Helpers;
using System.Linq;
using DartTracker.Lib.Extensions;

namespace DartTracker.Lib.Test.Helpers
{
    [TestClass]
    public class CricketExtensionsTests
    {

        [TestMethod]
        public async Task BothBoardsClosed_HitsAllTripples_WinTwoDoubleBulls_TestCalculate()
        {
            var game = CricketGameServiceData.TwoPlayers();

            List<Player> players = game.Players;
            List<Shot> shots = new List<Shot>()
            {
                new Shot(){Contact = Model.Enum.ContactType.Triple, NumberHit = 15},
                new Shot(){Contact = Model.Enum.ContactType.Triple, NumberHit = 16},
                new Shot(){Contact = Model.Enum.ContactType.Triple, NumberHit = 17},

                new Shot(){Contact = Model.Enum.ContactType.Triple, NumberHit = 15},
                new Shot(){Contact = Model.Enum.ContactType.Triple, NumberHit = 16},
                new Shot(){Contact = Model.Enum.ContactType.Triple, NumberHit = 17},

                new Shot(){Contact = Model.Enum.ContactType.Triple, NumberHit = 18},
                new Shot(){Contact = Model.Enum.ContactType.Triple, NumberHit = 19},
                new Shot(){Contact = Model.Enum.ContactType.Triple, NumberHit = 20},

                new Shot(){Contact = Model.Enum.ContactType.Triple, NumberHit = 18},
                new Shot(){Contact = Model.Enum.ContactType.Triple, NumberHit = 19},
                new Shot(){Contact = Model.Enum.ContactType.Triple, NumberHit = 20},

                new Shot(){Contact = Model.Enum.ContactType.DoubleBullsEye, NumberHit = 25},
                new Shot(){Contact = Model.Enum.ContactType.DoubleBullsEye, NumberHit = 25}
            };

            var shotBoard = players.CalculateForCricket200(shots);
            Assert.AreEqual(25, shotBoard.ElementAt(0).Value.Score);
        }

        [TestMethod]
        public async Task ScoreOnFifteensThenCloseFifteens()
        {
            var game = CricketGameServiceData.TwoPlayers();

            List<Player> players = game.Players;
            List<Shot> shots = new List<Shot>()
            {
                new Shot(){Contact = Model.Enum.ContactType.Triple, NumberHit = 15},
                new Shot(){Contact = Model.Enum.ContactType.Double, NumberHit = 15},
                new Shot(){Contact = Model.Enum.ContactType.Miss, NumberHit = 0},

                new Shot(){Contact = Model.Enum.ContactType.Triple, NumberHit = 15},
                new Shot(){Contact = Model.Enum.ContactType.Triple, NumberHit = 15},
                new Shot(){Contact = Model.Enum.ContactType.Single, NumberHit = 16},

            };

            var shotBoard = players.CalculateForCricket200(shots);
            Assert.AreEqual(30, shotBoard.ElementAt(0).Value.Score);
            Assert.AreEqual(0, shotBoard.ElementAt(1).Value.Score);
        }

        [TestMethod]
        public async Task OneTriple15()
        {
            var game = CricketGameServiceData.OnePlayer();

            List<Player> players = game.Players;
            List<Shot> shots = new List<Shot>()
            {
                new Shot(){Contact = Model.Enum.ContactType.Triple, NumberHit = 15}
            };

            var shotBoard = players.CalculateForCricket200(shots);
            Assert.AreEqual(0, shotBoard.ElementAt(0).Value.Score);
            Assert.AreEqual(3, shotBoard.ElementAt(0).Value.MarksFor[15]);
        }

        [TestMethod]
        public async Task OneTriple16()
        {
            var game = CricketGameServiceData.OnePlayer();

            List<Player> players = game.Players;
            List<Shot> shots = new List<Shot>()
            {
                new Shot(){Contact = Model.Enum.ContactType.Triple, NumberHit = 16}
            };

            var shotBoard = players.CalculateForCricket200(shots);
            Assert.AreEqual(0, shotBoard.ElementAt(0).Value.Score);
            Assert.AreEqual(3, shotBoard.ElementAt(0).Value.MarksFor[16]);
        }

        [TestMethod]
        public async Task OneTriple17()
        {
            var game = CricketGameServiceData.OnePlayer();

            List<Player> players = game.Players;
            List<Shot> shots = new List<Shot>()
            {
                new Shot(){Contact = Model.Enum.ContactType.Triple, NumberHit = 17}
            };

            var shotBoard = players.CalculateForCricket200(shots);
            Assert.AreEqual(0, shotBoard.ElementAt(0).Value.Score);
            Assert.AreEqual(3, shotBoard.ElementAt(0).Value.MarksFor[17]);
        }

        [TestMethod]
        public async Task OneTriple18()
        {
            var game = CricketGameServiceData.OnePlayer();

            List<Player> players = game.Players;
            List<Shot> shots = new List<Shot>()
            {
                new Shot(){Contact = Model.Enum.ContactType.Triple, NumberHit = 18}
            };

            var shotBoard = players.CalculateForCricket200(shots);
            Assert.AreEqual(0, shotBoard.ElementAt(0).Value.Score);
            Assert.AreEqual(3, shotBoard.ElementAt(0).Value.MarksFor[18]);
        }

        [TestMethod]
        public async Task OneTriple19()
        {
            var game = CricketGameServiceData.OnePlayer();

            List<Player> players = game.Players;
            List<Shot> shots = new List<Shot>()
            {
                new Shot(){Contact = Model.Enum.ContactType.Triple, NumberHit = 19}
            };

            var shotBoard = players.CalculateForCricket200(shots);
            Assert.AreEqual(0, shotBoard.ElementAt(0).Value.Score);
            Assert.AreEqual(3, shotBoard.ElementAt(0).Value.MarksFor[19]);
        }

        [TestMethod]
        public async Task OneTriple20()
        {
            var game = CricketGameServiceData.OnePlayer();

            List<Player> players = game.Players;
            List<Shot> shots = new List<Shot>()
            {
                new Shot(){Contact = Model.Enum.ContactType.Triple, NumberHit = 20}
            };

            var shotBoard = players.CalculateForCricket200(shots);
            Assert.AreEqual(0, shotBoard.ElementAt(0).Value.Score);
            Assert.AreEqual(3, shotBoard.ElementAt(0).Value.MarksFor[20]);
        }

        [TestMethod]
        public async Task OneDoubleBull()
        {
            var game = CricketGameServiceData.OnePlayer();

            List<Player> players = game.Players;
            List<Shot> shots = new List<Shot>()
            {
                new Shot(){Contact = Model.Enum.ContactType.Triple, NumberHit = 15}
            };

            var shotBoard = players.CalculateForCricket200(shots);
            Assert.AreEqual(0, shotBoard.ElementAt(0).Value.Score);
            Assert.AreEqual(3, shotBoard.ElementAt(0).Value.MarksFor[15]);
        }


    }
}
