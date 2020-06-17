using DartTracker.Interface.Games;
using DartTracker.Mobile.Interface.ViewModels;
using DartTracker.Model.Events;
using DartTracker.Model.Shooting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace DartTracker.Mobile.ViewModels
{
    public class OhOneScoreboardVM : INotifyPropertyChanged, IScoreboardVM
    {
        public IGameService GameService { get; set; }

        public event EventHandler GameWonEvent;

        public event PropertyChangedEventHandler PropertyChanged;

        public OhOneScoreboardVM(
            IGameService gameService
            )
        {
            GameService = gameService;

            GameService.GameWonEvent += (sender, eventArgs) =>
            {
                if (eventArgs is GameWonEvenArgs args)
                {
                    this.GameWonEvent?.Invoke(this, args);
                }
            };
        }

        private ObservableCollection<PlayerScoreboardVM> Calculate()
        {
            ObservableCollection<PlayerScoreboardVM> result
                = new ObservableCollection<PlayerScoreboardVM>();
            var players = GameService.Game.Players;
            foreach (var player in players)
            {
                var vm = new PlayerScoreboardVM()
                {
                    PlayerName = player.Name,
                    Score = player.Score
                };
                if (player.Order == GameService.PlayerUp)
                {
                    vm.BackgroundColor = "LightGray";
                    vm.TextColor = "White";
                }
                result.Add(vm);
            }
            return result;
        }

        public ObservableCollection<PlayerScoreboardVM> PlayerScoreboards
        {
            get
            {
                var result = Calculate();
                return result;
            }
        }

        public async Task TakeShot(Shot shot)
        {
            await GameService.TakeShot(shot);
            var args = new PropertyChangedEventArgs(nameof(PlayerScoreboards));
            PropertyChanged?.Invoke(this, args);
        }

        public async Task RemoveLastShot()
        {
            await GameService.RemoveLastShot();
            var args = new PropertyChangedEventArgs(nameof(PlayerScoreboards));
            PropertyChanged?.Invoke(this, args);
        }
    }
}
