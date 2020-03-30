using DartTracker.Interface.Games;
using DartTracker.Model.Games;
using System.Threading.Tasks;

namespace DartTracker.Interface.Factories
{
    public interface IGameServiceFactory
    {
        Task<IGameService> Create(Game game);
    }
}
