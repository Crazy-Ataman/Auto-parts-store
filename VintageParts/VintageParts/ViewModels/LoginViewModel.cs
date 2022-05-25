using VintageParts.Commands;
using VintageParts.Database;
using VintageParts.Models;
using VintageParts.Properties;
using VintageParts.Services;
using VintageParts.SingletonView;
using VintageParts.Views.AdminViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace VintageParts.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public string login { get; set; }
        public string password { get; set; }
        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                this.errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }
        public Command authCommand;
        public ICommand AuthCommand
        {
            get
            {
                return authCommand ??
                 (authCommand = new Command(obj =>
                 {
                     try
                     {
                         using (PartShopDbContext db = new PartShopDbContext())
                         {
                             if (String.IsNullOrEmpty(login))
                             {
                                 switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                                 {
                                     case "ru-RU":
                                         {
                                             throw new Exception("Введите логин");
                                         }
                                     default:
                                         {
                                             throw new Exception("Enter login");
                                         }
                                 }
                             }
                             if (String.IsNullOrEmpty(password))
                             {
                                 switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                                 {
                                     case "ru-RU":
                                         {
                                             throw new Exception("Введите пароль");
                                         }
                                     default:
                                         {
                                             throw new Exception("Enter password");
                                         }
                                 }
                             }
                             if (password.Length < 4 || password.Length > 21)
                             {
                                 switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                                 {
                                     case "ru-RU":
                                         {
                                             throw new Exception("Пароль должен быть не менее 4 и не более 20 символов");
                                         }
                                     default:
                                         {
                                             throw new Exception("The password must be at least 4 and not more than 20 characters");
                                         }
                                 }
                             }
                             password = SecurePassService.Hash(password);
                             Authorization authUser = db.Authorizations.Where(a => a.Login == login && a.Password == password).FirstOrDefault();
                             if(authUser == null)
                             {
                                 switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                                 {
                                     case "ru-RU":
                                         {
                                             throw new Exception("Невозможно найти пользователя с введенными данными");
                                         }
                                     default:
                                         {
                                             throw new Exception("Unable to find user with entered data");
                                         }
                                 }
                             }
                             if (authUser != null)
                             {
                                 if (authUser.Is_admin == false)
                                 {
                                     MainWindow main = new MainWindow();
                                     main.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                                     main.Show();
                                     AuthViewModel.Close();
                                 }
                                 else
                                 {
                                     MainAdminView mainAdmin = new MainAdminView();
                                     mainAdmin.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                                     mainAdmin.Show();
                                     AuthViewModel.Close();
                                 }
                                 User user = db.Users.Where(u => u.Auth_id == authUser.Auth_id).FirstOrDefault();
                                 Settings.Default.AuthId = authUser.Auth_id;
                                 Settings.Default.UserMail = user.Email;
                                 Settings.Default.UserId = user.User_id;
                             }
                         }
                     }
                     catch(Exception e)
                     {
                         ErrorMessage = e.Message;
                     }
                 }));
            }
        }
        public Command openRegCommand;
        public ICommand OpenRegCommand
        {
            get
            {
                return openRegCommand ??
                 (openRegCommand = new Command(obj =>
                 {
                     SingletonAuth.getInstance(null).StartViewModel.CurrentViewModel = new RegViewModel();

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
    }
}
