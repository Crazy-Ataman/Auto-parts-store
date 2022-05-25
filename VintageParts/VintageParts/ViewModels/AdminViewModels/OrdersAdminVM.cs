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
    public class OrdersAdminVM : ViewModelBase
    {
        public ObservableCollection<Order> Orders { get; set; }
        public ObservableCollection<Order> deletedOrders { get; set; }
        public OrdersAdminVM()
        {
            Orders = new ObservableCollection<Order>(App.db.Orders);
            deletedOrders = new ObservableCollection<Order>();
        }
        private Command saveCommand;
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
        public Command deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new Command(obj =>
                  {
                      try
                      {
                          if (selectedOrder != null)
                          {
                              Order order = new Order();
                              order = selectedOrder;
                              Orders.Remove(order);
                              deletedOrders.Add(order);
                              switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                              {
                                  case "ru-RU":
                                      {
                                          App.NotifyWindow(Application.Current.Windows[0]).ShowWarning("Заказ был удален");
                                          break;
                                      }
                                  default:
                                      {
                                          App.NotifyWindow(Application.Current.Windows[0]).ShowWarning("The order has been deleted");
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
        public ICommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new Command(obj =>
                  {
                      try
                      {
                          foreach (Order i in deletedOrders)
                          {
                              App.db.Orders.Remove(i);
                          }
                          App.db.SaveChanges();
                          deletedOrders.Clear();
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
                      catch (Exception e)
                      {
                          MessageBox.Show(e.Message);
                      }
                  }));
            }
        }
    }
}
