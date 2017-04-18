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
    public class SolveMazeCommand : ICommand
    {
        private IModel model;
        public SolveMazeCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client, Controller control)
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
