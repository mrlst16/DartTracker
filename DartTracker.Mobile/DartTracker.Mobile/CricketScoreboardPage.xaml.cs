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
            )
        {
            
            InitializeComponent();
        }
    }
}