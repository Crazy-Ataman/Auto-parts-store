using VintageParts.Commands;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace VintageParts.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public List<CultureInfo> Langs { get; set; }
        private CultureInfo selectedLanguage;
        private CultureInfo currentLang { get; set; }
        public CultureInfo SelectedLanguage
        {
            get { return selectedLanguage; }
            set
            {
                selectedLanguage = value;
                OnPropertyChanged("SelectedLanguage");
            }
        }
        public SettingsViewModel()
        {
            Langs = new List<CultureInfo>();
            foreach(var i in App.Language.Languages)
            {
                if (i != null)
                {
                    Langs.Add(i);
                }
            }
            if (selectedLanguage != null)
            {
                App.Language.Name = selectedLanguage;
                
            }
            else
            {
                selectedLanguage = Properties.Settings.Default.DefaultLanguage;
            }
        }
        private Command languageChanged;
        public ICommand LanguageChanged
        {
            get
            {
                return languageChanged ??
                  (languageChanged = new Command(obj =>
                  {
                      if (selectedLanguage == null)
                      {
                          selectedLanguage = App.Language.Name;
                      }
                      else
                      {
                          App.Language.Name = selectedLanguage;
                          currentLang = selectedLanguage;
                          Properties.Settings.Default.DefaultLanguage = currentLang;
                          Properties.Settings.Default.Save();
                      }
                  }));
            }
        }
        private Command confirmChange;
        public ICommand ConfirmChange
        {
            get
            {
                return confirmChange ??
                  (confirmChange = new Command(obj =>
                  {
                      try
                      {
                          if (selectedLanguage == null)
                          {
                              selectedLanguage = App.Language.Name;
                          }
                          else
                          {
                              App.Language.Name = selectedLanguage;
                              currentLang = selectedLanguage;
                              VintageParts.Properties.Settings.Default.DefaultLanguage = currentLang;
                              VintageParts.Properties.Settings.Default.Save();
                          }
                      }
                      catch(Exception e)
                      {
                          MessageBox.Show(e.Message);
                      }
                  }));
            }
        }
    }
}
