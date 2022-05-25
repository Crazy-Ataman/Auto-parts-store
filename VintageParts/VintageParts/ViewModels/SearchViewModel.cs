using VintageParts.Commands;
using VintageParts.Database;
using VintageParts.Models;
using VintageParts.SingletonView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace VintageParts.ViewModels
{
    class SearchViewModel : ViewModelBase
    {
        public ObservableCollection<Media> Medias { get; set; }
        public ObservableCollection<Part> Parts { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Brand> Brands { get; set; }
        private double lowValue;
        public double LowValue
        {
            get { return lowValue; }
            set
            {
                if (value >= 9999)
                {
                    lowValue = 9999;
                    OnPropertyChanged("LowValue");
                }
                else if (value <= 0)
                {
                    lowValue = 0;
                    OnPropertyChanged("LowValue");
                }
                else
                {
                    lowValue = Math.Round(value);
                    OnPropertyChanged("LowValue");
                }
            }
        }
        private double maxValue;
        public double MaxValue
        {
            get { return maxValue; }
            set
            {
                if (value >= 9999)
                {
                    maxValue = 9999;
                    OnPropertyChanged("MaxValue");
                }
                else if (value <= 0)
                {
                    maxValue = 0;
                    OnPropertyChanged("MaxValue");
                }
                else
                {
                    maxValue = Math.Round(value);
                    OnPropertyChanged("MaxValue");
                }
            }
        }
        public string textForSearch { get; set; }
        public SearchViewModel() 
        {
            using (PartShopDbContext db = new PartShopDbContext())
            {
                Medias = new ObservableCollection<Media>(db.Medias);
                Parts = new ObservableCollection<Part>(db.Parts);
                Categories = new ObservableCollection<Category>(db.Categories);
                Brands = new ObservableCollection<Brand>(db.Brands);
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
        public SearchViewModel(ObservableCollection<Part> parts)
        {
            Categories = new ObservableCollection<Category>(App.db.Categories);
            Parts = new ObservableCollection<Part>();
            foreach(Part i in parts)
            {
                Parts.Add(i);
            }
            Medias = new ObservableCollection<Media>();
            foreach (Media i in App.db.Medias)
            {
                Medias.Add(i);
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
        private Command findByName;
        public ICommand FindByName
        {
            get
            {
                return findByName ??
                  (findByName = new Command(obj =>
                  {
                      try 
                      { 
                      Parts = new ObservableCollection<Part>(App.db.Parts.Where(x => x.Name.Contains(textForSearch)));
                      Singleton.getInstance(null).MainViewModel.CurrentViewModel = new SearchViewModel(Parts);
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }
        private Command detailedSearch;
        public ICommand DetailedSearch
        {
            get
            {
                return detailedSearch ??
                  (detailedSearch = new Command(obj =>
                  {
                      try
                      {
                          if (selectedCategory != null)
                          {
                              Parts = new ObservableCollection<Part>(App.db.Parts.Where(x => (x.Category_id == selectedCategory.Category_id) &&
                                                                    x.Price >= lowValue && x.Price <= maxValue));
                          }
                          else
                          {
                              Parts = new ObservableCollection<Part>(App.db.Parts.Where(x =>
                                                                     x.Price >= lowValue && x.Price <= maxValue));
                          }
                          Singleton.getInstance(null).MainViewModel.CurrentViewModel = new SearchViewModel(Parts);
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
