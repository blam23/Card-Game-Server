using System;

namespace CardGameServer
{
    /// <summary>
    /// The only Random Number Generator that should be used
    ///  throughout the code.
    /// </summary>
    public static class ServerRandom
    {
        public static int Seed { get; private set; }
        public static Random Generator;

        /// <summary>
        /// Initialises the Random class, using given or generated seed.
        /// </summary>
        /// <param name="randomSeed">If a random seed should be used</param>
        /// <param name="seed">What integer to use as a seed if randomSeed is set to false</param>
        public static void Initialise(bool randomSeed = true, int seed = -1)
        {
            Seed = randomSeed ? Guid.NewGuid().GetHashCode() : seed;
            Generator = new Random(Seed);
        }
    }
}
