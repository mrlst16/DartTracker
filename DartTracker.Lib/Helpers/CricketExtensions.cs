using DartTracker.Lib.Games.Cricket;
using DartTracker.Lib.Helpers;
using DartTracker.Model.Players;
using DartTracker.Model.Shooting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DartTracker.Lib.Helpers
{
    public static class CricketExtensions
    {
        public static Dictionary<Guid, CricketPlayerMarkTracker> StartShotboard(this List<Player> players)
        {
            return players.ToDictionary(
                (x) => x.ID,
                (y) => new CricketPlayerMarkTracker(y.ID));
        }

        public static Dictionary<Guid, CricketPlayerMarkTracker> Calculate(this List<Player> players, List<Shot> shots)
        {
            DartGameIncrementor incrementor = new DartGameIncrementor(players.Count);
            Dictionary<Guid, CricketPlayerMarkTracker> shotBoard = players.StartShotboard();

            for (int i = 0; i < shots.Count; i++)
            {
                var playerUp = incrementor.PlayerUp;
                var shot = shots[i];
                shot.Player = playerUp;
                var playerId = shotBoard.ElementAt(playerUp).Key;
                var tracker = shotBoard.ElementAt(playerUp).Value;

                bool closedout =
                CricketGameService.ScoringNumbers.Contains(shot.NumberHit)
                && shotBoard
                    .Where(x => x.Key != playerId)
                    .Select(x => x.Value.Marks.TryGetValue(shot.NumberHit, out int res) ? res : 0)
                    .Min() >= 3;

                var step1 = shotBoard
                    .Where(x => x.Key != playerId);

                var step2 = shotBoard
                    .Where(x => x.Key != playerId)
                    .Select(x => x.Value.Marks.TryGetValue(shot.NumberHit, out int res) ? res : 0);

                var step3 = shotBoard
                    .Where(x => x.Key != playerId)
                    .Select(x => x.Value.Marks.TryGetValue(shot.NumberHit, out int res) ? res : 0)
                    .Min();

                var step4 = shotBoard
                    .Where(x => x.Key != playerId)
                    .Select(x => x.Value.Marks.TryGetValue(shot.NumberHit, out int res) ? res : 0)
                    .Min() >= 3;

                tracker.MarkShot(shot, closedout);

                incrementor = incrementor.Increment();
            }

            return shotBoard;
        }
    }
}
