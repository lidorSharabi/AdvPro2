using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;
using ConsoleApp1;

namespace Server
{
    public class Model : IModel
    {
        //private Cache mazeCache = new Cache();
        private Dictionary<string, Maze> mazeDict = new Dictionary<string, Maze>();
        private Dictionary<string, Solution<Position>> solDict = new Dictionary<string, Solution<Position>>();
        private List<string> startMazes = new List<string>();

        public Maze GenerateMaze(string name, int rows, int cols)
        {
            DFSMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze m = mazeGenerator.Generate(rows, cols);
            mazeDict.Add(name, m);
            return m;
        }

        public string SolveMaze(string name, int algorithm)
        {
            Maze m = mazeDict[name];
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

            solDict.Add(name, sol);
            string stringSolution = maze.ToSolution(sol);
            int numberOfNodesevaluated = searcher.getNumberOfNodesEvaluated();
            stringSolution += " ";
            stringSolution += numberOfNodesevaluated;
            return stringSolution;
        }

        public string[] mazeList()
        {
            string[] stringArr = new string[startMazes.Count - 1];
            int i = 0;
            foreach (string s in startMazes)
            {
                stringArr[i++] = s;
            }
            return stringArr;
        }

        public Maze mazeStart(string name, int rows, int cols)
        {
            if (mazeDict.Keys.Contains(name))
            {
                mazeDict.Remove(name);
            }
            if (solDict.Keys.Contains(name))
            {
                solDict.Remove(name);
            }
            if (!startMazes.Contains(name))
            {
                startMazes.Add(name);
            }
            return GenerateMaze(name, rows, cols);
        }

        public Maze joinMaze(string name)
        {
            if (startMazes.Contains(name))
            {
                return mazeDict[name];
            }
            return null;
        }
    }
}
