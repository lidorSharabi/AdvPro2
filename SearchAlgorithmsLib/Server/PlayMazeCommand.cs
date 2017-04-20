using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class PlayMazeCommand : ICommand
    {
        private IModel model;
        public PlayMazeCommand(IModel model)
        {
            this.model = model;
        }
        public string Execute(string[] args, TcpClient client, Controller control)
        {

            //string name;
            string move;
            try
            {
                //name = args[1];
                move = args[0];
                if (!(move.Equals("up") || move.Equals("down") || move.Equals("left") || move.Equals("right")))
                {
                    return "Error in parameter for play command maze";
                }
                //name = args[1];
            }
            catch (Exception)
            {
                return "Error in parameter for play command maze";
            }
            return model.PlayMove(move, client);           
        }
    }
}
