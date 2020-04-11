using DartTracker.Model.Games;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DartTracker.Mobile.Interface.Services.Scoreboard
{
    public interface IScoreboardService
    {
        View BuildScoreboard(Game game);
    }
}
