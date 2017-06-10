
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using MazeLib;
using System.Threading;
using System.Configuration;

namespace MVVM.Model
{
    /// <summary>
    /// Class MultiPlayerModel.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    class MultiPlayerModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// The rows
        /// </summary>
        private int rows, cols;
        /// <summary>
        /// The game name
        /// </summary>
        private string gameName, mazeString;
        /// <summary>
        /// The game list
        /// </summary>
        private List<string> gameList;
        /// <summary>
        /// The games
        /// </summary>
        private string games;
        /// <summary>
        /// The opp direction
        /// </summary>
        private string oppDirection;
        /// <summary>
        /// The is closed
        /// </summary>
        private bool isClosed;
        /// <summary>
        /// The ep
        /// </summary>
        IPEndPoint ep;
        /// <summary>
        /// The client
        /// </summary>
        TcpClient client;
        /// <summary>
        /// The stream
        /// </summary>
        NetworkStream stream;
        /// <summary>
        /// The reader
        /// </summary>
        BinaryReader reader;
        /// <summary>
        /// The writer
        /// </summary>
        BinaryWriter writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerModel"/> class.
        /// </summary>
        public MultiPlayerModel()
        {
           try
            {
                this.Refresh();
            } catch (Exception) { }
            
        }
        /// <summary>
        /// Gets or sets the maze rows.
        /// </summary>
        /// <value>The maze rows.</value>
        public int MazeRows
        {
            get { return this.rows; }
            set
            {
                if (rows != value)
                {
                    this.rows = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MazeRows"));
                }
            }
        }

        /// <summary>
        /// Gets or sets the maze cols.
        /// </summary>
        /// <value>The maze cols.</value>
        public int MazeCols
        {
            get { return this.cols; }
            set
            {
                if (cols != value)
                {
                    this.cols = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MazeCols"));
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the maze.
        /// </summary>
        /// <value>The name of the maze.</value>
        public string MazeName
        {
            get { return this.gameName; }
            set
            {
                if (gameName != value)
                {
                    this.gameName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MazeName"));
                }
            }
        }

        /// <summary>
        /// Gets or sets the maze string.
        /// </summary>
        /// <value>The maze string.</value>
        public string MazeString
        {
            get { return this.mazeString; }
            set
            {
                this.mazeString = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MazeString"));
            }
        }

        /// <summary>
        /// Gets or sets the opp dir.
        /// </summary>
        /// <value>The opp dir.</value>
        public string OppDir
        {
            get { return this.oppDirection; }
            set
            {
                this.oppDirection = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OppDir"));
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is closed.
        /// </summary>
        /// <value><c>true</c> if this instance is closed; otherwise, <c>false</c>.</value>
        public bool IsClosed
        {
            get { return this.isClosed; }
            set
            {
                this.isClosed = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsClosed"));
            }
        }

        /// <summary>
        /// Gets or sets the game list.
        /// </summary>
        /// <value>The game list.</value>
        public List<string> GameList
        {
            get { return this.gameList; }
            set
            {
                if (gameList != value)
                {
                    this.gameList = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GameList"));
                }
            }
        }

        /// <summary>
        /// Froms the string to list.
        /// </summary>
        /// <param name="jarr">The jarr.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> FromStringToList(JArray jarr)
        {
            List<string> l = new List<string>();
            foreach (string s in jarr)
            {
                l.Add(s);
            }
            return l;
        }
        /// <summary>
        /// Starts the game play.
        /// </summary>
        /// <param name="command">The command.</param>
        public void StartGamePlay(string command)
        {
            string ip = MVVM.Properties.Settings.Default.ServerIP;
            int port = MVVM.Properties.Settings.Default.ServerPort;
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
            client = new TcpClient();
            client.Connect(ep);
            stream = client.GetStream();
            reader = new BinaryReader(stream);
            writer = new BinaryWriter(stream);
            writer.Write(command);
            MazeString = reader.ReadString();

            Task reciever = new Task(() =>
            {
                string result;

                while (true)
                {
                    result = reader.ReadString();

                    if (result.Contains("closeNow"))
                    {
                        IsClosed = true;
                        client.Close();
                        break;
                    } else if (result.Contains("isClose"))
                    {
                        client.Close();
                        break;
                    }

                    JObject j = JObject.Parse(result);
                    OppDir = (string)j["Direction"];
                    Thread.Sleep(100);
                    OppDir = null;
                }
            }); reciever.Start();
        }


        /// <summary>
        /// Lists the of games.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>System.String.</returns>
        public string ListOfGames(string command)
        {
            string r;
            string ip = MVVM.Properties.Settings.Default.ServerIP;
            int port = MVVM.Properties.Settings.Default.ServerPort;
            IPEndPoint ep2 = new IPEndPoint(IPAddress.Parse(ip), port);
            TcpClient client2 = new TcpClient();
            client2.Connect(ep2);
            using (NetworkStream stream2 = client2.GetStream())
            using (BinaryReader reader2 = new BinaryReader(stream2))
            using (BinaryWriter writer2 = new BinaryWriter(stream2))
            {
                writer2.Write(command);
                r = reader2.ReadString();
            }
            client2.Close();
            return r;
        }
        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        public void Refresh()
        {
            games = ListOfGames("list");
            JArray jgames = JArray.Parse(games);
            GameList = FromStringToList(jgames);
        }

        /// <summary>
        /// Plays the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        public void Play(string command)
        {
            writer.Write(command);
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            string s = "close " + MazeName;
            writer.Write(s);
            //client.Close();
        }
    }
}

