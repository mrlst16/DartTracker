using DartTracker.Model.Enum;
using DartTracker.Model.Shooting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DartTracker.Lib.Games.MarkTrackers
{
    public abstract class CricketPlayerMarkTrackerBase
    {
        public static readonly List<int> ScoringNumbers = new List<int>() { 15, 16, 17, 18, 19, 20, 25 };

        protected static List<ContactType> UnScorable = new List<ContactType>()
            {
                ContactType.Miss,
                ContactType.NotShot
            };
        public Guid PlayerID { get; protected set; }
        public Dictionary<int, int> MarksFor { get; protected set; } = new Dictionary<int, int>()
            {
                { 15, 0},
                { 16, 0},
                { 17, 0},
                { 18, 0},
                { 19, 0},
                { 20, 0},
                { 25, 0}
            };

        public virtual bool IsClosedOut
        {
            get => MarksFor.All((kvp) => kvp.Value >= 3);
        }

        public virtual int Score
        {
            get
            {
                int result = 0;
                foreach (var kvp in MarksFor)
                {
                    var shotsOverClose = kvp.Value - 3;
                    if (shotsOverClose < 1)
                        continue;

                    result += (kvp.Key * shotsOverClose);
                }
                return result;
            }
        }

        public CricketPlayerMarkTrackerBase(
                Guid playerId
            )
        {
            PlayerID = playerId;
        }

        public virtual void MarkShotFor(Shot shot, bool isClosedOut)
        {
            if (
                UnScorable.Contains(shot.Contact)
                || !MarksFor.ContainsKey(shot.NumberHit)
                ) return;

            int remainingShots = isClosedOut ? (3 - this.MarksFor[shot.NumberHit]) : 100;
            switch (shot.Contact)
            {
                case ContactType.Single:
                    MarksFor[shot.NumberHit] += Math.Min(remainingShots, 1);
                    break;
                case ContactType.Double:
                    MarksFor[shot.NumberHit] += Math.Min(remainingShots, 2);
                    break;
                case ContactType.Triple:
                    MarksFor[shot.NumberHit] += Math.Min(remainingShots, 3);
                    break;
                case ContactType.BullsEye:
                    MarksFor[25] += Math.Min(remainingShots, 1);
                    break;
                case ContactType.DoubleBullsEye:
                    MarksFor[25] += Math.Min(remainingShots, 2);
                    break;
                default:
                    break;
            }

            return;
        }

        public virtual bool ClosedOut(int number)
        {
            return MarksFor.ContainsKey(number) && MarksFor[number] >= 3;
        }
    }
}
