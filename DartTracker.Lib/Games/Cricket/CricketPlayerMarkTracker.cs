using DartTracker.Model.Enum;
using DartTracker.Model.Shooting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DartTracker.Lib.Games.Cricket
{
    public class CricketPlayerMarkTracker
    {
        private static List<ContactType> UnScorable = new List<ContactType>()
            {
                ContactType.Miss,
                ContactType.NotShot
            };
        public Guid PlayerID { get; protected set; }
        public Dictionary<int, int> Marks { get; protected set; } = new Dictionary<int, int>()
            {
                { 15, 0},
                { 16, 0},
                { 17, 0},
                { 18, 0},
                { 19, 0},
                { 20, 0},
                { 25, 0}
            };

        public bool IsClosedOut
        {
            get
            {
                return Marks.All((kvp) => kvp.Value >= 3);
            }
        }

        public int Score
        {
            get
            {
                int result = 0;
                foreach (var kvp in Marks)
                {
                    var shotsOverClose = kvp.Value - 3;
                    if (shotsOverClose < 1)
                        continue;

                    result += (kvp.Key * shotsOverClose);
                }
                return result;
            }
        }

        public CricketPlayerMarkTracker(
                Guid playerId
            )
        {
            PlayerID = playerId;
        }

        public Shot MarkShot(Shot shot, bool isClosedOut)
        {
            if (
                isClosedOut
                || UnScorable.Contains(shot.Contact)
                || !Marks.ContainsKey(shot.NumberHit)
                ) return shot;

            switch (shot.Contact)
            {
                case ContactType.Single:
                    Marks[shot.NumberHit]++;
                    break;
                case ContactType.Double:
                    Marks[shot.NumberHit] += 2;
                    break;
                case ContactType.Triple:
                    Marks[shot.NumberHit] += 3;
                    break;
                case ContactType.BullsEye:
                    Marks[25]++;
                    break;
                case ContactType.DoubleBullsEye:
                    Marks[25] += 2;
                    break;
                default:
                    break;
            }

            return shot;
        }
    }

}
