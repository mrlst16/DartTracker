using DartTracker.Interface.Games;
using DartTracker.Lib.Games.Cricket;
using DartTracker.Mobile.Interface.Services.Scoreboard;
using DartTracker.Mobile.ViewModels;
using DartTracker.Model.Games;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DartTracker.Mobile.Services
{
    public class CricketScoreboardService : IScoreboardService
    {
        public Page BuildScoreboard(IGameService gameService)
        {
            var result = new CricketScoreboardPage(gameService);
            result.Title = "ScoreBoard";
            result.BindingContext = new CricketViewModel(gameService);
            return result;
        }
    }
}
