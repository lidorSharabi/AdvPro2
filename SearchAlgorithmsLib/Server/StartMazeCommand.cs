using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// responsible for the command of starting a new multiplayer game
    /// </summary>
    class StartMazeCommand : ICommand
    {
        private IModel model;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="model"></param>
        public StartMazeCommand(IModel model)
        {
            this.model = model;
        }
        /// <summary>
        /// Executes the command of starting a multiplayer maze and
        /// applying the matching function in the model section
        /// </summary>
        /// <param name="args"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public string Execute(string[] args, TcpClient client)
        {
            string name;
            int rows;
            int cols;
            try
            {
                name = args[0];
                rows = int.Parse(args[1]);
                cols = int.Parse(args[2]);
            }
            catch (Exception)
            {
                return "Error in parameters for starting maze";
            }
            Maze maze = model.MazeStart(name, rows, cols, client);
            return "Waiting for other player to join...";
        }
    }
}
