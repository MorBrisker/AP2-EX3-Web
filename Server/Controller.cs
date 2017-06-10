
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// Class Controller.
    /// </summary>
    /// <seealso cref="Server.IController" />
    public class Controller : IController
	{

        /// <summary>
        /// The commands
        /// </summary>
        private Dictionary<string, ICommand> commands;
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;
        /// <summary>
        /// The client handler
        /// </summary>
        private IClientHandler clientHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        public Controller()
		{
			commands = new Dictionary<string, ICommand>();
		}

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="commandLine">The command line.</param>
        /// <param name="client">The client.</param>
        /// <returns>System.String.</returns>
        public string ExecuteCommand(string commandLine, TcpClient client)
		{
			string[] arr = commandLine.Split(' ');
			string commandKey = arr[0];
			if (!commands.ContainsKey(commandKey))
				return "Command not found";
			ICommand command = commands[commandKey];
			return command.Execute(arr, client);
		}

        /// <summary>
        /// Sets the model.
        /// </summary>
        /// <param name="m">The m.</param>
        public void SetModel(IModel m)
		{
			this.model = m;
		}

        /// <summary>
        /// Sets the client handler.
        /// </summary>
        /// <param name="ch">The ch.</param>
        public void SetClientHandler(IClientHandler ch)
		{
			this.clientHandler = ch;
		}

        /// <summary>
        /// Adds the commands.
        /// </summary>
        public void AddCommands() {
			commands.Add("generate", new GenerateMazeCommand(model));
			commands.Add("solve", new SolveMazeCommand(model));
			commands.Add("list", new ListGamesCommand(model));
			commands.Add("start", new StartGameCommand(model));
			commands.Add("join", new JoinGameCommand(model));
            commands.Add("play", new PlayGameCommand(model));
            commands.Add("close", new CloseGameCommand(model));
        }
    }
}