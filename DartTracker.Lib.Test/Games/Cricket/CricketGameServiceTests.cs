using DartTracker.Lib.Games.Cricket;
using DartTracker.Lib.Test.Data.Cricket;
using DartTracker.Model.Shooting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DartTracker.Lib.Test.Games.Cricket
{
    [TestClass]
    public class CricketGameServiceTests
    {


        [TestMethod]
        public async Task TwoTurns_NoHits()
        {

            var service = new CricketGameService(
                CricketGameServiceData.TwoPlayers()
                );

            Assert.AreEqual(2, service.Game.Players.Count);
            Assert.AreEqual(Model.Enum.GameType.Cricket, service.Game.Type);

            Assert.IsTrue(service.ShotCount == 0);
            Assert.IsTrue(service.Round == 1);
            Assert.IsTrue(service.PlayerMarker == 0);

            service.TakeShot(10, Model.Enum.ContactType.Miss);
            service.TakeShot(10, Model.Enum.ContactType.Miss);
            service.TakeShot(10, Model.Enum.ContactType.Miss);

            Assert.IsTrue(service.ShotCount == 3);
            Assert.IsTrue(service.Round == 1);
            Assert.IsTrue(service.PlayerMarker == 1);

            service.TakeShot(10, Model.Enum.ContactType.Miss);
            service.TakeShot(10, Model.Enum.ContactType.Miss);
            service.TakeShot(10, Model.Enum.ContactType.Miss);

            Assert.IsTrue(service.ShotCount == 6);
            Assert.IsTrue(service.Round == 2);
            Assert.IsTrue(service.PlayerMarker == 0);

            service.TakeShot(10, Model.Enum.ContactType.Miss);
            service.TakeShot(10, Model.Enum.ContactType.Miss);
            service.TakeShot(10, Model.Enum.ContactType.Miss);

            Assert.IsTrue(service.ShotCount == 9);
            Assert.IsTrue(service.Round == 2);
            Assert.IsTrue(service.PlayerMarker == 0);

            service.TakeShot(10, Model.Enum.ContactType.Miss);
            service.TakeShot(10, Model.Enum.ContactType.Miss);
            service.TakeShot(10, Model.Enum.ContactType.Miss);

            Assert.IsTrue(service.ShotCount == 12);
            Assert.IsTrue(service.Round == 2);
            Assert.IsTrue(service.PlayerMarker == 1);
        }
    }
}
