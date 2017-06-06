
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
    /// <summary>
    /// responsible for the client connection and writing and recieving to the client
    /// </summary>
    public class Client
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
        /// Ctor
        /// </summary>
        public Client()
        {
            ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Int32.Parse(ConfigurationManager.AppSettings["PortNumber"]));
        }
        /// <summary>
        /// starting the connection of the client
        /// </summary>
        public void Start()
        {
            string readConsoleLine;
            Console.WriteLine("Client started, Please enter operation:");
            Task task = new Task(() =>
            {
                while (true)
                {
                    try
                    {
                        //Send operation to server
                        readConsoleLine = Console.ReadLine();
                        if (!keepConnectionOpen)
                        {
                            //keep the connection open
                            keepConnectionOpen = true;
                            client = new TcpClient();
                            client.Connect(ep);
                            stream = client.GetStream();
                            writer = new StreamWriter(stream);
                            writer.AutoFlush = true;
                            reader = new StreamReader(stream);
                            //Ssend command to server
                            writer.WriteLine(readConsoleLine);
                            //get and print result from server
                            new Task(() =>
                            {
                                while (!reader.EndOfStream)
                                {
                                    Console.WriteLine(reader.ReadLine());
                                }
                            }).Start();
                        }
                        else 
                        {
                            writer.AutoFlush = true;
                            writer.WriteLine(readConsoleLine);
                        }
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
            });
            task.Start();
            //Blocks the current thread until the current WaitHandle receives a signal
            new System.Threading.AutoResetEvent(false).WaitOne();
        }
    }       
}