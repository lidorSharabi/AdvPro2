using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace AdvProg2WebApp.Models
{
    /// <summary>
    /// the hub of the multiplayer game connection
    /// </summary>
    public class MultiPlayerHandler : Hub
    {
        /// <summary>
        /// list of all the users connected in the hub
        /// </summary>
        public static List<string> MyUsers = new List<string>();
        /// <summary>
        /// the model of the maze
        /// </summary>
        Model model = new Model();

        /// <summary>
        /// adding the user when connected
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            MyUsers.Add(Context.ConnectionId);
            return base.OnConnected();
        }

        /// <summary>
        /// removing the user when disconnected
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {

            MyUsers.Remove(Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        /// <summary>
        /// starting the maze game
        /// </summary>
        /// <param name="name"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        public void StartGame(string name, int rows, int cols)
        {
            model.MazeStart(name, rows, cols, Context.ConnectionId);
        }

        /// <summary>
        /// joining a maze game that was started
        /// </summary>
        /// <param name="name"></param>
        public void JoinGame(string name)
        {
            Maze maze = model.JoinMaze(name,Context.ConnectionId);
            JObject jmaze = JObject.Parse(maze.ToJSON());
            MazeModel mazeModel = new MazeModel();
            mazeModel.Name = name;
            mazeModel.Cols = maze.Cols;
            mazeModel.Rows = maze.Rows;
            mazeModel.StartCol = maze.InitialPos.Col;
            mazeModel.StartRow = maze.InitialPos.Row;
            mazeModel.ExitCol = maze.GoalPos.Col;
            mazeModel.ExitRow = maze.GoalPos.Row;
            mazeModel.MazeString = jmaze.GetValue("Maze").ToString();
            foreach (string s in MyUsers)
            {
                if(Model.multiplayerGames.Keys.Contains(s))
                {
                    if(Model.multiplayerGames[s]==name && Context.ConnectionId != s)
                    {
                        Clients.Client(s).broadcastMessage(mazeModel);
                    }
                }

            }
            Clients.Client(Context.ConnectionId).broadcastMessage(mazeModel);
        }

        /// <summary>
        /// game over by one of the players winning
        /// </summary>
        /// <param name="name"></param>
        public void EndOfGame(string name)
        {
            if (Model.multiplayerGames.Keys.Contains(Context.ConnectionId))
            {
                if (Model.multiplayerGames[Context.ConnectionId] == name)
                {
                    Model.multiplayerGames.Remove(Context.ConnectionId);
                }
            }
            else
            {
                return;
            }
            foreach (string s in MyUsers)
            {
                if (Model.multiplayerGames.Keys.Contains(s))
                {
                    if (Model.multiplayerGames[s] == name && Context.ConnectionId != s)
                    {
                        Model.multiplayerGames.Remove(s);
                        Clients.Client(s).broadcastMessage("Game Ended");
                    }
                }

            }

        }

        /// <summary>
        /// playing a move in the game
        /// </summary>
        /// <param name="name"></param>
        /// <param name="move"></param>
        public void PlayMove(string name, string move)
        {
            foreach (string s in MyUsers)
            {
                if (Model.multiplayerGames.Keys.Contains(s))
                {
                    if (Model.multiplayerGames[s] == name && Context.ConnectionId != s)
                    {
                        Clients.Client(s).broadcastMessage(move);
                    }
                }

            }

        }

        /// <summary>
        /// game ended abruptly
        /// </summary>
        /// <param name="name"></param>
        public void CloseGame(string name)
        {
            if (Model.multiplayerGames.Keys.Contains(Context.ConnectionId))
            {
                if (Model.multiplayerGames[Context.ConnectionId] == name)
                {
                    Model.multiplayerGames.Remove(Context.ConnectionId);
                }
            }
            else
            {
                return;
            }

            foreach (string s in MyUsers)
            {
                if (Model.multiplayerGames.Keys.Contains(s))
                {
                    if (Model.multiplayerGames[s] == name && Context.ConnectionId != s)
                    {
                        Model.multiplayerGames.Remove(s);
                        Clients.Client(s).broadcastMessage("Game Closed");
                    }
                }

            }

        }

    }

}