using System;

namespace CardGameServer
{
    /// <summary>
    /// Used for naming an effect and marking what class is intended to use it.
    /// 
    /// The name is used in the XML as an identifier so it needs to be unique,
    ///  although a spell effect and creature effect can have the same names.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class NamedEffectAttribute : Attribute
    {
        public string ID;
        public Type Type;
        public NamedEffectAttribute(string id, Type t)
        {
            ID = id;
            Type = t;
        }
    }
}