using DartTracker.Mobile.Interface.Factories;
using DartTracker.Mobile.Interface.Services.Scoreboard;
using DartTracker.Mobile.Lib.Services;
using DartTracker.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartTracker.Mobile.Lib.Factories
{
    public class ScoreboardServiceFactory : IScoreboardServiceFactory
    {
        public IScoreboardService Create(GameType gameType)
        {
            switch (gameType)
            {
                case GameType.Cricket200:
                    return new Cricket200ScoreboardService();
                default:
                    return new Cricket200ScoreboardService();
            }
        }
    }
}
