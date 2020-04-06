using DartTracker.Model.Games;
using DartTracker.Model.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartTracker.Lib.Test.Data.Cricket
{
    public static class CricketGameServiceData
    {
        public static Game Onelayer()
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
    }
}
