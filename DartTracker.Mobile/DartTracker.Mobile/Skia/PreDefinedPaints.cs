using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartTracker.Mobile.Skia
{
    public static class PreDefinedPaints
    {
        public static SKPaint BlackFillPaint = new SKPaint()
        {
            Color = SKColors.Black,
            Style = SKPaintStyle.Fill
        };

        public static SKPaint RedFillPaint = new SKPaint()
        {
            Color = SKColors.Red,
            Style = SKPaintStyle.Fill
        };

        public static SKPaint GreenFillPaint = new SKPaint()
        {
            Color = SKColors.Green,
            Style = SKPaintStyle.Fill
        };

        public static SKPaint WhiteFillPaint = new SKPaint()
        {
            Color = SKColors.White,
            Style = SKPaintStyle.Fill
        };

        public static SKPaint WhiteStrokePaint = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.White,
            StrokeCap = SKStrokeCap.Round,
            StrokeWidth = 4,
            IsAntialias = true
        };

        public static SKPaint BlackFontOutline = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Black,
            StrokeCap = SKStrokeCap.Round,
            StrokeWidth = 4,
            TextSize = 75,
            IsAntialias = true
        };

        public static SKPaint BlackFontSolid = new SKPaint()
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.Black,
            StrokeCap = SKStrokeCap.Round,
            TextAlign = SKTextAlign.Center,
            IsAntialias = true
        };

        public static SKPaint RedThickStrokePaint = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Red,
            StrokeWidth = 50,
            IsAntialias = true
        };

        public static SKPaint GreenThickStrokePaint = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Green,
            StrokeWidth = 50,
            IsAntialias = true
        };

        public static SKPaint GetWhiteSpokedPaint(float width) => new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.White,
            StrokeCap = SKStrokeCap.Round,
            StrokeWidth = width / 250,
            IsAntialias = true
        };
    }
}
