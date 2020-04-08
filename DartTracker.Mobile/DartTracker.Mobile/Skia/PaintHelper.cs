using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartTracker.Mobile.Skia
{
    public static class PaintHelper
    {
        public static SKPaint blackFillPaint = new SKPaint()
        {
            Color = SKColors.Black,
            Style = SKPaintStyle.Fill
        };

        public static SKPaint redFillPaint = new SKPaint()
        {
            Color = SKColors.Red,
            Style = SKPaintStyle.Fill
        };

        public static SKPaint greenFillPaint = new SKPaint()
        {
            Color = SKColors.Green,
            Style = SKPaintStyle.Fill
        };

        public static SKPaint whiteFillPaint = new SKPaint()
        {
            Color = SKColors.White,
            Style = SKPaintStyle.Fill
        };

        public static SKPaint whiteStrokePaint = new SKPaint()
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
            StrokeWidth = 4,
            TextSize = 75,
            IsAntialias = true
        };

        public static SKPaint redThickStrokePaint = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Red,
            StrokeWidth = 50,
            IsAntialias = true
        };

        public static SKPaint greenThickStrokePaint = new SKPaint()
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Green,
            StrokeWidth = 50,
            IsAntialias = true
        };

    }
}
