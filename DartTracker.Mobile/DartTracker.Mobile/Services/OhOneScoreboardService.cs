using DartTracker.Mobile.Interface.Services.Scoreboard;
using DartTracker.Mobile.Interface.ViewModels;
using Xamarin.Forms;

namespace DartTracker.Mobile.Services
{
    public class OhOneScoreboardService : IScoreboardService
    {
        public Page BuildScoreboard(IScoreboardVM viewModel)
        {
            var result = new OhOneScoreboardPage();
            result.Title = "ScoreBoard";
            result.BindingContext = viewModel;
            return result;
        }
    }
}
