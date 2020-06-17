using CommonStandard.Extensions;
using CommonStandard.Interface.Mappers;
using DartTracker.Interface.Games;
using DartTracker.Lib.Mappers;
using DartTracker.Mobile.Interface.Services.Drawing;
using DartTracker.Mobile.Interface.ViewModels;
using DartTracker.Mobile.ViewModels;
using DartTracker.Model.Events;
using DartTracker.Model.Shooting;
using SkiaSharp.Views.Forms;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DartTracker.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DartboardPage : TabbedPage
    {
        private readonly IGameService _gameService;
        private readonly IDrawDartboardService _drawDartboardService;
        private readonly IScoreboardVM _scoreboardViewModel;
        private IMapper<CommonStandard.Models.Math.Point, Shot> _shotPointToShotMapper;
        private readonly Page _scoreboard;

        DartboardViewModel _dartboardViewModel;

        public DartboardPage(
            IGameService gameService,
            IDrawDartboardService drawDartboardService,
            IMapper<CommonStandard.Models.Math.Point, Shot> shotPointToShotMapper,
            IScoreboardVM scoreboardViewModel,
            Page scoreboard
            )
        {
            InitializeComponent();

            _gameService = gameService;
            _drawDartboardService = drawDartboardService;
            _shotPointToShotMapper = shotPointToShotMapper;
            _scoreboard = scoreboard;
            _scoreboardViewModel = scoreboardViewModel;
            this.Children.Add(_scoreboard);
            _dartboardViewModel = new DartboardViewModel(_gameService);

            this.BindingContext = _dartboardViewModel;

            _gameService = gameService;
            _gameService.GameWonEvent += async (sender, eventArgs) =>
            {
                if (eventArgs is GameWonEvenArgs arg)
                {
                    await DisplayAlert("Winner", $"Player {arg.WinningPlayer.Order} wins!", "Finsih Game");
                }
            };
        }

        private void canvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            _drawDartboardService.Draw(e);
            int width = e.Info.Width;
            int height = e.Info.Height;
            _shotPointToShotMapper = new ShotPointToShotMapper(App.DartboardDimensions);
            //Events
            this.canvasView.EnableTouchEvents = true;
            this.canvasView.Touch += async (touchSender, touchEvent) =>
            {
                try
                {
                    var x = touchEvent.Location.X - (width / 2);
                    var y = touchEvent.Location.Y - (height / 2);

                    //For some reason, the touch event fires wayyy too many times.
                    //so this HACK is here for a guard
                    var game = _gameService.Game;

                    if (game.Shots.Any()
                        && game.Shots.Last().X == x && game.Shots.Last().Y == y)
                        return;


                    var shot = await this._shotPointToShotMapper.Map(new CommonStandard.Models.Math.Point(x, y));

                    await _scoreboardViewModel.TakeShot(shot);

                    _dartboardViewModel.CalculatePlayerUpAndRoundShots();
                    this.canvasView.InvalidateSurface();
                }
                catch (Exception ex)
                {
                }
            };

        }

        private void UndoButtonClicked(object sender, EventArgs e)
        {
            _scoreboardViewModel.RemoveLastShot();
            _dartboardViewModel.CalculatePlayerUpAndRoundShots();
            this.canvasView.InvalidateSurface();
        }

    }
}
