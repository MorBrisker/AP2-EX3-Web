
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MazeLib;
using MVVM.ViewModel;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Windows.Threading;
using System.ComponentModel;

namespace MVVM
{
    /// <summary>
    /// Class SingleMaze.
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class SingleMaze : Window
    {
        /// <summary>
        /// The vm
        /// </summary>
        private SinglePlayerViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleMaze"/> class.
        /// </summary>
        /// <param name="vm">The vm.</param>
        public SingleMaze(SinglePlayerViewModel vm)
        {
            this.vm = vm;
            this.DataContext = vm;
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Loaded event of the UserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        public void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += Grid_KeyDown;
        }

        /// <summary>
        /// Handles the KeyDown event of the Grid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            mazey.mazeCanvas_KeyDown(sender, e.Key);
            mazey.msgShow();
        }

        /// <summary>
        /// Handles the Click event of the RestartGame control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void RestartGame_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
     new Action(() => mazey.RestarGame()));

        }

        /// <summary>
        /// Froms the json.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>System.String.</returns>
        public string FromJSON(string str)
        {
            string solution;
            JObject solObj = JObject.Parse(str);
            solution = (string)solObj["Solution"];
            return solution;
        }

        /// <summary>
        /// Handles the Click event of the SolveMaze control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SolveMaze_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string soultion = vm.SolveMaze();
                string sol = FromJSON(soultion);
                for (int i = 0; i < sol.Length; i++)
                {
                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                        new Action(() => move(sender, sol, i)));
                }
            } catch (Exception)
            {
                MessageBox.Show("Connection error");
                this.Close();
                MainWindow win = (MainWindow)Application.Current.MainWindow;
                win.Show();
            }
            
        }

        /// <summary>
        /// Moves the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="sol">The sol.</param>
        /// <param name="i">The i.</param>
        public void move(Object sender, string sol, int i)
        {
            if (sol[i] == '0')
            {
                Key k = Key.Left;
                mazey.mazeCanvas_KeyDown(sender, k);
            }
            else if (sol[i] == '1')
            {
                Key k = Key.Right;
                mazey.mazeCanvas_KeyDown(sender, k);
            }
            else if (sol[i] == '2')
            {
                Key k = Key.Up;
                mazey.mazeCanvas_KeyDown(sender, k);
            }
            else if (sol[i] == '3')
            {
                Key k = Key.Down;
                mazey.mazeCanvas_KeyDown(sender, k);
            }
            Thread.Sleep(300);
        }

        /// <summary>
        /// Handles the Click event of the MainMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Closing" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            //e.Cancel = true;
            MessageBoxButton mbb = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show("Are you sure you to exit?", "Exit Game", mbb);
            if (result == MessageBoxResult.Yes)
            {
                MainWindow win = (MainWindow)Application.Current.MainWindow;
                win.Show();
            } else
            {
                e.Cancel = true;
            }
        }
    }
}
