﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DartTracker.Mobile.ViewModels
{
    public class CricketPlayerScoreboardVM: PlayerScoreboardVM
    {
        public int Fifteens { get; set; } = 0;
        public int Sixteens { get; set; } = 0;
        public int Seventeens { get; set; } = 0;
        public int Eighteens { get; set; } = 0;
        public int Nineteens { get; set; } = 0;
        public int Twentys { get; set; } = 0;
        public int Bulls { get; set; } = 0;
    }
}
