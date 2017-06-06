using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient
{
    /// <summary>
    /// inteface for the client connection and reading an writing
    /// </summary>
    public interface ITelnetClient
    {
        /// <summary>
        /// manage the connection of the client to the server
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        void Connect(string ip, int port);
        /// <summary>
        /// manage the writing to the server
        /// </summary>
        /// <param name="command"></param>
        void Write(string command);
        /// <summary>
        /// manage the reading from the server
        /// </summary>
        /// <returns></returns>
        string Read();
        /// <summary>
        /// manage the disconnecting from the server
        /// </summary>
        void Disconnect();
    }
}
