using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CardGameServer.Effects
{
    public class EffectLoader<T> 
    {
        public Dictionary<String, Type> EffectTypes = new Dictionary<string, Type>();
        public Dictionary<Type, CustomAttributes> EffectAttributes = new Dictionary<Type, CustomAttributes>(); 

        public void LoadFromAssembly(Assembly assembly = null)
        {
            if (assembly == null) 
                assembly = Assembly.GetExecutingAssembly();

            foreach (var type in assembly.GetTypes())
            {  
                var attrs = type.GetCustomAttributes(typeof(NamedEffectAttribute), false);
                foreach (var nameAttr in from NamedEffectAttribute nameAttr in attrs where nameAttr != null where nameAttr.Type == typeof (T) select nameAttr)
                {
                    Console.WriteLine("Loaded: " + nameAttr.ID + " (" + type.Name + ") as a " + nameAttr.Type.Name +
                                      " effect.");

                    var customValues = type.GetCustomAttributes(typeof (CustomValueAttribute), false);
                    var effectAttributes = new CustomAttributes();
                    foreach (CustomValueAttribute cvAttribute in customValues)
                    {
                        Console.WriteLine("\t{0}",cvAttribute);
                        effectAttributes.AddAttribute(cvAttribute.Name, cvAttribute.Type, cvAttribute.DefaultValue);
                    }

                    EffectAttributes.Add(type, effectAttributes);
                    EffectTypes.Add(nameAttr.ID, type);
                }
            }
        }

        public IEffect<T> CreateInstance(String name) 
        {
            var type = EffectTypes[name];
            var instance = (IEffect<T>) Activator.CreateInstance(type);
            instance.Attributes = EffectAttributes[type];
            instance.Initialise();
            return instance;
        }

        public IEffect<T> CreateInstance(String name, CustomAttributes attributes)
        {
            var type = EffectTypes[name];
            var instance = (IEffect<T>)Activator.CreateInstance(type);
            instance.Attributes = attributes;
            instance.Initialise();
            return instance;
        }

        public int Count => EffectTypes.Count;

        public bool Exists(string name)
        {
            return EffectTypes.ContainsKey(name);
        }

        public Type this[string index] => EffectTypes[index];
    }
}
