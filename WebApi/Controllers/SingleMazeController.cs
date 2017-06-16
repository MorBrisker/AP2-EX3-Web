using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;
using Newtonsoft.Json.Linq;
using MazeLib;

namespace WebApi.Controllers
{
    public class SingleMazeController : ApiController
    {
        public static SingleMazeModel m = new SingleMazeModel();

        // GET: api/SingleMaze
        [HttpGet]
        [Route("api/SingleMaze/{name}/{rows}/{cols}")]
        public string Generate(string name, int rows, int cols)
        {
            Maze maze = m.GenerateMaze(name, rows, cols);
            //JObject obj = JObject.Parse(maze.ToJSON());
            return maze.ToJSON();
        }

        /*[HttpGet]
        [Route("api/GenerateMaze/{name}/{rows}/{cols}")]
        public JObject GenerateMaze(string name, int rows, int cols)
        {
            Maze maze = Maze.FromJSON(m.GenerateMaze(name, rows, cols));
            JObject obj = JObject.Parse(maze.ToJSON());
            return obj;
        }*/

        // GET: api/SingleMaze/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SingleMaze
        public string Post([FromBody]string value)
        {
            return value;
        }

        // PUT: api/SingleMaze/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SingleMaze/5
        public void Delete(int id)
        {
        }
    }
}
