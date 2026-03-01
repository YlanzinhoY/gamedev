using System.Collections.Generic;

namespace script
{
    public class Craps
    {

        private readonly Dictionary<string, List<int>> GameRules = new()
        {
            { "win", new List<int> { 7, 11 } },
            { "lose", new List<int> { 2, 3, 12 } },
            { "point", new List<int> { 4, 5, 6, 8, 9, 10 } }
        };
        

        public string GameState(int diceSum)
        {

            foreach (var rule in GameRules)
            {
                if (rule.Value.Contains(diceSum))
                {
                    return rule.Key;
                }
            }

            return "";
        }
        

    }
}