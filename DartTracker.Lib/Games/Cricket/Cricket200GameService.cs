using DartTracker.Lib.Extensions;
using DartTracker.Lib.Games.Cricket.MarkTracker;
using DartTracker.Model.Enum;
using DartTracker.Model.Events;
using DartTracker.Model.Games;
using DartTracker.Model.Players;
using DartTracker.Model.Shooting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DartTracker.Lib.Games.Cricket
{
    public class Cricket200GameService : GameServiceBase
    {
        private Dictionary<Guid, Cricket200PlayerMarkTracker> ShotBoard { get => _game.Players.CalculateForCricket200(_game.Shots); }

        public override Game Game
        {
            get
            {
                var shotboard = ShotBoard;

                _game.Players = _game.Players.Select((x, i) =>
                {
                    x.Score = shotboard[x.ID].Score;
                    x.Order = i;
                    x.Marks = shotboard[x.ID].MarksFor;
                    return x;
                }).ToList();
                return _game;
            }
            protected set
            {
                _game = value;
            }
        }

        protected override GameType Type => GameType.Cricket200;

        public Cricket200GameService(
            Game game
            ) : base(game)
        {
        }

        public override async Task<Player> WinningPlayer()
        {
            var winnningPlayerId = ShotBoard
                ?.Select(kvp => kvp.Value)
                ?.OrderByDescending(x => x.Score)
                ?.First().PlayerID ?? Guid.Empty;
            return this.Game.Players.FirstOrDefault(x => x.ID == winnningPlayerId);
        }

        public override async Task<bool> GameWon()
        {
            var result = ShotBoard
                   ?.Select(kvp => kvp.Value)
                   ?.OrderByDescending(x => x.Score)
                   ?.FirstOrDefault()
                   ?.IsClosedOut ?? false;
            if (result || this.Incrementor.Players < 2) return result;

            var winningPlayer = await WinningPlayer();
            var secondPlacePlayer = ShotBoard
                   ?.Select(kvp => kvp.Value)
                   ?.OrderByDescending(x => x.Score)
                   ?.ElementAt(1);

            result = winningPlayer.Score - secondPlacePlayer.Score >= 200
            && !ShotBoard
                .Where(x => x.Key != winningPlayer.ID)
                .Any(x => x.Value.IsClosedOut);

            return result;
        }


        public override event EventHandler GameWonEvent;

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
    }
}
