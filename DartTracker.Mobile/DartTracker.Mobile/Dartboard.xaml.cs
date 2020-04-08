using DartTracker.Interface.Games;
using DartTracker.Mobile.Skia;
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
        private readonly IGameService _gameService;
        public Dartboard(
            IGameService gameService
            )
        {
            _gameService = gameService;
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

        private List<ShotPointFromZero> shotPoints = new List<ShotPointFromZero>();

        private double DistanceFromZero(double x, double y) => Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        public static double RadiansToDegrees(double rad) => (rad * 180) / Math.PI;

        public static double AngleFromZeroInDegrees(double x, double y, double distanceFromZero)
        {
            var angleInRadians = Math.Acos(x / distanceFromZero);
            var tempResult = RadiansToDegrees(angleInRadians);

            return y < 0 ? 360 - tempResult : tempResult;
        }

        private string ShotResult(ShotPointFromZero shotPoint)
        {
            var x = shotPoint.X;
            var y = shotPoint.Y * -1;

            var distanceFromZero = DistanceFromZero(x, y);

            var angle = AngleFromZeroInDegrees(x, y, distanceFromZero);

            var result = $"@{angle}-> ";

            if (distanceFromZero > 525) return result + "Miss";
            if (distanceFromZero > 475 && distanceFromZero < 525) return result + "Double " + NumberHit(angle);
            if (distanceFromZero > 225 && distanceFromZero < 275) return result + "Triple " + NumberHit(angle);
            if (distanceFromZero > 50 && distanceFromZero < 100) return result + "Bull";
            if (distanceFromZero < 50) return result + "Double Bull";

            return result + "Single " + NumberHit(angle);
        }

        public static string NumberHit(double angle)
        {
            if ((angle > 0 && angle < 9) || (angle > 351 && angle < 360)) return "6";
            if (angle > 9 && angle < 27) return "13";
            if (angle > 27 && angle < 45) return "4";
            if (angle > 27 && angle < 45) return "4";
            if (angle > 45 && angle < 63) return "18";
            if (angle > 63 && angle < 81) return "1";
            if (angle > 81 && angle < 99) return "20";
            if (angle > 99 && angle < 117) return "5";
            if (angle > 117 && angle < 135) return "12";
            if (angle > 135 && angle < 153) return "9";
            if (angle > 153 && angle < 171) return "14";
            if (angle > 171 && angle < 189) return "11";
            if (angle > 189 && angle < 207) return "8";
            if (angle > 207 && angle < 225) return "16";
            if (angle > 225 && angle < 243) return "7";
            if (angle > 243 && angle < 261) return "19";
            if (angle > 261 && angle < 279) return "3";
            if (angle > 279 && angle < 297) return "17";
            if (angle > 297 && angle < 315) return "2";
            if (angle > 315 && angle < 333) return "15";
            if (angle > 333 && angle < 351) return "10";
            return "Nothing";
        }

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

            canvas.DrawOval(outerRect, PaintHelper.blackFillPaint);

            if (shotPoints.Any())
            {
                var lastShotText = ShotResult(shotPoints.Last());
                canvas.DrawText(lastShotText, (-1 * width / 2), 600, PaintHelper.BlackFontSolid);
            }

            for (int i = 0; i < 20; i++)
            {
                var degrees = (i * 18) - 9;
                canvas.Save();
                canvas.RotateDegrees(degrees);
                canvas.DrawLine(0, 0, 0, 500, PaintHelper.whiteStrokePaint);

                SKPath outerPath = new SKPath();
                outerPath.AddArc(outerRect, 0, 18);

                SKPaint paint1 = i % 2 == 0 ? PaintHelper.redThickStrokePaint : PaintHelper.greenThickStrokePaint;
                SKPaint paint2 = i % 2 != 0 ? PaintHelper.redThickStrokePaint : PaintHelper.greenThickStrokePaint;

                canvas.DrawPath(outerPath, paint1);

                SKPath innerPath = new SKPath();
                innerPath.AddArc(innerRect, 0, 18);

                canvas.DrawPath(innerPath, paint2);
                canvas.RotateDegrees(-9);
                canvas.DrawText(DisplayNumbers[i].ToString(), 0, 635, PaintHelper.BlackFontOutline);

                canvas.Restore();
            }

            canvas.DrawCircle(0, 0, 100, PaintHelper.greenFillPaint);
            canvas.DrawCircle(0, 0, 50, PaintHelper.redFillPaint);

            //Draw the shots
            for (int i = 0; i < shotPoints.Count; i++)
            {
                var shotPoint = shotPoints[i];
                canvas.DrawCircle(new SKPoint(shotPoint.X, shotPoint.Y), 10, PaintHelper.whiteFillPaint);
            }

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
