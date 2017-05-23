using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient
{
    public class MenuModel
    {
        public TelnetMultiClient client;
        public MenuModel(TelnetMultiClient client)
        {
            this.client = client;
        }

        internal void List()
        {
            client.List();
        }
    }
}
