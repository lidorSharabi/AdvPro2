using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Server
{
    class CloseMazeCommand : ICommand
    {
        private IModel model;
        public CloseMazeCommand(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client, Controller control = null)
        {
            string name;
            try
            {
                name = args[0];
                return model.closeMultiPlayerGame(name, client);
            }
            catch
            {
                return "Error in parameters for close maze";
            }
        }
    }
}
