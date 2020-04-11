using DartTracker.Mobile.Interface.Services.Scoreboard;
using DartTracker.Model.Games;
using DartTracker.Model.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace DartTracker.Mobile.Lib.Services
{
    public class Cricket200ScoreboardService : IScoreboardService
    {
        public View BuildScoreboard(Game game)
        {
            var result = new Grid();
            AddColumnDefinitions(ref result);
            AddHeaders(ref result);
            //AddRowefinitions(ref result, game.Players.Count);
            StartPlayers(ref result, game.Players);
            return result;
        }

        private void AddColumnDefinitions(ref Grid grid)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
        }

        private void AddRowefinitions(ref Grid grid, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition()
                {
                    Height = new GridLength(1, GridUnitType.Star)
                });
            }
        }

        private void AddHeaders(ref Grid grid)
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

        private void StartPlayers(ref Grid grid, List<Player> players)
        {
            var orderedPlayers = players.OrderBy(x => x.Order).ToList();
            for (int i = 0; i < orderedPlayers.Count; i++)
            {
                var player = orderedPlayers[i];
                var row = i + i;
                grid.Children.Add(new Label() { Text = player?.Order.ToString() }, 0, row);
                grid.Children.Add(new Label() { Text = player?.Score.ToString() }, 1, row);
                grid.Children.Add(new Label() { Text = Score(player, 15) }, 2, row);
                grid.Children.Add(new Label() { Text = Score(player, 16) }, 3, row);
                grid.Children.Add(new Label() { Text = Score(player, 17) }, 4, row);
                grid.Children.Add(new Label() { Text = Score(player, 18) }, 5, row);
                grid.Children.Add(new Label() { Text = Score(player, 19) }, 6, row);
                grid.Children.Add(new Label() { Text = Score(player, 20) }, 7, row);
                grid.Children.Add(new Label() { Text = Score(player, 25) }, 8, row);
            }
        }

        private string Score(Player player, int number)
            => player.Marks.TryGetValue(number, out int res) ? res.ToString() : "0";
    }
}
