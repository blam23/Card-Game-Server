using System.Collections.Generic;

namespace CardGameServer.Effects
{
    /// <summary>
    /// An effect that will trigger it's "ondeath" spells when the creature
    ///  it's attached to dies.
    /// </summary>
    [CustomValue("ondeath", typeof(List<Spell>))]
    [NamedEffect("deathrattle", typeof(Creature))]
    public class DeathRattle : IEffect<Creature>
    {
        // Actual instances of the spells that will be cast when the creature dies.
        private readonly List<Spell> _onDeathSpells = new List<Spell>();
        // Non instanciated spells, these are used only to generate the actual spells.
        // We need these because the actual spells need an owner Creature which we
        // don't get until the effect Starts.
        private List<Spell> _onDeathSpellData; 
        private Creature _owner;
        private string desc = "Deathrattle: ";

        public void Initialise()
        {
            // Store these for now until we can instanciate them in the Start method
            _onDeathSpellData = Attributes.GetArray<Spell>("ondeath");
        }

        public void Start(Creature ownerCreature)
        {
            _owner = ownerCreature;
            // Instanciate each spell and add it's description to our deathrattle
            // effect.
            foreach (var spell in _onDeathSpellData)
            {
                var instance = spell.CreateInstance(_owner.Owner);
                desc += $"{instance.GetDescription()}.";
                _onDeathSpells.Add(instance);
            }
            ownerCreature.Killed += OwnerCreatureOnKilled;
        }

        private void OwnerCreatureOnKilled(Creature creature)
        {
            // When the creature dies -> Cast all spells.
            foreach (var spell in _onDeathSpells)
            {
                Game.Board.Cast(creature.Owner, spell);
            }
        }


        public void End()
        {
            // If this effect is removed, remove it's effect!
            _owner.Killed -= OwnerCreatureOnKilled;
        }


        public void Update()
        {
            
        }

        public string Description => desc;

        // This needs to persist, as it can take multiple turns
        // for the effect to happen.
        public bool Persistent => true;
        public bool Remove { get; set; }
        public int TimeLeft { get; set; }
        public CustomAttributes Attributes { get; set; }
    }
}
