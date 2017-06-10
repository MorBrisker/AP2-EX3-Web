
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MVVM.Model;
using MazeLib;

namespace MVVM.ViewModel
{
    /// <summary>
    /// Class SinglePlayerViewModel.
    /// </summary>
    /// <seealso cref="MVVM.ViewModel.ViewModel" />
    public class SinglePlayerViewModel : ViewModel
    {
        /// <summary>
        /// The rows
        /// </summary>
        private int rows, cols, algo;
        /// <summary>
        /// The name
        /// </summary>
        private string name, mazeString;
        /// <summary>
        /// The maze puzzle
        /// </summary>
        private Maze mazePuzzle;
        /// <summary>
        /// The m
        /// </summary>
        private SinglePlayerModel m;

        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerViewModel"/> class.
        /// </summary>
        public SinglePlayerViewModel()
        {
            MazeCols = MVVM.Properties.Settings.Default.MazeCols;
            MazeRows = MVVM.Properties.Settings.Default.MazeRows;
            Algo = MVVM.Properties.Settings.Default.SearchAlgorithm;
            this.m = new SinglePlayerModel();
        }

        /// <summary>
        /// Gets or sets the maze rows.
        /// </summary>
        /// <value>The maze rows.</value>
        public int MazeRows {
            get { return this.rows; }
            set {
                this.rows = value;
                NotifyPropertyChanged("MazeRows");
            }
        }
        /// <summary>
        /// Gets or sets the maze cols.
        /// </summary>
        /// <value>The maze cols.</value>
        public int MazeCols
        {
            get { return this.cols; }
            set {
                this.cols = value;
                NotifyPropertyChanged("MazeCols");
            }
        }

        /// <summary>
        /// Gets or sets the name of the maze.
        /// </summary>
        /// <value>The name of the maze.</value>
        public string MazeName
        {
            get { return this.name; }
            set {
                this.name = value;
                NotifyPropertyChanged("MazeName");
            }
        }

        /// <summary>
        /// Gets or sets the maze puzzle.
        /// </summary>
        /// <value>The maze puzzle.</value>
        public Maze MazePuzzle
        {
            get { return this.mazePuzzle; }
            set
            {
                this.mazePuzzle = value;
                NotifyPropertyChanged("MazePuzzle");
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
                NotifyPropertyChanged("MazeString");
               
            }
        }

        /// <summary>
        /// Gets or sets the algo.
        /// </summary>
        /// <value>The algo.</value>
        public int Algo
        {
            get { return this.algo; }
            set
            {
                this.algo = value;
                //NotifyPropertyChanged("SearchAlgorithm");
            }
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            string command = "generate " + name + " " + rows + " " + cols;
            MazeString = m.StartGame(command);
            MazePuzzle = Maze.FromJSON(MazeString);
            NotifyPropertyChanged("MazeString");
            NotifyPropertyChanged("MazePuzzle");
        }
        /// <summary>
        /// Solves the maze.
        /// </summary>
        /// <returns>System.String.</returns>
        public string SolveMaze()
        {
            string command = "solve " + name + " " + Algo;
            return m.StartGame(command);
         }
    }
}
