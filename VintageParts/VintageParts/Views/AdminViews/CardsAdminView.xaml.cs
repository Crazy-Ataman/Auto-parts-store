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
    /// Interaction logic for CardsAdminVM.xaml
    /// </summary>
    public partial class CardsAdminVM : UserControl
    {
        public CardsAdminVM()
        {
            InitializeComponent();
        }
        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            switch (propertyDescriptor.DisplayName)
            {
                case "User":
                    {
                        e.Cancel = true;
                        break;
                    }

                case "Card_id":
                case "User_id":
                    {
                        e.Column.IsReadOnly = true;
                        break;
                    }
            }
        }
    }
}
