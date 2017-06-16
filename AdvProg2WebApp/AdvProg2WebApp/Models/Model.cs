using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;
using System.Net.Sockets;

namespace AdvProg2WebApp.Models
{
    /// <summary>
    /// responsible for saving the private games and the multiplayers games
    /// and the solution of the private games and the handler of the multiplayers games
    /// </summary>
    public class Model : IModel
    {
        /// <summary>
        /// the private games dictionary, the name is the key
        /// </summary>
        static Dictionary<string, Maze> privateMazeDict = new Dictionary<string, Maze>();
        /// <summary>
        /// the multiplayers games dictionary, the name is the key
        /// </summary>
        static Dictionary<string, Maze> multiplayerMazeDict = new Dictionary<string, Maze>();
        /// <summary>
        /// the private games solutions dictionary, the name is the key
        /// </summary>
        static Dictionary<string, Solution<Position>> privateSolDict = new Dictionary<string, Solution<Position>>();
        /// <summary>
        /// the multiplayers games solutions dictionary, the name is the key
        /// </summary>
        static Dictionary<string, Solution<Position>> multiplayerSolDict = new Dictionary<string, Solution<Position>>();
        /// <summary>
        /// dictionary of the clients that plays in a multyplayer game and the object that
        /// holds the two players of the game, the client is the key
        /// </summary>
        public static Dictionary<string, string> multiplayerGames = new Dictionary<string, string>();
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
                    searcher = new BFS<Position>();
                    CostComparator<Position> compare = new CostComparator<Position>();
                    sol = searcher.Search(maze, compare);
                }
                else
                {
                    searcher = new DFS<Position>();
                    sol = searcher.Search(maze);
                }

                privateSolDict.Add(name, sol);
            }
            else
            {
                sol = privateSolDict[name];
            }

            string stringSolution = maze.ToSolution(sol);
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
        public Maze MazeStart(string name, int rows, int cols, string client)
        {
            if (!multiplayerMazeDict.Keys.Contains(name))
            {
                Maze maze = Generate(name, rows, cols);
                multiplayerMazeDict.Add(name, maze);
                multiplayerGames.Add(client, name);
                return maze;
            }
            return null;
        }
        /// <summary>
        /// joining a multiplayer maze that was started by another client
        /// and sending the maze to the client that started it
        /// </summary>
        /// <param name="name"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public Maze JoinMaze(string name, string client)
        {
            if (multiplayerMazeDict.Keys.Contains(name))
            {
                Maze m = multiplayerMazeDict[name];
                multiplayerGames.Add(client, name);
                multiplayerMazeDict.Remove(name);
                return m;
            }
            return null;
        }
        /// <summary>
        ///  playing a move in the multiplayer maze (up, down, right, left)
        /// </summary>
        /// <param name="move"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public string PlayMove(string move, string client)
        {
                return String.Empty;
        }
        /// <summary>
        /// closing a multiplayer maze
        /// </summary>
        /// <param name="name"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public string CloseMultiPlayerGame(string name, string client)
        {
            if (multiplayerGames.Keys.Contains(client)) 
            {
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
