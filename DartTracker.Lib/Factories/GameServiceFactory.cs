using DartTracker.Interface.Factories;
using DartTracker.Interface.Games;
using DartTracker.Interface.Mapper;
using DartTracker.Lib.Games;
using DartTracker.Lib.Games.Cricket;
using DartTracker.Model.Enum;
using DartTracker.Model.Games;
using DartTracker.Model.Players;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DartTracker.Lib.Factories
{
    public class GameServiceFactory : IGameServiceFactory
    {

        public async Task<IGameService> Create(Game game)
        {
            switch (game.Type)
            {
                case GameType.Cricket:
                    return new CricketGameService(game);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
