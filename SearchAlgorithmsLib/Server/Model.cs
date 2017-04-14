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
        private Dictionary<string, Maze> mazeDict = new Dictionary<string, Maze>();
        private Dictionary<string, Solution<Position>> solDict = new Dictionary<string, Solution<Position>>();

        public Maze GenerateMaze(string name, int rows, int cols)
        {
            DFSMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze m = mazeGenerator.Generate(10, 10);
            mazeDict.Add(name, m);
            return m;
        }

        public String SolveMaze(string name, int algorithm)
        {
            Maze m = mazeDict[name];
            MazeAdapter<Position> maze = new MazeAdapter<Position>(m);
            Searcher<Position> searcher;
            if (algorithm == 0)
            {
                searcher = new DFS<Position>();
            }
            else
            {
                searcher = new BFS<Position>();
            }
            Solution<Position> sol = searcher.search(maze);
            solDict.Add(name, sol);
            string stringSolution =  maze.ToSolution(sol);
            int numberOfNodesevaluated = searcher.getNumberOfNodesEvaluated();
            stringSolution += " ";
            stringSolution += numberOfNodesevaluated;
            return stringSolution;
        }

        public string[] mazeList()
        {
            string[] stringArr = new string[mazeDict.Count - 1];
            int i = 0;
            foreach (string s in mazeDict.Keys)
            {
                stringArr[i++] = s;
            }
            return stringArr;
        }
    }
}
