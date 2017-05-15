using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient
{
    public class TelnetClient : ITelnetClient
    {
        /// <summary>
        /// running variables
        /// </summary>
        bool keepConnectionOpen = false, run = true;
        /// <summary>
        /// stream reader variable
        /// </summary>
        StreamReader reader;
        /// <summary>
        /// stream writing variable
        /// </summary>
        StreamWriter writer;
        /// <summary>
        /// stream variable
        /// </summary>
        NetworkStream stream;
        /// <summary>
        /// Ip address variable
        /// </summary>
        IPEndPoint ep;
        /// <summary>
        /// client variable
        /// </summary>
        TcpClient client;

        public void connect(string ip, int port)
        {
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
            client = new TcpClient();
            client.Connect(ep);
            stream = client.GetStream();
        }

        public void disconnect()
        {
            throw new NotImplementedException();
        }

        public string read()
        {
            reader = new StreamReader(stream);
            string s = "";
            new Task(() =>
            {
                while (!reader.EndOfStream)
                {
                    s = s + (reader.ReadLine());
                }
            }).Start();
            return s;
        }

        public void write(string command)
        {
            writer = new StreamWriter(command);
            writer.AutoFlush = true;
        }
    }
}
