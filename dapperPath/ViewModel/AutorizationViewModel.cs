using dapperPath.Model;
using dapperPath.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace dapperPath.ViewModel
{
    public class AutorizationViewModel:ViewModelBase
    {
        
        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                RaisePropertyChanged(nameof(Login));
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }
        private Users _adminUser;
        public Users AdminUser
        {
            get { return _adminUser; }
            set
            {
                _adminUser = value;
                RaisePropertyChanged(nameof(AdminUser));
            }
        }
        public ICommand SingIn { get; }
        public ICommand ShowRegistrationCommmad { get; }
        List<Users> users;
        public AutorizationViewModel()
        {
          
            users = dapperpathEntities.GetContext().Users.ToList();
            SingIn = new RelayCommand(SingInUser);
            ShowRegistrationCommmad = new RelayCommand(ShowRegistration);
        }
        private void SingInUser()
        {
            Users user = new Users();
            user.Username = Login;

            if (Password.Length < 8 || !Password.Any(char.IsUpper) || !Password.Any(char.IsLower) || !Password.Any(char.IsDigit))
            {
                MessageBox.Show("Пароль должен содержать более 8 символов, символ нижнего и верхнуго регистра, а также число");
                return;
            }

            string pas = HashPassword(Password);
            if (users.Any(u => u.Username == user.Username && u.Status==true)&&users.Any(u=>u.PasswordHash==pas))
            {
                foreach (var item in users)
                {
                    if(item.Username==user.Username&&item.PasswordHash == pas)
                    {
                        user.UserID = item.UserID;
                    }
                }
                user.IsConnected = true;
                AdminUser = user;
                Users closedAdmin = dapperpathEntities.GetContext().Users.Where(u => u.UserID == AdminUser.UserID).FirstOrDefault();
                closedAdmin.IsConnected = true;
                try
                {
                    dapperpathEntities.GetContext().SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                MainWindow main = new MainWindow(new MainViewModel(AdminUser));
                var window = Application.Current.MainWindow;
                window.Close();
                main.Show();
            }
            else if(users.Any(u => u.Username == user.Username) && users.Any(u => u.PasswordHash == pas))
            {
                MessageBox.Show("привет пользователь");
            }
            else
            {
                MessageBox.Show("неверный логин или пароль");
            }
        }
        private string HashPassword(string password)
        {
            MD5 mD5 = MD5.Create();
            byte[] b = Encoding.UTF8.GetBytes(password);
            byte[] hash = mD5.ComputeHash(b);
            StringBuilder sb = new StringBuilder();
            foreach (var item in hash)
            {
                sb.Append(item.ToString("X2"));
            }
            return sb.ToString();
        }
        private void ShowRegistration()
        {
            Registration registration = new Registration(new RegistrationViewModel());
            var window = Application.Current.MainWindow;
            Application.Current.MainWindow = registration;
            window.Close();
            registration.Show();

        }
    }
}
