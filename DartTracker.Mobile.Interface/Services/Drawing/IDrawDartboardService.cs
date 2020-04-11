using DartTracker.Model.Shooting;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DartTracker.Mobile.Interface.Services.Drawing
{
    public interface IDrawDartboardService
    {
        List<Point> ShotPoints { get; }
        void Draw(SKPaintSurfaceEventArgs eventArgs);
    }
}
