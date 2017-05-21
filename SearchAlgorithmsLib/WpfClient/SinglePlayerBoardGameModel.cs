using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;

namespace WpfClient
{
    class SinglePlayerBoardGameModel: ISinglePlayerBoardGameModel
    {
        private string serverMessage;
        public string MazeName; 
        public int MazeCols { get; set; }
        public int MazeRows { get; set; }
        public string MazeString { get; set; }
        public string GoalPoint { get; set; }
        public string InitialPoint { get; set; }
        


        public SinglePlayerBoardGameModel(string serverMessage)
        {
            Maze maze = Maze.FromJSON(serverMessage);
            this.serverMessage = serverMessage;
            this.MazeName = maze.Name;
            this.MazeCols = maze.Cols;
            this.MazeRows = maze.Rows;
            this.GoalPoint = maze.GoalPos.Row + "," + maze.GoalPos.Col;
            this.InitialPoint = maze.InitialPos.Row + "," + maze.InitialPos.Col;
            //calculate maze string path
            int pFrom = serverMessage.IndexOf("Maze\":") + "Maze".Length + 1;
            int pTo = serverMessage.LastIndexOf("Rows") - 5;
            this.MazeString  = serverMessage.Substring(pFrom, pTo - pFrom);
            //comment
    }


    }
}
