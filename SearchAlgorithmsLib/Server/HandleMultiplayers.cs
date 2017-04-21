using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Server
{
    /// <summary>
    /// handles the game of two players (multiplayers mode)
    /// </summary>
    class HandleMultiplayers
    {
        /// <summary>
        /// the host client, the client that did the start operation
        /// </summary>
        public TcpClient Host { get; set; }
        /// <summary>
        /// the guest client, the client that did the join operation
        /// </summary>
        public TcpClient Guest { get; set; }
        /// <summary>
        /// the Json object of the game
        /// </summary>
        public string GameToJason { get; set; }
        /// <summary>
        /// the name of the game
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// running variable
        /// </summary>
        private bool run = true;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="host"></param>
        public HandleMultiplayers(TcpClient host)
        {
            this.Host = host;
        }

        /// <summary>
        /// sends the maze to the host of this game (the client that did the start command)
        /// </summary>
        public void SendMazeToJsonToHost()
        {
            NetworkStream stream = Host.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.AutoFlush = true;
            writer.WriteLine(GameToJason);
        }

        /// <summary>
        /// sends a message to the one client that the other client has closed the game
        /// </summary>
        /// <param name="client"></param>
        public void Close(TcpClient client)
        {
            run = false;
            if (client == Host)
            {
                NetworkStream stream = Guest.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.AutoFlush = true;
                writer.WriteLine("The host has closed the game");
            }
            else if (client == Guest)
            {
                NetworkStream stream = Host.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.AutoFlush = true;
                writer.WriteLine("The guest has closed the game");
            }
        }
        
        /// <summary>
        /// responsible for sending the play commands in Json format for the matching client
        /// </summary>
        /// <param name="client"></param>
        /// <param name="move"></param>
        public void SendMessageToClient(TcpClient client, string move)
        {
            JObject playMoveFormat = new JObject();
            playMoveFormat["name"] = Name;
            playMoveFormat["move"] = move;
            if (client == Host)
            {
                NetworkStream stream = Guest.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.AutoFlush = true;
                writer.WriteLine(playMoveFormat.ToString());
            }
            else if (client == Guest)
            {
                NetworkStream stream = Host.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.AutoFlush = true;
                writer.WriteLine(playMoveFormat.ToString());
            }
        }
    }
}
