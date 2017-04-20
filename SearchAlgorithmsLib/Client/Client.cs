
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
    public class Client
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
                            this.client = new TcpClient();
                            this.client.Connect(ep);
                            this.stream = client.GetStream();
                            this.writer = new StreamWriter(stream);
                            this.writer.AutoFlush = true;
                            this.reader = new StreamReader(stream);
                            //Send operation to server
                            this.writer.WriteLine(readConsoleLine);
                            //new Task - Get and print result from server
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
                            this.writer.AutoFlush = true;
                            this.writer.WriteLine(readConsoleLine);
                        }
                    }
                    catch (Exception e)
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