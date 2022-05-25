using VintageParts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageParts.SingletonView
{
    public class SingletonAuth
    {
        private static SingletonAuth instance;
        public AuthViewModel StartViewModel { get; set; }
        private SingletonAuth(AuthViewModel authView)
        {
            StartViewModel = authView;
        }
        public static SingletonAuth getInstance(AuthViewModel startViewModel = null)
        {
            return instance ?? (instance = new SingletonAuth(startViewModel));
        }
    }
}
