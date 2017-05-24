using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient
{
    class MultiPlayerGameBoardViewModel:ViewModel
    {
        MultiPlayerGameBoardModel model;
        MultiPlayerGameBoard view;

        public MultiPlayerGameBoardViewModel(string serverMessage, TelnetMultiClient client, MultiPlayerGameBoard view)
        {
            this.model = new MultiPlayerGameBoardModel(serverMessage, client);
            this.view = view;
        }

        public string MazeName
        {
            get { return model.MazeName; }
            set
            {
                model.MazeName = value;
                NotifyPropertyChanged("MazeName");
            }
        }

        public int MazeRows
        {
            get { return model.MazeRows; }
            set
            {
                model.MazeRows = value;
                NotifyPropertyChanged("MazeRows");
            }
        }

        public int MazeCols
        {
            get { return model.MazeCols; }
            set
            {
                model.MazeCols = value;
                NotifyPropertyChanged("MazeCols");
            }
        }

        public string MazeString
        {
            get { return model.MazeString; }
            set
            {
                model.MazeString = value;
                NotifyPropertyChanged("MazeString");
            }
        }
        
        public string InitialPoint
        {
            get { return model.InitialPoint; }
            set
            {
                model.InitialPoint = value;
                NotifyPropertyChanged("StartPoint");
            }
        }

        public string GoalPoint
        {
            get { return model.GoalPoint; }
            set
            {
                model.GoalPoint = value;
                NotifyPropertyChanged("EndPoint");
            }
        }

        internal void JoinMaze()
        {
            model.JoinMaze();
        }

        internal string ContinuousReading()
        {
            return model.ContinuousReading();
        }

        internal string Read()
        {
            return model.Read();
        }

        internal void Move(string move)
        {
            model.Move(move);
        }


        internal bool Continue()
        {
            return model.Continue();
        }

    }
}
