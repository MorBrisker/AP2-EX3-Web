
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Class PlayGameCommand.
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    class PlayGameCommand : ICommand
    {
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;
        /// <summary>
        /// The i
        /// </summary>
        private IClientHandler i;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayGameCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public PlayGameCommand(IModel model)
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
            Game game = model.GetGame(client);
            TcpClient dest;
            if (game.GetHome().Equals(client))
            {
                dest = game.GetAway();
            }
            else
            {
                dest = game.GetHome();
            }
            string name = game.Maze.Name;
            JObject playObj = new JObject();
            playObj["Name"] = name;
            playObj["Direction"] = args[1];
            NetworkStream stream = dest.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Flush();
            writer.Write(playObj.ToString());
            writer.Flush();
            return playObj.ToString();
        }
    }
}
