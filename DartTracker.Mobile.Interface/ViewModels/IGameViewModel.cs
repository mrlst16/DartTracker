using DartTracker.Model.Shooting;
using System.Threading.Tasks;

namespace DartTracker.Mobile.Interface.ViewModels
{
    public interface IGameViewModel
    {
        Task TakeSot(Shot shot);
    }
}
