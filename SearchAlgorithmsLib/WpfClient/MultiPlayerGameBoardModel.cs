using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient
{
    /// <summary>
    /// the multiplayer model
    /// </summary>
    class MultiPlayerGameBoardModel
    {
        /// <summary>
        /// the client
        /// </summary>
        public TelnetMultiClient client;
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
        public MultiPlayerGameBoardModel(string serverMessage, TelnetMultiClient client)
        {
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
        /// <summary>
        /// sending the join command to the client
        /// </summary>
        internal void JoinMaze()
        {
            client.Join(this.MazeName);
        }
        /// <summary>
        /// sending the play command to the client
        /// </summary>
        /// <param name="move"></param>
        internal void Move(string move)
        {
            client.Move(move);
        }
        /// <summary>
        /// continue the play
        /// </summary>
        /// <returns></returns>
        internal bool Continue()
        {
            return client.Continue();
        }
        
        /// <summary>
        /// getting the opponent move in the game
        /// </summary>
        /// <returns></returns>
        internal string ReadMoveDirection()
        {
            string serverResponse = "";
            try
            {
                serverResponse = client.ReadMoveDirectionAndClose();
                JObject json = new JObject();
                json = JObject.Parse(serverResponse);
                return (string)json.GetValue("Direction");
            }
            catch
            {
                if (serverResponse.Contains("closed"))
                {
                    return "close";
                }
                return String.Empty;
            }
        }
        /// <summary>
        /// disconnecting from the sever at the end of the game
        /// </summary>
        internal void Disconnect()
        {
            client.CloseGame(this.MazeName);
        }
    }
}
