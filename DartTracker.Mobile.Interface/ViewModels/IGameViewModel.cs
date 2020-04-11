using DartTracker.Interface.Games;
using DartTracker.Model.Shooting;
using System.Threading.Tasks;

namespace DartTracker.Mobile.Interface.ViewModels
{
    public interface IGameViewModel
    {
        Task TakeShot(Shot shot);
        IGameService GameService { get; }
    }
}
