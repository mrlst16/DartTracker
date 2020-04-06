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

            var shotBoard = players.Calculate(shots);
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

            var shotBoard = players.Calculate(shots);
            Assert.AreEqual(30, shotBoard.ElementAt(0).Value.Score);
            Assert.AreEqual(0, shotBoard.ElementAt(1).Value.Score);
        }
    }
}
