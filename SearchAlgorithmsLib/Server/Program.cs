using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Server
{
    class Program
    {
        /// <summary>
        /// The main that runs the server
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Server server = new Server(Int32.Parse(ConfigurationManager.AppSettings["PortNumber"]), new ClientHandler());
            server.Start();
            Console.ReadKey();
        }
    }
}
