using CommonStandard.Extensions;
using CommonStandard.Math.Trigonometry;
using DartTracker.Interface.Games;
using DartTracker.Mobile.Interface.Services.Drawing;
using DartTracker.Mobile.Skia;
using DartTracker.Model.Drawing;
using DartTracker.Model.Shooting;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace DartTracker.Mobile.Services
{
    public class DrawDartboardService : IDrawDartboardService
    {
        private readonly IGameService _gameService;

        public List<Point> ShotPoints { get; }
            = new List<Point>();

        private List<int> _displayNumbers { get; } =
            new List<int>{
                 6, 10, 15, 2, 17,  3, 19, 7, 16, 8, 11, 14, 9, 12, 5, 20, 1, 18, 4, 13
            };

        public DrawDartboardService(
            IGameService gameService
            )
        {
            _gameService = gameService;
        }

        public void Draw(SKPaintSurfaceEventArgs eventArgs)
        {
            var dimensions = CalculateDartboardDimensions(eventArgs);

            PreDefinedPaints.RedThickStrokePaint.StrokeWidth = dimensions.DoublesAndTriplesStrokeWidth;
            PreDefinedPaints.GreenThickStrokePaint.StrokeWidth = dimensions.DoublesAndTriplesStrokeWidth;

            var surface = eventArgs.Surface;
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.LightGray);

            canvas.Translate(dimensions.CanvasWidth / 2, dimensions.CanvasHeight / 2);

            SKRect outerRect = new SKRect();
            outerRect.Size = new SKSize(dimensions.BackgroudCircleDiameter, dimensions.BackgroudCircleDiameter);
            outerRect.Location = new SKPoint(-1 * dimensions.BackgroudCircleRadius, -1 * dimensions.BackgroudCircleRadius);

            SKRect innerRect = new SKRect();
            innerRect.Size = new SKSize(dimensions.InnerCircleDiameter, dimensions.InnerCircleDiameter);
            innerRect.Location = new SKPoint(-1 * dimensions.InnerCircleDiameter / 2, -1 * dimensions.InnerCircleDiameter / 2);

            canvas.DrawOval(outerRect, PreDefinedPaints.BlackFillPaint);

            for (int i = 0; i < 20; i++)
            {
                var degrees = (i * 18) - 9;
                canvas.Save();
                canvas.RotateDegrees(degrees);
                canvas.DrawLine(0, 0, 0, dimensions.BackgroudCircleRadius,
                    PreDefinedPaints.GetWhiteSpokedPaint(dimensions.BackgroudCircleDiameter));

                SKPath outerPath = new SKPath();
                outerPath.AddArc(outerRect, 0, 18);

                SKPaint paint1 = i % 2 == 0 ? PreDefinedPaints.RedThickStrokePaint : PreDefinedPaints.GreenThickStrokePaint;
                SKPaint paint2 = i % 2 != 0 ? PreDefinedPaints.RedThickStrokePaint : PreDefinedPaints.GreenThickStrokePaint;

                canvas.DrawPath(outerPath, paint1);

                SKPath innerPath = new SKPath();
                innerPath.AddArc(innerRect, 0, 18);
                canvas.DrawPath(innerPath, paint2);

                canvas.Restore();
            }

            float numberDistance = dimensions.BackgroudCircleRadius * 1.15f;
            PreDefinedPaints.BlackFontSolid.TextSize = dimensions.BackgroudCircleDiameter * .075f;
            for (int i = 0; i < 20; i++)
            {
                var degrees = i * 18;

                string text = _displayNumbers[i].ToString();
                var (x, y) = RightTriangle.CalulatePointFromDegreesAndHyptoneus(degrees, numberDistance);
                canvas.DrawText(text, new SKPoint(x, y + (PreDefinedPaints.BlackFontSolid.TextSize / 2)), PreDefinedPaints.BlackFontSolid);
            }

            SKRect bullsEyeRect = new SKRect();
            bullsEyeRect.Size = new SKSize(dimensions.BullseyeCircleDiameter, dimensions.BullseyeCircleDiameter);
            bullsEyeRect.Location = new SKPoint(-1 * dimensions.BullseyeCircleDiameter / 2, -1 * dimensions.BullseyeCircleDiameter / 2);

            SKRect doubleBullseyeRect = new SKRect();
            doubleBullseyeRect.Size = new SKSize(dimensions.DoubleBullCircleDiameter, dimensions.DoubleBullCircleDiameter);
            doubleBullseyeRect.Location = new SKPoint(-1 * dimensions.DoubleBullCircleDiameter / 2, -1 * dimensions.DoubleBullCircleDiameter / 2);

            canvas.DrawOval(bullsEyeRect, PreDefinedPaints.GreenFillPaint);
            canvas.DrawOval(doubleBullseyeRect, PreDefinedPaints.RedFillPaint);

            //Draw the shots
            var last3 = _gameService
                ?.Game
                ?.Shots
                ?.Select(x => new Xamarin.Forms.Point(x.X, x.Y))
                .SplitSequentially(3)
                ?.LastOrDefault() ?? new List<Point>();

            for (int i = 0; i < last3.Count; i++)
            {
                var shotPoint = last3[i];
                canvas.DrawCircle(new SKPoint((float)shotPoint.X, (float)shotPoint.Y), dimensions.ShotDiameter, PreDefinedPaints.WhiteFillPaint);
            }
        }

        private DartboardDimensions CalculateDartboardDimensions(SKPaintSurfaceEventArgs eventArgs)
        {
            if (App.DartboardDimensions?.WasCalculated ?? false) return App.DartboardDimensions;

            var result = new DartboardDimensions();
            result.CanvasWidth = eventArgs.Info.Width;
            result.CanvasHeight = eventArgs.Info.Height;
            result.OneOneThousandthOfWitdth = result.CanvasWidth / 1000;
            result.OneOneThousandthOfHeight = result.CanvasHeight / 1000;
            result.BackgroudCircleDiameter = (float)(result.CanvasWidth * .8);
            result.InnerCircleDiameter = result.BackgroudCircleDiameter / 2;
            result.BackgroudCircleRadius = result.BackgroudCircleDiameter / 2;
            result.BullseyeCircleDiameter = result.BackgroudCircleDiameter / 7.5f;
            result.DoubleBullCircleDiameter = result.BackgroudCircleDiameter / 15;
            result.DoublesAndTriplesStrokeWidth = result.BackgroudCircleRadius / 15;
            result.ShotDiameter = result.BackgroudCircleDiameter / 100;
            result.WasCalculated = true;
            App.DartboardDimensions = result;
            return result;
        }
    }
}
