using DartTracker.Interface.Games;
using DartTracker.Lib.Factories;
using DartTracker.Lib.Games.Cricket;
using DartTracker.Model.Games;
using DartTracker.Model.Players;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DartTracker.Lib.Test.Data.Cricket
{
    public static class CricketGameServiceData
    {
        public static Game OnePlayer()
        {
            Game result = new Game();
            Player player1 = new Player()
            {
                GameID = result.ID,
                Name = "Fredo",
                Order = 0
            };

            result.Players.Add(player1);
            return result;
        }

        public static Game TwoPlayers()
        {
            Game result = new Game();
            Player player1 = new Player()
            {
                GameID = result.ID,
                Name = "Fredo",
                Order = 0
            };
            Player player2 = new Player()
            {
                GameID = result.ID,
                Name = "Bobby K",
                Order = 1
            };

            result.Players.Add(player1);
            result.Players.Add(player2);
            return result;
        }

        public static async Task<IGameService> TwoPlayersOneTurnAllDoubleBulls()
        {
            GameServiceFactory factory = new GameServiceFactory();
            Game game = TwoPlayers();
            IGameService result = await factory.Create(game);

            await result.TakeShot(25, Model.Enum.ContactType.DoubleBullsEye);
            await result.TakeShot(25, Model.Enum.ContactType.DoubleBullsEye);
            await result.TakeShot(25, Model.Enum.ContactType.DoubleBullsEye);

            return result;
        }
    }
}
