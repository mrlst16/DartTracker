using DartTracker.Interface.Games;
using DartTracker.Mobile.Interface.ViewModels;
using DartTracker.Mobile.ViewModels;
using DartTracker.Model.Games;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartTracker.Mobile.Factories
{
    public class GameViewModelFactory
    {
        public IGameViewModel Create(IGameService gameService)
        {
            switch (gameService.Game.Type)
            {
                case Model.Enum.GameType.Cricket200:
                    return new CricketScoreboardViewModel(gameService);
                default:
                    return new CricketScoreboardViewModel(gameService);
            }
        }
    }
}
