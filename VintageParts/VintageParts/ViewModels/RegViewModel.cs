using VintageParts.Commands;
using VintageParts.Database;
using VintageParts.Models;
using VintageParts.Services;
using VintageParts.SingletonView;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToastNotifications.Messages;

namespace VintageParts.ViewModels
{
    public class RegViewModel : ViewModelBase
    {
        public string login { get; set; }
        public string password { get; set; }
        public string mail { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        private string errorMes;
        public string ErrorMessage
        {
            get { return errorMes; }
            set
            {
                this.errorMes = value;
                OnPropertyChanged("ErrorMessage");
            }
        }
        private Command backCommand;
        public ICommand BackCommand
        {
            get
            {
                return backCommand ??
                  (backCommand = new Command(obj =>
                  {
                      try
                      {
                          SingletonAuth.getInstance(null).StartViewModel.CurrentViewModel = new LoginViewModel();
                      }
                      catch (Exception e)
                      {
                          MessageBox.Show(e.Message);
                      }
                  }));
            }
        }
        public Command regCommand;
        public ICommand RegCommand
        {
            get
            {
                return regCommand ??
                 (regCommand = new Command(obj =>
                 {
                     try
                     {
                         using (PartShopDbContext db = new PartShopDbContext())
                         {
                             Authorization auth = new Authorization();
                             User user = new User();
                             if(String.IsNullOrEmpty(login))
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
                             auth.Login = login;
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
                             if (password != null & password[0] != ' ')
                             {
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
                                 auth.Password = SecurePassService.Hash(password);
                             }
                             else
                             {
                                 switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                                 {
                                     case "ru-RU":
                                         {
                                             throw new Exception("Неверный формат пароля");
                                         }
                                     default:
                                         {
                                             throw new Exception("Invalid password format");
                                         }
                                 }
                             }
                         if (String.IsNullOrEmpty(firstname) || String.IsNullOrEmpty(lastname))
                             {
                                 switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                                 {
                                     case "ru-RU":
                                         {
                                             throw new Exception("Не введены имя или фамилия");
                                         }
                                     default:
                                         {
                                             throw new Exception("First or last name not entered");
                                         }
                                 }
                             }
                             user.FirstName = firstname;
                             user.LastName = lastname;
                             auth.Is_admin = false;
                             user.Email = mail;
                             if (login != null && password != null)
                             {
                                 if (db.Authorizations.Any(a => a.Login == login) || db.Users.Any(a => a.Email == mail))
                                 {
                                     switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                                     {
                                         case "ru-RU":
                                             {
                                                 throw new Exception("Пользователь с такими данными уже существует");
                                             }
                                         default:
                                             {
                                                 throw new Exception("User with such data already exists");
                                             }
                                     }
                                 }
                                 else
                                 {
                                     db.Authorizations.Add(auth);
                                     db.Users.Add(user);
                                     db.SaveChanges();
                                     SingletonAuth.getInstance(null).StartViewModel.CurrentViewModel = new LoginViewModel();
                                     switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                                     {
                                         case "ru-RU":
                                             {
                                                 App.NotifyWindow(Application.Current.Windows[0]).ShowSuccess("Регистрация прошла успешно");
                                                 break;
                                             }
                                         default:
                                             {
                                                 App.NotifyWindow(Application.Current.Windows[0]).ShowSuccess("Registration completed successfully");
                                                 break;
                                             }
                                     }
                                 }
                             }
                         }
                     }
                     catch(DbEntityValidationException e)
                     {
                         foreach(DbEntityValidationResult validationRes in e.EntityValidationErrors)
                         {
                             foreach(DbValidationError err in validationRes.ValidationErrors)
                             {
                                 ErrorMessage = err.ErrorMessage;
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
    }
}
