using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvProg2WebApp.Models
{
    public class MazeModel
    {
        public string Name { get; set; }
        public int Rows { get; set; }
        public int Cols { get; set; }
        public int InitialPosRow { get; set; }
        public int InitialPosCol { get; set; }
        public int GoalPosRow { get; set; }
        public int GoalPosCol { get; set; }
        public string MazeString { get; set; }
    }
}