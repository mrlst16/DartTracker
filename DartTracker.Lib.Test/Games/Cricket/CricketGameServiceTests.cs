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

            Assert.AreEqual(0, service.ShotCount());
            Assert.AreEqual(1, service.Round);
            Assert.AreEqual(0, service.PlayerMarker);

            await service.TakeShot(10, Model.Enum.ContactType.Miss);
            await service.TakeShot(10, Model.Enum.ContactType.Miss);
            await service.TakeShot(10, Model.Enum.ContactType.Miss);

            Assert.AreEqual(3, service.ShotCount());
            Assert.AreEqual(1, service.Round);
            Assert.AreEqual(1, service.PlayerMarker);

            await service.TakeShot(10, Model.Enum.ContactType.Miss);
            await service.TakeShot(10, Model.Enum.ContactType.Miss);
            await service.TakeShot(10, Model.Enum.ContactType.Miss);

            Assert.AreEqual(6, service.ShotCount());
            Assert.AreEqual(2, service.Round);
            Assert.AreEqual(0, service.PlayerMarker);

            await service.TakeShot(10, Model.Enum.ContactType.Miss);
            await service.TakeShot(10, Model.Enum.ContactType.Miss);
            await service.TakeShot(10, Model.Enum.ContactType.Miss);

            Assert.AreEqual(9, service.ShotCount());
            Assert.AreEqual(2, service.Round);
            Assert.AreEqual(1, service.PlayerMarker);

            await service.TakeShot(10, Model.Enum.ContactType.Miss);
            await service.TakeShot(10, Model.Enum.ContactType.Miss);
            await service.TakeShot(10, Model.Enum.ContactType.Miss);

            Assert.AreEqual(12, service.ShotCount());
            Assert.AreEqual(3, service.Round);
            Assert.AreEqual(0, service.PlayerMarker);
        }

        [TestMethod]
        public async Task BothBoardsClosed_HitsAllTripples_WinOnNextBullseye()
        {
            var service = new CricketGameService(
                CricketGameServiceData.TwoPlayers()
                );

            Assert.AreEqual(2, service.Game.Players.Count);
            Assert.AreEqual(Model.Enum.GameType.Cricket, service.Game.Type);

            Assert.AreEqual(0, service.ShotCount());
            Assert.AreEqual(1, service.Round);
            Assert.AreEqual(0, service.PlayerMarker);

            await service.TakeShot(15, Model.Enum.ContactType.Triple);
            await service.TakeShot(16, Model.Enum.ContactType.Triple);
            await service.TakeShot(17, Model.Enum.ContactType.Triple);

            Assert.AreEqual(3, service.ShotCount());
            Assert.AreEqual(1, service.Round);
            Assert.AreEqual(1, service.PlayerMarker);

            await service.TakeShot(15, Model.Enum.ContactType.Triple);
            await service.TakeShot(16, Model.Enum.ContactType.Triple);
            await service.TakeShot(17, Model.Enum.ContactType.Triple);

            Assert.AreEqual(6, service.ShotCount());
            Assert.AreEqual(2, service.Round);
            Assert.AreEqual(0, service.PlayerMarker);

            await service.TakeShot(18, Model.Enum.ContactType.Triple);
            await service.TakeShot(19, Model.Enum.ContactType.Triple);
            await service.TakeShot(20, Model.Enum.ContactType.Triple);

            Assert.AreEqual(9, service.ShotCount());
            Assert.AreEqual(2, service.Round);
            Assert.AreEqual(1, service.PlayerMarker);

            await service.TakeShot(18, Model.Enum.ContactType.Triple);
            await service.TakeShot(19, Model.Enum.ContactType.Triple);
            await service.TakeShot(20, Model.Enum.ContactType.Triple);

            Assert.AreEqual(12, service.ShotCount());
            Assert.AreEqual(3, service.Round);
            Assert.AreEqual(0, service.PlayerMarker);

        }

    }
}
