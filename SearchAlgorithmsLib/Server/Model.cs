using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;

namespace Server
{
    public class Model : IModel
    {
        private Dictionary<string, Maze> privateMazeDict = new Dictionary<string, Maze>();
        private Dictionary<string, Maze> multiplayerMazeDict = new Dictionary<string, Maze>();
        private Dictionary<string, Solution<Position>> privateSolDict = new Dictionary<string, Solution<Position>>();
        private Dictionary<string, Solution<Position>> multiplayerSolDict = new Dictionary<string, Solution<Position>>();

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
            string[] stringArr = new string[multiplayerMazeDict.Count - 1];
            int i = 0;
            foreach (string s in multiplayerMazeDict.Keys)
            {
                stringArr[i++] = s;
            }
            return stringArr;
        }

        public Maze mazeStart(string name, int rows, int cols)
        {
            if (!multiplayerMazeDict.Keys.Contains(name))
            {
                Maze m = generate(name, rows, cols);
                multiplayerMazeDict.Add(name, m);
                return m;
            }
            return multiplayerMazeDict[name];
        }

        public Maze joinMaze(string name)
        {
            if (multiplayerMazeDict.Keys.Contains(name))
            {
                return multiplayerMazeDict[name];
            }
            return null;
        }

        public string playMove(string move)
        {
            return null;
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
