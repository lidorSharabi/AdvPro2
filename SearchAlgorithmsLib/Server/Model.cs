using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;
using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// responsible for saving the private games and the multiplayers games
    /// and the solution of the private games and the handler of the multiplayers games
    /// </summary>
    public class Model : IModel
    {
        private Dictionary<string, Maze> privateMazeDict = new Dictionary<string, Maze>();
        private Dictionary<string, Maze> multiplayerMazeDict = new Dictionary<string, Maze>();
        private Dictionary<string, Solution<Position>> privateSolDict = new Dictionary<string, Solution<Position>>();
        private Dictionary<string, Solution<Position>> multiplayerSolDict = new Dictionary<string, Solution<Position>>();
        private Dictionary<string, HandleMultiplayers> HandleMultiplayersDict = new Dictionary<string, HandleMultiplayers>();
        private Dictionary<TcpClient, HandleMultiplayers> multiplayerGames = new Dictionary<TcpClient, HandleMultiplayers>();
        /// <summary>
        /// generating a new private maze
        /// if the maze already exists it returns it
        /// </summary>
        /// <param name="name"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        public Maze GenerateMaze(string name, int rows, int cols)
        {
            if (!privateMazeDict.Keys.Contains(name))
            {
                Maze m = Generate(name, rows, cols);
                privateMazeDict.Add(name, m);
                return m;
            }
            return privateMazeDict[name];
        }
        /// <summary>
        /// solving a private maze
        /// if the solution for this maze already exists it returns it
        /// </summary>
        /// <param name="name"></param>
        /// <param name="algorithm"></param>
        /// <returns></returns>
        public string SolveMaze(string name, int algorithm)
        {
            Maze m = privateMazeDict[name];
            MazeAdapter<Position> maze = new MazeAdapter<Position>(m);
            Searcher<Position> searcher;
            Solution<Position> sol;
            if (!privateSolDict.Keys.Contains(name))
            {
                if (algorithm == 0)
                {
                    searcher = new DFS<Position>();
                    sol = searcher.Search(maze);
                }
                else
                {
                    searcher = new BFS<Position>();
                    CostComparator<Position> compare = new CostComparator<Position>();
                    sol = searcher.Search(maze, compare);
                }

                privateSolDict.Add(name, sol);
            }
            else
            {
                sol = privateSolDict[name];
            }

            string stringSolution = maze.ToSolution(sol);
            int numberOfNodesevaluated = sol.evaluatedNodes;
            stringSolution += " ";
            stringSolution += numberOfNodesevaluated;
            return stringSolution;
        }
        /// <summary>
        /// list of all multiplayers maze that a client can join
        /// </summary>
        /// <returns></returns>
        public string[] MazeList()
        {
            string[] stringArr = new string[multiplayerMazeDict.Count];
            int i = 0;
            foreach (string s in multiplayerMazeDict.Keys)
            {
                stringArr[i++] = s;
            }
            return stringArr;
        }
        /// <summary>
        /// starting a new multiplayer maze
        /// if the maze already exists it returns it
        /// </summary>
        /// <param name="name"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public Maze MazeStart(string name, int rows, int cols, TcpClient client)
        {
            if (!multiplayerMazeDict.Keys.Contains(name))
            {
                Maze maze = Generate(name, rows, cols);
                multiplayerMazeDict.Add(name, maze);
                HandleMultiplayers handle = new HandleMultiplayers(client);
                handle.gameToJason = maze.ToJSON();
                handle.name = name;
                HandleMultiplayersDict.Add(name, handle);
                multiplayerGames.Add(client, handle);
                return maze;
            }
            return multiplayerMazeDict[name];
        }
        /// <summary>
        /// joining a multiplayer maze that was started by another client
        /// and sending the maze to the client that started it
        /// </summary>
        /// <param name="name"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public Maze JoinMaze(string name, TcpClient client)
        {
            if (multiplayerMazeDict.Keys.Contains(name))
            {
                HandleMultiplayers handle = HandleMultiplayersDict[name];
                handle.guest = client;
                handle.SendMazeToJsonToHost();
                multiplayerGames.Add(client, handle);
                return multiplayerMazeDict[name];
            }
            return null;
        }
        /// <summary>
        ///  playing a move in the multiplayer maze (up, down, right, left)
        /// </summary>
        /// <param name="move"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public string PlayMove(string move, TcpClient client)
        {
            if (multiplayerGames.Keys.Contains(client))
            {
                HandleMultiplayers handle = multiplayerGames[client];
                handle.SendMessageToClient(client, move);
            }
                return String.Empty;
        }
        /// <summary>
        /// closing a multiplayer maze
        /// </summary>
        /// <param name="name"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public string CloseMultiPlayerGame(string name, TcpClient client)
        {
            if (multiplayerMazeDict.Keys.Contains(name) && multiplayerGames.Keys.Contains(client)) 
            {
                HandleMultiplayers handle = HandleMultiplayersDict[name];
                handle.Close(client);
                multiplayerMazeDict.Remove(name);
                TcpClient guest = handle.guest;
                TcpClient host = handle.host;
                multiplayerGames.Remove(guest);
                multiplayerGames.Remove(host);
                HandleMultiplayersDict.Remove(name);
                return String.Empty;
            }
            return "Error Game not found";
        }
        /// <summary>
        /// generates a maze with the maze adapter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        public Maze Generate(string name, int rows, int cols)
        {
            DFSMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze m = mazeGenerator.Generate(rows, cols);
            m.Name = name;
            return m;

        }
    }
}
