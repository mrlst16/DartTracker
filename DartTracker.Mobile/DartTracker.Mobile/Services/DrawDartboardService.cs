using DartTracker.Mobile.Interface.Services.Drawing;
using DartTracker.Mobile.Skia;
using DartTracker.Model.Shooting;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DartTracker.Mobile.Services
{
    public class DrawDartboardService : IDrawDartboardService
    {
        public List<Point> ShotPoints { get; }
            = new List<Point>();

        private List<int> _displayNumbers { get; } =
            new List<int>{
                    17,  3, 19, 7, 16, 8, 11, 14, 9, 12, 5, 20, 1, 18, 4, 13, 6, 10, 15, 2
            };

        public void Draw(SKPaintSurfaceEventArgs eventArgs)
        {
            var surface = eventArgs.Surface;
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.LightGray);

            int width = eventArgs.Info.Width;
            int height = eventArgs.Info.Height;

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
                canvas.DrawText(_displayNumbers[i].ToString(), 0, 635, PreDefinedPaints.BlackFontOutline);

                canvas.Restore();
            }

            canvas.DrawCircle(0, 0, 100, PreDefinedPaints.GreenFillPaint);
            canvas.DrawCircle(0, 0, 50, PreDefinedPaints.RedFillPaint);

            //Draw the shots
            for (int i = 0; i < ShotPoints.Count; i++)
            {
                var shotPoint = ShotPoints[i];
                canvas.DrawCircle(new SKPoint((float)shotPoint.X, (float)shotPoint.Y), 10, PreDefinedPaints.WhiteFillPaint);
            }
        }
    }
}
