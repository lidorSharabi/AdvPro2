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
    class SinglePlayerBoardGameModel: ISinglePlayerBoardGameModel
    {
        private string serverMessage;
        public TelnetSingaleClient client;
        public string MazeName; 
        public int MazeCols { get; set; }
        public int MazeRows { get; set; }
        public string MazeString { get; set; }
        public string GoalPoint { get; set; }
        public string InitialPoint { get; set; }
        


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

        internal void SolveMaze()
        {
            client.Solve(this.MazeName);
        }
    }
}
