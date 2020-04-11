using DartTracker.Mobile.Interface.Factories;
using DartTracker.Mobile.Interface.Services.Scoreboard;
using DartTracker.Mobile.Services;
using DartTracker.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartTracker.Mobile.Factories
{
    public class ScoreboardServiceFactory : IScoreboardServiceFactory
    {
        public IScoreboardService Create(GameType gameType)
        {
            switch (gameType)
            {
                case GameType.Cricket200:
                    return new CricketScoreboardService();
                default:
                    return new CricketScoreboardService();
            }
        }
    }
}
