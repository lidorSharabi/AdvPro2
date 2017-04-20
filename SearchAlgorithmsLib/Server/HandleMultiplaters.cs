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
    class HandleMultiplaterGame
    {
        public TcpClient host { get; set; }
        public TcpClient guest { get; set; }
        public string gameToJason { get; set; }
        public string name { get; set; }
        private Controller control;
        private bool run = true;

        public HandleMultiplaterGame(TcpClient host, Controller control)
        {
            this.host = host;
            this.control = control;
        }

        public void startBothClients()
        {
            startThread(host);
            startThread(guest);
        }

        private void startThread(TcpClient client)
        {
            new Task(() =>
            {
                string result, commandLine;
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    while (run)
                    {
                        //TODO - add try & catch
                        commandLine = reader.ReadLine();
                        Console.WriteLine("Got command: {0}", commandLine);
                        commandLine = commandLine + " " + name;
                        result = control.ExecuteCommand(commandLine, client);
                        writer.Write(result);
                    }
                }
            }).Start();
        }

        public void sendMazeToJsonToHost()
        {
            NetworkStream stream = host.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.AutoFlush = true;
            writer.WriteLine(gameToJason);
        }

        public void close(TcpClient client)
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

        public void startHost()
        {
            startThread(host);
        }

        public void startGuest()
        {
            startThread(guest);
        }

        public void sendMessageToClient(TcpClient client, string move)
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
