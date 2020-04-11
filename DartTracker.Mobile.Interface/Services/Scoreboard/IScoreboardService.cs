using DartTracker.Interface.Games;
using DartTracker.Model.Games;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DartTracker.Mobile.Interface.Services.Scoreboard
{
    public interface IScoreboardService
    {
        Page BuildScoreboard(IGameService gameService);
    }
}
