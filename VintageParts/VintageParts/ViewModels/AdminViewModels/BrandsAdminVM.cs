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
    public class BrandsAdminVM : ViewModelBase
    {
        public ObservableCollection<Brand> Brands { get; set; }
        public ObservableCollection<Brand> deletedBrands { get; set; }
        public BrandsAdminVM()
        {
            Brands = new ObservableCollection<Brand>(App.db.Brands);
            deletedBrands = new ObservableCollection<Brand>();
        }
        public string NewBrand { get; set; }
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
                          if (SelectedBrand != null)
                          {
                              Brand mark = new Brand();
                              mark = selectedBrand;
                              Brands.Remove(mark);
                              deletedBrands.Add(mark);
                              switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                              {
                                  case "ru-RU":
                                      {
                                          App.NotifyWindow(Application.Current.Windows[0]).ShowWarning("Бренд был удален");
                                          break;
                                      }
                                  default:
                                      {
                                          App.NotifyWindow(Application.Current.Windows[0]).ShowWarning("The brand has been removed");
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
                          foreach (Brand i in deletedBrands)
                          {
                              App.db.Brands.Remove(i);
                          }
                          App.db.SaveChanges();
                          deletedBrands.Clear();
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
        private Command addCommand;
        public ICommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new Command(obj =>
                  {
                      try
                      {
                          if (String.IsNullOrEmpty(NewBrand))
                          {
                              switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                              {
                                  case "ru-RU":
                                      {
                                          throw new Exception("Для добавления бренда должно быть введено ее название");
                                      }
                                  default:
                                      {
                                          throw new Exception("To add a brand, its name must be entered");
                                      }
                              }
                          }
                          Brand mark = new Brand();
                          mark.Name = NewBrand;
                          App.db.Brands.Add(mark);
                          App.db.SaveChanges();
                          switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                          {
                              case "ru-RU":
                                  {
                                      App.NotifyWindow(Application.Current.Windows[0]).ShowSuccess("Бренд был успешно добавлена");
                                      break;
                                  }
                              default:
                                  {
                                      App.NotifyWindow(Application.Current.Windows[0]).ShowSuccess("The brand has been successfully added");
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
    }
}
