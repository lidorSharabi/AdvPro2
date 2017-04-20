
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.IO;
using System;
using System.Configuration;

namespace Client
{
    /*public class Client
    {
        bool keepConnectionOpen = false, run = true;
        StreamReader reader;
        StreamWriter writer;
        NetworkStream stream;
        IPEndPoint ep;
        TcpClient client;

        public Client()
        {
            this.ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Int32.Parse(ConfigurationManager.AppSettings["PortNumber"]));
        }
        public void start()
        {
            string writeLine;
            while (run)
                {
                
                this.client = new TcpClient();
                client.Connect(ep);
                Console.WriteLine("You are connected");
                stream = client.GetStream();
                writer = new StreamWriter(stream);
                reader = new StreamReader(stream);
                Console.WriteLine("Please enter operation:");
                // Send operation to server
                string readLine = Console.ReadLine();
                this.writer.AutoFlush = true;
                writer.WriteLine(readLine);
                HandleServerConnection(readLine);
                //delete
                //writer.WriteLine("generate sss 10 5");
                // Get and print result from server
                while (!reader.EndOfStream)
                {
                    writeLine = reader.ReadLine();
                    if (!String.IsNullOrEmpty(writeLine))
                    {
                        Console.WriteLine(writeLine);
                    }
                }
                
                 //client.Close();
            }
            //client.Close();
                
        }
            

        private void HandleServerConnection(string readLine)
        {
            string[] temp = readLine.Split(' ');
            string commandKey = temp[0];
            if (commandKey.Equals("start") || commandKey.Equals("join"))
            {
                keepConnectionOpen = true;
                Task[] handleServerTasks = new Task[2];
                //handle server response messages
                handleServerTasks[0] = new Task(() =>
                {
                    while (keepConnectionOpen)
                    {
                        while (!reader.EndOfStream)
                        {
                            Console.WriteLine(reader.ReadLine());
                        }
                    }
                });
                handleServerTasks[0].Start();

                //handle server request messages
                handleServerTasks[1] = new Task(() =>
                {
                    string readCommandLine;
                    while (keepConnectionOpen)
                    {
                        readCommandLine = Console.ReadLine();
                        this.writer.AutoFlush = true;
                    if (readCommandLine.Equals("this game is over"))
                        {
                            keepConnectionOpen = false;
                            break;
                        }
                        this.writer.AutoFlush = true;
                        writer.WriteLine(readCommandLine);
                    }
                });
                handleServerTasks[1].Start();
                Console.WriteLine("just checking point");
                Task.WaitAll(handleServerTasks);
            }
        }
    }*/

    /// <summary>
    /// client side
    /// </summary>
    class Client
    {
        /// <summary>
        /// ip and port.
        /// </summary>
        IPEndPoint ep;

        /// <summary>
        /// client tcp socket.
        /// </summary>
        TcpClient client;

        /// <summary>
        /// stream.
        /// </summary>
        NetworkStream stream;

        /// <summary>
        /// reader.
        /// </summary>
        StreamReader reader;

        /// <summary>
        /// writer.
        /// </summary>
        StreamWriter writer;

        /// <summary>
        /// is connect.
        /// </summary>
        bool isConnected;

        /// <summary>
        /// constructor.
        /// </summary>
        public Client()
        {
            /*
            int portnum;
            string portFromAppConfig = ConfigurationManager.AppSettings["port"].ToString();
            bool getPort = Int32.TryParse(portFromAppConfig, out portnum);
            if (!getPort)
                throw new System.InvalidOperationException("port in app.config not an integer");
            string ipAddress = ConfigurationManager.AppSettings["ipAddress"].ToString();*/
            this.ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            Console.WriteLine("You are connected");
            isConnected = false; ;
        }

        /// <summary>
        /// upload client side.
        /// </summary>
        public void Start()
        {
            Task send = new Task(() =>
            {
                while (true)
                {
                    try
                    {

                        string command = Console.ReadLine();
                        if (!isConnected)
                        {
                            isConnected = true;
                            this.client = new TcpClient();
                            client.Connect(ep);
                            stream = client.GetStream();
                            this.writer = new StreamWriter(stream);
                            this.reader = new StreamReader(stream);
                            this.writer.AutoFlush = true;
                            this.writer.WriteLine(command);
                            Task recv = new Task(() =>
                            {
                                List<string> result = new List<string>();
                                while (!reader.EndOfStream)
                                {
                                    string line = reader.ReadLine();
                                    Console.WriteLine(line);
                                    result.Add(line);
                                    /*
                                    // if need to close programe when get empty json - need those lines 
                                    string empty = "{}";
                                    if (string.Compare(line, empty) == 0)
                                        break;
                                        */
                                }

                            });
                            recv.Start();
                        }
                        else
                        {
                            this.writer.AutoFlush = true;
                            this.writer.WriteLine(command);
                        }
                    }
                    catch (Exception) { break; }
                }
            });
            send.Start();

            new System.Threading.AutoResetEvent(false).WaitOne();
            //client.Close();
        }
    }
}

