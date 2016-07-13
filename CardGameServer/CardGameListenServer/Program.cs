using System;
using CardGameServer;
using CardProtocolLibrary;

namespace CardGameListenServer
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Card Game Server Initialising..");
            Console.WriteLine("Protocol Version: " + GameActionWriter.PROTOCOL_VERSION);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Loading Data..");
            Console.ForegroundColor = ConsoleColor.Yellow;
            
            // Load in all the game data (xml)
            Game.Load();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Starting Server..");
            Console.ForegroundColor = ConsoleColor.White;


            var p = new Player();
            var test = Game.Spells["frostbolt"].CreateInstance(p);
            var test2 = Game.Spells["deepfreeze"].CreateInstance(p);
            Console.WriteLine(test.GetDescription());
            Console.WriteLine(test2.GetDescription());

            // Start the listen server and wait for connections!
            Server.Start();
        }
    }
}
