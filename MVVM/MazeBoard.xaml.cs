// ********

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MazeLib;
using System.Threading;

namespace MVVM
{
    /// <summary>
    /// Interaction logic for Maze.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MazeBoard : UserControl
    {
        /// <summary>
        /// The current position
        /// </summary>
        private Position currentPos;
        /// <summary>
        /// The rectangles arr
        /// </summary>
        private Rectangle[,] rectanglesArr;
        /// <summary>
        /// The m
        /// </summary>
        private Maze m;
        /// <summary>
        /// The record height
        /// </summary>
        private int recHeight, recWidth;
        /// <summary>
        /// The character arr
        /// </summary>
        private string charArr;
        /// <summary>
        /// My image
        /// </summary>
        private Image start;
        private Image end;

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeBoard"/> class.
        /// </summary>
        public MazeBoard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Loaded event of the UserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        public void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //MString = null;
            CreateMaze();
        }

        /// <summary>
        /// Gets or sets the m string.
        /// </summary>
        /// <value>The m string.</value>
        public string MString
        {
            get { return (string)GetValue(MStringProperty); }
            set { SetValue(MStringProperty, value); }
        }
        /// <summary>
        /// The m string property
        /// </summary>
        public static readonly DependencyProperty MStringProperty = DependencyProperty.Register("MString", typeof(string), typeof(MazeBoard));

        /// <summary>
        /// Gets or sets a value indicating whether this instance is closed.
        /// </summary>
        /// <value><c>true</c> if this instance is closed; otherwise, <c>false</c>.</value>
        public bool IsClosed
        {
            get { return (bool)GetValue(IsClosedProperty); }
            set
            {
                SetValue(IsClosedProperty, value);
            }
        }
        /// <summary>
        /// The is closed property
        /// </summary>
        public static readonly DependencyProperty IsClosedProperty = DependencyProperty.Register("IsClosed", typeof(bool), typeof(MazeBoard), new PropertyMetadata(closeMessage));

