using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        /// <summary>
        /// main for starting the client
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Client client = new Client();
            client.Start();
        }
    }
}
