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
            DFSMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze m = mazeGenerator.Generate(rows, cols);
            m.Name = name;
            privateMazeDict.Add(name, m);
            return m;
        }

        public string SolveMaze(string name, int algorithm)
        {
            Maze m = privateMazeDict[name];
            MazeAdapter<Position> maze = new MazeAdapter<Position>(m);
            Searcher<Position> searcher;
            Solution<Position> sol;
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
            string stringSolution = maze.ToSolution(sol);
            int numberOfNodesevaluated = searcher.getNumberOfNodesEvaluated();
            stringSolution += " ";
            stringSolution += numberOfNodesevaluated;
            return stringSolution;
        }

        //??? why only multi? and why decrease by one?
        public string[] mazeList()
        {
            string[] stringArr = new string[multiplayerMazeDict.Count - 1];
            int i = 0;
            foreach (string s in multiplayerMazeDict.Keys)
            {
                stringArr[i++] = s;
            }
            return stringArr;
        }

        public Maze mazeStart(string name, int rows, int cols, TcpClient client, Controller control)
        {
            if (multiplayerMazeDict.Keys.Contains(name))
            {
                multiplayerMazeDict.Remove(name);
            }
            if (multiplayerSolDict.Keys.Contains(name))
            {
                multiplayerSolDict.Remove(name);
            }
            //generate maaze
            DFSMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze maze = mazeGenerator.Generate(rows, cols);
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
    }
}
