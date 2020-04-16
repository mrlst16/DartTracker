using DartTracker.Model.Enum;
using DartTracker.Model.Games;
using DartTracker.Model.Shooting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DartTracker.Interface.Games
{
    public interface IGameService
    {
        event EventHandler GameWonEvent;
        Game Game { get; }
        Task TakeShot(int numberHit, ContactType contactType);
        Task TakeShot(Shot shot);
        Task RemoveLastShot();
        Task<bool> GameWon();
        int PlayerUp { get; }
    }
}
