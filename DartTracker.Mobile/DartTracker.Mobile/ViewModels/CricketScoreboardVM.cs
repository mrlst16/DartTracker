﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using DartTracker.Interface.Games;
using DartTracker.Mobile.Interface.ViewModels;
using DartTracker.Model.Events;
using DartTracker.Model.Shooting;

namespace DartTracker.Mobile.ViewModels
{
    public class CricketScoreboardVM : INotifyPropertyChanged, IScoreboardVM
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<CricketPlayerScoreboardVM> PlayerScoreboards
        {
            get
            {
                var result = Calculate();
                return result;
            }
        }

        public IGameService GameService { get; protected set; }

        public event EventHandler GameWonEvent;

        public CricketScoreboardVM(
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

        private ObservableCollection<CricketPlayerScoreboardVM> Calculate()
        {
            ObservableCollection<CricketPlayerScoreboardVM> result
                = new ObservableCollection<CricketPlayerScoreboardVM>();
            var players = GameService.Game.Players;
            foreach (var player in players)
            {
                var vm = new CricketPlayerScoreboardVM()
                {
                    PlayerName = player.Name,
                    Score = player.Score,
                    Fifteens = player.Marks[15],
                    Sixteens = player.Marks[16],
                    Seventeens = player.Marks[17],
                    Eighteens = player.Marks[18],
                    Nineteens = player.Marks[19],
                    Twentys = player.Marks[20],
                    Bulls = player.Marks[25]
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
