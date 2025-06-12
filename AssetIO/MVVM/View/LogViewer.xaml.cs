using AssetIO.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace AssetIO.MVVM.View
{
    /// <summary>
    /// Interaction logic for LogViewer.xaml
    /// </summary>
    public partial class LogViewer : Window
    {

        double _windowWidth = 720;
        double _windowHeight = 600;
        double mainWidth = 920;
        double mainHeight = 600;

        private string _folder;
        private string _search;
        public ObservableCollection<AssetEntry> AssetEntries { get; set; }

        public LogViewer(string Path)
        {
            InitializeComponent();
            this.DataContext = this;
            var appSettings = ConfigurationManager.AppSettings;
            this.Top = Double.Parse(appSettings["window_top"]) + (mainHeight / 2) - (_windowHeight / 2) + 40;
            this.Left = Double.Parse(appSettings["window_left"]) + (mainWidth / 2) - (_windowWidth / 2) + 40;
            this.Folder = Path;


            AssetEntries = DataManager.GetEntries(this.Folder);
            this._view = new ListCollectionView(AssetEntries);
        }

        private ListCollectionView _view;
        public ICollectionView View { get { return _view; } }


        public string Folder
        {
            get
            {
                return _folder;
            }
            set
            {
                _folder = value;
                OnPropertyChanged("Folder");
            }
        }
        public string Search
        {
            get
            {
                return _search;
            }
            set
            {
                _search = value;
                OnPropertyChanged("Search");
                if (String.IsNullOrEmpty(value))
                {
                    View.Filter = null;
                }
                else
                {
                    View.Filter += new Predicate<object>(o => Filter(o as AssetEntry));
                }
                View.Refresh();
            }
        }
        private bool Filter(AssetEntry entry)
        {
            //Debug.WriteLine("Filter: " + entry.ToString());
            return Search == null
                || entry.identifier.IndexOf(Search, StringComparison.OrdinalIgnoreCase) != -1;
        }
        

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
