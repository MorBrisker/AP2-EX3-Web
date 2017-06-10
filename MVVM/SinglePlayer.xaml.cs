
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
using MVVM.ViewModel;

namespace MVVM
{
    /// <summary>
    /// Class SinglePlayer.
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class SinglePlayer : UserControl
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayer"/> class.
        /// </summary>
        public SinglePlayer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the name of the m.
        /// </summary>
        /// <value>The name of the m.</value>
        public string MName
        {
            get { return (string)GetValue(MNameProperty); }
            set { SetValue(MNameProperty, value); }
        }
        /// <summary>
        /// The m name property
        /// </summary>
        public static readonly DependencyProperty MNameProperty =
         DependencyProperty.Register("MName", typeof(string), typeof(SinglePlayer));

        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>The rows.</value>
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Rows. This enables animation, styling, binding, etc...
        /// <summary>
        /// The rows property
        /// </summary>
        public static readonly DependencyProperty RowsProperty =
         DependencyProperty.Register("Rows", typeof(int), typeof(SinglePlayer));

        /// <summary>
        /// Gets or sets the cols.
        /// </summary>
        /// <value>The cols.</value>
        public int Cols
        {
            get { return (int)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }
        /// <summary>
        /// The cols property
        /// </summary>
        public static readonly DependencyProperty ColsProperty =
         DependencyProperty.Register("Cols", typeof(int), typeof(SinglePlayer));
    }
}