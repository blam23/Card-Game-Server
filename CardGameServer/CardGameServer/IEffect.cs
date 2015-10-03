namespace CardGameServer
{
    /// <summary>
    /// A generic interface to allow for all types of effects.
    /// </summary>
    /// <typeparam name="T">What type of effect it will be, creature, spell or card.</typeparam>
    public interface IEffect<in T>
    {
        // TODO: Comment on each field's purpose.
        void Initialise();
        void Start(T owner);
        void End();
        void Update();
        bool Persistent { get; }
        bool Remove { get; set; }
        int TimeLeft { get; set; }
        string Description { get; }
        CustomAttributes Attributes { get; set; }
    }
}

