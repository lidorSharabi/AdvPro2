using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvProg2WebApp.Models
{
    /// <summary>
    /// maze model for the controller
    /// </summary>
    public class MazeModel
    {
        /// <summary>
        /// the maze name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// the maze rows
        /// </summary>
        public int Rows { get; set; }
        /// <summary>
        /// the maze cols
        /// </summary>
        public int Cols { get; set; }
        /// <summary>
        /// the maze player start row
        /// </summary>
        public int StartRow { get; set; }
        /// <summary>
        /// the maze player start col
        /// </summary>
        public int StartCol { get; set; }
        /// <summary>
        /// the maze exit row
        /// </summary>
        public int ExitRow { get; set; }
        /// <summary>
        /// the maze exit column
        /// </summary>
        public int ExitCol { get; set; }
        /// <summary>
        /// the maze string
        /// </summary>
        public string MazeString { get; set; }
    }
}