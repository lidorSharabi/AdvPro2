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
    public class MultiPlayerHandler : Hub
    {
        public static List<string> MyUsers = new List<string>();
        Model model = new Model();

        public override Task OnConnected()
        {
            MyUsers.Add(Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {

            MyUsers.Remove(Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        public void StartGame(string name, int rows, int cols)
        {
            model.MazeStart(name, rows, cols, Context.ConnectionId);
        }

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

        public void EndOfGame(string name)
        {
            foreach (string s in MyUsers)
            {
                if (Model.multiplayerGames.Keys.Contains(s))
                {
                    if (Model.multiplayerGames[s] == name && Context.ConnectionId != s)
                    {
                        Clients.Client(s).broadcastMessage("Game Ended");
                    }
                }

            }

        }

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

    }

}