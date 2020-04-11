using DartTracker.Interface.Factories;
using DartTracker.Model.Games;
using DartTracker.Model.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartTracker.Lib.Factories
{
    public class GameFromNumberOfPlayersFactory : IGameFactory<int>
    {
        public Game Create(int request)
        {
            Game result = new Game();
            result.ID = Guid.NewGuid();
            result.Players = new List<Player>();
            for (int i = 0; i < request; i++)
            {
                result.Players.Add(new Player()
                {
                    GameID = result.ID,
                    Name = i.ToString(),
                    Order = i,
                    Score = 0
                });
            }
            return result;
        }
    }
}
