using VintageParts.Commands;
using VintageParts.SingletonView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace VintageParts.ViewModels.AdminViewModels
{
    class MainAdminVM : ViewModelBase
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
        public MainAdminVM()
        {
            SingletonAdmin.getInstance(this);
            CurrentViewModel = new HomeAdminVM();
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
        public Command openUsers;
        public ICommand OpenUsers
        {
            get
            {
                return openUsers ??
                  (openUsers = new Command(obj =>
                  {
                        SingletonAdmin.getInstance(null).StartViewModel.CurrentViewModel = new UsersAdminVM();
                  }));
            }
        }
        public Command openOrders;
        public ICommand OpenOrders
        {
            get
            {
                return openOrders ??
                  (openOrders = new Command(obj =>
                  {
                      SingletonAdmin.getInstance(null).StartViewModel.CurrentViewModel = new OrdersAdminVM();
                  }));
            }
        }
        public Command openProviders;
        public ICommand OpenProviders
        {
            get
            {
                return openProviders ??
                  (openProviders = new Command(obj =>
                  {
                      SingletonAdmin.getInstance(null).StartViewModel.CurrentViewModel = new ProvidersAdminVM();
                  }));
            }
        }
        public Command openParts;
        public ICommand OpenParts
        {
            get
            {
                return openParts ??
                  (openParts = new Command(obj =>
                  {
                      SingletonAdmin.getInstance(null).StartViewModel.CurrentViewModel = new PartsAdminVM();
                  }));
            }
        }
        public Command openDeliveries;
        public ICommand OpenDeliveries
        {
            get
            {
                return openDeliveries ??
                  (openDeliveries = new Command(obj =>
                  {
                      SingletonAdmin.getInstance(null).StartViewModel.CurrentViewModel = new DeliveriesAdminVM();
                  }));
            }
        }
        public Command openMarks;
        public ICommand OpenMarks
        {
            get
            {
                return openMarks ??
                  (openMarks = new Command(obj =>
                  {
                      SingletonAdmin.getInstance(null).StartViewModel.CurrentViewModel = new BrandsAdminVM();
                  }));
            }
        }
        public Command openCategories;
        public ICommand OpenCategories
        {
            get
            {
                return openCategories ??
                  (openCategories = new Command(obj =>
                  {
                      SingletonAdmin.getInstance(null).StartViewModel.CurrentViewModel = new CategoriesAdminVM();
                  }));
            }
        }
        public Command openCards;
        public ICommand OpenCards
        {
            get
            {
                return openCards ??
                  (openCards = new Command(obj =>
                  {
                      SingletonAdmin.getInstance(null).StartViewModel.CurrentViewModel = new CardsAdminVM();
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

                     SingletonAdmin.getInstance(null).StartViewModel.CurrentViewModel = new SettingsViewModel();
                     
                 }));
            }
        }
        private Command openImages;
        public ICommand OpenImages
        {
            get
            {
                return openImages ??
                    (openImages = new Command(obj =>
                    {
                        SingletonAdmin.getInstance(null).StartViewModel.CurrentViewModel = new MediasAdminVM();
                    }));
            }
        }
    }
}
