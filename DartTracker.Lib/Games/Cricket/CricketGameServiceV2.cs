using DartTracker.Interface.Games;
using DartTracker.Model.Enum;
using DartTracker.Model.Games;
using DartTracker.Model.Shooting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DartTracker.Lib.Games.Cricket
{
    public class CricketGameServiceV2 : IGameService
    {
        public Game Game { get; protected set; }

        public event EventHandler GameWonEvent;

        public Task<bool> GameWon()
        {
            throw new NotImplementedException();
        }

        public async Task TakeShot(int numberHit, ContactType contactType)
        {
            Shot shot = new Shot()
            {
                Contact = contactType,
                NumberHit = numberHit
            };
        }
    }
}
