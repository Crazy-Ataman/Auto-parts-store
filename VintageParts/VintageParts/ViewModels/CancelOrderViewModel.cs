using VintageParts.Commands;
using VintageParts.Models;
using VintageParts.Properties;
using VintageParts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToastNotifications.Messages;

namespace VintageParts.ViewModels
{
    public class CancelOrderViewModel : ViewModelBase 
    {
        public int code;
        public static int orderId;
        public double Sum = 0;
        public Order orderForCancelation;
        public Card userCard;
        public string codeFromView { get; set; }

        public CancelOrderViewModel()
        {
            orderForCancelation = App.db.Orders.Where(x => x.Order_id == orderId).FirstOrDefault();
            userCard = App.db.Cards.Where(x => x.User_id == Settings.Default.UserId).FirstOrDefault();
            Random random = new Random();
            code = random.Next(99999);
            switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "ru-RU":
                    {
                        EmailSenderService.SendCodeRefactor(Settings.Default.UserMail, code, "Код отмены", "Никому не сообщайте данный код! \nКод отмены: ").GetAwaiter();
                        break;
                    }
                default:
                    {
                        EmailSenderService.SendCodeRefactor(Settings.Default.UserMail, code, "Cancellation code", "Do not share this code with anyone! \nCancellation code: ").GetAwaiter();
                        break;
                    }
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
                          if (Convert.ToInt32(codeFromView) == code)
                          {
                              List<OrderedParts> prts = new List<OrderedParts>(App.db.OrderedParts.Where(x => x.Order_id == orderId));
                              foreach (var p in prts)
                              {
                                  App.db.Parts.Where(x => x.Part_id == p.Part_id & p.Order_id == orderId).FirstOrDefault().Quantity += p.Amount;
                                  Sum += App.db.Parts.Where(x => x.Part_id == p.Part_id).FirstOrDefault().Price * p.Amount;
                              }
                              Sum += orderForCancelation.Delivery.Price;
                              orderForCancelation.OrderState = Resources.canceled;
                              userCard.Balance += Sum;
                              App.db.SaveChangesAsync();
                              switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                              {
                                  case "ru-RU":
                                      {
                                          App.NotifyWindow(Application.Current.Windows[0]).ShowSuccess("Ваш заказ отменен");
                                          break;
                                      }
                                  default:
                                      {
                                          App.NotifyWindow(Application.Current.Windows[0]).ShowSuccess("Your order has been cancelled");
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
                                          App.NotifyWindow(Application.Current.Windows[0]).ShowError("Введен неверный код");
                                          break;
                                      }
                                  default:
                                      {
                                          App.NotifyWindow(Application.Current.Windows[0]).ShowError("Invalid code entered");
                                          break;
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
