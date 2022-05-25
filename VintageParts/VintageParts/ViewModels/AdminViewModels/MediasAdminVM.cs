using VintageParts.Commands;
using VintageParts.Models;
using VintageParts.Views.AdminViews;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Globalization;
using ToastNotifications.Messages;


namespace VintageParts.ViewModels.AdminViewModels
{
    public class MediasAdminVM : ViewModelBase
    {
        public ObservableCollection<Media> Medias { get; set; }

        public MediasAdminVM()
        {
            Medias = new ObservableCollection<Media>(App.db.Medias);
        }

        public string Name { get; set; }
        public string path;

        public string Path
        {
            get { return path ; }
            set
            {
                path = value;
                OnPropertyChanged("Path");
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
                            OpenFileDialog openFileDialog = new OpenFileDialog();
                            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                            if (openFileDialog.ShowDialog() == true)
                            {
                                Path = openFileDialog.FileName;
                            }
                            if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Path))
                            {
                                switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                                {
                                    case "ru-RU":
                                        {
                                            throw new Exception("Для добавления изображения должно быть введены все параметры");
                                        }
                                    default:
                                        {
                                            throw new Exception("All parameters must be entered to add an image");
                                        }
                                }
                            }
                            Media media = new Media();
                            media.Name = Name;
                            media.Path = Path;
                            App.db.Medias.Add(media);
                            App.db.SaveChanges();
                            switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                            {
                                case "ru-RU":
                                    {
                                        App.NotifyWindow(Application.Current.Windows[0]).ShowSuccess("Изображение было успешно добавлена");
                                        break;
                                    }
                                default:
                                    {
                                        App.NotifyWindow(Application.Current.Windows[0]).ShowSuccess("Image was added successfully");
                                        break;
                                    }
                            }
                        }
                        catch(Exception ex)
                        {
                            App.NotifyWindow(Application.Current.Windows[0]).ShowError(ex.Message);
                        }
                    }));
            }
        }
        private Command showDataImages;
        public ICommand ShowDataImages
        {
            get
            {
                return showDataImages ??
                    (showDataImages = new Command(obj =>
                    {
                        try
                        {
                            if (App.db.Medias.Count() == 0)
                            {
                                switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                                {
                                    case "ru-RU":
                                        {
                                            throw new Exception("Таблица пуста. Показать нечего.");
                                        }
                                    default:
                                        {
                                            throw new Exception("The table is empty. There is nothing to show.");
                                        }
                                }
                            }
                            else
                            {
                                MediasDataAdminView window = new MediasDataAdminView();
                                window.ShowDialog();
                            }
                        }
                        catch (Exception ex)
                        {
                            App.NotifyWindow(Application.Current.Windows[0]).ShowError(ex.Message);
                        }
                    }));
            }
        }
    }
}
