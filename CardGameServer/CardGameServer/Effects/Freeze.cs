using System.Collections.Generic;

namespace CardGameServer.Effects
{
    /// <summary>
    /// A spell effect that will freeze the target for [length] turns.
    /// </summary>
    [CustomValue("length", typeof(int), false, 1)]
    [NamedEffect("freeze", typeof(Spell))]
    class FreezeEffect : IEffect<Spell>
    {
        private Spell _owner;
        private string _desc;
        private List<Creature> _targets;

        public void Initialise()
        {
            // Load in the data from the provided attributes.
            TimeLeft = Attributes.GetInt("length");

            // Generate the description based on damage and type.
            _desc = $"apply freeze for {TimeLeft} turn" + (TimeLeft != 1 ? "s" : "");
        }

        public void Start(Spell owner)
        {
            _owner = owner;
            owner.SpellCast += OwnerOnSpellCast;
        }

        /// <summary>
        /// Marks all the creatures as unable to attack
        /// </summary>
        /// <param name="spell">The spell that is using this effect</param>
        /// <param name="creatures">Targets that will be frozen!</param>
        private void OwnerOnSpellCast(Spell spell, List<Creature> creatures)
        {
            _targets = creatures;
            
            foreach (var creature in creatures)
            {
                creature.CanAttack = false;
            }
        }

        public void End()
        {
            _owner.SpellCast -= OwnerOnSpellCast;
        }

        public void Update()
        {
            foreach (var creature in _targets)
            {
                creature.CanAttack = false;
            }
        }

        public bool Persistent => false;
        public bool Remove { get; set; }
        public int TimeLeft { get; set; }
        public string Description => _desc;
        public CustomAttributes Attributes { get; set; }
    }
}
