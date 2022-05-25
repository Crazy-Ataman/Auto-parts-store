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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace VintageParts.ViewModels.AdminViewModels
{
    public class AddPartVM : ViewModelBase
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Provider_id { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string FullDescription { get; set; }
        public string Media_id { get; set; }
        public string Category_id { get; set; }
        public ObservableCollection<Media> Medias { get; set; }
        public ObservableCollection<Brand> Brands { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Provider> Providers { get; set; }
        public AddPartVM()
        {
            Medias = new ObservableCollection<Media>(App.db.Medias);
            Brands = new ObservableCollection<Brand>(App.db.Brands);
            Categories = new ObservableCollection<Category>(App.db.Categories);
            Providers = new ObservableCollection<Provider>(App.db.Providers);
        }
        private Media selectedMedia;
        public Media SelectedMedia
        {
            get { return selectedMedia; }
            set
            {
                selectedMedia = value;
                OnPropertyChanged("SelectedMedia");
            }
        }
        private Brand selectedBrand;
        public Brand SelectedBrand
        {
            get { return selectedBrand; }
            set
            {
                selectedBrand = value;
                OnPropertyChanged("SelectedBrand");
            }
        }
        private Provider selectedProvider;
        public Provider SelectedProvider
        {
            get { return selectedProvider; }
            set
            {
                selectedProvider = value;
                OnPropertyChanged("SelectedProvider");
            }
        }
        private Category selectedCategory;
        public Category SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }
        private Command addPart;
        public ICommand AddPart
        {
            get
            {
                return addPart ??
                  (addPart = new Command(obj =>
                  {
                      try
                      {
                          if(String.IsNullOrEmpty(Name) | String.IsNullOrEmpty(Quantity) | selectedProvider == null| 
                          selectedBrand == null | String.IsNullOrEmpty(Price) | String.IsNullOrEmpty(Description) | 
                          String.IsNullOrEmpty(FullDescription) | selectedCategory == null)
                          {
                              switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                              {
                                  case "ru-RU":
                                      {
                                          throw new Exception("Все ключевые поля должны быть заполнены");
                                      }
                                  default:
                                      {
                                          throw new Exception("All key fields must be completed");
                                      }
                              }
                          }
                          if (Convert.ToInt32(Quantity) <= 0)
                          {
                              switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                              {
                                  case "ru-RU":
                                      {
                                          throw new Exception("Количество не может быть меньше или равна 0");
                                      }
                                  default:
                                      {
                                          throw new Exception("Quantity cannot be less than or equal to 0");
                                      }
                              }
                          }
                          if (Convert.ToDouble(Price) <= 0)
                          {
                              switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                              {
                                  case "ru-RU":
                                      {
                                          throw new Exception("Цена не может быть меньше или равно 0");
                                      }
                                  default:
                                      {
                                          throw new Exception("Price cannot be less than or equal to 0");
                                      }
                              }
                          }

                          Part part = new Part();
                          part.Name = Name;
                          part.Quantity = Convert.ToInt32(Quantity);
                          part.Provider_id = selectedProvider.Provider_id;
                          part.Price = Convert.ToDouble(Price);
                          part.Description = Description;
                          part.FullDescription = FullDescription;
                          part.Media_id = selectedMedia.Media_id;
                          part.Category_id = selectedCategory.Category_id;
                          part.Brand_id = selectedBrand.Brand_id;
                          App.db.Parts.Add(part);
                          App.db.SaveChanges();
                          this.Close();
                          switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                          {
                              case "ru-RU":
                                  {
                                      App.NotifyWindow(Application.Current.Windows[0]).ShowSuccess("Деталь была добавлена");
                                      break;
                                  }
                              default:
                                  {
                                      App.NotifyWindow(Application.Current.Windows[0]).ShowSuccess("Detail has been added");
                                      break;
                                  }
                          }
                      }
                      catch(Exception e)
                      {
                          App.NotifyWindow(Application.Current.Windows[0]).ShowError(e.Message);
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
