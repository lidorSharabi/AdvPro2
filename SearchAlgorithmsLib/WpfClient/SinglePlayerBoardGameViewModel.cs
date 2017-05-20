using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient
{
    class SinglePlayerBoardGameViewModel : ViewModel
    {
        SinglePlayerBoardGameModel model;
        private string name;
        private string rows;
        private string columns;
        private string mazeString;

        public SinglePlayerBoardGameViewModel(string serverMessage, string name, string rows, string columns)
        {
            this.model = new SinglePlayerBoardGameModel(serverMessage, name, rows, columns);
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
        

        internal void SolveMaze()
        {
            throw new NotImplementedException();
        }

    }
}
