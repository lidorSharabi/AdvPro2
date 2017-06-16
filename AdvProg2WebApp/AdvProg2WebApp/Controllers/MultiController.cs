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
    public class MultiController : ApiController
    {
        IModel model = new Model();

        // GET api/<controller>
        public string[] GetList()
        {
            string[] games = model.MazeList();
            return games;
        }

    }
}