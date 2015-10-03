namespace CardGameServer.Effects
{
    [CustomValue("hits", typeof(int), false, 1)]
    [NamedEffect("divineshield", typeof(Creature))]
    public class DivineShield : IEffect<Creature>
    {
        public int BlockLimit = 1;

        private int _blockCount;
        private Creature _owner;

        public void Initialise()
        {
            BlockLimit = Attributes.GetInt("hits");
        }

        public void Start(Creature ownerCreature)
        {
            _owner = ownerCreature;
            ownerCreature.DamageTaken += OwnerCreatureOnDamageTaken;
        }

        private void OwnerCreatureOnDamageTaken(int damage, DamageType damageType, Creature source)
        {
            // Remove this shield effect once it's blocked [blockLimit] attacks
            _blockCount++;
            if (_blockCount == BlockLimit) Remove = true;
        }

        public void End()
        {
            _owner.DamageTaken -= OwnerCreatureOnDamageTaken;
        }


        public void Update()
        {
            _owner.MagicImmune = true;
            _owner.PhysicalImmune = true;
        }

        public string Description => BlockLimit == 1 ? "Immune for 1 hit." : "Immune for " + BlockLimit + " hits.";
        public bool Persistent => true;
        public bool Remove { get; set; }
        public int TimeLeft { get; set; }
        public CustomAttributes Attributes { get; set; }
    }
}
