using CommonStandard.Interface.Mappers;
using DartTracker.Interface.Games;
using DartTracker.Mobile.Interface.Factories;
using DartTracker.Mobile.Interface.Services.Drawing;
using DartTracker.Mobile.Interface.ViewModels;
using DartTracker.Mobile.Skia;
using DartTracker.Mobile.ViewModels;
using DartTracker.Model.Shooting;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DartTracker.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DartboardPage : TabbedPage
    {
        private readonly IGameService _gameService;
        private readonly IDrawDartboardService _drawDartboardService;

        private readonly IMapper<ShotPointFromZero, Shot> _shotPointToShotMapper;
        private readonly Page _scoreboard;
        public DartboardPage(
            IGameService gameService,
            IDrawDartboardService drawDartboardService,
            IMapper<ShotPointFromZero, Shot> shotPointToShotMapper,
            Page scoreboard
            )
        {
            InitializeComponent();

            _gameService = gameService;
            _drawDartboardService = drawDartboardService;
            _shotPointToShotMapper = shotPointToShotMapper;
            _scoreboard = scoreboard;
            this.Children.Add(_scoreboard);
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

                    if (_drawDartboardService.ShotPoints.Any()
                        && _drawDartboardService.ShotPoints.Last().X == x && _drawDartboardService.ShotPoints.Last().Y == y)
                        return;

                    var shotPoint = new ShotPointFromZero(x, y);
                    _drawDartboardService.ShotPoints.Add(shotPoint);

                    var shot = await this._shotPointToShotMapper.Map(shotPoint);

                    if (_scoreboard.BindingContext is IGameViewModel viewModel)
                    {
                        await viewModel.TakeShot(shot);
                    }

                    this.canvasView.InvalidateSurface();
                }
                catch (Exception ex)
                {
                }
            };

        }
    }
}
