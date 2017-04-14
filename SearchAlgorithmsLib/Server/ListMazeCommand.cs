using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace Server
{
    class ListMazeCommand : ICommand 
    {
        private Model model;
        public ListMazeCommand(Model model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        { 
            string[] games = model.mazeList();
            return JsonConvert.SerializeObject(games);
            
        }
    }
}
