using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.Net.Sockets;

namespace Server
{
    public interface IModel
    {
        Maze GenerateMaze(string name, int rows, int cols);
        string SolveMaze(string name, int algorithm);
        string[] mazeList();
        Maze mazeStart(string name, int rows, int cols, TcpClient client, Controller control);
        Maze joinMaze(string name, TcpClient client);
        string playMove(string move, TcpClient client);
        string closeMultiPlayerGame(string name, TcpClient client);
    }
}
