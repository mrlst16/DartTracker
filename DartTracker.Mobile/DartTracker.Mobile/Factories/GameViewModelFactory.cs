using DartTracker.Interface.Games;
using DartTracker.Mobile.Interface.ViewModels;
using DartTracker.Mobile.ViewModels;

namespace DartTracker.Mobile.Factories
{
    public class GameViewModelFactory
    {
        public IScoreboardVM Create(IGameService gameService)
        {
            switch (gameService.Game.Type)
            {
                case Model.Enum.GameType.Cricket200:
                case Model.Enum.GameType.CricketCutthroat:
                    return new CricketScoreboardVM(gameService);
                case Model.Enum.GameType.ThreeOhOneOInOOut:
                    return new OhOneScoreboardVM(gameService);
                default:
                    return new CricketScoreboardVM(gameService);
            }
        }
    }
}
