using VintageParts.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VintageParts.Views.AdminViews
{
    /// <summary>
    /// Логика взаимодействия для MainAdminView.xaml
    /// </summary>
    public partial class MainAdminView : Window
    {
        MainAdminVM a = new MainAdminVM();
        public MainAdminView()
        {
            InitializeComponent();
            DataContext = a;
        }
    }
}
