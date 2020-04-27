using DartTracker.Interface.Games;
using DartTracker.Lib.Helpers;
using DartTracker.Model.Enum;
using DartTracker.Model.Games;
using DartTracker.Model.Players;
using DartTracker.Model.Shooting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DartTracker.Lib.Games
{
    public abstract class GameServiceBase : IGameService
    {
        protected abstract GameType Type { get; }
        protected DartGameIncrementor Incrementor { get => new DartGameIncrementor(_game.Players.Count).SetShots(_game.Shots.Count); }

        protected Game _game;
        public abstract Game Game { get; protected set; }

        public int PlayerUp => Incrementor.PlayerUp;

        public int Round => Incrementor.Round;

        public int ShotCount => Incrementor.Shots;

        public abstract event EventHandler GameWonEvent;

        public abstract Task<bool> GameWon();

        public virtual async Task RemoveLastShot()
        {
            if (_game.Shots.Count < 1) return;
            _game.Shots.RemoveAt(_game.Shots.Count - 1);
        }

        public virtual async Task TakeShot(int numberHit, ContactType contactType)
        {
            Shot shot = new Shot()
            {
                Contact = contactType,
                NumberHit = NumberHit(numberHit, contactType)
            };
            await TakeShot(shot);
        }

        public abstract Task TakeShot(Shot shot);

        public abstract Task<Player> WinningPlayer();

        protected GameServiceBase(
            Game game
            )
        {
            _game = game;
            _game.Type = Type;
        }

        protected int NumberHit(int numberHit, ContactType contactType)
        {
            switch (contactType)
            {
                case ContactType.BullsEye:
                case ContactType.DoubleBullsEye:
                    return 25;
                default:
                    return numberHit;
            }
        }
    }
}
