using DartTracker.Interface.Games;
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
        private Dictionary<Guid, CricketPlayerMarkTracker> ShotBoard;
        protected Player PlayerUp
        {
            get
            {
                return Game.Players[PlayerMarker];
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

        private Turn _turn;

        public int PlayerMarker { get; protected set; } = 0;
        public int Round { get; protected set; } = 1;

        /// <summary>
        /// _shotCount starts at 3 so that we can use the modulus operator
        /// </summary>
        private int _shotCount = 3;

        public event EventHandler GameWonEvent;

        public int ShotCount() => _shotCount - 3;

        public CricketGameService(
            Game game
            )
        {
            game.Type = GameType.Cricket;
            Game = game;
            ShotBoard = StartShotboard(game.Players);
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
                .OrderBy(x => x.Score)
                .First()
                .IsClosedOut;

        private void IncrementEverything()
        {
            if (_shotCount % 3 == 0)
            {
                if (_turn != null)
                {
                    Game.Players[PlayerMarker].Turns.Add(_turn);
                }
                //We start a new turn
                this._turn = new Turn();
            }
            if ((_shotCount + 1) % 3 == 0)
            {
                if (PlayerMarker >= Game.Players.Count - 1)
                {
                    PlayerMarker = 0;
                    Round++;
                }
                else if (_shotCount > 3)
                {
                    PlayerMarker++;
                }
            }
            _shotCount++;
        }

        public async Task TakeShot(int numberHit, ContactType contactType)
        {
            IncrementEverything();
            Shot shot = new Shot()
            {
                TurnId = this._turn.ID
            };
            _turn.Shots.Add(shot);
            ShotBoard[PlayerUp.ID].MarkShot(shot);
            if (await GameWon())
            {
                GameWonEvent(this, new GameWonEvent()
                {
                    Players = this.Game.Players,
                    WinningPlayer = this.WinningPlayer()
                });
            }
        }
    }
}
