using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient
{
    class MenuViewModel : ViewModel
    {
        private MenuModel model;
        public ObservableCollection<string> listOfGames;

        public MenuViewModel(MenuModel model)
        {
            this.model = model;
        }

        public ObservableCollection<string> ListOfGames
        {
            get { return this.listOfGames; }
            set
            {
                this.listOfGames = value;
                NotifyPropertyChanged("ListOfGames");
            }
        }

        internal void ListMaze()
        {
            model.List();
            Task<string> t = Task.Factory.StartNew(() => { return model.client.read(); });
            t.ContinueWith(ListMaze_Raed_OnComplited);
        }

        private void ListMaze_Raed_OnComplited(Task<string> obj)
        {
            string response = obj.Result;
            ListOfGames = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<string>>(response);
        }
    }
}
