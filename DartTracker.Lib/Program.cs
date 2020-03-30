using DartTracker.Interface.Factories;
using DartTracker.Interface.Mapper;
using DartTracker.Lib.Factories;
using DartTracker.Lib.Helpers;
using DartTracker.Lib.Mappers;
using DartTracker.Model.ConversionRequests;
using DartTracker.Model.Enum;
using DartTracker.Model.Games;
using DartTracker.Model.Players;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DartTracker.Lib
{
    class Program
    {

        private static IGameServiceFactory _gameServiceFactory
            = new GameServiceFactory();
        private static IMapper<ConsolePlayerConversionRequest, List<Player>> _playerMapper
            = new ConsolePlayerMapper();

        private static async Task<GameType> AskForGameType()
        {
            int selection = await ConsoleHelpers.AskForIntegerInput(
                "Please Select a game.... 1 for cricket, 2 for 301",
                (res) => res >= 1 && res <= 2
                );

            return (GameType)selection;
        }

        private static void AskForPlayersInput(ref List<string> results)
        {
            Console.WriteLine("Enter Player Name; type DONE when done entering names");
            var playerName = Console.ReadLine();
            if (playerName.ToLowerInvariant() != "done")
            {
                results.Add(playerName);
                AskForPlayersInput(ref results);
            }
        }

        private static void OutputPlayers(List<Player> players)
        {
            foreach (var player in players)
            {
                Console.WriteLine($"Player numer {player.Order}: {player.Name}");
            }
        }

        static async Task Main(string[] args)
        {
            var game = new Game()
            {
                ID = Guid.NewGuid()
            };
            game.Type = await AskForGameType();

            Console.WriteLine($"You selcted to play {game.Type}");

            List<string> playerNames = new List<string>();
            AskForPlayersInput(ref playerNames);

            game.Players = await _playerMapper.Map(new ConsolePlayerConversionRequest()
            {
                Game = game,
                Names = playerNames
            });

            var service = await _gameServiceFactory.Create(game);

            OutputPlayers(service.Game.Players);



            var stopResponse = Console.Read();
        }
    }
}
