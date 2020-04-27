using DartTracker.Interface.Games;
using DartTracker.Lib.Extensions;
using DartTracker.Lib.Games.Cricket.MarkTracker;
using DartTracker.Lib.Games.MarkTrackers;
using DartTracker.Lib.Helpers;
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

namespace DartTracker.Lib.Games.Cricket
{
    public class CricketCutthroatGameService : GameServiceBase
    {
        private Dictionary<Guid, CricketCutthroatPlayerMarkTracker> ShotBoard { get => _game.Players.CalculateForCricketCutthroat(_game.Shots); }

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

        protected override GameType Type => GameType.CricketCutthroat;

        public override event EventHandler GameWonEvent;

        public CricketCutthroatGameService(
            Game game
            ) : base(game)
        {
        }

        public override async Task<bool> GameWon()
        {
            var result = ShotBoard
                   ?.Select(kvp => kvp.Value)
                   ?.OrderBy(x => x.Score)
                   ?.FirstOrDefault()
                   ?.IsClosedOut ?? false;
            return result;
        }

        public override async Task<Player> WinningPlayer()
        {
            var winnningPlayerId = ShotBoard
                ?.Select(kvp => kvp.Value)
                ?.OrderBy(x => x.Score)
                ?.First().PlayerID ?? Guid.Empty;
            return this.Game.Players.FirstOrDefault(x => x.ID == winnningPlayerId);
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
    }
}
