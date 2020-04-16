using CommonStandard.Interface.Mappers;
using CommonStandard.Models.Math;
using DartTracker.Model.Drawing;
using DartTracker.Model.Enum;
using DartTracker.Model.Shooting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DartTracker.Lib.Mappers
{
    public class ShotPointToShotMapper : IMapper<Point, Shot>
    {
        private readonly DartboardDimensions _dartboardDimensions;
        public ShotPointToShotMapper(
                DartboardDimensions dartboardDimensions
            )
        {
            _dartboardDimensions = dartboardDimensions;
        }

        public async Task<Shot> Map(Point source)
        {
            var x = source.X;
            var y = source.Y * -1;
            var distanceFromZero = DistanceFromZero(x, y);
            var angle = AngleFromZeroInDegrees(x, y, distanceFromZero);

            return new Shot()
            {
                Contact = CalculateContactType(distanceFromZero),
                NumberHit = CalculateNumberHit(angle),
                X = source.X,
                Y = source.Y
            };
        }

        private double DistanceFromZero(double x, double y) => Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));

        private double RadiansToDegrees(double rad) => (rad * 180) / Math.PI;

        private double AngleFromZeroInDegrees(double x, double y, double distanceFromZero)
        {
            var angleInRadians = Math.Acos(x / distanceFromZero);
            var tempResult = RadiansToDegrees(angleInRadians);
            return y < 0 ? 360 - tempResult : tempResult;
        }

        private ContactType CalculateContactType(double distanceFromZero)
        {
            var doublesEnd = _dartboardDimensions.BackgroudCircleRadius + (_dartboardDimensions.DoublesAndTriplesStrokeWidth / 2);
            var doublesStart = _dartboardDimensions.BackgroudCircleRadius - (_dartboardDimensions.DoublesAndTriplesStrokeWidth / 2);

            var triplesEnd = (_dartboardDimensions.InnerCircleDiameter / 2) + (_dartboardDimensions.DoublesAndTriplesStrokeWidth / 2);
            var triplesStart = (_dartboardDimensions.InnerCircleDiameter / 2) - (_dartboardDimensions.DoublesAndTriplesStrokeWidth / 2);

            var doubleBullRadius = _dartboardDimensions.DoubleBullCircleDiameter / 2;
            var singleBullRadius = _dartboardDimensions.BullseyeCircleDiameter / 2;

            if (distanceFromZero > doublesEnd) return ContactType.Miss;
            if (distanceFromZero > doublesStart && distanceFromZero < doublesEnd) return ContactType.Double;
            if (distanceFromZero > triplesStart && distanceFromZero < triplesEnd) return ContactType.Triple;
            if (distanceFromZero > doubleBullRadius && distanceFromZero < singleBullRadius) return ContactType.BullsEye;
            if (distanceFromZero < doubleBullRadius) return ContactType.DoubleBullsEye;

            return ContactType.Single;
        }

        private int CalculateNumberHit(double angle)
        {
            if ((angle >= 0 && angle < 9) || (angle > 351 && angle <= 360)) return 6;
            if (angle > 9 && angle < 27) return 13;
            if (angle > 27 && angle < 45) return 4;
            if (angle > 45 && angle < 63) return 18;
            if (angle > 63 && angle < 81) return 1;
            if (angle > 81 && angle < 99) return 20;
            if (angle > 99 && angle < 117) return 5;
            if (angle > 117 && angle < 135) return 12;
            if (angle > 135 && angle < 153) return 9;
            if (angle > 153 && angle < 171) return 14;
            if (angle > 171 && angle < 189) return 11;
            if (angle > 189 && angle < 207) return 8;
            if (angle > 207 && angle < 225) return 16;
            if (angle > 225 && angle < 243) return 7;
            if (angle > 243 && angle < 261) return 19;
            if (angle > 261 && angle < 279) return 3;
            if (angle > 279 && angle < 297) return 17;
            if (angle > 297 && angle < 315) return 2;
            if (angle > 315 && angle < 333) return 15;
            if (angle > 333 && angle < 351) return 10;
            return 0;
        }

    }
}
