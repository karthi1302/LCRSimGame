using LCRSimGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LCRSimGame
{
    /// <summary>
    /// LCR Model
    /// </summary>
    public class LCRModel
    {
        /// <summary>
        /// The data
        /// </summary>
        private List<KeyValuePair<int, int>> _data;
        /// <summary>
        /// Initializes a new instance of the <see cref="LCRModel"/> class.
        /// </summary>
        public LCRModel()
        {
            _data = new List<KeyValuePair<int, int>>();
        }
        /// <summary>
        /// Gets the presets.
        /// </summary>
        /// <value>
        /// The presets.
        /// </value>
        public List<string> Presets
        {
            get
            {
                return new List<string> {
             "3 players x 100 games",
            "4 players x 100 games",
            "5 players x 100 games",
            "5 players x 1000 games",
            "5 players x 10000 games",
            "5 players x 100000 games",
            "6 players x 100 games",
            "7 players x 100 games" };
            }
        }

        /// <summary>
        /// Sets the column chart.
        /// </summary>
        /// <param name="turns">The turns.</param>
        /// <returns>Data</returns>
        public List<KeyValuePair<int, int>> SetColumnChart(List<int> turns)
        {

            for (int i = 0; i < turns.Count; i++)
            {
                _data.Add(new KeyValuePair<int, int>(i + 1, turns[i]));
            }

            return _data;
        }

        /// <summary>
        /// Gets the winner.
        /// </summary>
        /// <param name="turnsAndPlayWin">The turns and play win.</param>
        /// <returns>Winner</returns>
        public int GetWinner(List<KeyValuePair<int,Player>> turnsAndPlayWin)
        {
            var winnerList = turnsAndPlayWin.Select(p => p.Value).ToList();

            var winner = winnerList.Average(w => Convert.ToInt32(w.Name));
            return Convert.ToInt32(winner);
        }

        /// <summary>
        /// Operations the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <returns>operation</returns>
        public string Operations(string operation)
        {
            double result = 0;

            switch (operation)
            {
                case "Shortest":
                    result = _data.Min(v => v.Value);
                    break;
                case "Average":
                    result = _data.Average(v => v.Value);
                    break;
                case "Longest":
                    result = _data.Max(v => v.Value);
                    break;
            }

            var pairValue = _data.Find(k => k.Value == Convert.ToInt32(result));

            return $"Game :{pairValue.Key} Turns: {pairValue.Value}"; ;
        }

    }
}
