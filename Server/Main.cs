
using System;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using MazeLib;
using MazeGeneratorLib;
using SearchAlgorithmsLib;
using System.Configuration;

namespace Server
{
    /// <summary>
    /// Class MainClass.
    /// </summary>
    class MainClass
	{
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
		{
			IController c = new Controller();
			IModel m = new Model(c);
			c.SetModel(m);
			c.AddCommands();
			IClientHandler ch = new ClientHandler(c);
            int port = int.Parse(ConfigurationManager.AppSettings["PORT"]);
			Server server = new Server(port, ch);
			server.Start();
		}
	}
}
