using AssetIO;
using AssetIO.Core;
using AssetIO.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
using WinForms = System.Windows.Forms;

namespace AssetIO.MVVM.View
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window, INotifyPropertyChanged
    {
        double _windowWidth = 400;
        double _windowHeight = 200;
        double mainWidth = 920;
        double mainHeight = 600;

        private string _folder;
        public Settings()
        {
            InitializeComponent();
            var appSettings = ConfigurationManager.AppSettings;
            this.Top = Double.Parse(appSettings["window_top"]) + (mainHeight / 2) - (_windowHeight / 2);
            this.Left = Double.Parse(appSettings["window_left"]) + (mainWidth / 2) - (_windowWidth / 2);
            this.Folder = appSettings["database_folder_path"];
            DataContext = this;
        }

        public string Folder
        {
            get {
                return _folder; }
            set 
            {
                _folder = value;
                OnPropertyChanged("Folder");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void btnPicker_Click(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog dialog = new WinForms.FolderBrowserDialog();
            dialog.InitialDirectory = ".\\";
            dialog.Description = "Select database folder.";
            dialog.ShowNewFolderButton = true;
            WinForms.DialogResult result = dialog.ShowDialog();

            if (result == WinForms.DialogResult.OK)
            {
                this.Folder = dialog.SelectedPath;
            }
        }

        private void submit(object sender, RoutedEventArgs e)
        {
            try
            {
                var oldFolder = MainWindow.ReadSetting("database_folder_path");
                if (this.Folder.Length > 1 && this.Folder != oldFolder)
                {
                    MainWindow.AddUpdateAppSettings("database_folder_path", this.Folder);
                    //MainViewModel.SelectedDatabasePath = this.Folder;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

            }
            Close();
        }
    }
}

