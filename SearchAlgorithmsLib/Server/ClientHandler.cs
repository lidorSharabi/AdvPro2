using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace Server
{
    /// <summary>
    /// Handles the server connection with the clients
    /// </summary>
    public class ClientHandler : IClientHandler
    {
        /// <summary>
        /// the controller of the game
        /// </summary>
        private Controller control = new Controller();

        /// <summary>
        /// gets the stream from the client and executes the command that the client sent
        /// </summary>
        /// <param name="client"></param>
        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    while (true)
                    {
                        try
                        {
                            string commandLine = reader.ReadLine();
                            Console.WriteLine("Got command: {0}", commandLine);
                            string result = control.ExecuteCommand(commandLine, client);
                            writer.AutoFlush = true;
                            writer.WriteLine(result);
                            writer.WriteLine("end of message");
                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }
                }
                client.Close();
            }).Start();
        }
    }
}
