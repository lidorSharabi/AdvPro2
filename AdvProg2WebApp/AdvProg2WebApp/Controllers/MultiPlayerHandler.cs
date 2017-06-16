using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace AdvProg2WebApp.Models
{
    public class MultiPlayerHandler : Hub
    {
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients
            Clients.All.broadcastMessage(name, message);
        }
    }
}