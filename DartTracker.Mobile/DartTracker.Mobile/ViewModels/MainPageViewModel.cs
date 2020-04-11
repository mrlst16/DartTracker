using DartTracker.Lib.Games.Cricket;
using DartTracker.Mobile.Interface.Factories;
using DartTracker.Mobile.Lib.Factories;
using DartTracker.Mobile.Lib.Mappers;
using DartTracker.Model.Enum;
using DartTracker.Model.Games;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace DartTracker.Mobile.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Command GoToGameCommand { get; protected set; }

        private int _numPlayers = 1;

        public int NumberOfPlayers
        {
            get => _numPlayers;
            set
            {
                _numPlayers = value;
                var args = new PropertyChangedEventArgs(nameof(NumberOfPlayers));
                PropertyChanged?.Invoke(this, args);
            }
        }

        private string _gameType = "Darts";

        public string GameType
        {
            get => _gameType;
            set
            {
                _gameType = value;
                var args = new PropertyChangedEventArgs(nameof(GameType));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public List<string> SavedGames
        {
            get
            {
                return new List<string>() { "game1" };
            }
        }

        public GameType ToGameType(string str)
        {
            switch (str.ToLowerInvariant())
            {
                case "cricket 200":
                    return Model.Enum.GameType.Cricket200;
                default:
                    return 0;
            }
        }

        private IScoreboardServiceFactory _scoreboardServiceFactory
            = new ScoreboardServiceFactory();

        public MainPageViewModel()
        {
            GoToGameCommand = GoToGame();

        }

        private Command GoToGame()
        {
            return new Command(async () =>
             {
                 var vm = new CricketGameViewModel(NumberOfPlayers);
                 var gameType = ToGameType(GameType);
                 var scoreboardService = _scoreboardServiceFactory.Create(gameType);
                 var scoreboard = scoreboardService.BuildScoreboard(vm.GameService.Game);
                 DartTracker.Mobile.MainPage.Game = vm.GameService.Game;

                 var page = new Dartboard(
                     vm.GameService,
                     new ShotPointToShotMapper(),
                     scoreboard
                     );
                 page.BindingContext = page;
                 await Application.Current.MainPage.Navigation.PushAsync(page);
             });
        }
    }
}
