using DartTracker.Model.Enum;
using DartTracker.Model.Games;
using DartTracker.Model.Players;
using DartTracker.Model.Shooting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DartTracker.Lib.Games.OhOne
{
    public class ThreeOhOneGameService : GameServiceBase
    {

        public override Game Game { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

        protected override GameType Type => GameType.ThreeOOne;

        public override event EventHandler GameWonEvent;

        public override Task<bool> GameWon()
        {
            throw new NotImplementedException();
        }

        public override Task TakeShot(Shot shot)
        {
            throw new NotImplementedException();
        }

        public override Task<Player> WinningPlayer()
        {
            throw new NotImplementedException();
        }

        public ThreeOhOneGameService(Game game) : base(game)
        {
        }

    }
}
