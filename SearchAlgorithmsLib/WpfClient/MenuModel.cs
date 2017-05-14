using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient
{
    public class MenuModel : IMenuModel
    {
        public int MazeRows
        {
            get { return Properties.Settings.Default.MazeRows; }
            set {; }
        }

        public int MazeCols
        {
            get { return Properties.Settings.Default.MazeCols; }
            set {; }
        }
    }
}
