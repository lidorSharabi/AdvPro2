using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace WpfClient
{
    public class ApplicationSettingsModel : ISettingsModel
    {
        public string ServerIP
        {
            get; set;
            //get { return Properties.Settings.Default.ServerIP; }
            //set { Properties.Settings.Default.ServerIP = value; }
        }
        public int ServerPort
        {
            get; set;
            //get { return Properties.Settings.Default.ServerPort; }
            //set { Properties.Settings.Default.ServerPort = value; }
        }
        public int MazeRows { get; set; }
        public int MazeCols { get; set; }
        public int SearchAlgorithm { get; set; }

        public void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }
    }
}
