using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CardProtocolLibrary
{
    public class GameActionWriter
    {
        public static readonly Version PROTOCOL_VERSION = new Version(1,0,0,0);
        private readonly StreamWriter _dataWriter;

        public GameActionWriter(Stream data)
        {
            _dataWriter = new StreamWriter(data);
        }

        public void SendAction(GameDataAction gameDataAction)
        {
            SendAction(gameDataAction.Action, gameDataAction.Data);
        }

        public void SendAction(GameAction gameAction, Dictionary<string, GameData> data)
        {
            var sb = new StringBuilder();

            sb.Append((int) gameAction);

            if (data != null)
            {
                sb.Append(":");
                foreach (var d in data)
                {
                    var valueStr = d.Value.String();
                    if (d.Value.Type == GameDataType.String)
                    {
                        valueStr = "'" + valueStr + "'";
                    }
                    sb.Append($"['{d.Key}':{valueStr}]");
                }
            }
            sb.Append(";\n");

            Console.WriteLine(sb);

            _dataWriter.Write(sb.ToString());
            _dataWriter.Flush();
        }
    }
}