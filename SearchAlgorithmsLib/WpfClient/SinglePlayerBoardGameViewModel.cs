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
        SinglePlayerGameBoard view;

        public SinglePlayerBoardGameViewModel(string serverMessage, TelnetSingaleClient client, SinglePlayerGameBoard view)
        {
            this.model = new SinglePlayerBoardGameModel(serverMessage , client);
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

        internal void SolveMaze()
        {
            model.SolveMaze();
            Task<string> t = Task.Factory.StartNew(() => { return model.client.read(); });
            t.ContinueWith(SolveMaze_Raed_OnComplited);
        }

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
