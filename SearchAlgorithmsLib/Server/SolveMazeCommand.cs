using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using SearchAlgorithmsLib;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace Server
{
    /// <summary>
    /// responsible for the command of solving a private maze
    /// </summary>
    public class SolveMazeCommand : ICommand
    {
        private IModel model;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="model"></param>
        public SolveMazeCommand(IModel model)
        {
            this.model = model;
        }
        /// <summary>
        /// Executes the command of solving a private maze and
        /// applying the matching function in the model section
        /// and returning a Json object of the solution
        /// </summary>
        /// <param name="args"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public string Execute(string[] args, TcpClient client)
        {
            string name;
            int algorithm;
            try
            {
                name = args[0];
                algorithm = int.Parse(args[1]);
            }
            catch (Exception)
            {
                return "Error in parameters for solving maze";
            }
            string sol = model.SolveMaze(name, algorithm);
            string[] solution = sol.Split(' ');
            JObject solObj = new JObject();
            solObj["name"] = name;
            solObj["Solution"] = solution[0];
            solObj["NodesEvaluated"] = Int32.Parse(solution[1]);
            return solObj.ToString();
        }
    }
}
