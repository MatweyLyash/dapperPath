using dapperPath.Model;
using dapperPath.View;
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
   

    public class CardPageViewModel:ViewModelBase
    {
        private double _totalPrice;
        public double TotalPrice
        {
            get
            {
                return _totalPrice;
            }
            set
            {
                _totalPrice = value;
                RaisePropertyChanged(nameof(TotalPrice));   
            }
        }

        public ICommand BackCommand { get; }
        public ICommand DeleteFromCart { get; }
        public ICommand AddPrevCartCommand { get; }
        public ICommand OpenOrderCommand { get; }
        public ObservableCollection<Cart> UserCart { get; set; }
        public ObservableCollection<Cart> PrevCartList { get; set; }
        public CardPageViewModel()
        {
            PrevCartList = new ObservableCollection<Cart>();
            UserCart = new ObservableCollection<Cart>(dapperpathEntities.GetContext().Cart.Where(c => c.UserID == ActiveUser.Users.UserID));
            BackCommand = new RelayCommand(Back);
            DeleteFromCart = new RelayCommand<Cart>(DeleteShoeFromCart);
            AddPrevCartCommand = new RelayCommand<Cart>(AddPrevCart);
            OpenOrderCommand = new RelayCommand(OpenOrder);
        }
        private void Back()
        {
            CustomNavigate.GoBack();
            ShoesViewModel.Instance.RefreshShoes();
        }
        private void DeleteShoeFromCart(Cart cart)
        {
            Cart cartlist = dapperpathEntities.GetContext().Cart.Where(w => w.CartID == cart.CartID).FirstOrDefault();
            try
            {
                dapperpathEntities.GetContext().Cart.Remove(cartlist);
                dapperpathEntities.GetContext().SaveChanges();
                CustomNavigate.RefreshPeak(new CardPage(new CardPageViewModel()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddPrevCart(Cart cart)
        {

            if (cart.Shoes.Sale == 0)
            {
                TotalPrice += (double)cart.Shoes.Price;
            }
            else
            {
                TotalPrice += (double)cart.Shoes.Sale;
            }
            
            PrevCartList.Add(cart);
       }
        private void OpenOrder()
        {
            if (PrevCartList.Count == 0)
            {
                MessageBox.Show("Добавьте обуви в заказ");
            }
            var window = Application.Current.MainWindow;
            Order order = new Order(new OrderViewModel(PrevCartList));
            Application.Current.MainWindow = order;
            order.ShowDialog();
            Application.Current.MainWindow = window;
        }


    }
}
