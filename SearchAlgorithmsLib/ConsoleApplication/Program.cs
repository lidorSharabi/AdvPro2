using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            DFSMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze m = mazeGenerator.Generate(10, 20);
            MazeAdapter<Position> maze = new MazeAdapter<Position>(m);
            maze.printMaze();
        }

        //TODO - implements
        void CompareSolvers() { }
    }
}
