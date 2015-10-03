using System;

namespace CardGameServer.Effects
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class CustomValueAttribute : Attribute
    {
        public object DefaultValue;
        public string Name;
        public Type Type;
        public bool Required;


        public CustomValueAttribute(string name, Type type, bool required = true, object def = null)
        {
            Name = name;
            DefaultValue = def;
            Required = required;
            Type = type;
        }

        public override string ToString()
        {
            return String.Format("{0} {3}{1} [= {2}]", Type, Name, DefaultValue, Required ? "*" : "");
        }
    }
}