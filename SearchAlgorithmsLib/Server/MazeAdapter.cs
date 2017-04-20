using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;

namespace Server
{
    public class MazeAdapter<Position> : ISearchable<MazeLib.Position>
    {
        private Maze maze;
        public MazeAdapter(Maze m)
        {
            maze = m;
        }
        public Maze GetMaze()
        {
            return maze;
        }
        public State<MazeLib.Position> GetInitializeState()
        {
            return State<MazeLib.Position>.StatePool.GetState(maze.InitialPos);
        }
        public State<MazeLib.Position> GetGoalState()
        {
            return State<MazeLib.Position>.StatePool.GetState(maze.GoalPos);
        }
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

        public void printMaze()
        {
            Console.WriteLine(maze.ToString());
        }

        public String ToSolution(Solution<MazeLib.Position> sol)
        {
            List<State<MazeLib.Position>> list = new List<State<MazeLib.Position>>(sol.vertex);

            String solution = "";
            for (int i = list.Count - 1; i > 0; i--)
            {
                if (list[i].Getstate().Col > list[i - 1].Getstate().Col)
                {
                    //solution += MazeLib.Direction.Left;
                    solution += 0;
                }

                if (list[i].Getstate().Row > list[i - 1].Getstate().Row)
                {
                    //solution += MazeLib.Direction.Up;
                    solution += 2;
                }

                if (list[i].Getstate().Col < list[i - 1].Getstate().Col)
                {
                    //solution += MazeLib.Direction.Right;
                    solution += 1;
                }

                if (list[i].Getstate().Row < list[i - 1].Getstate().Row)
                {
                    //solution += MazeLib.Direction.Down;
                    solution += 3;
                }
            }
            return solution;
        }
    }
}
