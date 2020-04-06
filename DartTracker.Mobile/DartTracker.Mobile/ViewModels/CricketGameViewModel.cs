using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DartTracker.Lib.Games.Cricket;
using DartTracker.Lib.Helpers;
using DartTracker.Model.Events;
using DartTracker.Model.Games;
using DartTracker.Model.Players;
using Xamarin.Forms;

namespace DartTracker.Mobile.ViewModels
{
    public class CricketGameViewModel : INotifyPropertyChanged
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

        public Command HitSingleCommand { get; }
        public Command HitDoubleCommand { get; }
        public Command HitTripleCommand { get; }
        public Command HitBullsEyeCommand { get; }
        public Command HitDoubleBullCommand { get; }
        public Command MissCommand { get; }
        public Command UndoCommand { get; }

        public CricketGameService GameService { get; protected set; }

        public event EventHandler GameWonEvent;

        public CricketGameViewModel(
            int players
            )
        {
            Game game = new Game();
            game.Players = new List<Player>();
            for (int i = 0; i < players; i++)
            {
                game.Players.Add(new Player()
                {
                    GameID = game.ID,
                    Name = i.ToString(),
                    Order = i,
                    Score = 0
                });
            }

            GameService = new CricketGameService(game);

            GameService.GameWonEvent += (sender, eventArgs) =>
            {
                if (eventArgs is GameWonEvenArgs args)
                {
                    this.GameWonEvent?.Invoke(this, args);
                }
            };

            HitSingleCommand = new Command(async (i) =>
            {
                if (i is string ii && int.TryParse(ii, out int ri))
                {
                    await GameService.TakeShot(ri, Model.Enum.ContactType.Single);
                    var args = new PropertyChangedEventArgs(nameof(PlayerScoreboards));
                    PropertyChanged?.Invoke(this, args);
                }
            });

            HitDoubleCommand = new Command(async (i) =>
            {
                if (i is string ii && int.TryParse(ii, out int ri))
                {
                    await GameService.TakeShot(ri, Model.Enum.ContactType.Double);
                    var args = new PropertyChangedEventArgs(nameof(PlayerScoreboards));
                    PropertyChanged?.Invoke(this, args);
                }
            });

            HitTripleCommand = new Command(async (i) =>
            {
                if (i is string ii && int.TryParse(ii, out int ri))
                {
                    await GameService.TakeShot(ri, Model.Enum.ContactType.Triple);
                    var args = new PropertyChangedEventArgs(nameof(PlayerScoreboards));
                    PropertyChanged?.Invoke(this, args);
                }
            });

            HitBullsEyeCommand = new Command(async () =>
            {
                await GameService.TakeShot(25, Model.Enum.ContactType.BullsEye);
                var args = new PropertyChangedEventArgs(nameof(PlayerScoreboards));
                PropertyChanged?.Invoke(this, args);
            });

            HitDoubleBullCommand = new Command(async () =>
            {
                await GameService.TakeShot(25, Model.Enum.ContactType.DoubleBullsEye);
                var args = new PropertyChangedEventArgs(nameof(PlayerScoreboards));
                PropertyChanged?.Invoke(this, args);
            });

            MissCommand = new Command(async (i) =>
            {
                await GameService.TakeShot(0, Model.Enum.ContactType.Miss);
                var args = new PropertyChangedEventArgs(nameof(PlayerScoreboards));
                PropertyChanged?.Invoke(this, args);
            });

            UndoCommand = new Command(async (i) =>
            {
                await GameService.RemoveLastShot();
                var args = new PropertyChangedEventArgs(nameof(PlayerScoreboards));
                PropertyChanged?.Invoke(this, args);
            });

        }

        private ObservableCollection<CricketPlayerScoreboardVM> Calculate()
        {
            ObservableCollection<CricketPlayerScoreboardVM> result
                = new ObservableCollection<CricketPlayerScoreboardVM>();
            var players = GameService.Game.Players;
            var playerUp = GameService.Incrementor.PlayerUp;
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
                if (player.Order == playerUp) {
                    vm.BackgroundColor = "Pink";
                    vm.TextColor = "White";
                }
                result.Add(vm);
            }
            return result;
        }


    }
}
