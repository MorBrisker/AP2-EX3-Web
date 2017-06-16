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
        private static Dictionary<String, Maze> mazeCache;
        private static Dictionary<String, MazeSolution> mazeSolutionCache;

        public String GenerateMaze(string name, int rows, int cols)
        {
            IMazeGenerator mg = new DFSMazeGenerator();
            Maze m = mg.Generate(rows, cols);
            m.Name = name;
            if (mazeCache.ContainsKey(name))
            {
                return null;
            }
            mazeCache.Add(m.Name, m);
            return m.ToJSON().ToString();
        }
    }
}