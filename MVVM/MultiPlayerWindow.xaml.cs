
using MVVM.ViewModel;
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

namespace MVVM
{
    /// <summary>
    /// Interaction logic for MultiPlayerWindow.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MultiPlayerWindow : Window
    {
        /// <summary>
        /// The vm
        /// </summary>
        private MultiPlayerViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerWindow"/> class.
        /// </summary>
        public MultiPlayerWindow()
        {
            InitializeComponent();
            vm = new MultiPlayerViewModel();
            this.DataContext = vm;
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
                Window multiMaze = new MultiMaze(vm);
                multiMaze.Show();
                this.Hide();
            } catch (Exception)
            {
                MessageBox.Show("Connection error");
                this.Close();
                MainWindow win = (MainWindow)Application.Current.MainWindow;
                win.Show();
            }
            
            
        }

        /// <summary>
        /// Handles the Click event of the btnJoin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnJoin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Object selectedItem = txtGameName.SelectedItem;
                vm.Join(selectedItem.ToString());
                Window multiMaze = new MultiMaze(vm);
                multiMaze.Show();
                this.Hide();
            } catch (Exception)
            {
                MessageBox.Show("Connection error");
                this.Close();
                MainWindow win = (MainWindow)Application.Current.MainWindow;
                win.Show();
            }
            
        }

        /// <summary>
        /// Handles the Click event of the btnRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            vm = new MultiPlayerViewModel();
            this.DataContext = vm;
        }
    }
}
