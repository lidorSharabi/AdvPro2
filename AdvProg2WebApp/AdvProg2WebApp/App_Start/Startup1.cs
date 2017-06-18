using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AdvProg2WebApp.App_Start.Startup1))]

namespace AdvProg2WebApp.App_Start
{
    /// <summary>
    /// class for the signalR mapping
    /// </summary>
    public class Startup1
    {
        /// <summary>
        /// configuration for the signalR
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            
        }
    }
}
