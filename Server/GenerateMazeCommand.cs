
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using MazeLib;
using MazeGeneratorLib;

namespace Server
{
    /// <summary>
    /// Class GenerateMazeCommand.
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    public class GenerateMazeCommand : ICommand
	{
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateMazeCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public GenerateMazeCommand(IModel model)
		{
			this.model = model;
		}
        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns>System.String.</returns>
        public string Execute(string[] args, TcpClient client)
		{
			string name = args[1];
			int rows = int.Parse(args[2]);
			int cols = int.Parse(args[3]);
			Maze maze = model.GenerateMaze(name, rows, cols);
			if (maze == null) {
				return "maze already exists";
			}
			return maze.ToJSON();
		}
	}
}