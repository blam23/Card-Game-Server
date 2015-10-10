using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace CardGameServer
{
    /// <summary>
    /// Class for loading in Creatures and Spells from XML data.
    /// </summary>
    public class DataLoader
    {
        /// <summary>
        /// Processes the input XML to output a simple XMLAttributes 
        /// wrapped Dictionary, where the tag name is the key and it's
        /// contents are the value.
        /// 
        /// For example this documentation would return a "summary" key
        /// with the value being this text you are reading now, as well
        /// as a "param" key and "returns" key.
        ///   
        /// 
        /// </summary>
        /// <param name="effectData">XML to parse</param>
        /// <returns>A KVP of nodes and their values</returns>
        private static XMLAttributes GetAttributes(XmlNode effectData)
        {
            var attributes = new XMLAttributes();
            foreach (XmlNode effectParameter in effectData)
            {
                if (effectParameter.SelectNodes("*").Count > 0)
                {
                    attributes.Add(effectParameter.Name, GetAttributes(effectParameter));
                }
                else
                {
                    attributes.Add(effectParameter.Name, effectParameter.InnerText);
                }
            }
            return attributes;
        }

        /// <summary>
        /// This enum is used to make sure we load in creature effects
        ///  onto creatures, spells onto spells, cards onto cards.
        /// Because we store the effects seperately we need this to
        ///  access the correct Dictionary of effects.
        /// </summary>
        enum EffectType
        {
            spell,
            creature,
            card
        }

        /// <summary>
        /// Loads in and generates a list of EffectData from the given XML node.
        /// It assumes that the node is the holder of the effects, usually named
        /// "effects".
        /// </summary>
        /// <param name="effects">The node holding the effect(s)</param>
        /// <param name="effectType">What type of effects we are trying to load (spell, creature, card)</param>
        /// <returns></returns>
        private static IEnumerable<EffectData> LoadEffects(XmlNode effects, EffectType effectType)
        {
            // If the node is empty then return an empty array.
            if (effects == null) yield break;

            foreach (XmlNode effectData in effects)
            {
                // Ignore comments
                if (effectData.NodeType == XmlNodeType.Comment)
                {
                    continue;
                }
                // We use the xml node name to relate to the
                //  given effect's name, ex: <deathrattle> maps to
                //  the NamedEffect attribute on class DeathRattle:
                //   [NamedEffect("deathrattle", typeof(Creature))]
                var effectName = effectData.Name;
            
                // Break the XML data down into simple KVP dictionary of
                // strings to objects.
                var attributeData = GetAttributes(effectData);

                // Load in what attributes are needed for this effect,
                // and what types those attribute need to be.
                CustomAttributes baseAttributes;
                switch (effectType)
                {
                    case EffectType.spell:
                        baseAttributes = Game.SpellEffects.EffectAttributes[Game.SpellEffects.EffectTypes[effectName]].CreateTypedCopy();
                        break;
                    case EffectType.creature:
                        baseAttributes = Game.CreatureEffects.EffectAttributes[Game.CreatureEffects.EffectTypes[effectName]].CreateTypedCopy();
                        break;
                    case EffectType.card:
                        baseAttributes = Game.CardEffects.EffectAttributes[Game.CardEffects.EffectTypes[effectName]].CreateTypedCopy();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("Invalid Effect Type");
                }

                // Load in the data for each attribute.
                // For Example: How much damage this effect will deal,
                // or what spell a deathrattle effect should call.
                foreach (var attribute in attributeData.BaseDictionary)
                {
                    var name = attribute.Key;

                    // Default value
                    var baseData = attribute.Value;

                    // What type the value SHOULD be.
                    var type = baseAttributes.types[name];

                    // Convert from the XML data within the attribute to
                    // the actual type the data needs to be.
                    // Example:
                    //  <damage>10</damage> -> int(10)
                    //  <target>Enemies, Minions</target> -> TargetGroup.Enemies | TargetGroup.Minions
                    if (type == typeof (string))
                    {
                        // No need to convert if it's already a string!
                        baseAttributes.Add(name, baseData);
                    }
                    else if (type == typeof (int))
                    {
                        baseAttributes.Add(name, int.Parse((string) baseData));
                    }
                    else if (type.IsEnum)
                    {
                        Console.WriteLine(baseData);
                        Console.WriteLine(Enum.Parse(type, (string)baseData));
                        baseAttributes.Add(name, Enum.Parse(type, (string) baseData));
                    }
                    // TODO: Make this generic if possible ?
                    else if (type == typeof (List<Spell>))
                    {
                        // TODO: Let this reference existing spells!
                        var spells = (from XmlNode spellData in effectData[name] select GetSpell(spellData, "<gen>")).ToList();
                        baseAttributes.Add(name, spells);
                    }
                }
                // Use yield return so C# builds our enumerable for us.
                yield return new EffectData(effectName, baseAttributes);
            }
        }

        /// <summary>
        /// Loads in spells from the "spell.xml" resource file.
        /// </summary>
        /// <returns>A dictionary mapping spell names to the actual Spell class.</returns>
        public static Dictionary<string, Spell> LoadSpells()
        {
            // Load in and parse our "spell.xml" file
            var spellXmlDocument = new XmlDocument();
            spellXmlDocument.Load(GetResource("spells.xml"));

            var spells = new Dictionary<string, Spell>();

            // Loop over each node and add it to our dictionary
            foreach (XmlNode spellData in spellXmlDocument.GetElementsByTagName("spell"))
            {
                string name = spellData.Attributes["ID"].Value;
                var spell = GetSpell(spellData, name);

                spells.Add(name, spell);
            }
            return spells;
        }

        /// <summary>
        /// Generates a Spell base from an XML node
        /// </summary>
        /// <param name="spellData">XML containing the spell data</param>
        /// <param name="name">The ID the spell is to be given.</param>
        /// <returns></returns>
        private static Spell GetSpell(XmlNode spellData, string name)
        {
            // Create a new blank spell, with the given name and
            // TargetGroup + Type
            var spell = new Spell
            {
                ID = name,
                TargetGroup = Conversion.StringToEnum<TargetGroup>(spellData["targetGroup"].InnerText),
                TargetType = Conversion.StringToEnum<TargetType>(spellData["targetType"].InnerText)
            };
            
            // Load in the spell's effects using the generic effect loading method
            XmlNode effects = spellData["effects"];
            spell.EffectData.AddRange(LoadEffects(effects, EffectType.spell));

            return spell;
        }

        /// <summary>
        /// Loads in Creatures from the "creatures.xml" resource file.
        /// </summary>
        /// <returns>A dictionary mapping creature ID's to the actual Creature base instance.</returns>
        public static Dictionary<string, Creature> LoadCreatures()
        {
            // Parse the XML document.
            var creatureXmlDocument = new XmlDocument();
            creatureXmlDocument.Load(GetResource("creatures.xml"));

            var creatures = new Dictionary<string, Creature>();

            // Loop over each creature in the XML's base "creature" node.
            foreach (XmlNode creatureData in creatureXmlDocument.GetElementsByTagName("creature"))
            {
                var name = creatureData.Attributes["ID"].Value;

                // Load in all of the creature's base data
                // We load in booleans such as Taunt or MagicImmune
                //  by way of checking if that tag exists in the data.
                // All creatures need to have an ID, BaseHealth, Damage
                // Image value.
                var creature = new Creature
                {
                    ID = name,
                    Name = creatureData["name"].InnerText,
                    BaseHealth = int.Parse(creatureData["health"].InnerText),
                    Damage = int.Parse(creatureData["attack"].InnerText),
                    Taunt = creatureData.SelectSingleNode("taunt") != null,
                    SleepSickness = creatureData.SelectSingleNode("charge") == null,
                    Commander = creatureData.SelectSingleNode("commander") != null,
                    MagicImmune = creatureData.SelectSingleNode("magicimmune") != null,
                    PhysicalImmune = creatureData.SelectSingleNode("physicalimmune") != null,
                    MagicTargetable = creatureData.SelectSingleNode("magictargetable") != null,
                    PhysicalTargetable = creatureData.SelectSingleNode("physicaltargetable") != null,
                    Stealth = creatureData.SelectSingleNode("stealth") != null,
                    Image = creatureData.SelectSingleNode("image").InnerText
                };

                // Load in any effects with our super handy generic effect loading method.
                XmlNode effects = creatureData["effects"];
                creature.EffectData.AddRange(LoadEffects(effects, EffectType.creature));
                creatures.Add(name, creature);
            }

            return creatures;
        }

        /// <summary>
        /// Loads in card data from the "cards.xml" resource.
        /// 
        /// TODO: Remove this & generate cards from the spell and creature data
        /// </summary>
        /// <returns>Dictionary mapping card names to Card class instance</returns>
        public static Dictionary<string, Card> LoadCards()
        {
            // Load in and parse our "cards.xml" resource file.
            var cardXmlDocument = new XmlDocument();
            cardXmlDocument.Load(GetResource("cards.xml"));

            var cards = new Dictionary<string, Card>();

            foreach (XmlNode cardData in cardXmlDocument.GetElementsByTagName("card"))
            {
                var name = cardData.Attributes["ID"].Value;

                // We load in the card's data, A card must have an
                //  ID, Cost, Type and related Creature or Spell ID.
                var card = new Card
                {
                    ID = name,
                    Type = Conversion.StringToEnum<CardType>(cardData["type"].InnerText),
                    CreatureID = cardData["creatureID"] == null ? null : cardData["creatureID"].InnerText,
                    SpellID = cardData["spellID"] == null ? null : cardData["spellID"].InnerText,
                    Cost = int.Parse(cardData["cost"].InnerText)
                };

                XmlNode effects = cardData["effects"];
                card.EffectData.AddRange(LoadEffects(effects, EffectType.card));

                cards.Add(name, card);
            }

            return cards;
        }

        /// <summary>
        /// Loads a specific embedded resource file, returns a 
        /// Stream that can be used to read through it (StreamReader)
        /// or handed to the XMLDocument class if it's an XML file.
        /// </summary>
        /// <param name="filename">Resource name to load (starts in "Data" folder)</param>
        /// <returns>Stream to read file data from.</returns>
        public static Stream GetResource(String filename)
        {
            // Use reflection to get the embedded resource stream.
            // TODO: Does this have to use reflection?
            return typeof(DataLoader).Module.Assembly.GetManifestResourceStream("CardGameServer.Data." + filename);
        }
    }
}