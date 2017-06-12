using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AdvProg2WebApp.Models;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace AdvProg2WebApp.Controllers
{
    public class SingleController : ApiController
    {
        IModel model = new Model();

        // GET api/<controller>
        public IHttpActionResult GetMaze(string name, int cols, int rows)
        {
            Maze maze = model.GenerateMaze(name, rows, cols);
            JObject jmaze = JObject.Parse(maze.ToJSON());
            MazeModel mazeModel = new MazeModel();
            mazeModel.Name = name;
            mazeModel.Cols = cols;
            mazeModel.Rows = rows;
            mazeModel.StartCol = maze.InitialPos.Col;
            mazeModel.StartRow = maze.InitialPos.Row;
            mazeModel.ExitCol = maze.GoalPos.Col;
            mazeModel.ExitRow = maze.GoalPos.Row;
            mazeModel.MazeString = jmaze.GetValue("Maze").ToString();
            return Ok(mazeModel);
        }

    }
}