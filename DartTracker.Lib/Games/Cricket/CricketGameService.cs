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
        public DartGameIncrementor Incrementor { get => new DartGameIncrementor(_game.Players.Count).SetShots(Shots.Count); }
        private Dictionary<Guid, CricketPlayerMarkTracker> ShotBoard;

        public List<Shot> Shots { get; protected set; } = new List<Shot>();

        private Game _game;
        public Game Game
        {
            get
            {
                var shotboard = _game.Players.Calculate(Shots);

                _game.Players = _game.Players.Select(x =>
                {
                    x.Score = shotboard[x.ID].Score;
                    return x;
                }).ToList();
                return _game;
            }
            protected set
            {
                _game = value;
            }
        }

        public event EventHandler GameWonEvent;

        public CricketGameService(
            Game game
            )
        {
            game.Type = GameType.Cricket;
            Game = game;
        }

        private Player WinningPlayer()
        {
            var winnningPlayerId = ShotBoard
                .Select(kvp => kvp.Value)
                .OrderBy(x => x.Score)
                .First().PlayerID;
            return this.Game.Players.FirstOrDefault(x => x.ID == winnningPlayerId);
        }

        public async Task<bool> GameWon()
            => ShotBoard
                ?.Select(kvp => kvp.Value)
                .OrderByDescending(x => x.Score)
                .First()
                .IsClosedOut ?? false;

        public static readonly List<int> ScoringNumbers = new List<int>() { 15, 16, 17, 18, 19, 20, 25 };

        public async Task TakeShot(int numberHit, ContactType contactType)
        {
            Shot shot = new Shot()
            {
                Contact = contactType,
                NumberHit = numberHit
            };

            Shots.Add(shot);
            ShotBoard = _game.Players.Calculate(Shots);
            if (GameWonEvent != null && await GameWon())
            {
                GameWonEvent(this, new GameWonEvent()
                {
                    Players = this.Game.Players,
                    WinningPlayer = this.WinningPlayer()
                });
            }
        }

        public async Task RemoveLastShot()
        {
            if (Shots.Count < 1) return;

            Shots.RemoveAt(Shots.Count - 1);
            ShotBoard = _game.Players.Calculate(Shots);
        }
    }


}
