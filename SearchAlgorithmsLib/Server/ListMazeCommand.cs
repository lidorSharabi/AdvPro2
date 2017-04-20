using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace Server
{
    /// <summary>
    /// responsible for the printing of a list of all started mazes
    /// </summary>
    class ListMazeCommand : ICommand 
    {
        private IModel model;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="model"></param>
        public ListMazeCommand(IModel model)
        {
            this.model = model;
        }
        /// <summary>
        /// Executes the command of printing a list of all started mazes and
        /// applying the matching function in the model section
        /// </summary>
        /// <param name="args"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public string Execute(string[] args, TcpClient client)
        { 
            string[] games = model.MazeList();
            return JsonConvert.SerializeObject(games);
            
        }
    }
}
