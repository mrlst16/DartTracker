using DartTracker.Model.Enum;
using System;

namespace DartTracker.Model.Shooting
{
    public class Shot : EntityBase
    {
        public Guid TurnId { get; set; }
        public int NumberHit { get; set; }
        public ContactType Contact { get; set; } = ContactType.NotShot;

        public int Score { get; set; }

        public Shot() { }
    }
}
