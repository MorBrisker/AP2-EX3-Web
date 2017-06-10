
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
    /// Class CloseGameCommand.
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    class CloseGameCommand : ICommand
    {
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;
        /// <summary>
        /// Initializes a new instance of the <see cref="CloseGameCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public CloseGameCommand(IModel model)
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
            JObject playObj = new JObject();
            playObj["isClose"] = true;
            NetworkStream stream = dest.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Flush();
            writer.Write("closeNow");
            //writer.Write(playObj.ToString());
            writer.Flush();
            return playObj.ToString();
        }

    }
}
