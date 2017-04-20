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
    public class Model : IModel
    {
        private Dictionary<string, Maze> privateMazeDict = new Dictionary<string, Maze>();
        private Dictionary<string, Maze> multiplayerMazeDict = new Dictionary<string, Maze>();
        private Dictionary<string, Solution<Position>> privateSolDict = new Dictionary<string, Solution<Position>>();
        private Dictionary<string, Solution<Position>> multiplayerSolDict = new Dictionary<string, Solution<Position>>();
        private Dictionary<string, HandleMultiplaterGame> HandleMultiplatersDict = new Dictionary<string, HandleMultiplaterGame>();
        private Dictionary<TcpClient, HandleMultiplaterGame> multiplayerGames = new Dictionary<TcpClient, HandleMultiplaterGame>();

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

        public Maze MazeStart(string name, int rows, int cols, TcpClient client, Controller control)
        {
            if (!multiplayerMazeDict.Keys.Contains(name))
            {
                Maze maze = Generate(name, rows, cols);
                multiplayerMazeDict.Add(name, maze);
                //maze.Name = name;
                //multiplayerMazeDict.Add(name, maze);
                //handle multiplayer game
                HandleMultiplaterGame handle = new HandleMultiplaterGame(client, control);
                handle.gameToJason = maze.ToJSON();
                handle.name = name;
                HandleMultiplatersDict.Add(name, handle);
                multiplayerGames.Add(client, handle);
                //handle.startHost();
                return maze;
            }
            return multiplayerMazeDict[name];
        }

        public Maze JoinMaze(string name, TcpClient client)
        {
            if (multiplayerMazeDict.Keys.Contains(name))
            {
                HandleMultiplaterGame handle = HandleMultiplatersDict[name];
                handle.guest = client;
                //handle.startGuest();
                handle.SendMazeToJsonToHost();
                multiplayerGames.Add(client, handle);
                return multiplayerMazeDict[name];
            }
            return null;
        }

        public string PlayMove(string move, TcpClient client)
        {
            if (multiplayerGames.Keys.Contains(client))
            {
                HandleMultiplaterGame handle = multiplayerGames[client];
                handle.SendMessageToClient(client, move);
            }
                return String.Empty;
        }

        public string CloseMultiPlayerGame(string name, TcpClient client)
        {
            if (multiplayerMazeDict.Keys.Contains(name) && multiplayerGames.Keys.Contains(client)) 
            {
                HandleMultiplaterGame handle = HandleMultiplatersDict[name];
                handle.Close(client);
                multiplayerMazeDict.Remove(name);
                TcpClient guest = handle.guest;
                TcpClient host = handle.host;
                multiplayerGames.Remove(guest);
                multiplayerGames.Remove(host);
                HandleMultiplatersDict.Remove(name);
                return String.Empty;
            }
            return "Error Game not found";
        }

        public Maze Generate(string name, int rows, int cols)
        {
            DFSMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze m = mazeGenerator.Generate(rows, cols);
            m.Name = name;
            return m;

        }
    }
}
