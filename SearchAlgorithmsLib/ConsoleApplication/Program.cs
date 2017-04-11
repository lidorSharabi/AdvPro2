using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;

namespace ConsoleApp1
{
    class Program
    {
        public static void Main(string[] args)
        {
            CompareSolvers();
        }

        public static void CompareSolvers()
        {
            DFSMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze m = mazeGenerator.Generate(10, 10);
            MazeAdapter<Position> maze = new MazeAdapter<Position>(m);
            maze.printMaze();
            BFS<Position> dfs = new BFS<Position>();
            Solution<Position> sol = dfs.search(maze);
            Console.WriteLine("DFS maze solution: " + maze.ToSolution(sol));
            Console.WriteLine("DFS number of nodes evaluated: " + dfs.getNumberOfNodesEvaluated());
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
            return;
        }


        //david test
        /*
          ISearcher<int> ser = new DFSSearcher<int>();

        Dictionary<State<int>, List<State<int>>> Adj = new Dictionary<State<int>, List<State<int>>>();
        State<int> one = new State<int>(1);
        State<int> two = new State<int>(2);
        State<int> three = new State<int>(3);
        State<int> four = new State<int>(4);
        State<int> five = new State<int>(5);
        State<int> six = new State<int>(6);
        State<int> seven = new State<int>(7);

        Adj[one] = new List<State<int>> { two, three };
        Adj[two] = new List<State<int>> { four, five };
        Adj[three] = new List<State<int>> { two, six };
        Adj[four] = new List<State<int>>();
        Adj[five] = new List<State<int>> { six };
        Adj[six] = new List<State<int>> { seven };
        Adj[seven] = new List<State<int>> { three };

        TestSearchable<int> test1 = new TestSearchable<int>(one, six, Adj);
        Solution<int> sol = ser.Search(test1);

        printSol(sol);
    }


    static void printSol<T>(Solution<T> s)
    {
        for (int i = 0; i < s.Count; i++)
        {
            Console.Write(s[i].ToString() + ",");
        }
        Console.WriteLine();
    }
         */
    }
}
