using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using MazeLib;

namespace ConsoleApplication
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

            if (maze[s.getstate().Row, s.getstate().Col + 1] == 0)
            {
                MazeLib.Position position = new MazeLib.Position(s.getstate().Row, s.getstate().Col + 1);
                State<MazeLib.Position> state = State<MazeLib.Position>.StatePool.getState(position);
                possibleStates.Add(state);
            }

            if (maze[s.getstate().Row - 1, s.getstate().Col] == 0)
            {
                MazeLib.Position position = new MazeLib.Position(s.getstate().Row - 1, s.getstate().Col);
                State<MazeLib.Position> state = State<MazeLib.Position>.StatePool.getState(position);
                possibleStates.Add(state);
            }

            if (maze[s.getstate().Row, s.getstate().Col - 1] == 0)
            {
                MazeLib.Position position = new MazeLib.Position(s.getstate().Row, s.getstate().Col - 1);
                State<MazeLib.Position> state = State<MazeLib.Position>.StatePool.getState(position);
                possibleStates.Add(state);
            }

            if (maze[s.getstate().Row + 1, s.getstate().Col] == 0)
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
    }
}
