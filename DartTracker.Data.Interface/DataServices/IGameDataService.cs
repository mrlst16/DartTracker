using DartTracker.Data.Model;
using DartTracker.Model.Games;
using System.Collections.Generic;

namespace DartTracker.Data.Interface.DataServices
{
    public interface IGameDataService
    {
        bool SaveGame(Game game, string index);

        List<EntityIndex> GameIndexes { get; }
    }
}
