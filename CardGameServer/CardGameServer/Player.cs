using System;
using System.Collections.Generic;
using CardProtocolLibrary;

namespace CardGameServer
{
    /// <summary>
    /// Class representing a player.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Starts a turn for the player.
        /// </summary>
        public void DoMove()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Which Creature is the "Commander" for this player.
        /// If this commander dies, the player loses.
        /// </summary>
        public Creature Commander { get; set; }

        /// <summary>
        /// Determines if this player has sent all the relevant
        ///  data and can start the match.                   5
        /// </summary>
        public bool SetupComplete { get; set; }

        /// <summary>
        /// The list of active players for a given player.
        /// </summary>
        public List<Creature> Creatures = new List<Creature>();

        /// <summary>
        /// Cards in the player's hand.
        /// </summary>
        public Dictionary<SID, Card> Cards = new Dictionary<SID, Card>();

        /// <summary>
        /// Cards in the player's deck.
        /// </summary>
        public Deck Deck;

        /// <summary>
        /// The network interface for the player.
        /// </summary>
        public GameActionWriter DataWriter;

        /// <summary>
        /// Player's ID !! FOR THIS GAME ONLY !!
        /// </summary>
        public int ID;

        public string Name;
    }
}