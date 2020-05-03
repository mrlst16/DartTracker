using DartTracker.Lib.Extensions;
using DartTracker.Lib.Games.MarkTrackers;
using DartTracker.Model.Enum;
using DartTracker.Model.Events;
using DartTracker.Model.Games;
using DartTracker.Model.Players;
using DartTracker.Model.Shooting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTracker.Lib.Games.OhOne
{
    public class ThreeOhOneOinOOutGameService : GameServiceBase
    {
        private Dictionary<Guid, OhOneOInOOutMarkTracker> ShotBoard { get => _game.Players.CalculateFor01OpenInOpenOut(_game.Shots, 301); }

        public override Game Game
        {
            get
            {
                var shotboard = ShotBoard;

                _game.Players = _game.Players.Select((x, i) =>
                {
                    x.Score = shotboard[x.ID].Score;
                    x.Order = i;
                    return x;
                }).ToList();
                return _game;
            }
            protected set
            {
                _game = value;
            }
        }

        protected override GameType Type => GameType.ThreeOhOneOInOOut;

        public override event EventHandler GameWonEvent;

        public async override Task<bool> GameWon()
        {
            return (await WinningPlayer()).Score == 0;
        }

        public override async Task TakeShot(Shot shot)
        {
            shot.NumberHit = NumberHit(shot.NumberHit, shot.Contact);
            _game.Shots.Add(shot);

            if (await GameWon())
            {
                GameWonEvent?.Invoke(this, new GameWonEvenArgs()
                {
                    Players = this._game.Players,
                    WinningPlayer = await this.WinningPlayer()
                });
            }
        }

        public async override Task<Player> WinningPlayer()
        {
            Guid winningPlayerId = ShotBoard
                .Values
                .OrderBy(x => x.Score)
                .Select(x => x.PlayerID)
                .FirstOrDefault();
            return Game.Players.FirstOrDefault(x => x.ID == winningPlayerId);
        }

        public ThreeOhOneOinOOutGameService(Game game) : base(game)
        {
        }

    }
}
