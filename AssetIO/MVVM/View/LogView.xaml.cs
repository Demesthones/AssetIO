using AssetIO;
using AssetIO.Core;
using AssetIO.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
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
    public partial class LogView : Window, INotifyPropertyChanged
    {
        double _windowWidth = 400;
        double _windowHeight = 200;
        double mainWidth = 920;
        double mainHeight = 600;

        private string _folder;
        public string FolderContains = "Selected Directory file count:  ";
        public string _output;
        public ICommand ShowLogViewerCommand { get; set; }

        public LogView()
        {
            InitializeComponent();
            var appSettings = ConfigurationManager.AppSettings;
            this.Top = Double.Parse(appSettings["window_top"]) + (mainHeight / 2) - (_windowHeight / 2);
            this.Left = Double.Parse(appSettings["window_left"]) + (mainWidth / 2) - (_windowWidth / 2);
            this.Folder = "";
            this.Output = FolderContains;
            DataContext = this;
        }

        public string Output
        {
            get { 
                return _output; }
            set 
            { 
                _output = value; 
                OnPropertyChanged("Output"); 
            }
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
                var AllFiles = Directory.EnumerateFiles(this.Folder, "*.aio", SearchOption.TopDirectoryOnly);
                this.Output = String.Concat(FolderContains, AllFiles.Count().ToString());
                //Debug.WriteLine(this.Output);
            }
            
        }

        private void submit(object sender, RoutedEventArgs e)
        {
            ShowLogViewerCommand = new RelayCommand(ShowLogViewerWindow, CanShowWindow);
            try
            {
                if (this.Folder.Length > 0)
                {
                    //MainViewModel.LogWindowPath = this.Folder;
                    if (ShowLogViewerCommand.CanExecute(CanShowWindow))
                    {
                        ShowLogViewerCommand.Execute(CanShowWindow);
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Invalid Path Selected");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

            }
            Close();
        }
        private void ShowLogViewerWindow(object obj)
        {
            LogViewer logViewerWin = new LogViewer(this.Folder);
            logViewerWin.Show();
        }
        private bool CanShowWindow(object arg)
        {
            return true;
        }
    }
}

