using CommonStandard.Interface.Mappers;
using DartTracker.Interface.Games;
using DartTracker.Mobile.Interface.Factories;
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
        private readonly IMapper<ShotPointFromZero, Shot> _shotPointToShotMapper;
        private readonly Page _scoreboard;
        public DartboardPage(
            IGameService gameService,
            IMapper<ShotPointFromZero, Shot> shotPointToShotMapper,
            Page scoreboard
            )
        {
            InitializeComponent();

            _gameService = gameService;
            _shotPointToShotMapper = shotPointToShotMapper;
            _scoreboard = scoreboard;
            this.Children.Add(_scoreboard);
        }

        private List<int> DisplayNumbers { get; } =
           new List<int>{
                17,  3, 19, 7, 16, 8, 11, 14, 9, 12, 5, 20, 1, 18, 4, 13, 6, 10, 15, 2
           };

        private List<ShotPointFromZero> shotPoints = new List<ShotPointFromZero>();

        private void canvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var surface = e.Surface;
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.LightGray);

            int width = e.Info.Width;
            int height = e.Info.Height;

            canvas.Translate(width / 2, height / 2);

            SKRect outerRect = new SKRect();
            outerRect.Size = new SKSize(1000, 1000);
            outerRect.Location = new SKPoint(-500, -500);

            SKRect innerRect = new SKRect();
            innerRect.Size = new SKSize(500, 500);
            innerRect.Location = new SKPoint(-250, -250);

            canvas.DrawOval(outerRect, PreDefinedPaints.BlackFillPaint);

            for (int i = 0; i < 20; i++)
            {
                var degrees = (i * 18) - 9;
                canvas.Save();
                canvas.RotateDegrees(degrees);
                canvas.DrawLine(0, 0, 0, 500, PreDefinedPaints.WhiteStrokePaint);

                SKPath outerPath = new SKPath();
                outerPath.AddArc(outerRect, 0, 18);

                SKPaint paint1 = i % 2 == 0 ? PreDefinedPaints.RedThickStrokePaint : PreDefinedPaints.GreenThickStrokePaint;
                SKPaint paint2 = i % 2 != 0 ? PreDefinedPaints.RedThickStrokePaint : PreDefinedPaints.GreenThickStrokePaint;

                canvas.DrawPath(outerPath, paint1);

                SKPath innerPath = new SKPath();
                innerPath.AddArc(innerRect, 0, 18);

                canvas.DrawPath(innerPath, paint2);
                canvas.RotateDegrees(-9);
                canvas.DrawText(DisplayNumbers[i].ToString(), 0, 635, PreDefinedPaints.BlackFontOutline);

                canvas.Restore();
            }

            canvas.DrawCircle(0, 0, 100, PreDefinedPaints.GreenFillPaint);
            canvas.DrawCircle(0, 0, 50, PreDefinedPaints.RedFillPaint);

            //Draw the shots
            for (int i = 0; i < shotPoints.Count; i++)
            {
                var shotPoint = shotPoints[i];
                canvas.DrawCircle(new SKPoint(shotPoint.X, shotPoint.Y), 10, PreDefinedPaints.WhiteFillPaint);
            }

            //Events
            this.canvasView.EnableTouchEvents = true;
            this.canvasView.Touch += async (touchSender, touchEvent) =>
            {
                try
                {
                    var x = touchEvent.Location.X - (width / 2);
                    var y = touchEvent.Location.Y - (height / 2);

                    if (shotPoints.Any() && shotPoints.Last().X == x && shotPoints.Last().Y == y)
                        return;

                    var shotPoint = new ShotPointFromZero(x, y);
                    shotPoints.Add(shotPoint);

                    var shot = await this._shotPointToShotMapper.Map(shotPoint);

                    if (_scoreboard.BindingContext is IGameViewModel viewModel)
                    {
                        await viewModel.TakeSot(shot);
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
