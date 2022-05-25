using VintageParts.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VintageParts.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        private Command openVk;
        public ICommand OpenVk
        {
            get
            {
                return openVk ??
                 (openVk = new Command(obj =>
                 {
                     Process.Start("https://vk.com/id546952255");
                 }));
            }
        } 
        private Command openSteam;
        public ICommand OpenSteam
        {
            get
            {
                return openSteam ??
                 (openSteam = new Command(obj =>
                 {
                     Process.Start("https://steamcommunity.com/id/pro11/");
                 }));
            }
        }
        private Command openGitHub;
        public ICommand OpenGitHub
        {
            get
            {
                return openGitHub ??
                 (openGitHub = new Command(obj =>
                 {
                     Process.Start("https://github.com/Crazy-Ataman");
                 }));
            }
        }
    }
}
