
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// Interface IController
    /// </summary>
    public interface IController
	{
        /// <summary>
        /// Sets the model.
        /// </summary>
        /// <param name="m">The m.</param>
        void SetModel(IModel m);
        /// <summary>
        /// Sets the client handler.
        /// </summary>
        /// <param name="ch">The ch.</param>
        void SetClientHandler(IClientHandler ch);
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="commandLine">The command line.</param>
        /// <param name="client">The client.</param>
        /// <returns>System.String.</returns>
        string ExecuteCommand(string commandLine, TcpClient client);
        /// <summary>
        /// Adds the commands.
        /// </summary>
        void AddCommands();
	}
}
