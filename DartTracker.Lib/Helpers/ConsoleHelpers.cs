using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DartTracker.Lib.Helpers
{
    public static class ConsoleHelpers
    {
        public static async Task<T> AskForInput<T>(Action action, Func<string, bool> stopWhenTrue, Func<string, T> convert)
        {
            action();
            string input = Console.ReadLine();
            if (stopWhenTrue(input))
                return convert(input);
            else
                return await AskForInput(action, stopWhenTrue, convert);
        }

        public static async Task<int> AskForIntegerInput(string askFor)
        {
            return await AskForInput<int>(() =>
            {
                Console.WriteLine(askFor);
            },
            (s) => int.TryParse(s, out int res),
                (s) =>
                {
                    return int.Parse(s);
                }
            );
        }

        public static async Task<int> AskForIntegerInput(string askFor, Func<int, bool> predicate)
        {
            return await AskForInput<int>(() =>
            {
                Console.WriteLine(askFor);
            },
            (s) => {
                if (int.TryParse(s, out int res))
                    return predicate(res);
                else
                    return false;
            },
                (s) =>
                {
                    return int.Parse(s);
                }
            );
        }
    }
}
