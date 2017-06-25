
using System;
using System.Net.Sockets;
using MazeLib;
namespace WebApi.Models
{
    /// <summary>
    /// Class Game.
    /// </summary>
    public class Game
	{
        /// <summary>
        /// The name
        /// </summary>
        private string name;
        /// <summary>
        /// The maze
        /// </summary>
        private Maze maze;
        /// <summary>
        /// The is available
        /// </summary>
        private bool isAvailable;
        /// <summary>
        /// The home
        /// </summary>
        private string home;
        /// <summary>
        /// The away
        /// </summary>
        private string away;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="maze">The maze.</param>
        public Game(string name, Maze maze)
		{
			this.Name = name;
			this.Maze = maze;
			this.IsAvailable = true;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get => name; set => name = value; }
        /// <summary>
        /// Gets or sets the maze.
        /// </summary>
        /// <value>The maze.</value>
        public Maze Maze { get => maze; set => maze = value; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is available.
        /// </summary>
        /// <value><c>true</c> if this instance is available; otherwise, <c>false</c>.</value>
        public bool IsAvailable { get => isAvailable; set => isAvailable = value; }
        /// <summary>
        /// Gets the home.
        /// </summary>
        /// <returns>TcpClient.</returns>
        public string GetHome()
        {
            return this.home;
        }
        /// <summary>
        /// Sets the home.
        /// </summary>
        /// <param name="home">The home.</param>
        public void SetHome(string home)
        {
            this.home = home;
        }
        /// <summary>
        /// Gets the away.
        /// </summary>
        /// <returns>TcpClient.</returns>
        public string GetAway()
        {
            return this.away;
        }
        /// <summary>
        /// Sets the away.
        /// </summary>
        /// <param name="away">The away.</param>
        public void SetAway(string away)
        {
            this.away = away;
        }
    }
}
