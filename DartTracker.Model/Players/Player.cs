using DartTracker.Model.Shooting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartTracker.Model.Players
{
    public class Player : EntityBase
    {
        public Guid GameID { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public List<Turn> Turns { get; set; }
        public int Score { get; set; }
    }
}
