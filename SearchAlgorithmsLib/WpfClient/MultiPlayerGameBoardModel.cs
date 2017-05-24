using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient
{
    class MultiPlayerGameBoardModel
    {
        private string serverMessage;
        public TelnetMultiClient client;
        public string MazeName;
        public int MazeCols { get; set; }
        public int MazeRows { get; set; }
        public string MazeString { get; set; }
        public string GoalPoint { get; set; }
        public string InitialPoint { get; set; }

        public MultiPlayerGameBoardModel(string serverMessage, TelnetMultiClient client)
        {
            this.serverMessage = serverMessage;
            this.client = client;
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

        internal void JoinMaze()
        {
            client.Join(this.MazeName);
        }

        internal void Move(string move)
        {
            client.Move(move);
        }

        internal bool Continue()
        {
            return client.Continue();
        }
        

        internal string ReadMoveDirection()
        {
            string serverResponse = client.readMoveDirection();
            JObject json = new JObject();
            json = JObject.Parse(serverResponse);
            string s = (string)json.GetValue("Direction");
            return s;
        }
    }
}
