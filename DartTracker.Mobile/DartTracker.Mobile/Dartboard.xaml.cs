using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DartTracker.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dartboard : ContentPage
    {
        public Dartboard()
        {
            InitializeComponent();
        }

        private List<int> DisplayNumbers { get; } =
           new List<int>{
                17,  3, 19, 7, 16, 8, 11, 14, 9, 12, 5, 20, 1, 18, 4, 13, 6, 10, 15, 2
           };

        private class ShotPointFromZero
        {
            public float X;
            public float Y;

            public ShotPointFromZero(float x, float y)
            {
                X = x;
                Y = y;
            }
        }

        SKPaint blackFillPaint = new SKPaint()
        {
            Color = SKColors.Black,
            Style = SKPaintStyle.Fill
        };

        SKPaint redFillPaint = new SKPaint()
        {
            Color = SKColors.Red,
            Style = SKPaintStyle.Fill
        };

        SKPaint greenFillPaint = new SKPaint()
        {
            Color = SKColors.Green,
            Style = SKPaintStyle.Fill
        };

        SKPaint whiteFillPaint = new SKPaint()
        {
            Color = SKColors.White,
            Style = SKPaintStyle.Fill
        };

        SKPaint whiteStrokePaint = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.White,
            StrokeCap = SKStrokeCap.Round,
            StrokeWidth = 4,
            IsAntialias = true
        };

        SKPaint blackStrokePaint = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Black,
            StrokeCap = SKStrokeCap.Round,
            StrokeWidth = 4,
            TextSize = 75,
            IsAntialias = true
        };

        SKPaint redThickStrokePaint = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Red,
            StrokeWidth = 50,
            IsAntialias = true
        };

        SKPaint greenThickStrokePaint = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Green,
            StrokeWidth = 50,
            IsAntialias = true
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

            canvas.DrawOval(outerRect, blackFillPaint);

            for (int i = 0; i < 20; i++)
            {
                var degrees = (i * 18) - 9;
                canvas.Save();
                canvas.RotateDegrees(degrees);
                canvas.DrawLine(0, 0, 0, 500, whiteStrokePaint);

                SKPath outerPath = new SKPath();
                outerPath.AddArc(outerRect, 0, 18);

                SKPaint paint1 = i % 2 == 0 ? redThickStrokePaint : greenThickStrokePaint;
                SKPaint paint2 = i % 2 != 0 ? redThickStrokePaint : greenThickStrokePaint;

                canvas.DrawPath(outerPath, paint1);

                SKPath innerPath = new SKPath();
                innerPath.AddArc(innerRect, 0, 18);

                canvas.DrawPath(innerPath, paint2);
                canvas.RotateDegrees(-9);
                canvas.DrawText(DisplayNumbers[i].ToString(), 0, 635, blackStrokePaint);
                //canvas.RotateDegrees(9);

                canvas.Restore();
            }

            canvas.DrawCircle(0, 0, 100, greenFillPaint);
            canvas.DrawCircle(0, 0, 50, redFillPaint);

            //Draw the shots
            for (int i = 0; i < shotPoints.Count; i++)
            {
                var shotPoint = shotPoints[i];
                canvas.DrawCircle(new SKPoint(shotPoint.X, shotPoint.Y), 10, whiteFillPaint);
            }

            //canvas.RotateDegrees(18);

            //Events
            this.canvasView.EnableTouchEvents = true;
            this.canvasView.Touch += async (touchSender, touchEvent) =>
            {
                try
                {

                    var x = touchEvent.Location.X - (width / 2);
                    var y = touchEvent.Location.Y - (height / 2);
                    var shotPoint = new ShotPointFromZero(x, y);
                    shotPoints.Add(shotPoint);
                    this.canvasView.InvalidateSurface();
                }
                catch (Exception ex)
                {
                }
            };
        }
    }
}
