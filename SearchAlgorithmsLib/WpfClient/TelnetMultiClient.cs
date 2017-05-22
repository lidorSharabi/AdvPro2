using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient
{
    class TelnetMultiClient : ITelnetClient
    {
        public void connect(string ip, int port)
        {
            throw new NotImplementedException();
        }

        public void disconnect()
        {
            throw new NotImplementedException();
        }

        public string read()
        {
            throw new NotImplementedException();
        }

        public void write(string command)
        {
            throw new NotImplementedException();
        }

        void Start(string name, string rows, string cols)
        {
            write(string.Format("Start {0} {1} {2}", name, rows, cols));
        }
    }
}
