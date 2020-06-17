using System;
using System.Collections.Generic;
using System.Text;

namespace DartTracker.Mobile.ViewModels
{
    public class PlayerScoreboardVM
    {
        public string BackgroundColor { get; set; }
        public string TextColor { get; set; } = "Black";
        public string PlayerName { get; set; }
        public int Score { get; set; }
    }
}
