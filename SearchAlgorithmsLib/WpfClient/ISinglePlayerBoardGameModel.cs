using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient
{
    class ISinglePlayerBoardGameModel
    {
        int MazeName { get; set; }
        int MazeCols { get; set; }
        int MazeRows { get; set; }
        int MazeString { get; set; }
    }
}
