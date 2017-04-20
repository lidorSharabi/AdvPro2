using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class JoinMazeCommand : ICommand
    {
        private IModel model;
        public JoinMazeCommand(IModel model)
        {
            this.model = model;
        }
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
