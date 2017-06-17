using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MazeGeneratorLib;
using MazeLib;

namespace WebApi.Models
{
    public class SingleMazeModel
    {
        private static Dictionary<String, Maze> mazeCache = new Dictionary<string, Maze>();
        private static Dictionary<String, MazeSolution> mazeSolutionCache = new Dictionary<string, MazeSolution>();

        public string GenerateMaze(string name, int rows, int cols)
        {
            IMazeGenerator mg = new DFSMazeGenerator();
            Maze m = mg.Generate(rows, cols);
            m.Name = name;
            if (mazeCache.ContainsKey(name))
            {

                return null;
            }
            mazeCache.Add(m.Name, m);
            return m.ToJSON();
        }
    }
}