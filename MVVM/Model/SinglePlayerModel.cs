
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Model
{
    /// <summary>
    /// Class SinglePlayerModel.
    /// </summary>
    public class SinglePlayerModel
    {
        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>System.String.</returns>
        public string StartGame(string command)
        {
            string r;
            string ip = MVVM.Properties.Settings.Default.ServerIP;
            int port = MVVM.Properties.Settings.Default.ServerPort;
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            TcpClient client = new TcpClient();
            client.Connect(ep);
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(command);
                r = reader.ReadString();
            }
            client.Close();
            return r;
        }
    }
}
