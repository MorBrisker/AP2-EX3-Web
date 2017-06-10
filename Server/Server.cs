
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Configuration;

namespace Server
{
    /// <summary>
    /// Class Server.
    /// </summary>
    public class Server
	{
        /// <summary>
        /// The port
        /// </summary>
        private int port;
        /// <summary>
        /// The listener
        /// </summary>
        private TcpListener listener;
        /// <summary>
        /// The ch
        /// </summary>
        private IClientHandler ch;
        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <param name="ch">The ch.</param>
        public Server(int port, IClientHandler ch)
		{
			this.port = port;
			this.ch = ch;
		}
        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
		{
            string ip = ConfigurationManager.AppSettings["IP"];
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
			listener = new TcpListener(ep);
			listener.Start();
			Console.WriteLine("Waiting for connections...");
			Task task = new Task(() =>
			{
				while (true)
				{
					try
					{
						TcpClient client = listener.AcceptTcpClient();
						Console.WriteLine("Got new connection");
						ch.HandleClient(client);
					}
					catch (SocketException)
					{
						break;
					}
				}
				Console.WriteLine("Server stopped");
			});
			task.Start();
			task.Wait();
		}
        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
		{
			listener.Stop();
		}
	}
}
