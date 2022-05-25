using VintageParts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageParts.SingletonView
{
    public class Singleton
    {
        private static Singleton instance;
        public MainViewModel MainViewModel { get; set; }
        private Singleton(MainViewModel mainView)
        {
            MainViewModel = mainView;
        }
        public static Singleton getInstance(MainViewModel mainViewModel = null)
        {
            return instance ?? (instance = new Singleton(mainViewModel));
        }
    }
}
