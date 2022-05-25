using VintageParts.Database;
using VintageParts.Models;
using VintageParts.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VintageParts.Views
{
    /// <summary>
    /// Логика взаимодействия для HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        HomeViewModel homeV = new HomeViewModel();
        public HomeView()
        {
            InitializeComponent();
            DataContext = homeV;
        }

        private void partList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            homeV.id = partList.SelectedIndex;
        }
    }
}
