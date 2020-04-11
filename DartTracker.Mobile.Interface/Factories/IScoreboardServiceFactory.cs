using DartTracker.Mobile.Interface.Services.Scoreboard;
using DartTracker.Model.Enum;

namespace DartTracker.Mobile.Interface.Factories
{
    public interface IScoreboardServiceFactory
    {
        IScoreboardService Create(GameType gameType);
    }
}