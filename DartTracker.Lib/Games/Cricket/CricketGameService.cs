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
using System.Text;
using System.Threading.Tasks;
namespace DartTracker.Lib.Games.Cricket
{
    public class CricketGameService : IGameService
    {
        public DartGameIncrementor Incrementor { get; protected set; }
        private Dictionary<Guid, CricketPlayerMarkTracker> ShotBoard;

        protected Player PlayerUp
        {
            get
            {
                return Game.Players[Incrementor.PlayerUp];
            }
        }

        protected CricketPlayerMarkTracker CurrentPlayerTracker
        {
            get
            {
                return ShotBoard[PlayerUp.ID];
            }
        }

        private Game _game;
        public Game Game
        {
            get
            {
                if (this.ShotBoard == null) ShotBoard = StartShotboard(_game.Players);

                _game.Players = _game.Players.Select(x =>
                {
                    x.Score = this.ShotBoard[x.ID].Score;
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

        private Turn _turn = new Turn();

        public CricketGameService(
            Game game
            )
        {
            game.Type = GameType.Cricket;
            Game = game;
            ShotBoard = StartShotboard(game.Players);
            Incrementor = new DartGameIncrementor(game.Players.Count);
        }

        private Dictionary<Guid, CricketPlayerMarkTracker> StartShotboard(List<Player> players)
        {
            return players.ToDictionary(
                (x) => x.ID,
                (y) => new CricketPlayerMarkTracker(y.ID));
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
                .Select(kvp => kvp.Value)
                .OrderByDescending(x => x.Score)
                .First()
                .IsClosedOut;

        private List<int> _scoringShots = new List<int>() { 15, 16, 17, 18, 19, 20, 25 };

        public async Task TakeShot(int numberHit, ContactType contactType)
        {
            Shot shot = new Shot()
            {
                TurnId = this._turn.ID,
                Contact = contactType,
                NumberHit = numberHit
            };

            _turn.Shots.Add(shot);

            bool closedout =
                _scoringShots.Contains(numberHit)
                && this.ShotBoard
                    .Where(x => x.Key != this.PlayerUp.ID)
                    .Select(x => x.Value.Marks[shot.NumberHit])
                    .Min() == 3;

            CurrentPlayerTracker.MarkShot(shot, closedout);

            if (GameWonEvent != null && await GameWon())
            {
                GameWonEvent(this, new GameWonEvent()
                {
                    Players = this.Game.Players,
                    WinningPlayer = this.WinningPlayer()
                });
            }
            Incrementor = Incrementor.Increment();
        }
    }
}
