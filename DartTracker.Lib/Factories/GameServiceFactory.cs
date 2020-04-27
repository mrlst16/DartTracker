using DartTracker.Interface.Factories;
using DartTracker.Interface.Games;
using DartTracker.Lib.Games.Cricket;
using DartTracker.Model.Enum;
using DartTracker.Model.Games;
using System;
using System.Threading.Tasks;

namespace DartTracker.Lib.Factories
{
    public class GameServiceFactory : IGameServiceFactory
    {

        public async Task<IGameService> Create(Game game)
        {
            switch (game.Type)
            {
                case GameType.Cricket200:
                    return new Cricket200GameService(game);
                default:
                    return new Cricket200GameService(game);
            }
        }
    }
}
