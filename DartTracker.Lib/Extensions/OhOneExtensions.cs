using DartTracker.Lib.Games.MarkTrackers;
using DartTracker.Lib.Helpers;
using DartTracker.Model.Players;
using DartTracker.Model.Shooting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DartTracker.Lib.Extensions
{
    public static class OhOneExtensions
    {
        private static Dictionary<Guid, OhOneShotTracker> StartShotboardForOhOne(this List<Player> players, int startingScore)
        {
            return players.ToDictionary(
                (x) => x.ID,
                (y) => new OhOneShotTracker(y.ID, startingScore));
        }

        public static Dictionary<Guid, OhOneShotTracker> CalculateFor01OpenInOpenOut(this List<Player> players, List<Shot> shots, int startingScore)
        {
            DartGameIncrementor incrementor = new DartGameIncrementor(players.Count);
            Dictionary<Guid, OhOneShotTracker> shotBoard = players.StartShotboardForOhOne(startingScore);

            for (int i = 0; i < shots.Count; i++)
            {
                var playerUp = incrementor.PlayerUp;
                var shot = shots[i];
                shot.Player = playerUp;
                var tracker = shotBoard.ElementAt(playerUp).Value;

                //If the shot caused a bust
                if (!tracker.MarkShot(shot))
                {
                    //Roll it back
                    int increment = 3 - (i % 3);
                    incrementor = incrementor.Increment(increment);
                    continue;
                }

                incrementor.Increment();
            }

            return shotBoard;
        }
    }
}
