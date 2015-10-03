using System;

namespace CardGameServer
{
    public static class Conversion
    {
        /// <summary>
        /// Converts a string to the appropriate Enum value.
        /// For flag based enumators use comma separated values.
        /// </summary>
        /// <typeparam name="T">Enum Type</typeparam>
        /// <param name="data">Comma seperated enum values</param>
        /// <returns>An enumerator of type T with appropriate value(s) set</returns>
        public static T StringToEnum<T>(string data)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
            var op = (T) Enum.Parse(typeof (T), data);
            Console.WriteLine($"{data} ==> {op}");
            return op;
        }
    }
}
