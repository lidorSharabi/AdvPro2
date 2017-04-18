using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Client
{
    class Client
    {
        IPEndPoint ep;
        TcpClient client;
        NetworkStream stream;
        StreamReader reader;
        StreamWriter writer;
        bool isConnected;


        public Client()
        {
            this.ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            //this.client = new TcpClient();
            //client.Connect(ep);
            Console.WriteLine("You are connected");
            isConnected = false; ;
        }

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
                                List<string> result= new List<string>(); 
                                while (!reader.EndOfStream)
                                {
                                    string line = reader.ReadLine();
                                    Console.WriteLine(line);
                                    result.Add(line);
                                    string empty = "{}";
                                    if (string.Compare(line, empty) == 0)
                                        break;
                                }

                            });
                            recv.Start();
                            Task.WaitAll(recv);
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
