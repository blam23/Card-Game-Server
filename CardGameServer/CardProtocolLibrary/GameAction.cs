namespace CardProtocolLibrary
{
    public enum GameAction
    {
        Error,
        GameStart,
        TurnStart,
        TurnEnd,
        DrawCard,
        PlayCard,
        SetMana,
        SetHealth,
        Attack,
        Trigger,
        Kill,
        MinionSummoned,
        SpellCast,
        GameOver,
        Meta,
        Ping
    }
}
