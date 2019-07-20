using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace LambentLight
{
    /// <summary>
    /// A class that represents a folder with FiveM server data.
    /// </summary>
    public class DataFolder
    {
        /// <summary>
        /// The location of the server data folder
        /// </summary>
        public string Location { get; private set; }
        /// <summary>
        /// If the data folder exists.
        /// </summary>
        public bool Exists => Directory.Exists(Location);
        /// <summary>
        /// If the folder has a FiveM server configuration file.
        /// </summary>
        public bool HasConfiguration => File.Exists(Path.Combine(Location, "server.cfg"));

        /// <summary>
        /// Creates a new instance of the data folder.
        /// </summary>
        /// <param name="location">The location of the data folder.</param>
        public DataFolder(string location)
        {
            Location = location;
        }

        /// <summary>
        /// Gets the directory name.
        /// </summary>
        /// <returns>The name of the directory.</returns>
        public override string ToString()
        {
            return Path.GetFileName(Location);
        }
    }

    /// <summary>
    /// Managers for the folders that contain our data.
    /// </summary>
    public static class DataFolderManager
    {
        /// <summary>
        /// Our current set of data folders.
        /// </summary>
        public static List<DataFolder> Folders = new List<DataFolder>();

        /// <summary>
        /// Refreshes the builds with data.
        /// </summary>
        public static void Refresh()
        {
            // Reset the list of data folders
            Folders = new List<DataFolder>();

            // If the data folder does not exists
            if (!Directory.Exists("Data"))
            {
                // Create it
                Directory.CreateDirectory("Data");
            }

            // Iterate over the folders on our Data folder
            foreach (string Dir in Directory.GetDirectories("Data"))
            {
                // And add our data folder
                Folders.Add(new DataFolder(Dir));
            }
        }

        /// <summary>
        /// Fills the specified ComboBox with 
        /// </summary>
        /// <param name="box"></param>
        public static void Fill(ComboBox box)
        {
            // Refresh the list of folders first
            Refresh();

            // Remove all of the items
            box.Items.Clear();

            // For every folder
            foreach (DataFolder Folder in Folders)
            {
                // Add it to the combo box
                box.Items.Add(Folder);
            }
        }
    }
}
