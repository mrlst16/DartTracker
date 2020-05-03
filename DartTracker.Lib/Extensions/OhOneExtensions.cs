using DartTracker.Interface.Games.MarkTracker;
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
        private static Dictionary<Guid, T> StartShotboardForOhOne<T>(this List<Player> players, int startingScore)
            where T : IOhOneMarkTracker, new()
        {
            return players.ToDictionary(
                (x) => x.ID,
                (y) =>
                {
                    var result = new T();
                    result.PlayerID = y.ID;
                    result.Score = startingScore;
                    return result;
                });
        }

        public static Dictionary<Guid, OhOneOInOOutMarkTracker> CalculateFor01OpenInOpenOut(
            this List<Player> players, List<Shot> shots, int startingScore)
        {
            return Calculate<OhOneOInOOutMarkTracker>(players, shots, startingScore);
        }

        private static Dictionary<Guid, T> Calculate<T>(
            this List<Player> players,
            List<Shot> shots,
            int startingScore
            ) where T : IOhOneMarkTracker, new()
        {
            DartGameIncrementor incrementor = new DartGameIncrementor(players.Count);
            Dictionary<Guid, T> shotBoard = players.StartShotboardForOhOne<T>(startingScore);

            for (int i = 0; i < shots.Count; i++)
            {
                var playerUp = incrementor.PlayerUp;
                var shot = shots[i];
                shot.Player = playerUp;
                var tracker = shotBoard.ElementAt(playerUp).Value;

                //If the shot caused a bust
                if (!tracker.MarkShot(shot))
                {
                    //skip the rest of the turn
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
