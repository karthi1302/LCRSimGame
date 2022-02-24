using System;
using System.Collections.Generic;

namespace LCRSimGame.Model
{
    /// <summary>
    /// Game
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The players
        /// </summary>
        private List<Player> _players;
        /// <summary>
        /// The players with chips
        /// </summary>
        private int _playersWithChips;
        /// <summary>
        /// The turns
        /// </summary>
        private int _turns;
        // plays a single game and returns turn count
        /// <summary>
        /// Plays the specified number players.
        /// </summary>
        /// <param name="numPlayers">The number players.</param>
        /// <returns></returns>
        public int Play(int numPlayers)
        {
            // Initialize all players
            _players = new List<Player>();
            for (int i = 0; i < numPlayers; i++)
                _players.Add(new Player());
            _playersWithChips = numPlayers; // all players will have 3 chips

            // Play
            while (_playersWithChips > 1)
            {
                PlayerWon = PlayRound();
            }
            return _turns;
        }

        /// <summary>
        /// Gets or sets the player won.
        /// </summary>
        /// <value>
        /// The player won.
        /// </value>
        public Player PlayerWon
        {
            get;set;
        }

        // makes each player roll dice and return turn count
        /// <summary>
        /// Plays the round.
        /// </summary>
        /// <returns></returns>
        private Player PlayRound()
        {
            int n = 0;
            foreach (Player p in _players)
            {
                n++;
                if (p.ChipsPresent > 0) // play his turn only if he has chips
                {
                    Console.Write($"Player {n} ");
                    if (!p.PlayTurn())
                        _playersWithChips--;
                    _turns++;
                    if (_playersWithChips == 1) // we have a winner
                    {
                        p.Name = n.ToString();                  
                        return p;
                    }
                    Console.WriteLine();
                }
                else
                    Console.WriteLine($"Player {n} skipped as he has no chips.");
            }
            return null;
        }

    }
}
