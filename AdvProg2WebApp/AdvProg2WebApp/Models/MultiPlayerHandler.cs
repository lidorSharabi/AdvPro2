using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace AdvProg2WebApp.Models
{
    public class MultiPlayerHandler : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}