using CommonStandard.Extensions;
using DartTracker.Interface.Games;
using DartTracker.Lib.Helpers;
using DartTracker.Model.Shooting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DartTracker.Mobile.ViewModels
{
    public class DartboardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IGameService _gameService;

        public DartboardViewModel(
            IGameService gameService
            )
        {
            _gameService = gameService;
            CalculatePlayerUpAndRoundShots();
        }

        private string _playerUp = string.Empty;
        public string PlayerUp
        {
            get => _playerUp; set
            {
                _playerUp = value;
                var args = new PropertyChangedEventArgs(nameof(PlayerUp));
                PropertyChanged?.Invoke(this, args);
            }
        }

        private string _shot1Contact = string.Empty;
        public string Shot1Contact
        {
            get => _shot1Contact; set
            {
                _shot1Contact = value;
                var args = new PropertyChangedEventArgs(nameof(Shot1Contact));
                PropertyChanged?.Invoke(this, args);
            }
        }

        private string _shot1NumberHit = string.Empty;
        public string Shot1NumberHit
        {
            get => _shot1NumberHit; set
            {
                _shot1NumberHit = value;
                var args = new PropertyChangedEventArgs(nameof(Shot1NumberHit));
                PropertyChanged?.Invoke(this, args);
            }
        }

        private string _shot2Contact = string.Empty;
        public string Shot2Contact
        {
            get => _shot2Contact; set
            {
                _shot2Contact = value;
                var args = new PropertyChangedEventArgs(nameof(Shot2Contact));
                PropertyChanged?.Invoke(this, args);
            }
        }

        private string _shot2NumberHit = string.Empty;
        public string Shot2NumberHit
        {
            get => _shot2NumberHit; set
            {
                _shot2NumberHit = value;
                var args = new PropertyChangedEventArgs(nameof(Shot2NumberHit));
                PropertyChanged?.Invoke(this, args);
            }
        }

        private string _shot3Contact = string.Empty;
        public string Shot3Contact
        {
            get => _shot3Contact; set
            {
                _shot3Contact = value;
                var args = new PropertyChangedEventArgs(nameof(Shot3Contact));
                PropertyChanged?.Invoke(this, args);
            }
        }

        private string _shot3NumberHit = string.Empty;
        public string Shot3NumberHit
        {
            get => _shot3NumberHit; set
            {
                _shot3NumberHit = value;
                var args = new PropertyChangedEventArgs(nameof(Shot3NumberHit));
                PropertyChanged?.Invoke(this, args);
            }
        }

        public void CalculatePlayerUpAndRoundShots()
        {
            var game = _gameService.Game;

            DartGameIncrementor incrementor =
                new DartGameIncrementor(_gameService.Game.Players.Count)
                .SetShots(Math.Max(0, game.Shots.Count - 1));
            //adjust player up for display
            var last3 = game.Shots.SplitSequentially(3)?.LastOrDefault()
                ?? new System.Collections.Generic.List<Shot>();
            PlayerUp = $"Player {incrementor.PlayerUp}";

            if (last3.Count > 0)
            {
                Shot shot = last3[0];
                Shot1Contact = Contact(shot);
                Shot1NumberHit = NumberHit(shot);
            }
            else
            {
                Shot1Contact = string.Empty;
                Shot1NumberHit = string.Empty;
            }
            if (last3.Count > 1)
            {
                Shot shot = last3[1];
                Shot2Contact = Contact(shot);
                Shot2NumberHit = NumberHit(shot);
            }
            else
            {
                Shot2Contact = string.Empty;
                Shot2NumberHit = string.Empty;
            }
            if (last3.Count > 2)
            {
                Shot shot = last3[2];
                Shot3Contact = Contact(shot);
                Shot3NumberHit = NumberHit(shot);
            }
            else
            {
                Shot3Contact = string.Empty;
                Shot3NumberHit = string.Empty;
            }

        }

        private string NumberHit(Shot shot)
        {
            switch (shot.Contact)
            {
                case Model.Enum.ContactType.Miss:
                    return "X";
                case Model.Enum.ContactType.NotShot:
                    return "";
                case Model.Enum.ContactType.Single:
                case Model.Enum.ContactType.Double:
                case Model.Enum.ContactType.Triple:
                    return shot.NumberHit.ToString().ToUpper();
                case Model.Enum.ContactType.BullsEye:
                case Model.Enum.ContactType.DoubleBullsEye:
                    return "BULL";
                default:
                    return "";
            }
        }

        private string Contact(Shot shot)
        {
            switch (shot.Contact)
            {
                case Model.Enum.ContactType.NotShot:
                    return "";
                case Model.Enum.ContactType.BullsEye:
                    return "SINGLE";
                case Model.Enum.ContactType.DoubleBullsEye:
                    return "DOUBLE";
                default:
                    return shot.Contact.ToString().ToUpper();
            }
        }
    }
}
