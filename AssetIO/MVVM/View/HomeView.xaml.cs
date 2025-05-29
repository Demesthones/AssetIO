using AssetIO.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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
using UserControl = System.Windows.Controls.UserControl;


namespace AssetIO.MVVM.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        string SelectedDatabasePath = "";
        public HomeView()
        {

            InitializeComponent();
            
            MainViewModel mainViewModel = new MainViewModel();
            this.DataContext = mainViewModel;

        }

        /*public void DateTimeCM_Click(EventArgs e)
        {

        }
        public void TechnicianCM_Click(EventArgs e)
        {

        }
        public void AssetIdentifierCM_Click(EventArgs e)
        {

        }*/

    }
}
