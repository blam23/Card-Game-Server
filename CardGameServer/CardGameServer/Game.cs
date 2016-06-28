using System.Collections.Generic;
using CardGameServer.Effects;

namespace CardGameServer
{
    /// <summary>
    /// This class loads in and holds all of the actual game data.
    /// 
    /// It also keeps a singleton of the Board class.
    /// 
    /// Make sure to use Load before StartGame and only use StartGame
    ///  once every player has connected as it will actually start the 
    ///  game!
    /// </summary>
    public static class Game
    {
        public static readonly EffectLoader<Creature> CreatureEffects = new EffectLoader<Creature>();
        public static Dictionary<string, Creature> Creatures = new Dictionary<string, Creature>();

        public static readonly EffectLoader<Spell> SpellEffects = new EffectLoader<Spell>();
        public static Dictionary<string, Spell> Spells;

        public static readonly EffectLoader<Card> CardEffects = new EffectLoader<Card>();
        public static Dictionary<string, Card> Cards = new Dictionary<string, Card>();

        public static int TurnInterval = 60000;

        public static Board Board;

        public static void Load()
        {
            CreatureEffects.LoadFromAssembly();
            SpellEffects.LoadFromAssembly();
            CardEffects.LoadFromAssembly();


            Cards = new Dictionary<string, Card>();
            Spells = DataLoader.LoadSpells(ref Cards);
            Creatures = DataLoader.LoadCreatures(ref Cards);
        }

        public static void StartGame(List<Player> players)
        {
            Board = new Board(players);            
        }
    }
}
