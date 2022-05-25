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
using System.Windows.Shapes;

namespace VintageParts.Views
{
    /// <summary>
    /// Interaction logic for AddCardView.xaml
    /// </summary>
    public partial class AddCardView : Window
    {
        AddCardViewModel a = new AddCardViewModel();
        public AddCardView()
        {
            InitializeComponent();
            DataContext = a;
        }
    }
}
