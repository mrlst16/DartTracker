using DartTracker.Interface.Mapper;
using DartTracker.Model.ConversionRequests;
using DartTracker.Model.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DartTracker.Lib.Mappers
{
    public class ConsolePlayerMapper : IMapper<ConsolePlayerConversionRequest, List<Player>>
    {
        public async Task<List<Player>> Map(ConsolePlayerConversionRequest source)
            => source
                .Names
                .Select((x, i) => new Player() {
                    GameID = source.Game.ID,
                    Name = x,
                    Order = i
                })
            .ToList();
    }
}
