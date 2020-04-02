using System;
using System.Collections.Generic;
using System.Text;

namespace DartTracker.Lib.Helpers
{
    public class DartGameIncrementor
    {
        public int Players { get; protected set; }
        public int ShotsPerTurn { get; protected set; }
        public int Shots { get; protected set; } = 0;
        public int PlayerUp
        {
            get
            {
                var shotsPerRound = Players * ShotsPerTurn;
                var shotsToRemove = shotsPerRound * RoundsAlreadyTaken;
                var shotsTakenInThisRound = Shots - shotsToRemove;
                var result = shotsTakenInThisRound / ShotsPerTurn;
                return result;
            }
        }

        public int Round
        {
            get
            {
                return RoundsAlreadyTaken + 1;
            }
        }

        public int RoundsAlreadyTaken
        {
            get
            {
                double divided = (double)TurnsAlreadyTaken / Players;
                return (int)Math.Floor(divided);
            }
        }

        public int TurnsAlreadyTaken
        {
            get
            {
                var result = (double)(Shots / ShotsPerTurn);
                return (int)Math.Floor(result);
            }
        }

        public int Turn
        {
            get
            {
                return TurnsAlreadyTaken + 1;
            }
        }

        public DartGameIncrementor(
            int players, int shotsPerTurn = 3
            )
        {
            Players = players;
            ShotsPerTurn = shotsPerTurn;
        }

        public DartGameIncrementor Increment(int by = 1)
        {
            this.Shots += by;
            return this;
        }

        public DartGameIncrementor Decrement(int by = 1)
        {
            this.Shots -= by;
            return this;
        }

    }
}
