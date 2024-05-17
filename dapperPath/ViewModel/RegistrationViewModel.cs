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
    public class RegistrationViewModel:ViewModelBase
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
        public ICommand RegistNow { get; }
        public ICommand ShowAutorizationCommand { get; }
        List<Users> users;
        public RegistrationViewModel()
        {
             users = dapperpathEntities.GetContext().Users.ToList();
            RegistNow = new RelayCommand(RegistrationUser);
            ShowAutorizationCommand = new RelayCommand(ShowAutorization);
        }
        private void RegistrationUser()
        {
            Users user =new Users();
            if (users.Any(u => u.Username == Login))
            {
                MessageBox.Show("Пользователь с таким логином уже существует");
            }
            user.Username = Login;
            
            if (Password.Length < 8 || !Password.Any(char.IsUpper) || !Password.Any(char.IsLower) || !Password.Any(char.IsDigit))
            {
                MessageBox.Show("Пароль должен содержать более 8 символов, символ нижнего и верхнуго регистра, а также число");
                return;
            }
            
            string pas=  HashPassword(Password);
            user.PasswordHash = pas;
            dapperpathEntities.GetContext().Users.Add(user);
            try
            {
                dapperpathEntities.GetContext().SaveChanges();
                MessageBox.Show("Пользователь создан");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
        private void ShowAutorization()
        {
            Autorization autorization = new Autorization();
            var window = Application.Current.MainWindow;
            Application.Current.MainWindow = autorization;
            window.Close();
            autorization.Show();
        }
    }
}
