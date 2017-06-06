using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient
{
    /// <summary>
    /// inteface for the single player game model
    /// </summary>
    class ISinglePlayerBoardGameModel
    {
        /// <summary>
        /// the maze name
        /// </summary>
        int MazeName { get; set; }
        /// <summary>
        /// the maze columns
        /// </summary>
        int MazeCols { get; set; }
        /// <summary>
        /// the maze rows
        /// </summary>
        int MazeRows { get; set; }
        /// <summary>
        /// the string of the maze
        /// </summary>
        int MazeString { get; set; }
    }
}
