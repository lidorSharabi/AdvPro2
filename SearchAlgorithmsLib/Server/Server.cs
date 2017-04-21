using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Server
{
    /// <summary>
    /// responsible for the connection of the server to the ip address and the port
    /// </summary>
    class Server
    {
        /// <summary>
        /// the port of the server
        /// </summary>
        private int port;
        /// <summary>
        /// listener that listens for new clients connections
        /// </summary>
        private TcpListener listener;
        /// <summary>
        /// client handler
        /// </summary>
        private IClientHandler ch;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="port"></param>
        /// <param name="ch"></param>
        public Server(int port, IClientHandler ch)
        {
            this.port = port;
            this.ch = ch;
        }
        /// <summary>
        /// starting the connection of the server and waiting for connections to clients
        /// </summary>
        public void Start()
        {
            IPEndPoint ep = new
            IPEndPoint(IPAddress.Parse("127.0.0.1"), Int32.Parse(ConfigurationManager.AppSettings["PortNumber"]));
            listener = new TcpListener(ep);

            listener.Start();
            Console.WriteLine("Waiting for connections...");
            Task task = new Task(() => {
                while (true)
                {
                    try
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        Console.WriteLine("Got new connection");
                        ch.HandleClient(client);
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            });
            task.Start();
        }
        /// <summary>
        /// stopping the listner from listening to new connections
        /// </summary>
        public void Stop()
        {
            listener.Stop();
        }

    }
}
