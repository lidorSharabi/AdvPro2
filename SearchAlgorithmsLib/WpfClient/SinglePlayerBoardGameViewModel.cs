using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient
{
    /// <summary>
    /// the single player gmae viewmodel
    /// </summary>
    class SinglePlayerBoardGameViewModel : ViewModel
    {
        /// <summary>
        /// the model of the single player game
        /// </summary>
        SinglePlayerBoardGameModel model;
        /// <summary>
        /// the single player game board
        /// </summary>
        SinglePlayerGameBoard view;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="serverMessage"></param>
        /// <param name="client"></param>
        /// <param name="view"></param>
        public SinglePlayerBoardGameViewModel(string serverMessage, TelnetSingaleClient client, SinglePlayerGameBoard view)
        {
            this.model = new SinglePlayerBoardGameModel(serverMessage , client);
            this.view = view;
        }
        /// <summary>
        /// the maze name property
        /// </summary>
        public string MazeName
        {
            get { return model.MazeName; }
            set
            {
                model.MazeName = value;
                NotifyPropertyChanged("MazeName");
            }
        }
        /// <summary>
        /// the maze rows property
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
        /// the maze columns property
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
        /// the string maze property
        /// </summary>
        public string MazeString
        {
            get { return model.MazeString; }
            set
            {
                model.MazeString = value;
                NotifyPropertyChanged("MazeString");
            }
        }
        /// <summary>
        /// the initial position property
        /// </summary>
        public string InitialPoint
        {
            get { return model.InitialPoint; }
            set
            {
                model.InitialPoint = value;
                NotifyPropertyChanged("StartPoint");
            }
        }
        /// <summary>
        /// the goal position property
        /// </summary>
        public string GoalPoint
        {
            get { return model.GoalPoint; }
            set
            {
                model.GoalPoint = value;
                NotifyPropertyChanged("EndPoint");
            }
        }
        /// <summary>
        /// calling the solve function in the model
        /// </summary>
        internal void SolveMaze()
        {
            model.SolveMaze();
            Task<string> t = Task.Factory.StartNew(() => { return model.client.Read(); });
            t.ContinueWith(SolveMaze_Raed_OnComplited);
        }
        /// <summary>
        /// getting the solve string from the server response
        /// </summary>
        /// <param name="obj"></param>
        private void SolveMaze_Raed_OnComplited(Task<string> obj)
        {
            string response = obj.Result;
            int pFrom = response.IndexOf("Solution\":") + "Solution\":".Length + 2;
            int pTo = response.LastIndexOf(",") - 1;
            response = response.Substring(pFrom, pTo - pFrom);
            view.SolveMazeAnimation(response);  
        }
    }
}
