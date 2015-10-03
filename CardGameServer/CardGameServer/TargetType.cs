namespace CardGameServer
{
    /// <summary>
    /// Specifies whether or not an ability needs to be targetted:
    ///     Single -> User picks a target
    ///     Random -> A single target is picked
    ///     None   -> Abilities that don't have targets 
    ///               such as Area of Effect
    /// 
    /// Examples in TargetGroup.cs
    /// </summary>
    public enum TargetType
    {
        Single,
        Random,
        None
    }
}