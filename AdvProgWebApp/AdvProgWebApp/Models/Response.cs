using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvProgWebApp.Models
{
    public class Response
    {
        public string message = String.Empty;
        public bool close = true;
        public bool error = false;

        public Response(string message, bool close, bool error)
        {
            this.close = close;
            this.error = error;
            this.message = message;
        }
        
    }
}
