using SkiaSharp.Views.Forms;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DartTracker.Mobile.Interface.Services.Drawing
{
    public interface IDrawDartboardService
    {
        List<Point> ShotPoints { get; }
        void Draw(SKPaintSurfaceEventArgs eventArgs);
    }
}
