
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MVVM
{
    /// <summary>
    /// Class ApplicationSettingsModel.
    /// </summary>
    /// <seealso cref="MVVM.ISettingsModel" />
    public class ApplicationSettingsModel : ISettingsModel
    {
        /// <summary>
        /// Gets or sets the server ip.
        /// </summary>
        /// <value>The server ip.</value>
        public string ServerIP
        {
            get { return MVVM.Properties.Settings.Default.ServerIP; }
            set { MVVM.Properties.Settings.Default.ServerIP = value; }
        }
        /// <summary>
        /// Gets or sets the server port.
        /// </summary>
        /// <value>The server port.</value>
        public int ServerPort
        {
            get { return MVVM.Properties.Settings.Default.ServerPort; }
            set { MVVM.Properties.Settings.Default.ServerPort = value; }
        }
        /// <summary>
        /// Gets or sets the maze rows.
        /// </summary>
        /// <value>The maze rows.</value>
        public int MazeRows
        {
            get { return MVVM.Properties.Settings.Default.MazeRows; }
            set { MVVM.Properties.Settings.Default.MazeRows = value; }
        }
        /// <summary>
        /// Gets or sets the maze cols.
        /// </summary>
        /// <value>The maze cols.</value>
        public int MazeCols
        {
            get { return MVVM.Properties.Settings.Default.MazeCols; }
            set { MVVM.Properties.Settings.Default.MazeCols = value; }
        }
        /// <summary>
        /// Gets or sets the search algorithm.
        /// </summary>
        /// <value>The search algorithm.</value>
        public int SearchAlgorithm
        {
            get { return MVVM.Properties.Settings.Default.SearchAlgorithm; }
            set { MVVM.Properties.Settings.Default.SearchAlgorithm = value; }
        }
        /// <summary>
        /// Saves the settings.
        /// </summary>
        public void SaveSettings()
        {
            MVVM.Properties.Settings.Default.Save();
        }
    }
}
