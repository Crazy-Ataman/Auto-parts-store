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
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        LoginViewModel a = new LoginViewModel();
        public LoginView()
        {
            InitializeComponent();
            DataContext = a;
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            a.password = password_textbox.Password;
        }
    }
}
