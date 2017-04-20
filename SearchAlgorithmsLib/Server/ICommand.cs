using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// interface of the commands
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// executes the matching command function of the client
        /// </summary>
        /// <param name="args"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        string Execute(string[] args, TcpClient client = null);
    }
}
