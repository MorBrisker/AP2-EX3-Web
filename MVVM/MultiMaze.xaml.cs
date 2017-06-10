
using MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace MVVM
{

    /// <summary>
    /// Class MultiMaze.
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MultiMaze : Window
    {
        /// <summary>
        /// The vm
        /// </summary>
        private MultiPlayerViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiMaze"/> class.
        /// </summary>
        /// <param name="vm">The vm.</param>
        public MultiMaze(MultiPlayerViewModel vm)
        {
            this.vm = vm;
            this.DataContext = vm;
            InitializeComponent();
        }
        /// <summary>
        /// Handles the Loaded event of the UserControlHome control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        public void UserControlHome_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += Grid_KeyDownHome;
        }

        /// <summary>
        /// Handles the Loaded event of the UserControlAway control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        public void UserControlAway_Loaded(object sender, RoutedEventArgs e)
        {
            //var window = Window.GetWindow(this);
            //window.KeyDown += Grid_KeyDown;
        }

        /// <summary>
        /// Handles the KeyDownHome event of the Grid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void Grid_KeyDownHome(object sender, KeyEventArgs e)
        {
            mazeyHome.mazeCanvas_KeyDown(sender, e.Key);
            mazeyHome.msgShow();
            try
            {
                vm.Play(e.Key);
            } catch (Exception)
            {
                MessageBox.Show("Connection error");
                this.Close();
                MainWindow win = (MainWindow)Application.Current.MainWindow;
                win.Show();
            }
        }
        /// <summary>
        /// Handles the KeyDownAway event of the Grid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void Grid_KeyDownAway(object sender, KeyEventArgs e)
        {
            //mazeyHome.mazeCanvas_KeyDown(sender, e.Key);
            //mazeyHome.msgShow();
        }
        /// <summary>
        /// Handles the Click event of the backToMainMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void backToMainMenu_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.Closing" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            MessageBoxButton mbb = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show("Are you sure you to exit?", "Exit Game", mbb);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    vm.Close();
                } catch (Exception)
                {

                } finally
                {
                    MainWindow win = (MainWindow)Application.Current.MainWindow;
                    win.Show();
                }
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
