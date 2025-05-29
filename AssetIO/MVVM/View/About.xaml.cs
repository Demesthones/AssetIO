using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.RightsManagement;
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
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        private string _author;
        private string _version;

        double _windowWidth = 280;
        double _windowHeight = 120;
        double mainWidth = 920;
        double mainHeight = 600;

        public About()
        {
            InitializeComponent();

            var appSettings = ConfigurationManager.AppSettings;
            this.Top = Double.Parse(appSettings["window_top"]) + (mainHeight / 2) - (_windowHeight / 2);
            this.Left = Double.Parse(appSettings["window_left"]) + (mainWidth / 2) - (_windowWidth / 2);

            this.Author = "Teagan Cornejo";
            this.Version = "1.0.0";
            this.DataContext = this;
            
        }

        public string Author { get { return _author; } set { _author = value; } }
        public string Version { get { return _version; } set { _version = value; } }

    }
}
