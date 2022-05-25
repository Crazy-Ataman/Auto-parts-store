using VintageParts.Commands;
using VintageParts.Database;
using VintageParts.Models;
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
    public class FullInfoViewModel : ViewModelBase
    {
        public Part Part { get; set; }
        public Media Media { get; set; }
        public Category Category { get; set; }

        public FullInfoViewModel(Part d)
        {
            Part = d;
            Media = App.db.Medias.Where(x => x.Media_id == Part.Media_id).FirstOrDefault();
            Category = App.db.Categories.Where(x => x.Category_id == Part.Category_id).FirstOrDefault();
        }
        public FullInfoViewModel()
        {

        }
        public Command addToCart;
        public ICommand AddToCart
        {
            get
            {
                return addToCart ??
                  (addToCart = new Command(obj =>
                  {
                      try
                      {
                          Part item = CartViewModel.Parts.Where(x => x.Part_id == Part.Part_id).FirstOrDefault();
                          if (item != null)
                          {
                              item.Amount++;
                          }
                          else
                          {
                              Part.Amount = 1;
                              CartViewModel.Parts.Add(Part);
                          }
                          switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                          {
                              case "ru-RU":
                                  {
                                      App.NotifyWindow(Application.Current.Windows[0]).ShowSuccess("Товар был добавлен в корзину");
                                      break;
                                  }
                              default:
                                  {
                                      App.NotifyWindow(Application.Current.Windows[0]).ShowSuccess("This product was added to the cart");
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
