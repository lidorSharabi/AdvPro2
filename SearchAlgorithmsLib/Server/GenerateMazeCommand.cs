using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.Net.Sockets;

namespace Server
{
    public class GenerateMazeCommand : ICommand
    {
        private IModel model;
        public GenerateMazeCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client, Controller control)
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
