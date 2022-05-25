using VintageParts.Commands;
using VintageParts.Properties;
using VintageParts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace VintageParts.ViewModels
{
    public class ChangePasswordViewModel : ViewModelBase
    {
        public string newPassword { get; set; }
        public string repeatPassword { get; set; }
        public int code;
        public int codeFromView { get; set; }

        public ChangePasswordViewModel()
        {

        }
        private Command generteCode;
        public ICommand GenerateCode
        {
            get
            {
                return generteCode ??
                  (generteCode = new Command(obj =>
                  {
                      try
                      {
                          Random random = new Random();
                          code = random.Next(99999);
                          switch (System.Threading.Thread.CurrentThread.CurrentUICulture.Name)
                          {
                              case "ru-RU":
                                  {
                                      EmailSenderService.SendCodeRefactor(Settings.Default.UserMail, code, "Код для смены пароля", "Никому не сообщайте данный код! \nКод для смены пароля: ").GetAwaiter();
                                      break;
                                  }
                              default:
                                  {
                                      EmailSenderService.SendCodeRefactor(Settings.Default.UserMail, code, "Password change code", "Do not share this code with anyone! \nPassword change code: ").GetAwaiter();
                                      break;
                                  }
                          }
                      }
                      catch(Exception e)
                      {
                          MessageBox.Show(e.Message);
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
                          if (newPassword == repeatPassword & code == codeFromView)
                          {
                              App.db.Authorizations.Where(x => x.Auth_id == Settings.Default.AuthId).FirstOrDefault().Password = SecurePassService.Hash(newPassword);
                              App.db.SaveChanges();
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
