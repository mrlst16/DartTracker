using DartTracker.Model.Shooting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartTracker.Interface.Games.MarkTracker
{
    public interface IOhOneMarkTracker : IMarkTracker
    {
        Guid PlayerID { get; set; }
        int Score { get; set; }
        bool MarkShot(Shot shot);
    }
}
