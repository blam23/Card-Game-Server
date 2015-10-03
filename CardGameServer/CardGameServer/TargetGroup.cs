using System;

namespace CardGameServer
{
    /// <summary>
    /// Determines what the ability will or can target.
    /// 
    /// If we make an ability with TargetType.None and set
    ///  TargetGroup to Enemies and Minions, the ability will
    ///  be given an array of creatures containing the enemy
    ///  minions.
    ///
    /// 
    /// This is a Flag type Enum so the groups can be combined.
    /// </summary>
    /// 
    /// <example>
    /// Mixed Groups Example:
    /// 
    ///   Spell Desc: "Heal all of your minions for 4 health."
    ///   TargetGroup: Allies|Minions
    ///   TargetType: None
    ///   Effect: Heal(4)
    /// </example>
    /// 
    /// <example>
    /// If we leave out one of the specifiers, the ability
    ///  will target both, e.g.:
    /// 
    ///   Spell Desc: "Give all minions a 1 hit shield."
    ///   TargetGroup: Minions
    ///   TargetType: None
    ///   Effect: DivineShield(1)
    /// </example>
    [Flags]
    public enum TargetGroup
    {
        None = 1,
        Minions = 2,
        Champions = 4,
        Allies = 8,
        Enemies = 16
    }
}