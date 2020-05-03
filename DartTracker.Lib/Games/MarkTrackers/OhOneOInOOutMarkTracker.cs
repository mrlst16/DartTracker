using DartTracker.Interface.Games.MarkTracker;
using DartTracker.Model.Shooting;
using System;

namespace DartTracker.Lib.Games.MarkTrackers
{
    public class OhOneOInOOutMarkTracker : IOhOneMarkTracker
    {
        public Guid PlayerID { get; set; }

        public int Score { get; set; }

        public OhOneOInOOutMarkTracker()
        {
        }

        public OhOneOInOOutMarkTracker(Guid playerid, int startingScore)
        {
            this.PlayerID = playerid;
            Score = startingScore;
        }

        /// <summary>
        /// Marks a Shot.  This method will not subtract from score if the shot causes a bust. Returns false if bust.
        /// </summary>
        /// <param name="shot"></param>
        /// <returns>eFalse if bust. True if score => 0</returns>
        public bool MarkShot(Shot shot)
        {
            int changeInScore = 0;
            switch (shot.Contact)
            {
                case Model.Enum.ContactType.Single:
                    changeInScore = shot.NumberHit;
                    break;
                case Model.Enum.ContactType.Double:
                    changeInScore = shot.NumberHit * 2;
                    break;
                case Model.Enum.ContactType.Triple:
                    changeInScore = shot.NumberHit * 3;
                    break;
                case Model.Enum.ContactType.BullsEye:
                    changeInScore = 25;
                    break;
                case Model.Enum.ContactType.DoubleBullsEye:
                    changeInScore = 50;
                    break;
                case Model.Enum.ContactType.NotShot:
                case Model.Enum.ContactType.Fluff:
                case Model.Enum.ContactType.Miss:
                    break;
                default:
                    break;
            }
            if (Score - changeInScore < 0) return false;

            Score -= changeInScore;
            return true;
        }
    }
}
