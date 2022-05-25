using VintageParts.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace VintageParts.Views.AdminViews
{
    /// <summary>
    /// Логика взаимодействия для OrdersAdminView.xaml
    /// </summary>
    public partial class OrdersAdminView : UserControl
    {
        public OrdersAdminVM a = new OrdersAdminVM();
        public OrdersAdminView()
        {
            InitializeComponent();
            DataContext = a;
        }
        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            switch(propertyDescriptor.DisplayName)
            {
                case "Parts":
                case "User":
                case "Delivery":
                    {
                        e.Cancel = true;
                        break;
                    }
            }
            e.Column.IsReadOnly = true;
        }
    }
}
