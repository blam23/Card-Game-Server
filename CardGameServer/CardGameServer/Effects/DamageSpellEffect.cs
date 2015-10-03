using System.Collections.Generic;

namespace CardGameServer.Effects
{
    /// <summary>
    /// A simple Spell Effect that will deal [amount] damage of specified [type].
    /// </summary>
    [CustomValue("amount", typeof(int), false, 1)]
    [CustomValue("type", typeof(DamageType), false, DamageType.Magic)]
    [NamedEffect("spellDamage", typeof(Spell))]
    class DamageSpellEffect : IEffect<Spell>
    {
        public int Damage = 1;
        public DamageType DamageType = DamageType.Magic;
        private Spell _owner;
        private string _desc;

        public void Initialise()
        {
            // Load in the data from the provided attributes.
            Damage = Attributes.GetInt("amount");
            DamageType = Attributes.GetEnum<DamageType>("type");

            // Generate the description based on damage and type.
            _desc = $"Deal {Damage} {DamageType.ToString().ToLower()} damage";
        }

        public void Start(Spell owner)
        {
            // Set up the SpellCast event so this spell
            // actually does something
            _owner = owner;
            owner.SpellCast += OwnerOnSpellCast;

            // No need for it to "persist", only happens once.
            Remove = true;
        }

        /// <summary>
        /// Deals X damage of type Y to the input list of creatures
        /// </summary>
        /// <param name="spell">The spell that is using this effect</param>
        /// <param name="creatures">Creatures that will be damaged!</param>
        private void OwnerOnSpellCast(Spell spell, List<Creature> creatures)
        {
            foreach (var creature in creatures)
            {
                creature.TakeDamage(Damage, DamageType, null);
            }
        }

        public void End()
        {
            // Not really needed as the effect shouldn't ever "end",
            //  but just incase it does end.
            _owner.SpellCast -= OwnerOnSpellCast;
        }

        public void Update()
        {
            
        }

        public bool Persistent => false;
        public bool Remove { get; set; }
        public int TimeLeft { get; set; }
        public string Description => _desc;
        public CustomAttributes Attributes { get; set; }
    }
}
