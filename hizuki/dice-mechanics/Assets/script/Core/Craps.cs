using System.Collections.Generic;
using script.Controller;

namespace script
{
    public class Craps
    {

        private readonly Dictionary<MatchState, List<int>> GameRules = new()
        {
            { MatchState.Win, new List<int> { 7, 11 } },
            { MatchState.Lose, new List<int> { 2, 3, 12 } },
            { MatchState.Point, new List<int> { 4, 5, 6, 8, 9, 10 } }
        };
        

        public MatchState? GameState(int diceSum)
        {

            foreach (var rule in GameRules)
            {
                if (rule.Value.Contains(diceSum))
                {
                    return rule.Key;
                }
            }

            return null;
        }
        

    }
}