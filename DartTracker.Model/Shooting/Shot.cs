using DartTracker.Model.Enum;
using System;

namespace DartTracker.Model.Shooting
{
    public class Shot : EntityBase
    {
        public int Player { get; set; }
        public int NumberHit { get; set; }
        public ContactType Contact { get; set; } = ContactType.NotShot;
        public Shot() { }
    }
}
