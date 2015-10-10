using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameServer
{
    /// <summary>
    /// Base spell class
    /// !! Use a base type and create instances of it !!
    /// </summary>
    public class Spell
    {
        // The Unique Identifier will be different each time
        //  we create a new instance of this spell.
        // For example: If we casted two "Fireball" spells,
        //  they would have unique UIDs but the same ID.
        // This is useful for keeping track of a single instance
        //  of a spell.
        public SID UID;

        public string ID;
        public TargetType TargetType;
        public TargetGroup TargetGroup;

        // This is the metadata for the effects of this spell.
        // Used when creating new instances of this spell, if you wanted one
        // that did 10 damage, one that did 2.
        public List<EffectData> EffectData = new List<EffectData>(); 

        // The actual, instanciated effects that will be used by the spell.
        public List<IEffect<Spell>> Effects = new List<IEffect<Spell>>();

        // An event that is called when this spell is actually cast,
        // can be subscribed to by effects to trigger something
        // when the spell is cast.
        public event Action<Spell, List<Creature>> SpellCast;

        protected virtual void OnSpellCast(Spell arg1, List<Creature> arg2)
        {
            var handler = SpellCast;
            handler?.Invoke(arg1, arg2);
        }

        /// <summary>
        /// Creates a new, unique clone of the spell.
        /// Uses the effect metadata to create new instances of
        ///  effects too.
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public Spell CreateInstance(Player owner)
        {
            var spell = new Spell
            {
                ID = ID,
                TargetType = TargetType,
                TargetGroup = TargetGroup,
                UID = SID.New()
            };

            foreach (var effect in EffectData)
            {
                // Need to create a new instance of effect for every spell!
                spell.Effects.Add(Game.SpellEffects.CreateInstance(effect.Name, effect.Attributes));
            }

            return spell;
        }

        /// <summary>
        /// Generates a basic description of the spell and it's effects by
        ///  iterating over the effects and simply concatenating the descriptions
        ///  of each one in a simple "x, y and z" format.
        /// </summary>
        /// <returns></returns>
        public string GetDescription()
        {
            // TODO: Clean up all of this terrible, terrible code.

            var desc = new StringBuilder(); // at least I'm using a StringBuilder.

            // Loop over each effect and concatenate the descriptions
            for (var i = 0; i < Effects.Count; i++)
            {
                var effect = Effects[i];
                desc.Append(effect.Description);
                if (i + 1 < Effects.Count)
                {
                    desc.Append("and ");
                }
                else if (i + 2 < Effects.Count)
                {
                    desc.Append(", ");
                }
            }

            // Add onto the end of the descriptions what the spell will be targetting.
            // We already know what the effects are, so we just need to say who they apply to.
            // Example:
            //  If the above loop has given us:
            //   "Freeze and deal 2 damage"
            //  The below generates
            //   "to a random enemy minion"
            //  To get the result
            //   "Freeze and deal 2 damage to a random enemy minion"
            
            // Problems:
            //  If we flip the above example, it would read:
            //   "Deal 2 damage and freeze to a random enemy minion"
            //  Which doesn't make gramatical sense.
            // Solution: Consistent effect wording?
            // Better solution: Rewrite all of this!

            // A whole mess of if statements that can and eventually will be
            //  simplified.
            if (TargetType == TargetType.Single || TargetType == TargetType.Random)
            {
                desc.Append(TargetType == TargetType.Single ? " to " : " to a random ");
                if (TargetGroup.IsFlagSet(TargetGroup.Allies))
                {
                    desc.Append(TargetGroup.IsFlagSet(TargetGroup.Enemies) ? "a " : "a friendly ");
                }
                else if (TargetGroup.IsFlagSet(TargetGroup.Enemies))
                {
                    desc.Append("an enemy ");
                }
                if (TargetGroup.IsFlagSet(TargetGroup.Champions))
                {
                    desc.Append(TargetGroup.IsFlagSet(TargetGroup.Minions) ? "character" : "hero");
                }
                else if (TargetGroup.IsFlagSet(TargetGroup.Minions))
                {
                    desc.Append("minion");
                }
            }
            else
            {
                desc.Append(" to ");
                if (TargetGroup.IsFlagSet(TargetGroup.Champions))
                {
                    desc.Append(TargetGroup.IsFlagSet(TargetGroup.Minions) ? "every " : "the ");
                }
                else
                {
                    desc.Append("all ");
                }
                if (TargetGroup.IsFlagSet(TargetGroup.Allies))
                {
                    desc.Append(TargetGroup.IsFlagSet(TargetGroup.Enemies) ? "" : "friendly ");
                }
                else if (TargetGroup.IsFlagSet(TargetGroup.Enemies))
                {
                    desc.Append("enemy ");
                }
                if (TargetGroup.IsFlagSet(TargetGroup.Champions))
                {
                    desc.Append(TargetGroup.IsFlagSet(TargetGroup.Minions) ? "character" : "hero");
                }
                else if (TargetGroup.IsFlagSet(TargetGroup.Minions))
                {
                    desc.Append("minions");
                }
            }
            return desc.ToString().Trim();
        }

        public void Cast(List<Creature> creatures = null)
        {
            OnSpellCast(this, creatures);
        }
    }
}
