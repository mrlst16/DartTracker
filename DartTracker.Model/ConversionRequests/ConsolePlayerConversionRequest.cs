using DartTracker.Model.Games;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartTracker.Model.ConversionRequests
{
    public class ConsolePlayerConversionRequest
    {
        public List<string> Names { get; set; }
        public Game Game { get; set; }
    }
}
