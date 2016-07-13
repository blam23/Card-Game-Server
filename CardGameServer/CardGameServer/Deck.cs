using System.Collections.Generic;

namespace CardGameServer
{
    /// <summary>
    /// Holds a deck of cards!
    /// Provides methods for viewing, adding and removing cards.
    /// </summary>
    public class Deck
    {
        public List<Card> Cards = new List<Card>();
        public Player Owner;
        public static int CardLimit => 30;

        /// <summary>
        /// How many of the same card are allowed in a deck
        /// </summary>
        public static int CardDuplicateLimit => 2;

        /// <summary>
        /// Removes the topmost card from the deck.
        /// </summary>
        /// <returns></returns>
        public Card Pop()
        {
            var card = Cards[Cards.Count - 1];
            Cards.RemoveAt(Cards.Count - 1);
            return card;
        }

        /// <summary>
        /// Get the top X cards of the deck, without removing them.
        /// !! Doesn't generate new card instances !!
        /// </summary>
        /// <returns>Top cards in this deck</returns>
        public IEnumerable<Card> Peek(int amount)
        {
            return Cards.GetRange(Cards.Count-amount-1,amount);
        }

        /// <summary>
        /// Get the top card in the deck, without removing it.
        /// !! Doesn't generate new card instances !!
        /// </summary>
        /// <returns>Top card in this deck</returns>
        public Card Peek()
        {
            return Cards[Cards.Count - 1];
        }

        /// <summary>
        /// Gets a random card from the deck.
        /// </summary>
        /// <returns>A random card</returns>
        public Card PeekRandom()
        {
            var random = ServerRandom.Generator.Next(0, Cards.Count);
            return Cards[random];
        }  

        // TODO: Remove Hard Coded Draw from Board.cs
        /// <summary>
        /// Draws the specified amount of cards from the deck and
        /// returns them.
        /// </summary>
        /// <param name="amount">Amount of cards to draw</param>
        /// <returns>The cards drawn</returns>
        public IEnumerable<Card> Draw(int amount = 1)
        {
            for (var i = 0; i < amount; i++)
            {
                var drawn = Pop();
                var instance = drawn.CreateInstance();
                yield return instance;
            }
        }

        /// <summary>
        /// Adds a card into the deck at the specified position.
        /// If place is blank then card is added to the bottom of the deck.
        /// </summary>
        /// <param name="c">Card to push</param>
        /// <param name="place">Place where we want to insert card</param>
        public void Push(Card c, int place = -1)
        {
            Cards.Insert(place, c);
        }

        /// <summary>
        /// Adds specified card into deck at random position.
        /// </summary>
        /// <param name="c"></param>
        public void PushRandom(Card c)
        {
            Push(c, ServerRandom.Generator.Next(0, Cards.Count+1));
        }
    }
}
