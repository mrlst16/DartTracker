using CommonStandard.Extensions;
using CommonStandard.Interface.Mappers;
using DartTracker.Interface.Games;
using DartTracker.Lib.Helpers;
using DartTracker.Mobile.Interface.Services.Drawing;
using DartTracker.Mobile.Interface.ViewModels;
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

        private readonly IMapper<Point, Shot> _shotPointToShotMapper;
        private readonly Page _scoreboard;
        public DartboardPage(
            IGameService gameService,
            IDrawDartboardService drawDartboardService,
            IMapper<Point, Shot> shotPointToShotMapper,
            Page scoreboard
            )
        {
            InitializeComponent();

            _gameService = gameService;
            _drawDartboardService = drawDartboardService;
            _shotPointToShotMapper = shotPointToShotMapper;
            _scoreboard = scoreboard;
            this.Children.Add(_scoreboard);
            this.playerLabel.Text = PlayerLableText();
        }

        private void canvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            _drawDartboardService.Draw(e);
            int width = e.Info.Width;
            int height = e.Info.Height;

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
                    if (_drawDartboardService.ShotPoints.Any()
                        && _drawDartboardService.ShotPoints.Last().X == x && _drawDartboardService.ShotPoints.Last().Y == y)
                        return;

                    var shotPoint = new Point(x, y);
                    _drawDartboardService.ShotPoints.Add(shotPoint);

                    var shot = await this._shotPointToShotMapper.Map(shotPoint);

                    if (_scoreboard.BindingContext is IGameViewModel viewModel)
                    {
                        await viewModel.TakeShot(shot);
                    }

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
            _gameService.RemoveLastShot();
            if (_drawDartboardService.ShotPoints.Any())
            {
                _drawDartboardService.ShotPoints.RemoveAt(_drawDartboardService.ShotPoints.Count - 1);
                this.canvasView.InvalidateSurface();
            }
            this.playerLabel.Text = PlayerLableText();
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
