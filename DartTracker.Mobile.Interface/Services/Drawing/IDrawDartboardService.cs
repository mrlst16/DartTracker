using DartTracker.Model.Shooting;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DartTracker.Mobile.Interface.Services.Drawing
{
    public interface IDrawDartboardService
    {
        List<ShotPointFromZero> ShotPoints { get; }
        void Draw(SKPaintSurfaceEventArgs eventArgs);
    }
}
