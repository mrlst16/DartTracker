using DartTracker.Model.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartTracker.Model.Events
{
    public class GameWonEvenArgs : EventArgs
    {
        public Player WinningPlayer { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
    }
}
