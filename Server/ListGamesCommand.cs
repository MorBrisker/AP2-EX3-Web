
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
    /// Class ListGamesCommand.
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    public class ListGamesCommand : ICommand
	{
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListGamesCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public ListGamesCommand(IModel model)
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
			return model.ListGames();
		}
	}
}