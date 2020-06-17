using DartTracker.Data.Interface.DataServices;
using DartTracker.Lib.Mappers;
using DartTracker.Mobile.Factories;
using DartTracker.Mobile.Interface.Factories;
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
        private SaveGameVM _saveGameViewModel;

        public SaveGamePage(
            MainPageViewModel viewModel,
            IScoreboardServiceFactory scoreboardServiceFactory,
            IGameDataService gameDataService
            )
        {
            _viewModel = viewModel;
            _scoreboardServiceFactory = scoreboardServiceFactory;
            _gameDataService = gameDataService;
            _saveGameViewModel = new SaveGameVM();
            this.BindingContext = _saveGameViewModel;
            InitializeComponent();
        }

        private void FilenameTextChanged(object sender, TextChangedEventArgs e)
        {
            _saveGameViewModel.SaveAs = e.NewTextValue;
        }

        private async void CancelButtonClicked(object sender, System.EventArgs e)
        {
            var scoreboardService = _scoreboardServiceFactory.Create(App.GameService.Game.Type);
            GameViewModelFactory factory = new GameViewModelFactory();
            var viewModel = factory.Create(DartTracker.Mobile.App.GameService);

            var scoreboard = scoreboardService.BuildScoreboard(viewModel);

            var page = new DartboardPage(
                DartTracker.Mobile.App.GameService,
                new DrawDartboardService(DartTracker.Mobile.App.GameService),
                new ShotPointToShotMapper(App.DartboardDimensions),
                viewModel,
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
            if (string.IsNullOrWhiteSpace(_saveGameViewModel.SaveAs)) return;

            var success = _gameDataService.SaveGame(App.GameService.Game, _saveGameViewModel.SaveAs);
            if (success)
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
                _viewModel.AddSavedGameName(_saveGameViewModel.SaveAs);
            }
        }

    }
}
