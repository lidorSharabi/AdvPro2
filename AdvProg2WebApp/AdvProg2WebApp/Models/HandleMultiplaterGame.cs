using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;

namespace AdvProg2WebApp.Models
{
    class HandleMultiplaterGame
    {
        public TcpClient host { get; set; }
        public TcpClient guest { get; set; }
        public string gameToJason { get; set; }
        public string name { get; set; }
        private bool run = true;

        public HandleMultiplaterGame(TcpClient host)
        {
            this.host = host;
        }

        
        public void sendMazeToJsonToHost()
        {
            NetworkStream stream = host.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(gameToJason);
        }

        public void close()
        {
            run = false;
            host.Close();
            guest.Close();
        }


        public void sendMessageToClient(TcpClient client)
        {
            string[] palyMoveFormat = new string[2];
            palyMoveFormat[0] = "''Name'':''mygame'',";
            palyMoveFormat[1] = "''Direction'': ''right''";
            NetworkStream stream;
            StreamWriter writer;
            try
            {
                if (client == host)
                {
                    stream = guest.GetStream();
                    writer = new StreamWriter(stream);
                }
                else if (client == guest)
                {
                    stream = host.GetStream();
                    writer = new StreamWriter(stream);
                }
                else return;
                writer.Write(JsonConvert.SerializeObject(palyMoveFormat));
            }
            catch
            {
                return;
            }
        }
    }
}
