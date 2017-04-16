
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
        private bool closeConnction = true;

        public void start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            TcpClient client = new TcpClient();
            client.Connect(ep);
            bool run = true;
            Console.WriteLine("You are connected");
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                while (run)
                {
                    Console.WriteLine("Please enter operation:");
                    // Send operation to server
                    //string readLine = Console.ReadLine();
                    //shouldCloseConnection(readLine);
                    writer.Write("generate sss 1 2");
                    // Get and print result from server
                    string result = reader.ReadString();
                    Console.WriteLine("Result = {0}", result);
                    if (closeConnction)
                    {
                        client.Close();
                    }
                }
            }
            client.Close();
        }

        private void shouldCloseConnection(string readLine)
        {
            string[] temp = readLine.Split(' ');
            string commandKey = temp[0];
            if (commandKey.Equals("close"))
            {
                closeConnction = true;
            }
            else if (commandKey.Equals("start") || commandKey.Equals("join"))
            {
                closeConnction = false;
            }

        }

        public void HandleServer(TcpClient client)
        {
            Task[] handleServerTasks = new Task[2];
            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {

                    string commandLine = reader.ReadLine();
                    Console.WriteLine(commandLine);
                    Console.WriteLine("Got command: {0}", commandLine);
                    
                }
                client.Close();
            }).Start();

            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream))
                using (StreamWriter writer = new StreamWriter(stream))
                {

                    string commandLine = reader.ReadLine();
                    Console.WriteLine(commandLine);
                    Console.WriteLine("Got command: {0}", commandLine);

                }
                client.Close();
            }).Start();
        }
    }
}
