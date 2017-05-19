using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient
{
    class SinglePlayerBoardGameModel: ISinglePlayerBoardGameModel
    {
        private string serverMessage;
        private string name;
        private string rows;
        private string columns;

        public int MazeName { get; set; }
        public int MazeCols { get; set; }
        public int MazeRows { get; set; }
        public string MazeString { get; set; }


        public SinglePlayerBoardGameModel(string serverMessage, string name, string rows, string columns)
        {
            this.serverMessage = serverMessage;
            this.name = name;
            this.rows = rows;
            this.columns = columns;
            //calculate maze string path
            int pFrom = serverMessage.IndexOf("Maze") + "Maze".Length + 4;
            int pTo = serverMessage.LastIndexOf("Rows") - 5;
            this.MazeString  = serverMessage.Substring(pFrom, pTo - pFrom);
            
        }


    }
}
