using CommonStandard.Interface.Repository;
using CommonStandard.Repository;
using Couchbase.Lite;
using DartTracker.Data.Interface.DataServices;
using DartTracker.Data.Model;
using DartTracker.Data.Services;
using DartTracker.Interface.Factories;
using DartTracker.Interface.Games;
using DartTracker.Lib.Factories;
using DartTracker.Mobile.Factories;
using DartTracker.Mobile.Interface.Factories;
using DartTracker.Mobile.ViewModels;
using DartTracker.Model.Drawing;
using DartTracker.Model.Games;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace DartTracker.Mobile
{
    public partial class App : Application
    {
        public static IGameService GameService { get; set; }
        public static DartboardDimensions DartboardDimensions { get; set; }

        private readonly IScoreboardServiceFactory _scoreboardServiceFactory
            = new ScoreboardServiceFactory();
        private readonly IGameFactory<int> _gameFactory
            = new GameFromNumberOfPlayersFactory();
        private readonly IGameServiceFactory _gameServiceFactory
            = new GameServiceFactory();
        private readonly IRepository<Entity<List<EntityIndex>>, string> _gameIndexResposity;
        private readonly IRepository<Entity<Game>, string> _gameResposity;
        private readonly IGameDataService _gameDataService;

        private MainPageViewModel viewModel;

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

        public App()
        {
            InitializeComponent();

            _gameIndexResposity = new CouchbaseJsonRepository<Entity<List<EntityIndex>>>(DataBase);
            _gameResposity = new CouchbaseJsonRepository<Entity<Game>>(DataBase);
            _gameDataService = new GameDataService(_gameIndexResposity, _gameResposity);
            viewModel = new MainPageViewModel(
                   _scoreboardServiceFactory,
                   _gameFactory,
                   _gameServiceFactory,
                   _gameDataService
               );

            var page = new NavigationPage(new MainPage(viewModel));
            page.Popped += PromptToSaveGame();


            page.BindingContext = viewModel;
            MainPage = page;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private EventHandler<NavigationEventArgs> PromptToSaveGame()
            => new EventHandler<NavigationEventArgs>(async (s, e) =>
                {
                    if (e.Page.Title.ToLowerInvariant() == "dartboard")
                    {
                        var page = new SaveGamePage(
                            viewModel,
                            _scoreboardServiceFactory,
                            _gameDataService
                            );
                        page.BindingContext = viewModel;
                        await Application.Current.MainPage.Navigation.PushModalAsync(page);
                    }
                });
    }
}
