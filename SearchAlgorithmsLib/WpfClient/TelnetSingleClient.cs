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
        /// <summary>
        /// event for getting the message from the server
        /// </summary>
        public event EventHandler ServerMessageArrivedEvent;
        /// <summary>
        /// the server message
        /// </summary>
        public string ServerMessage;
        /// <summary>
        /// handling the event of the message from the server
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnServerMessageArrived(EventArgs args)
        {
            ServerMessageArrivedEvent?.Invoke(this, args);
        }
        /// <summary>
        /// connecting to the server
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void connect(string ip, int port)
        {
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
        }
        /// <summary>
        /// sending the generate command to the server
        /// </summary>
        /// <param name="txtMazeName"></param>
        /// <param name="txtRows"></param>
        /// <param name="txtCols"></param>
        public void Generate(string txtMazeName, string txtRows, string txtCols)
        {
            write(String.Format("generate {0} {1} {2}", txtMazeName, txtRows, txtCols));
        }
        /// <summary>
        /// sending the solve command to the server
        /// </summary>
        /// <param name="mazeName"></param>
        public void Solve(string mazeName)
        {
            write(String.Format("solve {0} {1} ", mazeName, Properties.Settings.Default.SearchAlgorithm));
        }
        /// <summary>
        /// discnnecting from the server
        /// </summary>
        public void disconnect()
        {
            throw new NotImplementedException("single player shouldn't call this function");
        }
        /// <summary>
        /// reading from the server
        /// </summary>
        /// <returns></returns>
        public string read()
        {
            reader = new StreamReader(stream);
            string serverResponse = "";
            while (!reader.EndOfStream)
            {
                serverResponse += reader.ReadLine();
                if (serverResponse.Contains("end of message"))
                    break;
            }
            return serverResponse.Replace("end of message", "");
        }

        /// <summary>
        /// writing to the server
        /// </summary>
        /// <param name="command"></param>
        public void write(string command)
        {
            client = new TcpClient();
            client.Connect(ep);
            stream = client.GetStream();
            writer = new StreamWriter(stream);
            writer.AutoFlush = true;
            //Send command to server
            writer.WriteLine(command);
        }
    }
}
