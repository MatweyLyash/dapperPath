using dapperPath.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace dapperPath.ViewModel
{
    public class PersonalCabinetViewModel:ViewModelBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public string AllOrdersSum { get; set; }
        public ICommand BackCommand { get; }
        
        public ObservableCollection<Orders> Orders { get; set; }
        public PersonalCabinetViewModel()
        {

            string id = ActiveUser.Users.UserID.ToString();
            string name = ActiveUser.Users.Username;
            List<Orders> orders = dapperpathEntities.GetContext().Orders.Where(o => o.UserID == ActiveUser.Users.UserID).ToList();
            Orders = new ObservableCollection<Orders>(orders);
            decimal sum=0; 
            int counter=0;
            foreach (var item in Orders)
            {
                sum += (decimal)item.Sum;
                counter++;
            }
            Id = "Номер личного кабинета: " + id;
            Name = "Имя пользователя: " + name;
            Amount = "Количество заказов: " + counter;
            AllOrdersSum = "Сумма заказов: " + sum;
            BackCommand = new RelayCommand(Back);


        }
        private void Back()
        {
            CustomNavigate.GoBack();
        }
    }
}
