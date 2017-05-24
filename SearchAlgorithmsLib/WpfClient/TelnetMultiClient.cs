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
    public class TelnetMultiClient : ITelnetClient
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

        public event EventHandler ServerWaitingEvent;
        public event EventHandler ServerJoinEvent;

        public string ServerMessage;

        protected virtual void OnServerWaitingEvent(EventArgs args)
        {
            ServerWaitingEvent?.Invoke(this, args);
        }

        protected virtual void OnServerJoinEvent(EventArgs args)
        {
            ServerJoinEvent?.Invoke(this, args);
        }

        public void connect(string ip, int port)
        {
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
            client = new TcpClient();
            client.Connect(ep);
            stream = client.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
        }

        public void disconnect()
        {
            this.client.Close();
        }

        public string read()
        {
            string serverResponse = "";
            while (!reader.EndOfStream)
            {
                serverResponse += reader.ReadLine();
                if (serverResponse.Contains("end of message"))
                    break;
            }
            return serverResponse.Replace("end of message", "");
        }

        internal bool Continue()
        {
            return !keepConnectionOpen;
        }

        internal void Move(string move)
        {
            write(move);
        }

        public string ContinuousReading()
        {
            //reader = new StreamReader(stream);
            string serverResponse = "";
            while (!reader.EndOfStream)
            {
                serverResponse += reader.ReadLine();
                Console.WriteLine(serverResponse);
                if (serverResponse.Contains("}}") || serverResponse.Contains("Direction"))
                    break;
            }
            return serverResponse;
        }


        public void write(string command)
        {
            writer.AutoFlush = true;
            //Send command to server
            writer.WriteLine(command);
        }

        public void Start(string name, string rows, string cols)
        {
            write(string.Format("start {0} {1} {2}", name, rows, cols));
        }

        public void Join(string name)
        {
            write(string.Format("join {0}", name));
        }
        public void List()
        {
            write(string.Format("list"));
        }
    }
}
