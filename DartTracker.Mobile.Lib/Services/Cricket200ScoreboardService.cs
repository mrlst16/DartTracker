using DartTracker.Mobile.Interface.Services.Scoreboard;
using DartTracker.Model.Games;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DartTracker.Mobile.Lib.Services
{
    public class Cricket200ScoreboardService : IScoreboardService
    {
        public View BuildScoreboard(Game game)
        {
            var result = new Grid();
            AddHeaders(result);
            return result;
        }

        private void AddHeaders(Grid grid)
        {
            grid.Children.Add(new Label() { Text = "Player" }, 0, 0);
            grid.Children.Add(new Label() { Text = "Score" }, 1, 0);
            grid.Children.Add(new Label() { Text = "15" }, 2, 0);
            grid.Children.Add(new Label() { Text = "16" }, 3, 0);
            grid.Children.Add(new Label() { Text = "17" }, 4, 0);
            grid.Children.Add(new Label() { Text = "18" }, 5, 0);
            grid.Children.Add(new Label() { Text = "19" }, 6, 0);
            grid.Children.Add(new Label() { Text = "20" }, 7, 0);
            grid.Children.Add(new Label() { Text = "Bulls" }, 8, 0);
        }
    }
}
