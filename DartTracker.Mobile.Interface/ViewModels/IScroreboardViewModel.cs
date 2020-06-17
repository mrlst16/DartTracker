using DartTracker.Interface.Games;
using DartTracker.Model.Shooting;
using System.Threading.Tasks;

namespace DartTracker.Mobile.Interface.ViewModels
{
    public interface IScoreboardVM
    {
        Task TakeShot(Shot shot);
        IGameService GameService { get; }
        Task RemoveLastShot();
    }
}
