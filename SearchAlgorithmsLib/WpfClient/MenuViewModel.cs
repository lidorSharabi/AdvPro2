using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient
{
    /// <summary>
    /// the menu viewmodel - for the multi player menu game to
    /// display the list of the games in the menu
    /// </summary>
    class MenuViewModel : ViewModel
    {
        /// <summary>
        /// the model
        /// </summary>
        private MenuModel model;
        /// <summary>
        /// the list of the gamed
        /// </summary>
        public ObservableCollection<string> listOfGames;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="model"></param>
        public MenuViewModel(MenuModel model)
        {
            this.model = model;
        }
        /// <summary>
        /// the property of the list games
        /// </summary>
        public ObservableCollection<string> ListOfGames
        {
            get { return this.listOfGames; }
            set
            {
                this.listOfGames = value;
                NotifyPropertyChanged("ListOfGames");
            }
        }
        /// <summary>
        /// calling the model to send the server the list command and reading
        /// from the server the list
        /// </summary>
        internal void ListMaze()
        {
            model.List();
            Task<string> t = Task.Factory.StartNew(() => { return model.client.Read(); });
            t.ContinueWith(ListMaze_Raed_OnComplited);
        }
        /// <summary>
        /// when we get the list we put it in the list of games property
        /// </summary>
        /// <param name="obj"></param>
        private void ListMaze_Raed_OnComplited(Task<string> obj)
        {
            string response = obj.Result;
            ListOfGames = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<string>>(response);
        }
    }
}
