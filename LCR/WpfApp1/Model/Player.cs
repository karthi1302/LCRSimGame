using System;

namespace LCRSimGame.Model
{
    /// <summary>
    /// Player
    /// </summary>  
    public class Player : IPlayer
    {
        /// <summary>
        /// Gets the chips present.
        /// </summary>
        /// <value>
        /// The chips present.
        /// </value>
        public int ChipsPresent { get; private set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Player"/> is winner.
        /// </summary>
        /// <value>
        ///   <c>true</c> if winner; otherwise, <c>false</c>.
        /// </value>
        public bool Winner { get; set; }
        /// <summary>
        /// The random
        /// </summary>
        private Random _rnd;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        public Player()
        {
            Reset();
        }



        /// <summary>
        /// Resets this instance.
        /// </summary>
        private void Reset()
        {
            ChipsPresent = 3;
            _rnd = new Random(Guid.NewGuid().GetHashCode());
        }

        // Returns false if the player lost (doesn't have any chips)
        /// <summary>
        /// Plays the turn.
        /// </summary>
        /// <returns></returns>
        public bool PlayTurn()
        {
            Console.Write("rolled ");
            int count = ChipsPresent;
            for (int i = 0; i < count; i++)
            {
                int r = _rnd.Next(1, 6);
                Console.Write($"{r} ");
                // assume 1-3 are LCR and 4-6 are dots
                if (r < 4) // this dice takes off one chip
                {
                    ChipsPresent--;
                }
            }
            Console.Write($". Chips remaining: {ChipsPresent}");
            return ChipsPresent > 0;
        }

      
    }
}
