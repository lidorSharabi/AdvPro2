using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient
{
    /// <summary>
    /// the menu model - for the multi player menu game to
    /// display the list of the games in the menu
    /// </summary>
    public class MenuModel
    {
        /// <summary>
        /// the client
        /// </summary>
        public TelnetMultiClient client;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="client"></param>
        public MenuModel(TelnetMultiClient client)
        {
            this.client = client;
        }
        /// <summary>
        /// calls for the command list in the client
        /// </summary>
        internal void List()
        {
            client.List();
        }
    }
}
