
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;
using Newtonsoft.Json.Linq;
using System.Net.Sockets;
using System.Threading;

namespace Server
{

    /// <summary>
    /// Class Model.
    /// </summary>
    /// <seealso cref="Server.IModel" />
    public class Model : IModel
	{
        /// <summary>
        /// The controller
        /// </summary>
        private IController controller;
        /// <summary>
        /// The maze cache
        /// </summary>
        private Dictionary<String, Maze> mazeCache;
        /// <summary>
        /// The maze solution cache
        /// </summary>
        private Dictionary<String, MazeSolution> mazeSolutionCache;
        /// <summary>
        /// The games
        /// </summary>
        private List<Game> games;

        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        /// <param name="c">The c.</param>
        public Model(IController c)
		{
			this.controller = c;
			this.mazeCache = new Dictionary<string, Maze>();
			this.mazeSolutionCache = new Dictionary<string, MazeSolution>();
			this.games = new List<Game>();
		}

        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns>Maze.</returns>
        public Maze GenerateMaze(string name, int rows, int cols)
		{
			IMazeGenerator mg = new DFSMazeGenerator();
			Maze m = mg.Generate(rows, cols);
			m.Name = name;
			if (this.mazeCache.ContainsKey(name))
			{
				return null;
			}
			this.mazeCache.Add(m.Name, m);
			return m;
		}

        /// <summary>
        /// Solves the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="alg">The alg.</param>
        /// <returns>MazeSolution.</returns>
        public MazeSolution SolveMaze(string name, int alg)
		{
			Solution<Position> sol;
			int nodesEvaluated;

			if (this.mazeSolutionCache.ContainsKey(name))
			{
				return this.mazeSolutionCache[name];
			}

			if (this.mazeCache.ContainsKey(name))
			{
				Maze m = this.mazeCache[name];
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
				this.mazeSolutionCache.Add(name, ms);
				return ms;
			}
			else
			{
				return null;
			}
		}

        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="client">The client.</param>
        /// <returns>Maze.</returns>
        public Maze StartGame(string name, int rows, int cols, TcpClient client) {
			IMazeGenerator mg = new DFSMazeGenerator();
			Maze m = mg.Generate(rows, cols);
			m.Name = name;
			Game g = new Game(name, m);
            g.SetHome(client);
			this.games.Add(g);
			while (g.IsAvailable) {
                Thread.Sleep(100);
            }
			return m;
		}
        /// <summary>
        /// Joins the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="client">The client.</param>
        /// <returns>Maze.</returns>
        public Maze JoinGame(string name, TcpClient client) {
			Game game = null;
			foreach (Game g in this.games) {
				if (g.Name == name) {
					game = g;
				}
			}
			game.IsAvailable = false;
            game.SetAway(client);
			return game.Maze;
		}
        /// <summary>
        /// Lists the games.
        /// </summary>
        /// <returns>System.String.</returns>
        public string ListGames()
		{
            JArray gameList = new JArray();
			foreach (Game g in this.games) {
				if (g.IsAvailable) {
					gameList.Add(g.Name);
				}
			}
			return gameList.ToString();
		}

        /// <summary>
        /// Gets the game.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns>Game.</returns>
        public Game GetGame(TcpClient client)
        {
            foreach (Game g in this.games)
            {
                if (g.GetHome().Equals(client) || g.GetAway().Equals(client))
                {
                    return g;
                }
            }
            return null;
        }

        /*public Game PlayGame(TcpClient client)
        {
            TcpClient away=null;
            string name=null;
            foreach (Game g in this.games)
            {
                if (g.GetHome().Equals(client) || g.GetAway().Equals(client))
                {
                    return g;
                }
            }
            return null;
            JObject playObj = new JObject();
            playObj["Name"] = name;
            playObj["Direction"] = direction;
            MessageDetails det = new MessageDetails(away, playObj.ToString());
            return away;
        }*/

        /// <summary>
        /// Closes the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>System.String.</returns>
        public string CloseGame(string name)
        {
            TcpClient away;
            foreach (Game g in this.games)
            {
                if (g.Name.Equals(name))
                {
                    // TODO: send empty message to away
                    away = g.GetAway();
                    //away.
                }
            }
            return null;
        }
    }
}