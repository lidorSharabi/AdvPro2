using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient
{
    /// <summary>
    /// interface for the settings model
    /// </summary>
    public interface ISettingsModel
    {
        /// <summary>
        /// the ip of the server property
        /// </summary>
        string ServerIP { get; set; }
        /// <summary>
        /// the port of the server property
        /// </summary>
        int ServerPort { get; set; }
        /// <summary>
        /// the defaul maze rows property
        /// </summary>
        int MazeRows { get; set; }
        /// <summary>
        /// the default maze cols property
        /// </summary>
        int MazeCols { get; set; }
        /// <summary>
        /// the default search algorithm
        /// </summary>
        int SearchAlgorithm { get; set; }
        /// <summary>
        /// saving the settings the user changed
        /// </summary>
        void SaveSettings();
    }
}
