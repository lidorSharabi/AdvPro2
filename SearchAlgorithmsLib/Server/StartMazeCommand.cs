using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class StartMazeCommand : ICommand
    {
        private IModel model;
        public StartMazeCommand(IModel model)
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
                return "Error in parameters for starting maze";
            }
            Maze maze = model.MazeStart(name, rows, cols, client, control);
            return "Waiting for other player to join...";
        }
    }
}
