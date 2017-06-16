using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AdvProg2WebApp.Models
{
    /// <summary>
    /// handles the game of two players (multiplayers mode)
    /// </summary>
    class HandleMultiplayers
    {
        /// <summary>
        /// the host client, the client that did the start operation
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// the guest client, the client that did the join operation
        /// </summary>
        public string Guest { get; set; }
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
        public HandleMultiplayers(string host)
        {
            this.Host = host;
        }

        /// <summary>
        /// sends the maze to the host of this game (the client that did the start command)
        /// </summary>
        public void SendMazeToJsonToHost()
        {
        }

        /// <summary>
        /// sends a message to the one client that the other client has closed the game
        /// </summary>
        /// <param name="client"></param>
        public void Close(string client)
        {
            run = false;
            if (client == Host)
            {
            }
            else if (client == Guest)
            {
            }
        }
        
        /// <summary>
        /// responsible for sending the play commands in Json format for the matching client
        /// </summary>
        /// <param name="client"></param>
        /// <param name="move"></param>
        public void SendMessageToClient(string client, string move)
        {
            JObject playMoveFormat = new JObject();
            playMoveFormat["name"] = Name;
            playMoveFormat["Direction"] = move.ToLower();
            if (client == Host)
            {
            }
            else if (client == Guest)
            {
            }
        }
    }
}
