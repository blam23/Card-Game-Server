using System;
using System.Collections.Generic;
using System.Globalization;

namespace CardProtocolLibrary
{
    public class GameDataAction
    {
        public GameAction Action;
        public Dictionary<string, GameData> Data;

        public GameDataAction(GameAction action, Dictionary<string, GameData> data)
        {
            Action = action;
            Data = data;
        }

        public GameDataAction(string actionString)
        {
            Data = new Dictionary<string, GameData>();

            var start = actionString.IndexOf(':');
            var enumString = actionString.Substring(0, start);
            Action = (GameAction) Enum.Parse(typeof (GameAction), enumString);

            var i = start;

            while (++i < actionString.Length)
            {
                if (actionString[i] != '[') continue;
                i++;
                var param = GetString(actionString, ref i);
                if (actionString[i] != ':')
                    throw new FormatException($"Was expecting ':' @ {i}, found '{actionString[i]}' instead.");
                i++;
                if (actionString[i] == '\'')
                {
                    Data.Add(param, GetString(actionString, ref i));
                }
                else if (char.IsDigit(actionString[i]) || actionString[i] == '-' || actionString[i] == '.')
                {
                    var numStart = i;
                    while (actionString[++i] != ']') ;
                    var sub = actionString.Substring(numStart, i - numStart);
                    if (sub.EndsWith("f"))
                    {
                        Data.Add(param,
                            float.Parse(sub.Substring(0, sub.Length - 1),
                                NumberStyles.Float | NumberStyles.AllowThousands));
                    }
                    else if (sub.Contains("."))
                    {
                        Data.Add(param, double.Parse(sub, NumberStyles.Float | NumberStyles.AllowThousands));
                    }
                    else
                    {
                        Data.Add(param, int.Parse(sub, NumberStyles.Integer));
                    }
                }
            }
        }

        private static string GetString(string input, ref int i)
        {
            if (input[i] != '\'') return null;
            var start = i++;
            while (input[i++] != '\'' || (i > 1 && input[i - 1] == '\\')) ;
            return input.Substring(start+1, i - start -2);
        }
    }
}