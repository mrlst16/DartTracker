using CommonStandard.Helpers;
using Couchbase.Lite;
using DartTracker.Data.Interface.DataServices;
using DartTracker.Interface.Factories;
using DartTracker.Mobile.Interface.Factories;
using DartTracker.Mobile.Mappers;
using DartTracker.Mobile.Services;
using DartTracker.Model.Enum;
using DartTracker.Model.Games;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
            LoadGameCommand = LoadGame();

            if (FileHelper.TryLoadEmbeddedResource<MainPage>("DartTracker.Mobile.Resources.darts.txt", out string result))
            {
                _about = result;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private List<GameTypeDescription> _gameTypeDescriptions
            = new List<GameTypeDescription>()
            {
                new GameTypeDescription(){
                    Name = "Cricket 200",
                    GameType = Model.Enum.GameType.Cricket200
                }
            };

        public List<string> GameTypeNames => _gameTypeDescriptions.Select(x => x.Name).ToList();

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

        private GameTypeDescription _selectedGameDescription;

        private string _gameType = "Darts";
        public string GameType
        {
            get => _gameType;
            set
            {
                _gameType = value;
                if (_gameType == "Darts") return;

                _selectedGameDescription = _gameTypeDescriptions.FirstOrDefault(x => x.Name.ToLowerInvariant() == _gameType.ToLowerInvariant());
                if (string.IsNullOrWhiteSpace(_selectedGameDescription?.About))
                {
                    _selectedGameDescription.About = AboutGame(_selectedGameDescription.GameType);
                }
                this.About = _selectedGameDescription.About;
                var args = new PropertyChangedEventArgs(nameof(GameType));
                PropertyChanged?.Invoke(this, args);
            }
        }

        private string _about = string.Empty;
        public string About
        {
            get => _about;
            set
            {
                _about = value;
                var args = new PropertyChangedEventArgs(nameof(About));
                PropertyChanged?.Invoke(this, args);
            }
        }

        private string AboutGame(GameType gameType)
        {
            string fileName = null;

            switch (gameType)
            {
                case Model.Enum.GameType.Cricket200:
                    fileName = "DartTracker.Mobile.Resources.cricket.txt";
                    break;
                default:
                    fileName = null;
                    break;
            }

            if (fileName == null) return string.Empty;

            if (FileHelper.TryLoadEmbeddedResource<MainPage>(fileName, out string result))
            {
                return result;
            }
            return string.Empty;
        }

        private ObservableCollection<string> _savedGames = new System.Collections.ObjectModel.ObservableCollection<string>();
        public ObservableCollection<string> SavedGames
        {
            get
            {
                var fromDatabase = _gameDataService
                  ?.GameIndexes
                  ?.Select(x => x.Name)
                  ?.ToList()
                      ?? new List<string>();
                _savedGames = new ObservableCollection<string>(fromDatabase);
                return _savedGames;
            }
        }

        public void AddSavedGameName(string name)
        {
            _savedGames.Add(name);
            var args = new PropertyChangedEventArgs(nameof(SavedGames));
            PropertyChanged?.Invoke(this, args);
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
                game.Type = _selectedGameDescription.GameType;
                DartTracker.Mobile.App.GameService = await _gameServiceFactory.Create(game);

                var scoreboardService = _scoreboardServiceFactory.Create(game.Type);
                var scoreboard = scoreboardService.BuildScoreboard(DartTracker.Mobile.App.GameService);

                var page = new DartboardPage(
                    DartTracker.Mobile.App.GameService,
                    new DrawDartboardService(),
                    new ShotPointToShotMapper(),
                    scoreboard
                    );
                await Application.Current.MainPage.Navigation.PushAsync(page);
            });
        }

        private string _selectedSavedGame;
        public string SelectedSavedGame
        {
            get => _selectedSavedGame;
            set
            {
                _selectedSavedGame = value;
                var args = new PropertyChangedEventArgs(nameof(SelectedSavedGame));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public Command LoadGameCommand { get; protected set; }
        public Command LoadGame()
        {
            return new Command(async () =>
            {
                if (string.IsNullOrWhiteSpace(SelectedSavedGame))
                {
                    await Application.Current.MainPage.DisplayAlert("Put in better information", "Select a game", "Thanks for telling me.");
                    return;
                }
                var game = _gameDataService.LoadGame(SelectedSavedGame);

                var gameService = await _gameServiceFactory.Create(game);
                App.GameService = gameService;
                var scoreboardService = _scoreboardServiceFactory.Create(game.Type);
                var scoreboard = scoreboardService.BuildScoreboard(gameService);

                var page = new DartboardPage(
                    gameService,
                    new DrawDartboardService(),
                    new ShotPointToShotMapper(),
                    scoreboard
                    );

                await Application.Current.MainPage.Navigation.PushAsync(page);
            });
        }

    }
}
