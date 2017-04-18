using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace Server
{
    public interface IModel
    {
        Maze GenerateMaze(string name, int rows, int cols);
        string SolveMaze(string name, int algorithm);
        string[] mazeList();
        Maze mazeStart(string name, int rows, int cols);
        Maze joinMaze(string name);
        string playMove(string move);
    }
}
