
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Class MessageDetails.
    /// </summary>
    public class MessageDetails
    {
        /// <summary>
        /// The dest
        /// </summary>
        private TcpClient dest;
        /// <summary>
        /// The message
        /// </summary>
        private string message;
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageDetails"/> class.
        /// </summary>
        /// <param name="dest">The dest.</param>
        /// <param name="message">The message.</param>
        public MessageDetails(TcpClient dest, string message)
        {
            this.dest = dest;
            this.message = message;
        }
        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <returns>TcpClient.</returns>
        public TcpClient GetClient()
        {
            return this.dest;
        }
        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetMessage()
        {
            return this.message;
        }
    }
}
