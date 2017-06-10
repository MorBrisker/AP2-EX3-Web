
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace Server
{
    /// <summary>
    /// Class GameHandler.
    /// </summary>
    /// <seealso cref="Server.IClientHandler" />
    public class GameHandler : IClientHandler
	{
        /// <summary>
        /// The controller
        /// </summary>
        private IController controller;

        /// <summary>
        /// Handles the client.
        /// </summary>
        /// <param name="client">The client.</param>
        public void HandleClient(TcpClient client)
		{
			new Task(() =>
			  {
				  using (NetworkStream stream = client.GetStream())
				  using (BinaryReader reader = new BinaryReader(stream))
				  using (BinaryWriter writer = new BinaryWriter(stream))
				  {
					  string commandLine = reader.ReadString();
					  Console.WriteLine("Got command: {0}", commandLine);
					  string result = controller.ExecuteCommand(commandLine, client);
					  writer.Write(result);
				  }
				  client.Close();
			  }).Start();
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="GameHandler"/> class.
        /// </summary>
        /// <param name="c">The c.</param>
        public GameHandler(IController c) 
		{
			this.controller = c;
		}
	}
}
