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
    /// Interaction logic for PartsAdminView.xaml
    /// </summary>
    public partial class PartsAdminView : UserControl
    {
        public PartsAdminVM a = new PartsAdminVM();
        public PartsAdminView()
        {
            InitializeComponent();
            DataContext = a;
        }
        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            switch (propertyDescriptor.DisplayName)
            {
                case "Media":
                case "Brand":
                case "Category":
                case "Provider":
                case "Amount":
                    {
                        e.Cancel = true;
                        break;
                    }

                case "Category_id":
                case "Brand_id":
                case "Media_id":
                case "Provider_id":
                case "Part_id":
                    {
                        e.Column.IsReadOnly = true;
                        break;
                    }
            }
        }
    }
}
