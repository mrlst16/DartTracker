using DartTracker.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartTracker.Model.Shooting
{
    public class Turn : EntityBase
    {
        public List<Shot> Shots { get; set; } = new List<Shot>();
        public int Score { get; set; } = 0;

        public Turn()
        {

        }
    }
}
