using VintageParts.Database;
using VintageParts.Localization;
using VintageParts.Properties;
using VintageParts.SingletonView;
using VintageParts.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace VintageParts
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        CartViewModel a = new CartViewModel();
        public static PartShopDbContext db = new PartShopDbContext();
        private static Language language;
        public static Language Language
        {
            get => language ?? (language = new Language());
        }
        public App()
        {
            InitializeComponent();
            App.Language.Name = VintageParts.Properties.Settings.Default.DefaultLanguage;
        }
        public static Notifier NotifyWindow(Window window)
        {
            Notifier notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: window,
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
            return notifier;
        }
    }
}
