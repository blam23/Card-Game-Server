using System;
using System.Collections.Generic;

namespace CardGameServer
{
    /// <summary>
    /// Stores what attributes an effect has, what types those attributes 
    ///  should be and the actual data.
    /// </summary>
    public class CustomAttributes
    {
        // KVP mapping attribute names to their types.
        public Dictionary<string, Type> types = new Dictionary<string, Type>();
        // KVP mapping attribute names to values (initially their default but does change)
        public Dictionary<string, object> baseValues = new Dictionary<string, object>();

        /// <summary>
        /// Creates a hard copy of this CustomAttributes class.
        /// </summary>
        /// <returns>A new instance of this class, with the same types and baseValues</returns>
        public CustomAttributes CreateTypedCopy()
        {
            var newCA = new CustomAttributes();
            foreach (var kvp in types)
            {
                newCA.AddAttribute(kvp.Key, kvp.Value, baseValues[kvp.Key]);
            }
            return newCA;
        }

        /// <summary>
        /// Adds a new attribute with the given name, type and default value.
        /// </summary>
        /// <param name="name">Name of the new attribute</param>
        /// <param name="type">What type it needs to be</param>
        /// <param name="defaultValue">Default value (null if there is none)</param>
        public void AddAttribute(string name, Type type, object defaultValue)
        {
            types.Add(name, type);
            baseValues.Add(name, defaultValue);
        }

        /// <summary>
        /// Sets the specified Attribute to the specified value.
        /// Ensures that the type is correct.
        /// </summary>
        /// <param name="name">Attribute name</param>
        /// <param name="value">New value for attribute</param>
        public void Add(string name, object value)
        {
            if (types[name] == value.GetType()) // Ensure correct Type
            {
                if (baseValues.ContainsKey(name))
                {
                    // If it exists update the value
                    baseValues[name] = value;
                }
                else
                {
                    // Otherwise just add a new attribute value in there
                    baseValues.Add(name, value);
                }
            }
            else
            {
                throw new InvalidCastException($"Parameter {name} must be of type {types[name]}, supplied was {value.GetType()}");
            }
        }

        // Helper unboxing methods.

        public int GetInt(string name)
        {
            return (int)baseValues[name];
        }

        public float GetFloat(string name)
        {
            return (float)baseValues[name];
        }

        public bool GetBoolean(string name)
        {
            return (bool)baseValues[name];
        }

        public T GetEnum<T>(string name) where T : struct, IConvertible
        {
            return (T)baseValues[name];
        }

        public List<T> GetArray<T>(string name)
        {
            return (List<T>)baseValues[name];
        }

        public bool ContainsKey(string name)
        {
            return baseValues.ContainsKey(name);
        }

        // Maps a["b"] to a.baseValues["b"] for convenience.
        public object this[string name] => baseValues[name];
    }
}
