using DartTracker.Model.Enum;
using DartTracker.Model.Players;
using DartTracker.Model.Shooting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartTracker.Model.Games
{
    public class Game : EntityBase
    {
        public override Guid ID { get; set; }
        public GameType Type { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public List<Shot> Shots { get; set; } = new List<Shot>();
    }
}
