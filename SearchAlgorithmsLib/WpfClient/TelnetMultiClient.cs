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
        bool keepConnectionOpen = true;
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
        /// connecting to the server
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void connect(string ip, int port)
        {
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
            client = new TcpClient();
            client.Connect(ep);
            stream = client.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
        }
        /// <summary>
        /// disconnecting from the server
        /// </summary>
        public void disconnect()
        {
            this.client.Close();
        }
        /// <summary>
        /// reading from the server
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// reading the movements of the player in the multiplayer game
        /// </summary>
        /// <returns></returns>
        public string readMoveDirectionAndClose()
        {
            string serverResponse = "";
            try
            {
                while (keepConnectionOpen && !reader.EndOfStream)
                {
                    serverResponse += reader.ReadLine();
                    if (serverResponse.Contains("Direction") || serverResponse.Contains("has closed the game"))
                    {
                        serverResponse += reader.ReadLine();
                        break;
                    }
                }
                return serverResponse.Replace("end of message", "");
            }
            catch
            {
                return String.Empty;
            }
        }
        /// <summary>
        /// continue the connection while playing
        /// </summary>
        /// <returns></returns>
        internal bool Continue()
        {
            return keepConnectionOpen;
        }
        /// <summary>
        /// sending the play command to the server
        /// </summary>
        /// <param name="move"></param>
        internal void Move(string move)
        {
            write(string.Format("play {0}", move));
        }
        /// <summary>
        /// waiting for the maze of the multiplayer game
        /// </summary>
        /// <returns></returns>
        public string WatingForJoin()
        {
            //reader = new StreamReader(stream);
            string serverResponse = "";
            while (!reader.EndOfStream)
            {
                serverResponse += reader.ReadLine();
                Console.WriteLine(serverResponse);
                if (serverResponse.Contains("}}"))
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
            try
            {
                writer.AutoFlush = true;
                //Send command to server
                writer.WriteLine(command);
            }
            catch
            {

            }
        }
        /// <summary>
        /// sending the start command to the server
        /// </summary>
        /// <param name="name"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        public void Start(string name, string rows, string cols)
        {
            write(string.Format("start {0} {1} {2}", name, rows, cols));
        }
        /// <summary>
        /// sending the join command to the server
        /// </summary>
        /// <param name="name"></param>
        public void Join(string name)
        {
            write(string.Format("join {0}", name));
        }
        /// <summary>
        /// sending the list command to the server
        /// </summary>
        public void List()
        {
            write(string.Format("list"));
        }
        /// <summary>
        /// sending the close command to the server
        /// </summary>
        /// <param name="name"></param>
        public void CloseGame(string name)
        {
            write(string.Format("close {0}", name));
            this.keepConnectionOpen = false;
            disconnect();
        }

    }
}
