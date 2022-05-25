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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace VintageParts.ViewModels
{
    public class ConfirmOrderViewModel : ViewModelBase
    {
        public int code;
        public string codeFromBox { get; set; }
        public static int orderId;
        public Order order = App.db.Orders.Where(x => x.Order_id == orderId).FirstOrDefault();
        public ConfirmOrderViewModel()
        {
            Random rand = new Random();
            code = rand.Next(99999);
            switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "ru-RU":
                    {
                        EmailSenderService.SendCodeRefactor(Settings.Default.UserMail, code, "Код подтверждения", "Никому не сообщайте данный код! \nКод подтверждения: ").GetAwaiter();
                        break;
                    }
                default:
                    {
                        EmailSenderService.SendCodeRefactor(Settings.Default.UserMail, code, "Confirmation code", "Do not share this code with anyone! \nConfirmation code: ").GetAwaiter();
                        break;
                    }
            }
        }
        public Command submitCode;
        public ICommand SubmitCode
        {
            get
            {
                return submitCode ??
                  (submitCode = new Command(obj =>
                  {
                      try
                      {
                          if (code == Convert.ToInt32(codeFromBox))
                          {
                              App.db.Orders.Where(x => x.Order_id == orderId).FirstOrDefault().OrderState = Resources.acepted;
                              App.db.SaveChanges();
                              switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                              {
                                  case "ru-RU":
                                      {
                                          App.NotifyWindow(Application.Current.Windows[0]).ShowSuccess("Ваш заказ был подтвержден");
                                          EmailSenderService.SendTicket(Settings.Default.UserMail, "Чек заказа", EmailSenderService.GenerateTicket(order)).GetAwaiter();
                                          break;
                                      }
                                  default:
                                      {
                                          App.NotifyWindow(Application.Current.Windows[0]).ShowSuccess("Your order has been confirmed");
                                          EmailSenderService.SendTicket(Settings.Default.UserMail, "Order receipt", EmailSenderService.GenerateTicket(order)).GetAwaiter();
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
