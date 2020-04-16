using DartTracker.Interface.Games;
using DartTracker.Lib.Helpers;
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
    public class CricketGameService : IGameService
    {
        public DartGameIncrementor Incrementor { get => new DartGameIncrementor(_game.Players.Count).SetShots(_game.Shots.Count); }
        private Dictionary<Guid, CricketPlayerMarkTracker> ShotBoard { get => _game.Players.CalculateForCricket(_game.Shots); }

        private Game _game;
        public Game Game
        {
            get
            {
                var shotboard = ShotBoard;

                _game.Players = _game.Players.Select((x, i) =>
                {
                    x.Score = shotboard[x.ID].Score;
                    x.Order = i;
                    x.Marks = shotboard[x.ID].Marks;
                    return x;
                }).ToList();
                return _game;
            }
            protected set
            {
                _game = value;
            }
        }

        public int PlayerUp => Incrementor.PlayerUp;

        public event EventHandler GameWonEvent;

        public CricketGameService(
            Game game
            )
        {
            game.Type = GameType.Cricket200;
            Game = game;
        }

        private Player WinningPlayer()
        {
            var winnningPlayerId = ShotBoard
                ?.Select(kvp => kvp.Value)
                ?.OrderByDescending(x => x.Score)
                ?.First().PlayerID ?? Guid.Empty;
            return this.Game.Players.FirstOrDefault(x => x.ID == winnningPlayerId);
        }

        public async Task<bool> GameWon()
        {
            var result = ShotBoard
                   ?.Select(kvp => kvp.Value)
                   ?.OrderByDescending(x => x.Score)
                   ?.FirstOrDefault()
                   ?.IsClosedOut ?? false;
            if (result || this.Incrementor.Players < 2) return result;

            var winningPlayer = WinningPlayer();
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

        public static readonly List<int> ScoringNumbers = new List<int>() { 15, 16, 17, 18, 19, 20, 25 };

        public async Task TakeShot(int numberHit, ContactType contactType)
        {
            Shot shot = new Shot()
            {
                Contact = contactType,
                NumberHit = NumberHit(numberHit, contactType)
            };
            await TakeShot(shot);
        }

        public async Task TakeShot(Shot shot)
        {
            _game.Shots.Add(shot);

            if (await GameWon())
            {
                GameWonEvent?.Invoke(this, new GameWonEvenArgs()
                {
                    Players = this._game.Players,
                    WinningPlayer = this.WinningPlayer()
                });
            }
        }

        private int NumberHit(int numberHit, ContactType contactType)
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

        public async Task RemoveLastShot()
        {
            if (_game.Shots.Count < 1) return;
            _game.Shots.RemoveAt(_game.Shots.Count - 1);
        }
    }
}
