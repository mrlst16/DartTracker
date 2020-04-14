using DartTracker.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartTracker.Model.Games
{
    public class GameTypeDescription
    {
        public GameType GameType { get; set; }
        public string Name { get; set; }
        
        private string _about;
        public string About { get; set; }
    }
}
