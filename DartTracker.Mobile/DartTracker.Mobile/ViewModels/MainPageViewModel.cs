using Couchbase.Lite;
using DartTracker.Interface.Factories;
using DartTracker.Lib.Factories;
using DartTracker.Lib.Games.Cricket;
using DartTracker.Mobile.Factories;
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
using System.Text;
using Xamarin.Forms;

namespace DartTracker.Mobile.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {

        private DatabaseConfiguration databaseConfiguration = new DatabaseConfiguration
        {
            Directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "darttracker")
        };

        private Database _database;
        private Database DataBase
        {
            get
            {
                if (_database == null)
                    _database = new Database("darttracker", databaseConfiguration);
                return _database;
            }
        }

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
                var result = new List<string>();

                var document = DataBase.GetDocument("activegames");
                if (document == null) return result;

                var val = document.GetArray("value");
                if (val == null) return result;

                result = val.Select(x => x.ToString()).ToList();
                return result;
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

        private readonly IScoreboardServiceFactory _scoreboardServiceFactory
            = new ScoreboardServiceFactory();
        private readonly IGameFactory<int> _gameFactory
            = new GameFromNumberOfPlayersFactory();
        private readonly IGameServiceFactory _gameServiceFactory
            = new GameServiceFactory();

        public MainPageViewModel()
        {
            GoToGameCommand = GoToGame();

        }

        private Command GoToGame()
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
                 DartTracker.Mobile.MainPage.GameService = gameService;

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
