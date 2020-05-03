using DartTracker.Lib.Games.OhOne;
using DartTracker.Lib.Test.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DartTracker.Lib.Test.Games.OhOne
{
    [TestClass]
    public class ThreeOhOneOInOOutGameServiceTests
    {
        [TestMethod]
        public async Task TwoPlayersHit180And120()
        {
            ThreeOhOneOinOOutGameService service
                = new ThreeOhOneOinOOutGameService(GamesData.TwoPlayers());

            await service.TakeShot(20, Model.Enum.ContactType.Triple);
            await service.TakeShot(20, Model.Enum.ContactType.Triple);
            await service.TakeShot(20, Model.Enum.ContactType.Triple);
            Assert.AreEqual(121, service.Game.Players[0].Score);

            await service.TakeShot(20, Model.Enum.ContactType.Double);
            await service.TakeShot(20, Model.Enum.ContactType.Double);
            await service.TakeShot(20, Model.Enum.ContactType.Double);
            Assert.AreEqual(181, service.Game.Players[1].Score);

        }

        [TestMethod]
        public async Task TwoPlayersGetTo21AndOneWins()
        {
            ThreeOhOneOinOOutGameService service
                = new ThreeOhOneOinOOutGameService(GamesData.TwoPlayers());

            await service.TakeShot(20, Model.Enum.ContactType.Triple);
            await service.TakeShot(20, Model.Enum.ContactType.Triple);
            await service.TakeShot(20, Model.Enum.ContactType.Triple);
            Assert.AreEqual(121, service.Game.Players[0].Score);

            await service.TakeShot(20, Model.Enum.ContactType.Triple);
            await service.TakeShot(20, Model.Enum.ContactType.Triple);
            await service.TakeShot(20, Model.Enum.ContactType.Triple);
            Assert.AreEqual(121, service.Game.Players[1].Score);

            await service.TakeShot(20, Model.Enum.ContactType.Triple);
            await service.TakeShot(20, Model.Enum.ContactType.Single);
            await service.TakeShot(20, Model.Enum.ContactType.Single);
            Assert.AreEqual(21, service.Game.Players[0].Score);

            await service.TakeShot(20, Model.Enum.ContactType.Triple);
            await service.TakeShot(20, Model.Enum.ContactType.Single);
            await service.TakeShot(20, Model.Enum.ContactType.Single);
            Assert.AreEqual(21, service.Game.Players[1].Score);

            //Player zero should bust
            await service.TakeShot(20, Model.Enum.ContactType.Single);
            Assert.AreEqual(1, service.Game.Players[0].Score);
            await service.TakeShot(20, Model.Enum.ContactType.Single);
            Assert.AreEqual(1, service.Game.Players[0].Score);

            //Player one should win
            await service.TakeShot(20, Model.Enum.ContactType.Single);
            Assert.AreEqual(1, service.Game.Players[1].Score);
            await service.TakeShot(1, Model.Enum.ContactType.Single);
            Assert.AreEqual(0, service.Game.Players[1].Score);

            Assert.IsTrue(await service.GameWon());
            Assert.AreEqual((await service.WinningPlayer()).ID, service.Game.Players[1].ID);
        }

    }
}
