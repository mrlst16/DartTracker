using Couchbase.Lite;
using DartTracker.Data.Interface.DataServices;
using DartTracker.Interface.Factories;
using DartTracker.Mobile.Interface.Factories;
using DartTracker.Mobile.Mappers;
using DartTracker.Mobile.Services;
using DartTracker.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Xamarin.Forms;

namespace DartTracker.Mobile.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly IScoreboardServiceFactory _scoreboardServiceFactory;
        private readonly IGameFactory<int> _gameFactory;
        private readonly IGameServiceFactory _gameServiceFactory;
        private readonly IGameDataService _gameDataService;

        public MainPageViewModel(
            IScoreboardServiceFactory scoreboardServiceFactory,
            IGameFactory<int> gameFactory,
            IGameServiceFactory gameServiceFactory,
            IGameDataService gameDataService
            )
        {
            _scoreboardServiceFactory = scoreboardServiceFactory;
            _gameFactory = gameFactory;
            _gameServiceFactory = gameServiceFactory;
            _gameDataService = gameDataService;
            NewGameCommand = NewGame();
        }

        private DatabaseConfiguration databaseConfiguration = new DatabaseConfiguration
        {
            Directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "darttracker")
        };

        public event PropertyChangedEventHandler PropertyChanged;

        public Command NewGameCommand { get; protected set; }

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
            get =>  _gameDataService
                    ?.GameIndexes
                    ?.Select(x => x.Name)
                    ?.ToList()
                        ?? new List<string>();
        }

        private GameType ToGameType(string str)
        {
            switch (str.ToLowerInvariant())
            {
                case "cricket 200":
                    return Model.Enum.GameType.Cricket200;
                default:
                    return 0;
            }
        }

        private Command NewGame()
        {
            return new Command(async () =>
            {
                if (NumberOfPlayers <= 0 || GameType.ToLowerInvariant() == "darts")
                {
                    await Application.Current.MainPage.DisplayAlert("Put in better information", "Select a game type and number of players", "Thanks for telling me");
                    return;
                }
                var game = _gameFactory.Create(NumberOfPlayers);
                game.Type = ToGameType(GameType);
                var gameService = await _gameServiceFactory.Create(game);
                DartTracker.Mobile.App.GameService = gameService;

                var scoreboardService = _scoreboardServiceFactory.Create(game.Type);
                var scoreboard = scoreboardService.BuildScoreboard(gameService);

                var page = new DartboardPage(
                    gameService,
                    new DrawDartboardService(),
                    new ShotPointToShotMapper(),
                    scoreboard
                    );
                page.BindingContext = page;
                await Application.Current.MainPage.Navigation.PushAsync(page);
            });
        }
    }
}
