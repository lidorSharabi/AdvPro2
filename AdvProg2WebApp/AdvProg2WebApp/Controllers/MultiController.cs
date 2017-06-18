using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AdvProg2WebApp.Models;
using MazeLib;

namespace AdvProg2WebApp.Controllers
{
    /// <summary>
    /// multiplayer controller
    /// </summary>
    public class MultiController : ApiController
    {
        /// <summary>
        /// the model of the controller
        /// </summary>
        IModel model = new Model();

        /// <summary>
        /// getting the list of the games to join
        /// </summary>
        /// <returns></returns>
        public string[] GetList()
        {
            string[] games = model.MazeList();
            return games;
        }

    }
}