using System;

namespace CardGameServer
{
    /// <summary>
    /// Used primarily for metadata storage, provides a way to store
    ///  an effect and what values it should have in a particular case.
    /// 
    /// I.e. A spell may have a DamageSpellEffect that does 3 damage.
    /// The EffectData in that case will have a Name of "DamageSpellEffect"
    ///  and Attributes containing one entry "damage" -> int(3).
    /// </summary>
    public class EffectData
    {
        public String Name;
        public CustomAttributes Attributes;

        public EffectData(String name, CustomAttributes attributes)
        {
            Name = name;
            Attributes = attributes;
        }
    }
}
