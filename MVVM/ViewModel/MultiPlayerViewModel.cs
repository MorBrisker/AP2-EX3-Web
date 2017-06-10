
using MazeLib;
using MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM.ViewModel
{
    /// <summary>
    /// Class MultiPlayerViewModel.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class MultiPlayerViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The m
        /// </summary>
        private MultiPlayerModel m;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerViewModel"/> class.
        /// </summary>
        public MultiPlayerViewModel()
        {
            this.m = new MultiPlayerModel();
            this.m.PropertyChanged += M_PropertyChanged;
            m.MazeCols = MVVM.Properties.Settings.Default.MazeCols;
            m.MazeRows = MVVM.Properties.Settings.Default.MazeRows;
        }

        /// <summary>
        /// Handles the PropertyChanged event of the M control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void M_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(e.PropertyName));
        }


        /// <summary>
        /// Gets or sets the maze rows.
        /// </summary>
        /// <value>The maze rows.</value>
        public int MazeRows
        {
            get { return m.MazeRows; }
            set
            {
                m.MazeRows = value;
            }
        }
        /// <summary>
        /// Gets or sets the maze cols.
        /// </summary>
        /// <value>The maze cols.</value>
        public int MazeCols
        {
            get { return m.MazeCols; }
            set
            {
                m.MazeCols = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the maze.
        /// </summary>
        /// <value>The name of the maze.</value>
        public string MazeName
        {
            get { return m.MazeName; }
            set
            {
                m.MazeName = value;
            }
        }

        /// <summary>
        /// Gets or sets the maze string.
        /// </summary>
        /// <value>The maze string.</value>
        public string MazeString
        {
            get { return m.MazeString; }
            set
            {
                m.MazeString = value;
            }
        }

        /// <summary>
        /// Gets or sets the opp dir.
        /// </summary>
        /// <value>The opp dir.</value>
        public string OppDir
        {
            get { return m.OppDir; }
            set
            {
                m.OppDir = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is closed.
        /// </summary>
        /// <value><c>true</c> if this instance is closed; otherwise, <c>false</c>.</value>
        public bool IsClosed
        {
            get { return m.IsClosed; }
            set
            {
                m.IsClosed = value;
            }
        }


        /// <summary>
        /// Gets or sets the game list.
        /// </summary>
        /// <value>The game list.</value>
        public List<string> GameList
        {
            get { return m.GameList; }
            set
            {
                m.GameList = value;
            }
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            string command = "start " + MazeName + " " + MazeRows + " " + MazeCols;
            m.StartGamePlay(command);
        }
        /// <summary>
        /// Joins the specified game.
        /// </summary>
        /// <param name="game">The game.</param>
        public void Join(string game)
        {
            string command = "join " + game;
            MazeName = game;
            m.StartGamePlay(command);
        }
        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        public void Refresh()
        {
            m.Refresh();
        }
        /// <summary>
        /// Plays the specified e.
        /// </summary>
        /// <param name="e">The e.</param>
        public void Play(Key e)
        {
            string dir = null;
            if (e == Key.Left)
            {
                dir = "left";
            } else if (e == Key.Right)
            {
                dir = "right";
            } else if (e == Key.Up)
            {
                dir = "up";
            } else if (e == Key.Down)
            {
                dir = "down";
            }
            string s = "play " + dir;
            m.Play(s);
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            m.Close();
        }
    }
}
