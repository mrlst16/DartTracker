using CommonStandard.Extensions;
using CommonStandard.Interface.Mappers;
using DartTracker.Interface.Games;
using DartTracker.Lib.Helpers;
using DartTracker.Lib.Mappers;
using DartTracker.Mobile.Factories;
using DartTracker.Mobile.Interface.Services.Drawing;
using DartTracker.Mobile.Interface.ViewModels;
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
        private readonly IGameViewModel _viewModel;
        private IMapper<CommonStandard.Models.Math.Point, Shot> _shotPointToShotMapper;
        private readonly Page _scoreboard;

        public DartboardPage(
            IGameService gameService,
            IDrawDartboardService drawDartboardService,
            IMapper<CommonStandard.Models.Math.Point, Shot> shotPointToShotMapper,
            IGameViewModel viewModel,
            Page scoreboard
            )
        {
            InitializeComponent();

            _gameService = gameService;
            _drawDartboardService = drawDartboardService;
            _shotPointToShotMapper = shotPointToShotMapper;
            _scoreboard = scoreboard;
            _viewModel = viewModel;
            this.Children.Add(_scoreboard);
            this.playerLabel.Text = PlayerLableText();

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

                    await _viewModel.TakeShot(shot);

                    this.playerLabel.Text = PlayerLableText();
                    this.canvasView.InvalidateSurface();
                }
                catch (Exception ex)
                {
                }
            };

        }

        private void UndoButtonClicked(object sender, EventArgs e)
        {
            _viewModel.RemoveLastShot();
            this.playerLabel.Text = PlayerLableText();
            this.canvasView.InvalidateSurface();
        }

        private string PlayerLableText()
        {
            var game = _gameService.Game;

            DartGameIncrementor incrementor =
                new DartGameIncrementor(_gameService.Game.Players.Count)
                .SetShots(Math.Max(0, game.Shots.Count - 1));
            //adjust player up for display
            var last3 = game.Shots.SplitSequentially(3)?.LastOrDefault()
                ?? new System.Collections.Generic.List<Shot>();
            int displayPlayerUp = incrementor.PlayerUp;
            string result = $"Player {displayPlayerUp} ";

            for (int i = 0; i < last3.Count; i++)
            {
                var shot = last3[i];
                switch (shot.Contact)
                {
                    case Model.Enum.ContactType.Single:
                        result += $"1 x {shot.NumberHit} ";
                        break;
                    case Model.Enum.ContactType.Double:
                        result += $"2 x {shot.NumberHit} ";
                        break;
                    case Model.Enum.ContactType.Triple:
                        result += $"3 x {shot.NumberHit} ";
                        break;
                    case Model.Enum.ContactType.BullsEye:
                        result += $" B ";
                        break;
                    case Model.Enum.ContactType.DoubleBullsEye:
                        result += $" DB ";
                        break;
                }
            }
            return result;
        }
    }
}
