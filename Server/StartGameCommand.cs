
using System;
using System.Net.Sockets;
using MazeLib;

namespace Server
{
    /// <summary>
    /// Class StartGameCommand.
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    public class StartGameCommand : ICommand
	{
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartGameCommand"/> class.
        /// </summary>
        /// <param name="m">The m.</param>
        public StartGameCommand(IModel m)
		{
			this.model = m;
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
			Maze m = model.StartGame(name, rows, cols, client);
			return m.ToJSON();
		}
	}
}
