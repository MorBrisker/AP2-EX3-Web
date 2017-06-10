
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
using System.Windows.Shapes;
using MVVM.ViewModel;

namespace MVVM
{
    /// <summary>
    /// Class SinglePlayerWindow.
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class SinglePlayerWindow : Window
    {
        /// <summary>
        /// The vm
        /// </summary>
        private SinglePlayerViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerWindow"/> class.
        /// </summary>
        public SinglePlayerWindow()
        {
            vm = new SinglePlayerViewModel();
            this.DataContext = vm;
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the btnStart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                vm.Start();
                Window singleMaze = new SingleMaze(vm);
                singleMaze.Show();
                this.Hide();
            } catch (Exception)
            {
                MessageBox.Show("Connection error");
                this.Close();
                MainWindow win = (MainWindow)Application.Current.MainWindow;
                win.Show();
            }
            
        }
    }
}