using DartTracker.Data.Interface.DataServices;
using DartTracker.Mobile.Interface.Factories;
using DartTracker.Mobile.Mappers;
using DartTracker.Mobile.Services;
using DartTracker.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DartTracker.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SaveGamePage : ContentPage
    {
        private readonly MainPageViewModel _viewModel;
        private readonly IScoreboardServiceFactory _scoreboardServiceFactory;
        private readonly IGameDataService _gameDataService;

        private string _filename;

        public SaveGamePage(
            MainPageViewModel viewModel,
            IScoreboardServiceFactory scoreboardServiceFactory,
            IGameDataService gameDataService
            )
        {
            _viewModel = viewModel;
            _scoreboardServiceFactory = scoreboardServiceFactory;
            _gameDataService = gameDataService;
            InitializeComponent();
        }

        private void FilenameTextChanged(object sender, TextChangedEventArgs e)
        {
            _filename = e.NewTextValue;
        }

        private async void CancelButtonClicked(object sender, System.EventArgs e)
        {
            var scoreboardService = _scoreboardServiceFactory.Create(App.GameService.Game.Type);
            var scoreboard = scoreboardService.BuildScoreboard(DartTracker.Mobile.App.GameService);

            var page = new DartboardPage(
                DartTracker.Mobile.App.GameService,
                new DrawDartboardService(),
                new ShotPointToShotMapper(),
                scoreboard
                );
            await Application.Current.MainPage.Navigation.PopModalAsync();
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }

        private async void DontSaveButtonClicked(object sender, System.EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        private async void SaveButtonClicked(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_filename)) return;

            var success = _gameDataService.SaveGame(App.GameService.Game, _filename);
            if (success)
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
                _viewModel.AddSavedGameName(_filename);
            }
        }

    }
}
