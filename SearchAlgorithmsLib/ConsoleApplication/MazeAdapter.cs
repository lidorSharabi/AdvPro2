using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;

namespace ConsoleApp1
{
    class MazeAdapter<Position> : ISearchable<MazeLib.Position>
    {
        private Maze maze;
        public MazeAdapter(Maze m)
        {
            maze = m;
        }
        public Maze getMaze()
        {
            return maze;
        }
        public State<MazeLib.Position> getInitializeState()
        {
            return State<MazeLib.Position>.StatePool.getState(maze.InitialPos);
        }
        public State<MazeLib.Position> getGoalState()
        {
            return State<MazeLib.Position>.StatePool.getState(maze.GoalPos);
        }
        public List<State<MazeLib.Position>> getAllPossibleStates(State<MazeLib.Position> s)
        {
            List<State<MazeLib.Position>> possibleStates = new List<State<MazeLib.Position>>();

            if (s.getstate().Col + 1 <= maze.Cols && maze[s.getstate().Row, s.getstate().Col + 1] == CellType.Free)
            {
                MazeLib.Position position = new MazeLib.Position(s.getstate().Row, s.getstate().Col + 1);
                State<MazeLib.Position> state = State<MazeLib.Position>.StatePool.getState(position);
                possibleStates.Add(state);
            }

            if (s.getstate().Row - 1 >= 0 && maze[s.getstate().Row - 1, s.getstate().Col] == CellType.Free)
            {
                MazeLib.Position position = new MazeLib.Position(s.getstate().Row - 1, s.getstate().Col);
                State<MazeLib.Position> state = State<MazeLib.Position>.StatePool.getState(position);
                possibleStates.Add(state);
            }

            if (s.getstate().Col - 1 >= 0 && maze[s.getstate().Row, s.getstate().Col - 1] == CellType.Free)
            {
                MazeLib.Position position = new MazeLib.Position(s.getstate().Row, s.getstate().Col - 1);
                State<MazeLib.Position> state = State<MazeLib.Position>.StatePool.getState(position);
                possibleStates.Add(state);
            }

            if (s.getstate().Row + 1 < maze.Rows && maze[s.getstate().Row + 1, s.getstate().Col] == CellType.Free)
            {
                MazeLib.Position position = new MazeLib.Position(s.getstate().Row + 1, s.getstate().Col);
                State<MazeLib.Position> state = State<MazeLib.Position>.StatePool.getState(position);
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
                if (list[i].getstate().Col > list[i - 1].getstate().Col)
                {
                    //solution += MazeLib.Direction.Left;
                    solution += 0;
                }

                if (list[i].getstate().Row > list[i - 1].getstate().Row)
                {
                    //solution += MazeLib.Direction.Up;
                    solution += 2;
                }

                if (list[i].getstate().Col < list[i - 1].getstate().Col)
                {
                    //solution += MazeLib.Direction.Right;
                    solution += 1;
                }

                if (list[i].getstate().Row < list[i - 1].getstate().Row)
                {
                    //solution += MazeLib.Direction.Down;
                    solution += 3;
                }
            }
            return solution;
        }
    }
}
