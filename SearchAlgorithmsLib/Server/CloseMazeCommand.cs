using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// this class is responsible for passing to the model the command of closing
    /// a two players game and sending the result back to the client that closed it
    /// </summary>
    class CloseMazeCommand : ICommand
    {
        private IModel model;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="model"></param>
        public CloseMazeCommand(IModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Executes the command of closing a multiplayer game and
        /// applying the matching function in the model section
        /// </summary>
        /// <param name="args"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public string Execute(string[] args, TcpClient client)
        {
            string name;
            try
            {
                name = args[0];
                return model.CloseMultiPlayerGame(name, client);
            }
            catch
            {
                return "Error in parameters for close maze";
            }
        }
    }
}
