using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class PlayMazeCommand : ICommand
    {
        private IModel model;
        public PlayMazeCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        {
            string move = args[0];
            return null;
        }
    }
}
