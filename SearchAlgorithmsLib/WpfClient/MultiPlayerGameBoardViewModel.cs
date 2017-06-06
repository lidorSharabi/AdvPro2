using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient
{
    /// <summary>
    /// the multiplayer viewmodel
    /// </summary>
    class MultiPlayerGameBoardViewModel:ViewModel
    {
        /// <summary>
        /// the multiplayer model
        /// </summary>
        MultiPlayerGameBoardModel model;
        /// <summary>
        /// the multiplayer board
        /// </summary>
        MultiPlayerGameBoard view;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="serverMessage"></param>
        /// <param name="client"></param>
        /// <param name="view"></param>
        public MultiPlayerGameBoardViewModel(string serverMessage, TelnetMultiClient client, MultiPlayerGameBoard view)
        {
            this.model = new MultiPlayerGameBoardModel(serverMessage, client);
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
        /// calling the join function in the model
        /// </summary>
        internal void JoinMaze()
        {
            model.JoinMaze();
        }
        /// <summary>
        /// calling the read of the movement from the model
        /// </summary>
        /// <returns></returns>
        internal string Read()
        {
            return model.ReadMoveDirection();
        }
        /// <summary>
        /// calling the play command from the model
        /// </summary>
        /// <param name="move"></param>
        internal void Move(string move)
        {
            model.Move(move);
        }

        /// <summary>
        /// continue the reading
        /// </summary>
        /// <returns></returns>
        internal bool Continue()
        {
            return model.Continue();
        }
        /// <summary>
        /// calling the close of the game and disconnecting from the model
        /// </summary>
        internal void CloseGame()
        {
            model.Disconnect();
        }

    }
}
