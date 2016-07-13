using System.IO;
using System.Net.Sockets;
using CardGameServer;

namespace CardProtocolLibrary
{
    /// <summary>
    /// Handles connection between server and player.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Actual TCP Connection!
        /// </summary>
        public readonly TcpClient RawClient;
        /// <summary>
        /// Basic Reader to get incoming data as Text
        /// </summary>
        public readonly StreamReader Reader;
        /// <summary>
        /// A custom data writer that sends formed commands via the RawClient.
        /// </summary>
        public readonly GameActionWriter Writer;
        /// <summary>
        /// A ping counter, helps to detect timeouts and temporary connection drops.
        /// </summary>
        public int PingCounter = 0;

        /// <summary>
        /// The player associated with the client.
        /// Null if client is a spectator
        /// </summary>
        public Player Player;

        public Client(TcpClient tcpClient)
        {
            RawClient = tcpClient;
            Reader = new StreamReader(RawClient.GetStream());
            Writer = new GameActionWriter(RawClient.GetStream());
        }
    }
}
