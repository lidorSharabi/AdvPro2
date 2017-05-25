using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using Newtonsoft.Json.Linq;

namespace WpfClient
{
    /// <summary>
    /// the single player game model
    /// </summary>
    class SinglePlayerBoardGameModel: ISinglePlayerBoardGameModel
    {
        /// <summary>
        /// the message from the server
        /// </summary>
        private string serverMessage;
        /// <summary>
        /// the client
        /// </summary>
        public TelnetSingaleClient client;
        /// <summary>
        /// the maze name
        /// </summary>
        public string MazeName;
        /// <summary>
        /// the maze columns
        /// </summary>
        public int MazeCols { get; set; }
        /// <summary>
        /// the maze rows
        /// </summary>
        public int MazeRows { get; set; }
        /// <summary>
        /// the string of the maze
        /// </summary>
        public string MazeString { get; set; }
        /// <summary>
        /// the goal position
        /// </summary>
        public string GoalPoint { get; set; }
        /// <summary>
        /// the initial position of the player
        /// </summary>
        public string InitialPoint { get; set; }
        

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="serverMessage"></param>
        /// <param name="client"></param>
        public SinglePlayerBoardGameModel(string serverMessage, TelnetSingaleClient client)
        {
            this.serverMessage = serverMessage;
            this.client = client;
            /*
            Maze maze = Maze.FromJSON(serverMessage);
            this.MazeName = maze.Name;
            this.MazeCols = maze.Cols;
            this.MazeRows = maze.Rows;
            this.GoalPoint = maze.GoalPos.Row + "," + maze.GoalPos.Col;
            this.InitialPoint = maze.InitialPos.Row + "," + maze.InitialPos.Col;
            //calculate maze string path
            int pFrom = serverMessage.IndexOf("Maze\":") + "Maze".Length + 1;
            int pTo = serverMessage.LastIndexOf("Rows") - 5;
            this.MazeString  = serverMessage.Substring(pFrom, pTo - pFrom);
            */
            JObject json = new JObject();
            json = JObject.Parse(serverMessage);
            this.MazeName = (string)json.GetValue("Name");
            this.MazeString = (string)json.GetValue("Maze");
            this.MazeRows = (int)json.GetValue("Rows");
            this.MazeCols = (int)json.GetValue("Cols");
            JObject PosJ = (JObject)json.GetValue("Start");
            this.InitialPoint = (string)PosJ.GetValue("Row") + "," + (string)PosJ.GetValue("Col");
            PosJ = (JObject)json.GetValue("End");
            this.GoalPoint = (string)PosJ.GetValue("Row") + "," + (string)PosJ.GetValue("Col");
        }
        /// <summary>
        /// calling the solve command in the client
        /// </summary>
        internal void SolveMaze()
        {
            client.Solve(this.MazeName);
        }
    }
}
