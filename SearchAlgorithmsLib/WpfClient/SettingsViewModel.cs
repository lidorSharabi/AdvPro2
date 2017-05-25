using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient
{
    /// <summary>
    /// the view model of the settings window
    /// </summary>
    public class SettingsViewModel : ViewModel
    {
        /// <summary>
        /// the model of the settings
        /// </summary>
        private ISettingsModel model;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="model"></param>
        public SettingsViewModel(ISettingsModel model)
        {
            this.model = model;
        }
        /// <summary>
        /// the ip of the server property
        /// </summary>
        public string ServerIP
        {
            get { return model.ServerIP; }
            set
            {
                model.ServerIP = value;
                NotifyPropertyChanged("ServerIP");
            }
        }
        /// <summary>
        /// the port of the server property
        /// </summary>
        public int ServerPort
        {
            get { return model.ServerPort; }
            set
            {
                model.ServerPort = value;
                NotifyPropertyChanged("ServerPort");
            }
        }
        /// <summary>
        /// the defaul maze rows property
        /// </summary>
        public int MazeRows
        {
            get { return model.MazeRows; }
            set
            {
                model.MazeRows = value;
                NotifyPropertyChanged("MazeRows");
            }
        }
        /// <summary>
        /// the default maze cols property
        /// </summary>
        public int MazeCols
        {
            get { return model.MazeCols; }
            set
            {
                model.MazeCols = value;
                NotifyPropertyChanged("MazeCols");
            }
        }
        /// <summary>
        /// the default search algorithm
        /// </summary>
        public int SearchAlgorithm
        {
            get { return model.SearchAlgorithm; }
            set
            {
                model.SearchAlgorithm = value;
                NotifyPropertyChanged("SearchAlgorithm");
            }
        }
        /// <summary>
        /// saving the settings the user changed
        /// </summary>
        public void SaveSettings()
        {
            model.SaveSettings();
        }
    }
}
