using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// responsible for generating of a new maze command in private mode
    /// </summary>
    public class GenerateMazeCommand : ICommand
    {
        private IModel model;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="model"></param>
        public GenerateMazeCommand(IModel model)
        {
            this.model = model;
        }
        /// <summary>
        /// Executes the command of generating a private maze and
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
                return "Error in parameters for generating maze";
            }
            Maze maze = model.GenerateMaze(name, rows, cols);
            return maze.ToJSON();
        }
    }
}
