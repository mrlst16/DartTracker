﻿using DartTracker.Lib.Games.Cricket;
using DartTracker.Model.Shooting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using NSubstitute;
using System.Threading.Tasks;
using DartTracker.Model.Players;
using DartTracker.Lib.Test.Helpers;
using System.Linq;
using DartTracker.Lib.Factories;
using DartTracker.Lib.Test.Data;

namespace DartTracker.Lib.Test.Games.Cricket
{
    [TestClass]
    public class CricketGameServiceTests
    {
        [TestMethod]
        public async Task TwoTurns_NoHits()
        {

            var service = new Cricket200GameService(
                GamesData.TwoPlayers()
                );

            Assert.AreEqual(2, service.Game.Players.Count);
            Assert.AreEqual(Model.Enum.GameType.Cricket200, service.Game.Type);

            Assert.AreEqual(0, service.ShotCount);
            Assert.AreEqual(1, service.Round);
            Assert.AreEqual(0, service.PlayerUp);

            await service.TakeShot(10, Model.Enum.ContactType.Miss);
            await service.TakeShot(10, Model.Enum.ContactType.Miss);
            await service.TakeShot(10, Model.Enum.ContactType.Miss);

            Assert.AreEqual(3, service.ShotCount);
            Assert.AreEqual(1, service.Round);
            Assert.AreEqual(1, service.PlayerUp);

            await service.TakeShot(10, Model.Enum.ContactType.Miss);
            await service.TakeShot(10, Model.Enum.ContactType.Miss);
            await service.TakeShot(10, Model.Enum.ContactType.Miss);

            Assert.AreEqual(6, service.ShotCount);
            Assert.AreEqual(2, service.Round);
            Assert.AreEqual(0, service.PlayerUp);

            await service.TakeShot(10, Model.Enum.ContactType.Miss);
            await service.TakeShot(10, Model.Enum.ContactType.Miss);
            await service.TakeShot(10, Model.Enum.ContactType.Miss);

            Assert.AreEqual(9, service.ShotCount);
            Assert.AreEqual(2, service.Round);
            Assert.AreEqual(1, service.PlayerUp);

            await service.TakeShot(10, Model.Enum.ContactType.Miss);
            await service.TakeShot(10, Model.Enum.ContactType.Miss);
            await service.TakeShot(10, Model.Enum.ContactType.Miss);

            Assert.AreEqual(12, service.ShotCount);
            Assert.AreEqual(3, service.Round);
            Assert.AreEqual(0, service.PlayerUp);

            Assert.AreEqual(0, service.Game.Players[0].Score);
            Assert.AreEqual(0, service.Game.Players[1].Score);
        }

        [TestMethod]
        public async Task BothBoardsClosed_HitsAllTripples_WinTwoDoubleBulls()
        {
            var service = new Cricket200GameService(
                GamesData.TwoPlayers()
                );

            Assert.AreEqual(2, service.Game.Players.Count);
            Assert.AreEqual(Model.Enum.GameType.Cricket200, service.Game.Type);
            Assert.IsFalse(await service.GameWon());

            Assert.AreEqual(0, service.ShotCount);
            Assert.AreEqual(1, service.Round);
            Assert.AreEqual(0, service.PlayerUp);

            await service.TakeShot(15, Model.Enum.ContactType.Triple);
            await service.TakeShot(16, Model.Enum.ContactType.Triple);
            await service.TakeShot(17, Model.Enum.ContactType.Triple);

            Assert.AreEqual(3, service.ShotCount);
            Assert.AreEqual(1, service.Round);
            Assert.AreEqual(1, service.PlayerUp);

            await service.TakeShot(15, Model.Enum.ContactType.Triple);
            await service.TakeShot(16, Model.Enum.ContactType.Triple);
            await service.TakeShot(17, Model.Enum.ContactType.Triple);

            Assert.AreEqual(6, service.ShotCount);
            Assert.AreEqual(2, service.Round);
            Assert.AreEqual(0, service.PlayerUp);

            await service.TakeShot(18, Model.Enum.ContactType.Triple);
            await service.TakeShot(19, Model.Enum.ContactType.Triple);
            await service.TakeShot(20, Model.Enum.ContactType.Triple);

            Assert.AreEqual(9, service.ShotCount);
            Assert.AreEqual(2, service.Round);
            Assert.AreEqual(1, service.PlayerUp);

            await service.TakeShot(18, Model.Enum.ContactType.Triple);
            await service.TakeShot(19, Model.Enum.ContactType.Triple);
            await service.TakeShot(20, Model.Enum.ContactType.Triple);

            Assert.AreEqual(12, service.ShotCount);
            Assert.AreEqual(3, service.Round);
            Assert.AreEqual(0, service.PlayerUp);

            await service.TakeShot(25, Model.Enum.ContactType.DoubleBullsEye);

            Assert.IsFalse(await service.GameWon());

            await service.TakeShot(25, Model.Enum.ContactType.DoubleBullsEye);
            Assert.AreEqual(25, service.Game.Players[0].Score);
            Assert.IsTrue(await service.GameWon());
        }

        [TestMethod]
        public async Task PlaySaveAndReloadStateFromFactory()
        {
            var loaded = await GamesData.TwoPlayersOneTurnAllDoubleBulls();
            GameServiceFactory factory = new GameServiceFactory();
            var result = await factory.Create(loaded.Game);

            Assert.IsTrue(result.Game.Players.Any());
            Assert.AreNotEqual(0, result.Game.Players[0].Score);
            Assert.AreEqual(75, result.Game.Players[0].Score);
        }


    }
}
