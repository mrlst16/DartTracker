using CommonStandard.Interface.Repository;
using DartTracker.Data.Interface.DataServices;
using DartTracker.Data.Model;
using DartTracker.Model.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DartTracker.Data.Services
{
    public class GameDataService : IGameDataService
    {
        private readonly IRepository<Entity<List<EntityIndex>>, string> _gameIndexResposity;
        private readonly IRepository<Entity<Game>, string> _gameResposity;

        public GameDataService(
            IRepository<Entity<List<EntityIndex>>, string> gameIndexResposity,
            IRepository<Entity<Game>, string> gameResposity
            )
        {
            _gameIndexResposity = gameIndexResposity;
            _gameResposity = gameResposity;
        }

        public List<EntityIndex> GameIndexes
        {
            get
            {
                var entity = _gameIndexResposity.Get("savedgames");
                if (entity == null) return new List<EntityIndex>();
                return entity.Value;
            }
        }

        public Game LoadGame(string name)
        {
            var gameEntity = _gameResposity.Get(name);
            Game result = gameEntity?.Value ?? null;
            return result;
        }

        public bool SaveGame(Game game, string indexName)
        {
            try
            {
                Entity<Game> gameEntity = _gameResposity.Get(indexName);
                if (gameEntity == null)
                {
                    gameEntity = new Entity<Game>()
                    {
                        ID = game.ID,
                        CreatedUTC = DateTime.UtcNow,
                    };
                }

                gameEntity.Value = game;
                gameEntity.ModifiedUTC = DateTime.UtcNow;

                bool gameSaved = _gameResposity.Set(indexName, gameEntity);
                var gsd = _gameResposity.Get(indexName);
                if (!gameSaved) return false;

                Entity<List<EntityIndex>> gameIndexEntity = _gameIndexResposity.Get("savedgames");
                if (gameIndexEntity == null)
                {
                    gameIndexEntity = new Entity<List<EntityIndex>>()
                    {
                        CreatedUTC = DateTime.UtcNow,
                        ID = Guid.NewGuid(),
                        Value = new List<EntityIndex>()
                    };
                }
                gameIndexEntity.ModifiedUTC = DateTime.UtcNow;

                if (!gameIndexEntity.Value.Any(x => x.Name == indexName))
                {
                    gameIndexEntity.Value.Add(new EntityIndex()
                    {
                        Key = indexName,
                        Name = indexName
                    });
                    _gameIndexResposity.Set("savedgames", gameIndexEntity);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}