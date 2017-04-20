using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;

namespace Server
{
    /// <summary>
    /// adapter for the maze
    /// </summary>
    /// <typeparam name="Position"></typeparam>
    public class MazeAdapter<Position> : ISearchable<MazeLib.Position>
    {
        private Maze maze;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="m"></param>
        public MazeAdapter(Maze m)
        {
            maze = m;
        }
        /// <summary>
        /// returning the maze
        /// </summary>
        /// <returns></returns>
        public Maze GetMaze()
        {
            return maze;
        }
        /// <summary>
        /// returning the initial state of the maze
        /// </summary>
        /// <returns></returns>
        public State<MazeLib.Position> GetInitializeState()
        {
            return State<MazeLib.Position>.StatePool.GetState(maze.InitialPos);
        }
        /// <summary>
        /// returning the goal state of the maze
        /// </summary>
        /// <returns></returns>
        public State<MazeLib.Position> GetGoalState()
        {
            return State<MazeLib.Position>.StatePool.GetState(maze.GoalPos);
        }
        /// <summary>
        /// returning a list of the possible states for the given state
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public List<State<MazeLib.Position>> GetAllPossibleStates(State<MazeLib.Position> s)
        {
            List<State<MazeLib.Position>> possibleStates = new List<State<MazeLib.Position>>();

            if (s.Getstate().Col + 1 <= maze.Cols && maze[s.Getstate().Row, s.Getstate().Col + 1] == CellType.Free)
            {
                MazeLib.Position position = new MazeLib.Position(s.Getstate().Row, s.Getstate().Col + 1);
                State<MazeLib.Position> state = State<MazeLib.Position>.StatePool.GetState(position);
                possibleStates.Add(state);
            }

            if (s.Getstate().Row - 1 >= 0 && maze[s.Getstate().Row - 1, s.Getstate().Col] == CellType.Free)
            {
                MazeLib.Position position = new MazeLib.Position(s.Getstate().Row - 1, s.Getstate().Col);
                State<MazeLib.Position> state = State<MazeLib.Position>.StatePool.GetState(position);
                possibleStates.Add(state);
            }

            if (s.Getstate().Col - 1 >= 0 && maze[s.Getstate().Row, s.Getstate().Col - 1] == CellType.Free)
            {
                MazeLib.Position position = new MazeLib.Position(s.Getstate().Row, s.Getstate().Col - 1);
                State<MazeLib.Position> state = State<MazeLib.Position>.StatePool.GetState(position);
                possibleStates.Add(state);
            }

            if (s.Getstate().Row + 1 < maze.Rows && maze[s.Getstate().Row + 1, s.Getstate().Col] == CellType.Free)
            {
                MazeLib.Position position = new MazeLib.Position(s.Getstate().Row + 1, s.Getstate().Col);
                State<MazeLib.Position> state = State<MazeLib.Position>.StatePool.GetState(position);
                possibleStates.Add(state);
            }

            return possibleStates;
        }
        /// <summary>
        /// printing the maze
        /// </summary>
        public void printMaze()
        {
            Console.WriteLine(maze.ToString());
        }
        /// <summary>
        /// turning the solution to a string
        /// </summary>
        /// <param name="sol"></param>
        /// <returns></returns>
        public String ToSolution(Solution<MazeLib.Position> sol)
        {
            List<State<MazeLib.Position>> list = new List<State<MazeLib.Position>>(sol.vertex);

            String solution = "";
            for (int i = list.Count - 1; i > 0; i--)
            {
                if (list[i].Getstate().Col > list[i - 1].Getstate().Col)
                {
                    solution += 0;
                }

                if (list[i].Getstate().Row > list[i - 1].Getstate().Row)
                {
                    solution += 2;
                }

                if (list[i].Getstate().Col < list[i - 1].Getstate().Col)
                {
                    solution += 1;
                }

                if (list[i].Getstate().Row < list[i - 1].Getstate().Row)
                {
                    solution += 3;
                }
            }
            return solution;
        }
    }
}
