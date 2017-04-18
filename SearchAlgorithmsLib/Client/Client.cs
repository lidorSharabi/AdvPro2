
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
        public void start()
        {
           
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
                // Get and print result from server
                while (!reader.EndOfStream)
                {
                    Console.WriteLine(reader.ReadLine());
                }   
                 client.Close();
            }
            client.Close();
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
    }
}
