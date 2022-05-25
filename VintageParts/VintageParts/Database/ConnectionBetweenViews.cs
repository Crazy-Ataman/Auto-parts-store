using VintageParts.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageParts.Database
{
    public static class ConnectionBetweenViews
    {
        public static ObservableCollection<Part> Parts = new ObservableCollection<Part>();
        public static Part Part = new Part();
    }
}
