using System.Collections.Generic;

namespace CardGameServer
{
    /// <summary>
    /// Simple class to hold a KVP dictionary mapping the XML tag name (string) to
    /// it's value (usually string, can be another XML node).
    /// </summary>
    public class XMLAttributes
    {
        public Dictionary<string, object> BaseDictionary = new Dictionary<string, object>();

        public void Add(string name, object value)
        {
            BaseDictionary.Add(name, value);
        }
    }
}
