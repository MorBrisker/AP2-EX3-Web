
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// Interface IModel
    /// </summary>
    public interface IModel
	{
        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns>Maze.</returns>
        Maze GenerateMaze(string name, int rows, int cols);
        /// <summary>
        /// Solves the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="alg">The alg.</param>
        /// <returns>MazeSolution.</returns>
        MazeSolution SolveMaze(string name, int alg);
        /// <summary>
        /// Lists the games.
        /// </summary>
        /// <returns>System.String.</returns>
        string ListGames();
        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="client">The client.</param>
        /// <returns>Maze.</returns>
        Maze StartGame(string name, int rows, int cols, TcpClient client);
        /// <summary>
        /// Joins the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="client">The client.</param>
        /// <returns>Maze.</returns>
        Maze JoinGame(string name, TcpClient client);
        /// <summary>
        /// Gets the game.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns>Game.</returns>
        Game GetGame(TcpClient client);
        /// <summary>
        /// Closes the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>System.String.</returns>
        string CloseGame(string name);
	}
}
