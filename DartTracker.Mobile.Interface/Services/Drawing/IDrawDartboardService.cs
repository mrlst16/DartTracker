using SkiaSharp.Views.Forms;
using System.Collections.Generic;
using Xamarin.Forms;

namespace DartTracker.Mobile.Interface.Services.Drawing
{
    public interface IDrawDartboardService
    {
        void Draw(SKPaintSurfaceEventArgs eventArgs);
    }
}
