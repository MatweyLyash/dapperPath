using dapperPath.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace dapperPath.ViewModel
{
    public class DispatcherViewModel:ViewModelBase
    {
        public ObservableCollection<Users> UsersModel { get; set; } 
        public ICommand DeleteUserCommand { get; }
        public ICommand ChangeBlockUserCommand { get; }
        public DispatcherViewModel()
        {
            UsersModel = new ObservableCollection<Users>(dapperpathEntities.GetContext().Users.Where(u=>u.Status==false).ToList());
            DeleteUserCommand = new RelayCommand<Users>(DeleteUser);
            ChangeBlockUserCommand = new RelayCommand<Users>(ChangeBlock);
        }
        private void DeleteUser(Users user)
        {
            try
            {
                dapperpathEntities.GetContext().Users.Remove(user);
                dapperpathEntities.GetContext().SaveChanges();
                CustomNavigate.RefreshPeak(new View.DispatcherPage(new DispatcherViewModel()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ChangeBlock(Users user)
        {
            Users ChangedUser = dapperpathEntities.GetContext().Users.Where(u => u.UserID == user.UserID).FirstOrDefault();
            if (ChangedUser.IsBanned == false)
            {
                ChangedUser.IsBanned = true;
            }
            else
            {
                ChangedUser.IsBanned = false;
            }
            try
            {
                dapperpathEntities.GetContext().SaveChanges();
                CustomNavigate.RefreshPeak(new View.DispatcherPage(new DispatcherViewModel()));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
