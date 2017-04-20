using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// interface for handling a client
    /// </summary>
    public interface IClientHandler
    {
        /// <summary>
        /// handles the connections with the clients
        /// </summary>
        /// <param name="client"></param>
        void HandleClient(TcpClient client);
    }
}
