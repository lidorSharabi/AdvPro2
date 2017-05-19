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
    public class TelnetSingaleClient : ITelnetClient
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

        public event EventHandler ServerMessageArrivedEvent;

        public string ServerMessage;

        protected virtual void OnServerMessageArrived(EventArgs args)
        {
            ServerMessageArrivedEvent?.Invoke(this, args);
        }

        public void connect(string ip, int port)
        {
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
            client = new TcpClient();
            client.Connect(ep);
            stream = client.GetStream();
        }

        public void disconnect()
        {
            throw new NotImplementedException("single player shouldn't call this function");
        }

        public void read()
        {
            new Task(() =>
            {
                reader = new StreamReader(stream);
                string serverResponse = "";
                while (!reader.EndOfStream)
                {
                    serverResponse += reader.ReadLine();
                    if (serverResponse.Contains("end of message"))
                        break;
                }
                ServerMessage = serverResponse;
                OnServerMessageArrived(new EventArgs());
            }).Start();
        }

        public void write(string command)
        {
            client = new TcpClient();
            client.Connect(ep);
            stream = client.GetStream();
            writer = new StreamWriter(stream);
            writer.AutoFlush = true;
            //Send command to server
            writer.WriteLine(command);
            read();

             new Task(() =>
            {

               //string serverResponse = 
               //decipheringServerResponse(serverResponse);
            }).Start();
        }

        private void decipheringServerResponse(string serverResponse)
        {
            throw new NotImplementedException();
        }
    }
}
