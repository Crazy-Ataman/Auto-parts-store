using VintageParts.Commands;
using VintageParts.Models;
using VintageParts.Properties;
using VintageParts.Services;
using VintageParts.SingletonView;
using VintageParts.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToastNotifications.Messages;

namespace VintageParts.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        public ObservableCollection<Order> Orders { get; set; }
        public Authorization Authorization { get; set; }
        public User User { get; set; }
        public Card Card { get; set; }
        public int plusBalance { get; set; }
        public string FullName { get; set; }
        public double Sum = 0;
        public ProfileViewModel()
        {
            Orders = new ObservableCollection<Order>(App.db.Orders.Where(x => x.User_id == Settings.Default.UserId));
            Authorization = App.db.Authorizations.Where(x => x.Auth_id == Settings.Default.AuthId).FirstOrDefault();
            User = App.db.Users.Where(x => x.User_id == Settings.Default.UserId).FirstOrDefault();
            Card = App.db.Cards.Where(x => x.User_id == Settings.Default.UserId).FirstOrDefault();
            if(Card == null)
            {
                switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                {
                    case "ru-RU":
                        {
                            Balance = 0;
                            FullName = "Добавьте карту для покупок";
                            break;
                        }
                    default:
                        {
                            Balance = 0;
                            FullName = "Add a shopping card";
                            break;
                        }
                }
            }
            else
            {
                Balance = Card.Balance;
                FullName = User.FirstName + " " + User.LastName;
            }
        }
        private double balance;
        public double Balance
        {
            get { return balance; }
            set
            {
                balance = value;
                OnPropertyChanged("Balance");
            }
        }
        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }
        private Order selectedOrder;
        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                selectedOrder = value;
                OnPropertyChanged("SelectedOrder");
            }
        }
        private Command acceptOrder;
        public ICommand AcceptOrder
        {
            get
            {
                return acceptOrder ??
                  (acceptOrder = new Command(obj =>
                  {
                      try
                      {
                          if(selectedOrder.OrderState == Resources.canceled)
                          {
                              switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                              {
                                  case "ru-RU":
                                      {
                                          throw new Exception("Невозможно подтвердить отмененный заказ");
                                      }
                                  default:
                                      {
                                          throw new Exception("Unable to confirm canceled order");
                                      }
                              }
                          }
                          if(selectedOrder.OrderState == Resources.acepted)
                          {
                              switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                              {
                                  case "ru-RU":
                                      {
                                          throw new Exception("Заказ уже подтвержден");
                                      }
                                  default:
                                      {
                                          throw new Exception("Order already confirmed");
                                      }
                              }                              
                          }
                          ConfirmOrderViewModel.orderId = selectedOrder.Order_id;
                          Singleton.getInstance(null).MainViewModel.CurrentViewModel = new ConfirmOrderViewModel();
                      }
                      catch(Exception e)
                      {
                          ErrorMessage = e.Message;
                      }
                  }));
            }
        }
        private Command cancelOrder;
        public ICommand CancelOrder
        {
            get
            {
                return cancelOrder ??
                  (cancelOrder = new Command(obj =>
                  {
                      try
                      {
                          if(selectedOrder.OrderState == Resources.canceled)
                          {
                              switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                              {
                                  case "ru-RU":
                                      {
                                          throw new Exception("Данный заказ уже отменен");
                                      }
                                  default:
                                      {
                                          throw new Exception("This order has already been cancelled");
                                      }
                              }
                          }
                          CancelOrderViewModel.orderId = selectedOrder.Order_id;
                          Singleton.getInstance(null).MainViewModel.CurrentViewModel = new CancelOrderViewModel();
                      }
                      catch(Exception e)
                      {
                          ErrorMessage = e.Message;
                      }
                  }));
            }
        }
        private Command addCard;
        public ICommand AddCard
        {
            get
            {
                return addCard ??
                  (addCard = new Command(obj =>
                  {
                      try
                      {
                          if (App.db.Cards.Where(x => x.User_id == Settings.Default.UserId).FirstOrDefault() != null)
                          {
                              switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                              {
                                  case "ru-RU":
                                      {
                                          throw new Exception("Невозможно привязать еще 1 карту");
                                      }
                                  default:
                                      {
                                          throw new Exception("Unable to link 1 more card");
                                      }
                              }                              
                          }
                          else
                          {
                              AddCardView window = new AddCardView();
                              window.ShowDialog();
                          }
                      }
                      catch(Exception e)
                      {
                          MessageBox.Show(e.Message);
                      }
                  }));
            }
        }
        private Command deleteCard;
        public ICommand DeleteCard
        {
            get
            {
                return deleteCard ??
                  (deleteCard = new Command(obj =>
                  {
                      try
                      {
                          Card card = App.db.Cards.Where(x => x.User_id == Settings.Default.UserId).FirstOrDefault();
                          if (card != null)
                          {
                              App.db.Cards.Remove(card);
                              App.db.SaveChanges();
                              switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                              {
                                  case "ru-RU":
                                      {
                                          App.NotifyWindow(Application.Current.Windows[0]).ShowWarning("Привязка карты была отменена");
                                          break;
                                      }
                                  default:
                                      {
                                          App.NotifyWindow(Application.Current.Windows[0]).ShowWarning("Card linking has been canceled");
                                          break;
                                      }
                              }
                          }
                          else
                          {
                              switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                              {
                                  case "ru-RU":
                                      {
                                          App.NotifyWindow(Application.Current.Windows[0]).ShowError("Невозможно отвязать непривазанную карту");
                                          break;
                                      }
                                  default:
                                      {
                                          App.NotifyWindow(Application.Current.Windows[0]).ShowError("Unable to unlink an unattached card");
                                          break;
                                      }
                              }
                          }
                      }
                      catch (Exception e)
                      {
                          MessageBox.Show(e.Message);
                      }
                  }));
            }
        }
        private Command changePassword;
        public ICommand ChangePassword
        {
            get
            {
                return changePassword ??
                  (changePassword = new Command(obj =>
                  {
                      Singleton.getInstance(null).MainViewModel.CurrentViewModel = new ChangePasswordViewModel();
                  }));
            }
        }
        private Command deposit;
        public ICommand Deposit
        {
            get
            {
                return deposit ??
                  (deposit = new Command(obj =>
                  {
                      try
                      {
                          if (plusBalance > 0)
                          {
                              App.db.Cards.Where(x => x.Card_id == Card.Card_id).FirstOrDefault().Balance += plusBalance;
                              App.db.SaveChanges();
                              switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                              {
                                  case "ru-RU":
                                      {
                                          App.NotifyWindow(Application.Current.Windows[0]).ShowSuccess($"Баланс был пополнен на {plusBalance}");
                                          break;
                                      }
                                  default:
                                      {
                                          App.NotifyWindow(Application.Current.Windows[0]).ShowSuccess($"Баланс был пополнен на {plusBalance}");
                                          break;
                                      }
                              }
                          }
                          else
                          {
                              switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                              {
                                  case "ru-RU":
                                      {
                                          throw new Exception("Невозможно пополнить на отрицательную или нулевую сумму");
                                      }
                                  default:
                                      {
                                          throw new Exception("It isn't possible to top up with a negative or zero amount");
                                      }
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
