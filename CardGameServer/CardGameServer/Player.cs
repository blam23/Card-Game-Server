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
        /// The list of active players for a given player.
        /// </summary>
        public List<Creature> Creatures;

        /// <summary>
        /// Cards in the player's hand.
        /// </summary>
        public Dictionary<Guid, Card> Cards;

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
    }
}