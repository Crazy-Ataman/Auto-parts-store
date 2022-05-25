using VintageParts.Commands;
using VintageParts.Database;
using VintageParts.SingletonView;
using VintageParts.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace VintageParts.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                currentViewModel = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }
        public MainViewModel()
        {
            Singleton.getInstance(this);
            CurrentViewModel = new HomeViewModel();
        }
        private Command exit;
        public ICommand Exit
        {
            get
            {
                return exit ??
                 (exit = new Command(obj =>
                 {
                     Application.Current.Shutdown();
                     System.Diagnostics.Process.Start(Environment.GetCommandLineArgs()[0]);
                 }));
            }
        }
        public void Close()
        {
            foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
            {
                if (window.DataContext == this)
                {
                    window.Close();
                }
            }
        }
        private Command openProfile;
        public ICommand OpenProfile
        {
            get
            {
                return openProfile ??
                 (openProfile = new Command(obj =>
                 {
                     Singleton.getInstance(null).MainViewModel.CurrentViewModel = new ProfileViewModel();
                 }));
            }
        }
        private Command openSearch;
        public ICommand OpenSearch
        {
            get
            {
                return openSearch ??
                 (openSearch = new Command(obj =>
                 {
                     Singleton.getInstance(null).MainViewModel.CurrentViewModel = new SearchViewModel();
                 }));
            }
        }
        private Command openHome;
        public ICommand OpenHome
        {
            get
            {
                return openHome ??
                 (openHome = new Command(obj =>
                 {
                     Singleton.getInstance(null).MainViewModel.CurrentViewModel = new HomeViewModel();
                 }));
            }
        }
        private Command openSettings;
        public ICommand OpenSettings
        {
            get
            {
                return openSettings ??
                 (openSettings = new Command(obj =>
                 {
                     if (Singleton.getInstance(null).MainViewModel.CurrentViewModel != new SettingsViewModel())
                     {
                         Singleton.getInstance(null).MainViewModel.CurrentViewModel = new SettingsViewModel();
                     }
                 }));
            }
        }
        private Command openCart;
        public ICommand OpenCart
        {
            get
            {
                return openCart ??
                 (openCart = new Command(obj =>
                 {
                     Singleton.getInstance(null).MainViewModel.CurrentViewModel = new CartViewModel();
                 }));
            }
        }
        private Command openAbout;
        public ICommand OpenAbout
        {
            get
            {
                return openAbout ??
                 (openAbout = new Command(obj =>
                 {
                     Singleton.getInstance(null).MainViewModel.CurrentViewModel = new AboutViewModel();
                 }));
            }
        }
    }
}
