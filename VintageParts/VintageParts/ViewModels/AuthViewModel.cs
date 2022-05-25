using VintageParts.Commands;
using VintageParts.Database;
using VintageParts.Models;
using VintageParts.Services;
using VintageParts.SingletonView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace VintageParts.ViewModels
{
    public class AuthViewModel : ViewModelBase
    {
        ViewModelBase curViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return curViewModel; }
            set
            {
                curViewModel = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }
        public AuthViewModel()
        {
            SingletonAuth.getInstance(this);
            CurrentViewModel = new LoginViewModel();
        }
        public static void Close()
        {
            var window = System.Windows.Application.Current.Windows;
            window[0].Close();
        }
    }
}
