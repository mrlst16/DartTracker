using DartTracker.Model.Enum;
using DartTracker.Model.Shooting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DartTracker.Lib.Games.MarkTrackers
{
    public class CricketCutthroatPlayerMarkTracker : CricketPlayerMarkTrackerBase
    {
        public Dictionary<int, int> MarksAgaints { get; protected set; } = new Dictionary<int, int>()
            {
                { 15, 0},
                { 16, 0},
                { 17, 0},
                { 18, 0},
                { 19, 0},
                { 20, 0},
                { 25, 0}
            };

        public CricketCutthroatPlayerMarkTracker(
            Guid playerId
            ) : base(playerId)
        {

        }

        public override int Score => MarksAgaints.Sum(x => x.Key * Math.Max(x.Value - 3, 0));

        public void MarkShotAgainst(Shot shot)
        {
            if (
                UnScorable.Contains(shot.Contact)
                || !MarksAgaints.ContainsKey(shot.NumberHit)
                ) return;

            switch (shot.Contact)
            {
                case ContactType.Single:
                    MarksAgaints[shot.NumberHit] += 1;
                    break;
                case ContactType.Double:
                    MarksAgaints[shot.NumberHit] += 2;
                    break;
                case ContactType.Triple:
                    MarksAgaints[shot.NumberHit] += 3;
                    break;
                case ContactType.BullsEye:
                    MarksAgaints[25] += 1;
                    break;
                case ContactType.DoubleBullsEye:
                    MarksAgaints[25] += 2;
                    break;
                default:
                    break;
            }

            return;
        }

    }
}
