using AssetIO.Core;
using AssetIO.MVVM.Model;
using AssetIO.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AssetIO.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public ObservableCollection<AssetEntry> AssetEntries { get; set; }

        public ICommand ShowSettingsCommand { get; set; }
        public ICommand ShowLogSelectorCommand { get; set; }

        public ICommand ShowAboutCommand { get; set; }


        public HomeViewModel HomeVM { get; set; }
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel() {

            
            AssetEntries = DataManager.GetEntries();
            /* 
             * var appSettings = ConfigurationManager.AppSettings;

            if (appSettings["database_folder_path"] == null || appSettings["database_folder_path"].Length == 0) { }
            else
            {
                SelectedDatabasePath = appSettings["database_folder_path"];

            }
            */

            HomeVM = new HomeViewModel();
            CurrentView = HomeVM;

            ShowSettingsCommand = new RelayCommand(ShowSettingsWindow, CanShowWindow);
            ShowLogSelectorCommand = new RelayCommand(ShowLogSelectorWindow, CanShowWindow);
            ShowAboutCommand = new RelayCommand(ShowAboutWindow, CanShowWindow);
            
        }
        private void ShowAboutWindow(object obj)
        {
            About aboutWin = new About();
            aboutWin.Show();
        }
        private void ShowLogSelectorWindow(object obj)
        {
            LogView logViewWin = new LogView();
            logViewWin.Show();

        }

        private void ShowSettingsWindow(object obj)
        {
            Settings settingsWin = new Settings();
            settingsWin.Show();
        }

        private bool CanShowWindow(object arg)
        {
            return true;
        }
    }
}
