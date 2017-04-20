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
    /// 
    /// </summary>
    class HandleMultiplayers
    {
        public TcpClient host { get; set; }
        public TcpClient guest { get; set; }
        public string gameToJason { get; set; }
        public string name { get; set; }
        private bool run = true;

        public HandleMultiplayers(TcpClient host)
        {
            this.host = host;
        }

        public void SendMazeToJsonToHost()
        {
            NetworkStream stream = host.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.AutoFlush = true;
            writer.WriteLine(gameToJason);
        }

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
