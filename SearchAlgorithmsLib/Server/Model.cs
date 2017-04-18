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

        public Maze GenerateMaze(string name, int rows, int cols)
        {
            if (!privateMazeDict.Keys.Contains(name))
            {
                Maze m = generate(name, rows, cols);
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
                    sol = searcher.search(maze);
                }
                else
                {
                    searcher = new BFS<Position>();
                    CostComparator<Position> compare = new CostComparator<Position>();
                    sol = searcher.search(maze, compare);
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

        public string[] mazeList()
        {
            string[] stringArr = new string[multiplayerMazeDict.Count];
            int i = 0;
            foreach (string s in multiplayerMazeDict.Keys)
            {
                stringArr[i++] = s;
            }
            return stringArr;
        }

        public Maze mazeStart(string name, int rows, int cols, TcpClient client, Controller control)
        {
            if (!multiplayerMazeDict.Keys.Contains(name))
            {
                Maze maze = generate(name, rows, cols);
                multiplayerMazeDict.Add(name, maze);
                maze.Name = name;
                multiplayerMazeDict.Add(name, maze);
                //handle multiplayer game
                HandleMultiplaterGame handle = new HandleMultiplaterGame(client, control);
                handle.gameToJason = maze.ToJSON();
                handle.gameToJason = name;
                HandleMultiplatersDict.Add(name, handle);
                handle.startHost();
                return maze;
            }
            return multiplayerMazeDict[name];
        }

        public Maze joinMaze(string name, TcpClient client)
        {
            if (multiplayerMazeDict.Keys.Contains(name))
            {
                HandleMultiplaterGame handle = HandleMultiplatersDict[name];
                handle.guest = client;
                handle.startGuest();
                handle.sendMazeToJsonToHost();
                return multiplayerMazeDict[name];
            }
            return null;
        }

        public string playMove(string move, string name, TcpClient client)
        {
            if (multiplayerMazeDict.Keys.Contains(name))
            {
                HandleMultiplaterGame handle = HandleMultiplatersDict[name];
                handle.sendMessageToClient(client);
                return String.Empty;
            }
            return "Error Game not found";
        }

        public string closeMultiPlayerGame(string name)
        {
            if (multiplayerMazeDict.Keys.Contains(name))
            {
                HandleMultiplaterGame handle = HandleMultiplatersDict[name];
                handle.close();
                return String.Empty;
            }
            return "Error Game not found";
        }

        public Maze generate(string name, int rows, int cols)
        {
            DFSMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze m = mazeGenerator.Generate(rows, cols);
            m.Name = name;
            return m;

        }
    }
}
