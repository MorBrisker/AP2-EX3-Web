
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
    /// Class ClientHandler.
    /// </summary>
    /// <seealso cref="Server.IClientHandler" />
    public class ClientHandler : IClientHandler
	{
        /// <summary>
        /// The controller
        /// </summary>
        private IController controller;
        /// <summary>
        /// The non continuous commands
        /// </summary>
        private List<string> NonContinuousCommands;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientHandler"/> class.
        /// </summary>
        /// <param name="c">The c.</param>
        public ClientHandler(IController c)
		{
			this.controller = c;
            NonContinuousCommands = new List<string>();
            NonContinuousCommands.Add("genearate");
            NonContinuousCommands.Add("solve");
            NonContinuousCommands.Add("list");
		}
        /// <summary>
        /// handles the client
        /// </summary>
        /// <param name="client">the client to handle</param>
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
                      if (commandLine.Contains("generate") || commandLine.Contains("solve") ||
                      commandLine.Contains("list"))
                      {
                          NonContinuousCommand(commandLine, client, writer);
                      }
                      else
                      {
                          ContiniousCommand(commandLine, client, writer, reader);
                      }
                  }
		      }).Start();
		}

        /// <summary>
        /// Continiouses the command.
        /// </summary>
        /// <param name="commandLine">The command line.</param>
        /// <param name="client">The client.</param>
        /// <param name="writer">The writer.</param>
        /// <param name="reader">The reader.</param>
        public void ContiniousCommand(string commandLine, TcpClient client, BinaryWriter writer, BinaryReader reader)
        {
            string result;
            while (true)
            {
                result = controller.ExecuteCommand(commandLine, client);
                //avoiding writing the stream to the client itself if the commands are play/close
                if (!(commandLine.Contains("play") || commandLine.Contains("lose")))
                {
                    writer.Flush();
                    writer.Write(result);
                    writer.Flush();
                }
                //if a "close" command was accepted from one of the clients- close the socket
                //dont need to write to the other client- happens in the command itself
                //dont need to read another command from the client- the reading has to stop like in noncontinous commands
                if (result.Contains("lose") )
                {
                    writer.Flush();
                    writer.Write(result);
                    writer.Flush();
                    client.Close();
                    return;
                }
                if (commandLine.Contains("lose"))
                {
                    client.Close();
                    return;
                }

                try
                {
                    commandLine = reader.ReadString();
                } catch (EndOfStreamException)
                {
                    return;
                }

                Console.WriteLine("Got command: {0}", commandLine);
            }
        }

        /// <summary>
        /// Nons the continuous command.
        /// </summary>
        /// <param name="commandLine">The command line.</param>
        /// <param name="client">The client.</param>
        /// <param name="writer">The writer.</param>
        public void NonContinuousCommand(string commandLine, TcpClient client, BinaryWriter writer)
        {
            string result = controller.ExecuteCommand(commandLine, client);
            if(!commandLine.Contains("lose"))
            {
                writer.Flush();
                writer.Write(result);
                writer.Flush();
            }
            client.Close();
        }
    }
}