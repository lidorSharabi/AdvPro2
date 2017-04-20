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
        string[] MazeList();
        Maze MazeStart(string name, int rows, int cols, TcpClient client);
        Maze JoinMaze(string name, TcpClient client);
        string PlayMove(string move, TcpClient client);
        string CloseMultiPlayerGame(string name, TcpClient client);
    }
}
