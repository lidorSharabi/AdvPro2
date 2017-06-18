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
    /// <summary>
    /// single game controller
    /// </summary>
    public class SingleController : ApiController
    {
        IModel model = new Model();

        /// <summary>
        /// generating maze
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cols"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public IHttpActionResult GetMaze(string name, int cols, int rows)
        {
            try
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
            } catch
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// getting the solution of the maze
        /// </summary>
        /// <param name="name"></param>
        /// <param name="algo"></param>
        /// <returns></returns>
        public IHttpActionResult GetSolution(string name, int algo)
        {
            try
            {
                string solution = model.SolveMaze(name, algo);
                return Ok(solution);
            }
            catch
            {
                return InternalServerError();
            }
        }

    }
}