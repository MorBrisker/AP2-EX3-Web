using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;

namespace WebApi.Models
{
    public class SingleMazeModel
    {
        private static Dictionary<String, Maze> mazeCache = new Dictionary<string, Maze>();
        private static Dictionary<String, MazeSolution> mazeSolutionCache = new Dictionary<string, MazeSolution>();

        public Maze GenerateMaze(string name, int rows, int cols)
        {
            IMazeGenerator mg = new DFSMazeGenerator();
            Maze m = mg.Generate(rows, cols);
            m.Name = name;
            if (mazeCache.ContainsKey(name))
            {

                return null;
            }
            mazeCache.Add(m.Name, m);
            return m;
        }

        public MazeSolution SolveMaze(string name, int alg)
        {
            Solution<Position> sol;
            int nodesEvaluated;

            if (mazeSolutionCache.ContainsKey(name))
            {
                return mazeSolutionCache[name];
            }

            if (mazeCache.ContainsKey(name))
            {
                Maze m = mazeCache[name];
                if (alg == 0)
                {
                    BFS<Position> bfs = new BFS<Position>();
                    ISearchable<Position> newMaze = new MazeAdapter(m);
                    sol = bfs.Search(newMaze);
                    nodesEvaluated = bfs.GetNumOfNodesEvaluated();
                }
                else
                {
                    DFS<Position> dfs = new DFS<Position>();
                    ISearchable<Position> newMaze = new MazeAdapter(m);
                    sol = dfs.Search(newMaze);
                    nodesEvaluated = dfs.GetNumOfNodesEvaluated();
                }

                State<Position>.StatePool.ClearPool();
                MazeSolution ms = new MazeSolution(sol, name, nodesEvaluated);
                ms.SolutionPath();
                mazeSolutionCache.Add(name, ms);
                return ms;
            }
            else
            {
                return null;
            }
        }
    }
}