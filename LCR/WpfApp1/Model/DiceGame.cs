using System.Collections.Generic;

namespace LCRSimGame.Model
{
    /// <summary>
    /// DiceGame
    /// </summary>
    public class DiceGame
    {
        // Plays and returns list of turn count for each game (from which you can check min/max/avg)
        /// <summary>
        /// Plays the specified number players.
        /// </summary>
        /// <param name="numPlayers">The number players.</param>
        /// <param name="timesToPlay">The times to play.</param>
        /// <returns></returns>
        public static List<KeyValuePair<int,Player>> Play(int numPlayers, int timesToPlay)
        {
            List<KeyValuePair<int, Player>> turnCounts = new List<KeyValuePair<int, Player>>();
            for (int i = 0; i < timesToPlay; i++)
            {
                var game = new Game();
                var turns = game.Play(numPlayers);
                var won = game.PlayerWon;
                turnCounts.Add(new KeyValuePair<int, Player>(turns, won));
            }
            return turnCounts;
        }

    }
}
