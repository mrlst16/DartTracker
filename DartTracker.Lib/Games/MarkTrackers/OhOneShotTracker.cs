using DartTracker.Model.Shooting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartTracker.Lib.Games.MarkTrackers
{
    public class OhOneShotTracker
    {
        public Guid PlayerID { get; protected set; }

        public int Score { get; protected set; }

        public OhOneShotTracker(Guid playerid, int startingScore)
        {
            this.PlayerID = playerid;
            Score = startingScore;
        }

        /// <summary>
        /// Marks a Shot.  This method will not subtract from score if the shot causes a bust
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
