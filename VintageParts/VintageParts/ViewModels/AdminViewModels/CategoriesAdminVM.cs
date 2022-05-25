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
    public class CategoriesAdminVM : ViewModelBase
    {
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Category> deletedCategories { get; set; }
        public CategoriesAdminVM()
        {
            Categories = new ObservableCollection<Category>(App.db.Categories);
            deletedCategories = new ObservableCollection<Category>();
        }
        public string CategoryName { get; set; }
        public string Description { get; set; }
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
        private Command deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new Command(obj =>
                  {
                      if (SelectedCategory != null)
                      {
                          Category category = new Category();
                          category = selectedCategory;
                          Categories.Remove(category);
                          deletedCategories.Add(category);
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
                      foreach (Category i in deletedCategories)
                      {
                          App.db.Categories.Remove(i);
                      }
                      App.db.SaveChanges();
                      deletedCategories.Clear();
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
                          if (String.IsNullOrEmpty(CategoryName) | String.IsNullOrEmpty(Description))
                          {
                              switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                              {
                                  case "ru-RU":
                                      {
                                          throw new Exception("Для добавления категории должны быть введены все данные");
                                      }
                                  default:
                                      {
                                          throw new Exception("All data must be entered to add a category");
                                      }
                              }
                          }
                          Category category = new Category();
                          category.Name = CategoryName;
                          category.Description = Description;
                          App.db.Categories.Add(category);
                          App.db.SaveChanges();
                          switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                          {
                              case "ru-RU":
                                  {
                                      App.NotifyWindow(Application.Current.Windows[0]).ShowSuccess("Категория была добавлена");
                                      break;
                                  }
                              default:
                                  {
                                      App.NotifyWindow(Application.Current.Windows[0]).ShowSuccess("Category has been added");
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

