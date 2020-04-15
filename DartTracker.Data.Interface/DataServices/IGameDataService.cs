using DartTracker.Data.Model;
using DartTracker.Model.Games;
using System.Collections.Generic;

namespace DartTracker.Data.Interface.DataServices
{
    public interface IGameDataService
    {
        List<EntityIndex> GameIndexes { get; }
        bool SaveGame(Game game, string index);
        Game LoadGame(string name);
    }
}
