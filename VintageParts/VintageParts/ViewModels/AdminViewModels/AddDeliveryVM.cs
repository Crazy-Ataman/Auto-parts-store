using VintageParts.Commands;
using VintageParts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToastNotifications.Messages;

namespace VintageParts.ViewModels.AdminViewModels
{
    public class AddDeliveryVM : ViewModelBase
    {
        public string Description { get; set; }
        public string Price { get; set; }
        public string Name { get; set; }

        private Command addDelivery;
        public ICommand AddDelivery
        {
            get
            {
                return addDelivery ??
                  (addDelivery = new Command(obj =>
                  {
                      try
                      {
                          if (String.IsNullOrEmpty(Name) | String.IsNullOrEmpty(Price) | String.IsNullOrEmpty(Description))
                          {
                              switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                              {
                                  case "ru-RU":
                                      {
                                            throw new Exception("Для добавления должны быть введены все параметры");
                                      }
                                  default:
                                      {
                                            throw new Exception("All parameters must be entered to add");
                                      }
                              }
                          }
                          if (Convert.ToDouble(Price) < 0)
                          {
                              switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                              {
                                  case "ru-RU":
                                      {
                                            throw new Exception("Цена доставки не может быть меньше 0");
                                      }
                                  default:
                                      {
                                            throw new Exception("Delivery price cannot be less than 0");
                                      }
                              }                        
                          }
                          Delivery delivery = new Delivery();
                          delivery.Name = Name;
                          delivery.Price = Convert.ToDouble(Price);
                          delivery.Description = Description;
                          App.db.Deliveries.Add(delivery);
                          App.db.SaveChanges();
                          this.Close();
                          switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                          {
                              case "ru-RU":
                                  {
                                      foreach (Window wind in Application.Current.Windows)
                                      {
                                          if (wind.IsActive == true)
                                          {
                                              App.NotifyWindow(wind).ShowSuccess("Служба доставки была добавлена");
                                          }
                                      }
                                      break;
                                  }
                              default:
                                  {
                                      foreach (Window wind in Application.Current.Windows)
                                      {
                                          if (wind.IsActive == true)
                                          {
                                              App.NotifyWindow(wind).ShowSuccess("Delivery service has been added");
                                          }
                                      }
                                      break;
                                  }
                          }
                      }
                      catch(Exception e)
                      {
                          foreach (Window wind in Application.Current.Windows)
                          {
                              if (wind.IsActive == true)
                              {
                                  App.NotifyWindow(wind).ShowError(e.Message);
                              }
                          }
                      }
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
