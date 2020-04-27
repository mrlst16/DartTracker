using DartTracker.Lib.Games.MarkTrackers;
using DartTracker.Model.Enum;
using DartTracker.Model.Shooting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DartTracker.Lib.Games.Cricket.MarkTracker
{
    public class Cricket200PlayerMarkTracker : CricketPlayerMarkTrackerBase
    {
        public Cricket200PlayerMarkTracker(
                Guid playerId
            ) : base(playerId)
        {
        }

    }

}