        /// <summary>
        /// Closes the message.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void closeMessage(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MessageBox.Show("Your opponent quit the game", "You Are Too Good");
        }

        /// <summary>
        /// Gets or sets the opp direction.
        /// </summary>
        /// <value>The opp direction.</value>
        public string OppDirection
        {
            get { return (string)GetValue(OppDirectionProperty); }
            set {
                SetValue(OppDirectionProperty, value);
            }
        }
        /// <summary>
        /// The opp direction property
        /// </summary>
        public static readonly DependencyProperty OppDirectionProperty = DependencyProperty.Register("OppDirection", typeof(string), typeof(MazeBoard), new PropertyMetadata(moveOpp));

        /// <summary>
        /// Moves the opp.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void moveOpp(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeBoard mb = (MazeBoard)d;
            Key k = Key.Select;
            if (mb.OppDirection == null)
            {
                return;
            }
            if (mb.OppDirection == "left")
            {
                k = Key.Left;
            }
            else if (mb.OppDirection == "right")
            {
                k = Key.Right;
            }
            else if (mb.OppDirection == "up")
            {
                k = Key.Up;
            }
            else if (mb.OppDirection == "down")
            {
                k = Key.Down;
            }

            mb.mazeCanvas_KeyDown(mb, k);
        }


        /// <summary>
        /// Calculates the current position.
        /// </summary>
        /// <returns>Position.</returns>
        public Position CalcCurrentPos()
        {
            return new Position();
        }

        /// <summary>
        /// Creates the maze.
        /// </summary>
        public void CreateMaze()
        {
            while (MString == null)
            {
                Thread.Sleep(100);
            }
            this.m = Maze.FromJSON(MString);
            if (m.Rows == 0 || m.Cols == 0 || m.Name == null)
            {
                return;
            }

            this.recHeight = (int)mazeCanvas.Height / m.Rows;
            this.recWidth = (int)mazeCanvas.Width / m.Cols;
            this.rectanglesArr = new Rectangle[m.Rows, m.Cols];
            this.charArr = m.ToString();
            this.currentPos = m.InitialPos;
            DrawMaze();
        }
        /// <summary>
        /// Draws the maze.
        /// </summary>
        public void DrawMaze()
        {
            int counter = 0;
            for (int i = 0; i < m.Rows; i++)
            {
                for (int j = 0; j < m.Cols; j++)
                {
                    Rectangle rect = new Rectangle();
                    rect.Height = recHeight;
                    rect.Width = recWidth;
                    this.rectanglesArr[i, j] = rect;
                    if (this.charArr[counter] == '1')
                    {
                        this.rectanglesArr[i, j].Fill = new SolidColorBrush(System.Windows.Media.Colors.Black);
                    }
                    else if (this.charArr[counter] == '0')
                    {
                        this.rectanglesArr[i, j].Fill = new SolidColorBrush(System.Windows.Media.Colors.Transparent);
                    }
                    else if (this.charArr[counter] == '*')
                    {
                        //this.rectanglesArr[i, j].Fill = new SolidColorBrush(System.Windows.Media.Colors.Pink);

                        this.rectanglesArr[i, j].Fill = new SolidColorBrush(System.Windows.Media.Colors.Transparent);
                        start = new Image()
                        {
                            Source = new BitmapImage(new Uri("./Resources/unicorn.jpg", UriKind.Relative)),
                            Width = this.recWidth,
                            Height = this.recHeight
                        };
                        mazeCanvas.Children.Add(start);
                        Canvas.SetLeft(start, this.recWidth * j);
                        Canvas.SetTop(start, this.recHeight * i);
                        counter++;
                        continue;
                    }
                    else if (this.charArr[counter] == '#')
                    {
                        //this.rectanglesArr[i, j].Fill = new SolidColorBrush(System.Windows.Media.Colors.Blue);
                        this.rectanglesArr[i, j].Fill = new SolidColorBrush(System.Windows.Media.Colors.Transparent);
                        end = new Image()
                        {
                            Source = new BitmapImage(new Uri("./Resources/cloud.png", UriKind.Relative)),
                            Width = this.recWidth,
                            Height = this.recHeight
                        };
                        mazeCanvas.Children.Add(end);
                        Canvas.SetLeft(end, this.recWidth * j);
                        Canvas.SetTop(end, this.recHeight * i);
                        counter++;
                        continue;
                    }
                    else
                    {
                        counter++;
                        continue;
                    }
                    mazeCanvas.Children.Add(this.rectanglesArr[i, j]);
                    Canvas.SetLeft(this.rectanglesArr[i, j], this.recWidth * j);
                    Canvas.SetTop(this.rectanglesArr[i, j], this.recHeight * i);
                    counter++;
                }
                counter += 2;
            }
        }
        /// <summary>
        /// MSGs the show.
        /// </summary>
        public void msgShow()
        {
            if (currentPos.Col == m.GoalPos.Col && currentPos.Row == m.GoalPos.Row)
            {
                MessageBox.Show("You are almost as good as mor\n");
            }
        }

        /// <summary>
        /// Mazes the canvas key down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        public void mazeCanvas_KeyDown(object sender, Key e)
        {
            int row = this.currentPos.Row;
            int col = this.currentPos.Col;
            if (e == Key.Left)
            {
                if (col - 1 >= 0 && m[row, col - 1] == CellType.Free)
                {
                    Canvas.SetLeft(start, this.recWidth * (col - 1));
                    Canvas.SetTop(start, this.recHeight * row);
                    this.currentPos.Col -= 1;
                }
            }
            else if (e == Key.Right)
            {
                if (col + 1 < m.Cols && m[row, col + 1] == CellType.Free)
                {
                    Canvas.SetLeft(start, this.recWidth * (col + 1));
                    Canvas.SetTop(start, this.recHeight * row);
                    this.currentPos.Col += 1;
                }
            }
            else if (e == Key.Up)
            {
                if (row - 1 >= 0 && m[row - 1, col] == CellType.Free)
                {
                    Canvas.SetLeft(start, this.recWidth * col);
                    Canvas.SetTop(start, this.recHeight * (row - 1));
                    this.currentPos.Row -= 1;

                }
            }
            else if (e == Key.Down)
            {
                if (row + 1 < m.Rows && m[row + 1, col] == CellType.Free)
                {
                    Canvas.SetLeft(start, this.recWidth * col);
                    Canvas.SetTop(start, this.recHeight * (row + 1));
                    this.currentPos.Row += 1;
                }
            }
        }
        /// <summary>
        /// Restars the game.
        /// </summary>
        public void RestarGame()
        {
            int col = m.InitialPos.Col;
            int row = m.InitialPos.Row;
            Canvas.SetLeft(start, this.recWidth * col);
            Canvas.SetTop(start, this.recHeight * (row));
            this.currentPos.Row = m.InitialPos.Row;
            this.currentPos.Col = m.InitialPos.Col;

        }

    }
}
