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
        public int StartRow { get; set; }
        public int StartCol { get; set; }
        public int ExitRow { get; set; }
        public int ExitCol { get; set; }
        public string MazeString { get; set; }
    }
}