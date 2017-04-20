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
        public TcpClient host { get; set; }
        public TcpClient guest { get; set; }
        public string gameToJason { get; set; }
        public string name { get; set; }
        private bool run = true;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="host"></param>
        public HandleMultiplayers(TcpClient host)
        {
            this.host = host;
        }

        /// <summary>
        /// sends the maze to the host of this game (the client that did the start command)
        /// </summary>
        public void SendMazeToJsonToHost()
        {
            NetworkStream stream = host.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.AutoFlush = true;
            writer.WriteLine(gameToJason);
        }

        /// <summary>
        /// sends a message to the one client that the other client has closed the game
        /// </summary>
        /// <param name="client"></param>
        public void Close(TcpClient client)
        {
            run = false;
            if (client == host)
            {
                NetworkStream stream = guest.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.AutoFlush = true;
                writer.WriteLine("The host has closed the game");
            }
            else if (client == guest)
            {
                NetworkStream stream = host.GetStream();
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
            playMoveFormat["name"] = name;
            playMoveFormat["move"] = move;
            if (client == host)
            {
                NetworkStream stream = guest.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.AutoFlush = true;
                writer.WriteLine(playMoveFormat.ToString());
            }
            else if (client == guest)
            {
                NetworkStream stream = host.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.AutoFlush = true;
                writer.WriteLine(playMoveFormat.ToString());
            }
        }
    }
}
