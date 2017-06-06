using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows;
using WpfClient.Controls;

namespace WpfClient
{
    /// <summary>
    /// the settings model
    /// </summary>
    public class ApplicationSettingsModel : ISettingsModel
    {
        /// <summary>
        /// the ip of the server property
        /// </summary>
        public string ServerIP
        {
            get { return Properties.Settings.Default.ServerIP; }
            set { Properties.Settings.Default.ServerIP = value; }
        }
        /// <summary>
        /// the port of the server property
        /// </summary>
        public int ServerPort
        { 
            get { return Properties.Settings.Default.ServerPort; }
            set { Properties.Settings.Default.ServerPort = value; }
        }
        /// <summary>
        /// the defaul maze rows property
        /// </summary>
        public int MazeRows
        {
            get { return Properties.Settings.Default.MazeRows; }
            set { Properties.Settings.Default.MazeRows = value; }
        }
        /// <summary>
        /// the default maze cols property
        /// </summary>
        public int MazeCols
        {
            get { return Properties.Settings.Default.MazeCols; }
            set { Properties.Settings.Default.MazeCols = value; }
        }
        /// <summary>
        /// the default search algorithm
        /// </summary>
        public int SearchAlgorithm
        {
            get { return Properties.Settings.Default.SearchAlgorithm; }
            set { Properties.Settings.Default.SearchAlgorithm = value; }
        }
        /// <summary>
        /// saving the settings the user changed
        /// </summary>
        public void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }
    }
}
