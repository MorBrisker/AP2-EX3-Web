
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;

namespace Server
{
    /// <summary>
    /// Class SolveMazeCommand.
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    class SolveMazeCommand : ICommand
	{
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="SolveMazeCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SolveMazeCommand(IModel model)
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
			int alg = int.Parse(args[2]);
			MazeSolution ms = model.SolveMaze(name, alg);
			if (ms == null)
			{
				return "maze does not exist";
			}
			return ms.ToJSON();
		}

	}
}