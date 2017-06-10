
using System;
using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// Class JoinGameCommand.
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    public class JoinGameCommand : ICommand
	{
        /// <summary>
        /// The model
        /// </summary>
        IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="JoinGameCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public JoinGameCommand(IModel model) 
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
			MazeLib.Maze m = model.JoinGame(name, client);
			return m.ToJSON();
		}
	}
}
