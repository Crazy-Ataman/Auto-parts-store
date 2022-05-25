using VintageParts.Commands;
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
using ToastNotifications.Messages;

namespace VintageParts.ViewModels.AdminViewModels
{
    public class MediasDataAdminVM : ViewModelBase
    {
        public ObservableCollection<Media> Medias { get; set; }
        public ObservableCollection<Media> deletedMedias { get; set; }

        public MediasDataAdminVM()
        {
            Medias = new ObservableCollection<Media>(App.db.Medias);
            deletedMedias = new ObservableCollection<Media>();
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

        private Command deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new Command(obj =>
                    {
                        try
                        {
                            if (SelectedMedia != null)
                            {
                                Media media = new Media();
                                media = selectedMedia;
                                Medias.Remove(media);
                                deletedMedias.Add(media);
                                switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                                {
                                    case "ru-RU":
                                        {
                                            foreach (Window wind in Application.Current.Windows)
                                            {
                                                if (wind.IsActive == true)
                                                {
                                                    App.NotifyWindow(wind).ShowWarning("Изображение было удалено");
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
                                                    App.NotifyWindow(wind).ShowWarning("Image has been removed");
                                                }
                                            }
                                            break;
                                        }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
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
                            foreach (Media x in deletedMedias)
                            {
                                App.db.Medias.Remove(x);
                            }
                            App.db.SaveChanges();
                            deletedMedias.Clear();
                            switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                            {
                                case "ru-RU":
                                    {
                                        foreach (Window wind in Application.Current.Windows)
                                        {
                                            if (wind.IsActive == true)
                                            {
                                                App.NotifyWindow(wind).ShowInformation("Данные сохранены");
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
                                                App.NotifyWindow(wind).ShowInformation("Data saved");
                                            }
                                        }
                                        break;
                                    }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }));
            }
        }

        private Command refreshView;
        public ICommand RefreshView
        {
            get
            {
                return refreshView ??
                  (refreshView = new Command(obj =>
                  {
                      SingletonAdmin.getInstance(null).StartViewModel.CurrentViewModel = new DeliveriesAdminVM();
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
