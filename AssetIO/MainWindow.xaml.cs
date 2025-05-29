using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WinForms = System.Windows.Forms;
using static AssetIO.MVVM.Model.DataManager;
using MessageBox = System.Windows.MessageBox;
using System.Configuration;
using AssetIO.Core;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace AssetIO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        double _windowTop;
        double _windowLeft;
        double _windowHeight = 600;
        double _windowWidth = 920;
        double _windowState;

        
        public MainWindow()
        {
            ReadAllSettings();
            InitializeComponent();
            LoadPosition();
            MoveIntoView();
            
        }

        public static void ReadAllSettings()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                if (appSettings["database_folder_path"] == null || appSettings["database_folder_path"].Length == 0)
                {
                    MessageBox.Show("No Database folder selected. Please select a folder in Settings.");
                }
                else
                {
                    foreach (var setting in appSettings.AllKeys)
                    {
                        Debug.WriteLine("Key: {0} Value: {1}", setting, appSettings[setting]);
                    }
                }
            } catch (ConfigurationErrorsException)
            {
                Debug.WriteLine("Error reading App Settings");
            }
        }

        public static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                //Debug.WriteLine(result);
                return (result);
            }
            catch (ConfigurationErrorsException)
            {
                Debug.WriteLine("Error reading app settings");
                return "";
            }
        }
        public static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        private void closeApp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to quit?", "Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    SavePosition();
                    Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void minimizeApp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.WindowState = WindowState.Minimized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clearInputBoxes(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure you want to clear the input?", "Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    tbDevicesIn.Text = "";
                    tbDevicesOut.Text = "";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void submitInputBoxes(object sender, RoutedEventArgs e)
        {
            try
            {
                if(tbTechnicianName.Text == "" || tbTechnicianName.Text == null)
                {
                    MessageBox.Show("Technician Name Required", "Error");
                }
                else
                {
                    ParseIOBoxes(tbTechnicianName.Text, tbDevicesIn.Text, tbDevicesOut.Text);
                    tbTechnicianName.Text = "";
                    tbDevicesIn.Text = "";
                    tbDevicesOut.Text = "";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
            SavePosition();
        }
        private void LoadPosition()
        {
            var appSettings = ConfigurationManager.AppSettings;
            try
            {
                _windowTop = Double.Parse(appSettings["window_top"]);
                _windowLeft = Double.Parse(appSettings["window_left"]);
                this.Top = _windowTop;
                this.Left = _windowLeft;
            } catch(Exception ex)
            {
                MainWindow.AddUpdateAppSettings("window_top", this.Top.ToString());
                MainWindow.AddUpdateAppSettings("window_left", this.Left.ToString());
            } 
            
        }
        public void SavePosition()
        {
            var appSettings = ConfigurationManager.AppSettings;
            if (this.WindowState != WindowState.Minimized)
            {
                MainWindow.AddUpdateAppSettings("window_top", this.Top.ToString());
                MainWindow.AddUpdateAppSettings("window_left", this.Left.ToString());
            }
        }
        public void MoveIntoView()
        {
            if (_windowTop + _windowHeight / 2 > System.Windows.SystemParameters.VirtualScreenHeight)
            {
                _windowTop = System.Windows.SystemParameters.VirtualScreenHeight - _windowHeight;
            }
            if (_windowLeft + _windowWidth / 2 > System.Windows.SystemParameters.VirtualScreenWidth)
            {
                _windowLeft = System.Windows.SystemParameters.VirtualScreenHeight - _windowWidth;
            }
            if (_windowTop < 0)
            {
                _windowTop = 0;
            }
            if (_windowLeft < 0)
            {
                _windowLeft = 0;
            }
        }
    }
}