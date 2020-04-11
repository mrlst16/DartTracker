using DartTracker.Interface.Games;
using DartTracker.Lib.Games.Cricket;
using DartTracker.Model.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DartTracker.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CricketScoreboardPage : ContentPage
    {
        private readonly IGameService _gameService;

        public CricketScoreboardPage(
            IGameService gameService
            )
        {
            _gameService = gameService;
            _gameService.GameWonEvent += async (sender, eventArgs) =>
            {
                if (eventArgs is GameWonEvenArgs arg)
                {
                    await DisplayAlert("Winner", $"Player {arg.WinningPlayer.Order} wins!", "Finsih Game");
                }
            };
            InitializeComponent();
        }
    }
}