using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace Server
{
    public class ClientHandler : IClientHandler
    {
        private Controller control = new Controller();

        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                string result, commandLine;
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    commandLine = reader.ReadLine();
                    Console.WriteLine("Got command: {0}", commandLine);
                   result = control.ExecuteCommand(commandLine, client);
                    writer.Write(result);
                }
                string[] arr = commandLine.Split(' ');
                string commandKey = arr[0];
                if (!((commandKey.Equals("start") || commandKey.Equals("join")) && !result.Contains("Error")))
                {
                    client.Close();
                }
            }).Start();
        }
    }
}
