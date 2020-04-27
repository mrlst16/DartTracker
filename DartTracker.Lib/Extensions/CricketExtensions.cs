using DartTracker.Lib.Games.Cricket;
using DartTracker.Lib.Games.Cricket.MarkTracker;
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
    public static class CricketExtensions
    {
        public static Dictionary<Guid, Cricket200PlayerMarkTracker> StartShotboardForCricket200(this List<Player> players)
        {
            return players.ToDictionary(
                (x) => x.ID,
                (y) => new Cricket200PlayerMarkTracker(y.ID));
        }

        public static Dictionary<Guid, CricketCutthroatPlayerMarkTracker> StartShotboardForCricketCutthroat(this List<Player> players)
        {
            return players.ToDictionary(
                (x) => x.ID,
                (y) => new CricketCutthroatPlayerMarkTracker(y.ID));
        }

        public static Dictionary<Guid, Cricket200PlayerMarkTracker> CalculateForCricket200(this List<Player> players, List<Shot> shots)
        {
            DartGameIncrementor incrementor = new DartGameIncrementor(players.Count);
            Dictionary<Guid, Cricket200PlayerMarkTracker> shotBoard = players.StartShotboardForCricket200();

            for (int i = 0; i < shots.Count; i++)
            {
                var playerUp = incrementor.PlayerUp;
                var shot = shots[i];
                shot.Player = playerUp;
                var playerId = shotBoard.ElementAt(playerUp).Key;
                var tracker = shotBoard.ElementAt(playerUp).Value;

                bool closedout =
                    players.Count > 1
                && (CricketPlayerMarkTrackerBase.ScoringNumbers.Contains(shot.NumberHit)
                && (shotBoard
                    .Where(x => x.Key != playerId)
                    ?.Select(x => x.Value.MarksFor.TryGetValue(shot.NumberHit, out int res) ? res : 0)
                    ?.Min() >= 3)
                );

                tracker.MarkShotFor(shot, closedout);

                incrementor = incrementor.Increment();
            }

            return shotBoard;
        }

        public static Dictionary<Guid, CricketCutthroatPlayerMarkTracker> CalculateForCricketCutthroat(this List<Player> players, List<Shot> shots)
        {
            DartGameIncrementor incrementor = new DartGameIncrementor(players.Count);
            Dictionary<Guid, CricketCutthroatPlayerMarkTracker> shotBoard = players.StartShotboardForCricketCutthroat();

            for (int i = 0; i < shots.Count; i++)
            {
                var playerUp = incrementor.PlayerUp;
                var shot = shots[i];
                shot.Player = playerUp;
                var playerId = shotBoard.ElementAt(playerUp).Key;
                var tracker = shotBoard.ElementAt(playerUp).Value;

                bool otherPlayersClosedOut =
                    players.Count > 1
                && (CricketPlayerMarkTrackerBase.ScoringNumbers.Contains(shot.NumberHit)
                && (shotBoard
                    .Where(x => x.Key != playerId)
                    ?.Select(x => x.Value.MarksFor.TryGetValue(shot.NumberHit, out int res) ? res : 0)
                    ?.Min() >= 3)
                );

                tracker.MarkShotFor(shot, otherPlayersClosedOut);

                var otherPlayersNotClosedOut = shotBoard
                    .Where(x => x.Key != playerId && !x.Value.ClosedOut(shot.NumberHit))
                    ?.Select(x => x.Value);

                foreach (var otherPlayerMarkerTracker in otherPlayersNotClosedOut)
                    otherPlayerMarkerTracker.MarkShotAgainst(shot);

                incrementor = incrementor.Increment();
            }

            return shotBoard;
        }
    }
}
