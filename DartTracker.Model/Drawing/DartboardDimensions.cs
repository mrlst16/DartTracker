using System;
using System.Collections.Generic;
using System.Text;

namespace DartTracker.Model.Drawing
{
    public class DartboardDimensions
    {
        public bool WasCalculated { get; set; } = false;
        public int CanvasHeight { get; set; }
        public int CanvasWidth { get; set; }
        public float BackgroudCircleDiameter { get; set; }
        public float BackgroudCircleRadius { get; set; }
        public float InnerCircleDiameter { get; set; }
        public float BullseyeCircleDiameter { get; set; }
        public float DoubleBullCircleDiameter { get; set; }
        public float DoublesAndTriplesStrokeWidth { get; set; }
        public float ShotDiameter { get; set; }
        public float OneOneThousandthOfWitdth { get; set; }
        public float OneOneThousandthOfHeight { get; set; }
    }
}
