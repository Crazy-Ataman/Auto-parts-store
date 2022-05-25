using VintageParts.Commands;
using VintageParts.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToastNotifications.Messages;

namespace VintageParts.ViewModels.AdminViewModels
{
    public class UsersAdminVM : ViewModelBase
    {
        
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<User> deletedUsers { get; set; }
        public ObservableCollection<Authorization> Authorizations { get; set; }
        public ObservableCollection<Authorization> deletedAuthorizations { get; set; }

        public UsersAdminVM()
        {
            Users = new ObservableCollection<User>(App.db.Users);
            deletedUsers = new ObservableCollection<User>();
            Authorizations = new ObservableCollection<Authorization>(App.db.Authorizations);
            deletedAuthorizations = new ObservableCollection<Authorization>();
        }
        private User selectedUser;
        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged("SelectedUser");
            }
        }

        private Command deleteCommand;

        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new Command(obj =>
                  {
                      try
                      {
                          if (selectedUser != null)
                          {
                              User user = new User();
                              user = selectedUser;
                              Authorization authorization = new Authorization();
                              authorization = App.db.Authorizations.FirstOrDefault(a => a.Auth_id == user.Auth_id);
                              Users.Remove(user);
                              Authorizations.Remove(authorization);
                              deletedUsers.Add(user);
                              deletedAuthorizations.Add(authorization);
                              switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                              {
                                  case "ru-RU":
                                      {
                                          App.NotifyWindow(Application.Current.Windows[0]).ShowWarning("Пользователь был удален");
                                          break;
                                      }
                                  default:
                                      {
                                          App.NotifyWindow(Application.Current.Windows[0]).ShowWarning("User has been deleted");
                                          break;
                                      }
                              }
                          }
                      }
                      catch(Exception e)
                      {
                          MessageBox.Show(e.Message);
                      }
                  }
                ));
            }
        }
        private Command saveCommand;        
        public ICommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new Command(obj =>
                  {
                      try
                      {
                          foreach (User i in deletedUsers)
                          {
                              App.db.Users.Remove(i);
                          }
                          foreach (Authorization i in deletedAuthorizations)
                          {
                              App.db.Authorizations.Remove(i);
                          }
                          App.db.SaveChanges();
                          deletedUsers.Clear();
                          deletedAuthorizations.Clear();
                          switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                          {
                              case "ru-RU":
                                  {
                                      App.NotifyWindow(Application.Current.Windows[0]).ShowInformation("Данные сохранены");
                                      break;
                                  }
                              default:
                                  {
                                      App.NotifyWindow(Application.Current.Windows[0]).ShowInformation("Data saved");
                                      break;
                                  }
                          }
                      }
                      catch(Exception e)
                      {
                          MessageBox.Show(e.Message);
                      }
                  }));
            }
        }
        
    }
}
