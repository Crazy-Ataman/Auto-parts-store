using VintageParts.Commands;
using VintageParts.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using ToastNotifications.Messages;

namespace VintageParts.ViewModels.AdminViewModels
{
    public class CardsAdminVM : ViewModelBase
    {
        public ObservableCollection<Card> Cards { get; set; }
        public ObservableCollection<Card> deletedCards { get; set; }
        public CardsAdminVM()
        {
            Cards = new ObservableCollection<Card>(App.db.Cards);
            deletedCards = new ObservableCollection<Card>();
        }
        private Card selectedCard;
        public Card SelectedCard
        {
            get { return selectedCard; }
            set
            {
                selectedCard = value;
                OnPropertyChanged("SelectedCard");
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
                      if (SelectedCard != null)
                      {
                          Card card = new Card();
                          card = selectedCard;
                          Cards.Remove(card);
                          deletedCards.Add(card);
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
                      foreach (Card i in deletedCards)
                      {
                          App.db.Cards.Remove(i);
                      }
                      App.db.SaveChanges();
                      deletedCards.Clear();
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
    }
}
