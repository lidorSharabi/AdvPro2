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
    /// responsible for executing the join command for joining a started maze
    /// </summary>
    class JoinMazeCommand : ICommand
    {
        /// <summary>
        /// the model to perform the operation
        /// </summary>
        private IModel model;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="model"></param>
        public JoinMazeCommand(IModel model)
        {
            this.model = model;
        }
        /// <summary>
        /// Executes the command of joining a multiplayer maze and
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
            }
            catch (Exception)
            {
                return "Error in parameter for joining maze";
            }
            string notFount = "Error Game not found";
            Maze maze = model.JoinMaze(name, client);
            if (maze != null)
            {
                return maze.ToJSON();
            }
            return notFount;
        }
    }
}
