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
        private IModel model;
        public ListMazeCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client)
        { 
            string[] games = model.MazeList();
            return JsonConvert.SerializeObject(games);
            
        }
    }
}
