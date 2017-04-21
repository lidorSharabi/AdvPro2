using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// responisble for controling all the commands that comes from the clients
    /// </summary>
    public class Controller
    {
        /// <summary>
        /// dictionary of the command and his name, the name is the key
        /// </summary>
        private Dictionary<string, ICommand> commands;
        /// <summary>
        /// the model of the program
        /// </summary>
        private IModel model;
        /// <summary>
        /// Ctor
        /// </summary>
        public Controller()
        {
            model = new Model();
            commands = new Dictionary<string, ICommand>();
            commands.Add("generate", new GenerateMazeCommand(model));
            commands.Add("solve", new SolveMazeCommand(model));
            commands.Add("start", new StartMazeCommand(model));
            commands.Add("list", new ListMazeCommand(model));
            commands.Add("join", new JoinMazeCommand(model));
            commands.Add("play", new PlayMazeCommand(model));
            commands.Add("close", new CloseMazeCommand(model));

        }

        /// <summary>
        /// receives the command from the view and passes it to the model
        /// by executing the matching function for the command
        /// </summary>
        /// <param name="commandLine"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public string ExecuteCommand(string commandLine, TcpClient client)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (!commands.ContainsKey(commandKey))
                return "Command not found";
            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];
            return command.Execute(args, client);
        }
    }
}
