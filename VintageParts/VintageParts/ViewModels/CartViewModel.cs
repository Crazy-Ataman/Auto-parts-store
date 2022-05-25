using VintageParts.Commands;
using VintageParts.Database;
using VintageParts.Models;
using VintageParts.Properties;
using VintageParts.Services;
using VintageParts.SingletonView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace VintageParts.ViewModels
{
    public class CartViewModel : ViewModelBase
    {
        public static ObservableCollection<Media> Medias { get; set; }
        public static ObservableCollection<Part> Parts { get; set; }
        public static ObservableCollection<Category> Categories { get; set; }
        public static ObservableCollection<Brand> Brands { get; set; }
        public ObservableCollection<Delivery> Deliveries { get; set; }
        public Delivery tmpDelivary = new Delivery { Price = 0 };
        public Card Card { get; set; }
        public CartViewModel()
        {
            Parts = ConnectionBetweenViews.Parts;
            Medias = new ObservableCollection<Media>(App.db.Medias);
            Deliveries = new ObservableCollection<Delivery>(App.db.Deliveries);
            Brands = new ObservableCollection<Brand>(App.db.Brands);
            Categories = new ObservableCollection<Category>(App.db.Categories);
            foreach(Part i in Parts)
            {
                Summary += i.Price * i.Amount;
            }
            Card = App.db.Cards.Where(x => x.User_id == Settings.Default.UserId).FirstOrDefault();
        }

        private Delivery selectedDelivery;
        public Delivery SelectedDelivery
        {
            get { return selectedDelivery; }
            set
            {
                selectedDelivery = value;
                OnPropertyChanged("SelectedDelivery");
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
        private Part selectedPart;
        public Part SelectedPart
        {
            get { return selectedPart; }
            set
            {
                selectedPart = value;
                OnPropertyChanged("SelectedPart");
            }
        }
        private double summary;
        public double Summary
        {
            get { return summary; }
            set
            {
                summary = value;
                OnPropertyChanged("Summary");
            }
        }
        private Command deleteItem;
        public ICommand DeleteItem
        {
            get
            {
                return deleteItem ??
                  (deleteItem = new Command(obj =>
                  {
                      if(selectedPart.Amount > 1)
                      {
                          selectedPart.Amount--;
                          Summary -= selectedPart.Price;
                      }
                      else
                      {
                          Summary -= selectedPart.Price;
                          Parts.Remove(SelectedPart);
                      }
                      switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                      {
                          case "ru-RU":
                              {
                                  App.NotifyWindow(Application.Current.Windows[0]).ShowWarning("Товар был удален из корзины");
                                  break;
                              }
                          default:
                              {
                                  App.NotifyWindow(Application.Current.Windows[0]).ShowWarning("Item has been removed from cart");
                                  break;
                              }
                      }
                  }));
            }
        }

        public Command buyCommand;
        public ICommand BuyCommand
        {
            get
            {
                return buyCommand ??
                  (buyCommand = new Command(obj =>
                  {
                      try
                      {
                          if (Parts.Count() == 0)
                          {
                              switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                              {
                                  case "ru-RU":
                                      {
                                          throw new Exception("Невозможно заказать пустую корзину");
                                      }
                                  default:
                                      {
                                          throw new Exception("Unable to order an empty cart");
                                      }
                              }
                          }
                          else if (Card == null)
                          {
                              switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                              {
                                  case "ru-RU":
                                      {
                                          throw new Exception("Для покупки должна быть привязана карта");
                                      }
                                  default:
                                      {
                                          throw new Exception("Card must be linked to purchase.");
                                      }
                              }
                          }
                          else
                          {
                              AddOrder(Parts);
                          }
                      }
                      catch(Exception e)
                      {
                          ErrorMessage = e.Message;
                          App.NotifyWindow(Application.Current.Windows[0]).ShowError(e.Message);
                      }
                  }));
            }
        }
        public Command deliveryChanged;
        public ICommand DeliveryChanged
        {
            get
            {
                return deliveryChanged ??
                  (deliveryChanged = new Command(obj =>
                  {
                      Summary -= tmpDelivary.Price;
                      tmpDelivary = selectedDelivery;
                      Summary += selectedDelivery.Price;
                  }));
            }
        }
        private Command openFullInfoCommand;
        public ICommand OpenFullInfo
        {
            get
            {
                return openFullInfoCommand ??
                  (openFullInfoCommand = new Command(obj =>
                  {
                      Singleton.getInstance(null).MainViewModel.CurrentViewModel = new FullInfoViewModel(selectedPart);
                  }));
            }
        }

        public void AddOrder(ObservableCollection<Part> parts)
        {
            using (PartShopDbContext db = new PartShopDbContext())
            {
                try
                {
                    if(Card.Balance < Summary)
                    {
                        switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "ru-RU":
                                {
                                    throw new Exception("Недостаточно средств на счете");
                                }
                            default:
                                {
                                    throw new Exception("Insufficient funds on the account");
                                }
                        }
                    }
                    else if( selectedDelivery == null)
                    {
                        switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                        {
                            case "ru-RU":
                                {
                                    throw new Exception("Должна быть выбрана доставка");
                                }
                            default:
                                {
                                    throw new Exception("Delivery must be selected");
                                }
                        }
                    }
                    Order order = new Order();
                    order.OrderDate = DateTime.Now;
                    order.OrderState = Resources.waiting;
                    List<OrderedParts> details = new List<OrderedParts>();
                    foreach (Part i in Parts)                                      
                    {                                                              
                        if(i.Amount > db.Parts.Where(x => x.Part_id == i.Part_id).FirstOrDefault().Quantity)
                        {
                            switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                            {
                                case "ru-RU":
                                    {
                                        throw new Exception($"Товаров {i.Name} недостаточно на складе для заказа");
                                    }
                                default:
                                    {
                                        throw new Exception($"There are not enough {i.Name} items in stock to order");
                                    }
                            }
                        }
                        details.Add(new OrderedParts()
                        {
                            Order_id = order.Order_id,
                            Part_id = i.Part_id,
                            Amount = i.Amount
                        });
                        db.Parts.Where(x => x.Part_id == i.Part_id).FirstOrDefault().Quantity -= i.Amount;
                    }
                    order.Parts = details;
                    order.User_id = Settings.Default.UserId;
                    order.Delivery_id = selectedDelivery.Delivery_id;
                    db.Orders.Add(order);
                    Card.Balance -= Summary;
                    Parts.Clear();
                    Summary = 0;
                    db.SaveChanges();
                    ConfirmOrderViewModel.orderId = order.Order_id;
                    Singleton.getInstance(null).MainViewModel.CurrentViewModel = new ConfirmOrderViewModel();
                }
                catch(Exception e)
                {
                    ErrorMessage = e.Message;
                }
            }
            
        }
    }
}
