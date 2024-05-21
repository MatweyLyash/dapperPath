using dapperPath.Model;
using dapperPath.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace dapperPath.ViewModel
{
    public class OrderViewModel:ViewModelBase
    {
        private readonly Regex AdressReg = new Regex(@"^([a-zA-Zа-яА-Я0-9\s]+),\s*([a-zA-Zа-яА-Я0-9\s]+),\s*([a-zA-Zа-яА-Я0-9\s]+),\s*([a-zA-Zа-яА-Я0-9\s]+),\s*([a-zA-Zа-яА-Я0-9\s]+),\s*([a-zA-Zа-яА-Я0-9\s]+)$");
        public ICommand BuyCommand { get; }
        private string _adress;
        public string Adress
        {
            get
            {
                return _adress;
            }
            set
            {
                _adress = value;
                RaisePropertyChanged(nameof(Adress));
            }
        }
        public double TotalPrice {  get; set; }
        private List<string> _shippingCollection;
        public List<string> ShippingCollection
        {
            get { return _shippingCollection; }
            set
            {
                _shippingCollection = value;
                RaisePropertyChanged(nameof(ShippingCollection));
            }
        }
        private List<string> _paymentCollection;
        public List<string> PaymentCollection
        {
            get { return _paymentCollection; }
            set
            {
                _paymentCollection = value;
                RaisePropertyChanged(nameof(PaymentCollection));
            }
        }
        public int ShipID
        {
            get
            {
                Dictionary<string, int> shipDict = new Dictionary<string, int>()
                {
                    {"Курьер",1 },{ "Почтовое отделение",2}
                };
                if (Shipping != null)
                {
                    return shipDict[Shipping];
                }
                else
                {
                    return 0;
                }
            }

        }
        private string _shipping;
        public string Shipping
        {
            get { return _shipping; }
            set
            {
                _shipping = value;
                RaisePropertyChanged(nameof(Shipping));
            }
        }

        public ObservableCollection<Cart> order ;
        public OrderViewModel(ObservableCollection<Cart> carts)
        {
            ShippingCollection = new List<string> { "Курьер", "Почта" };
            PaymentCollection = new List<string> { "Наличными", "Карта" };
            order = carts;
            foreach (var item in carts)
            {
                if (item.Shoes.Sale == 0)
                {
                    TotalPrice += (double)item.Shoes.Price;
                }
                else
                {
                    TotalPrice += (double)item.Shoes.Sale;  
                }
                

            }
            BuyCommand = new RelayCommand(Buy);
        }
        private string _paymentMethod;
        public string PaymentMethod
        {
            get
            {
                return _paymentMethod;
            }
            set
            {
                _paymentMethod = value;
                RaisePropertyChanged(nameof(PaymentMethod));
            }
        }

        private void Buy()
        {
            StringBuilder sb = new StringBuilder();
            Orders orders = new Orders();
            if (Adress == null)
            {
                sb.AppendLine("Введите адрес");
            }
            else
            {
                if (!AdressReg.IsMatch(Adress))
                {
                    sb.AppendLine("Ошибка указания адреса\nПример: Московская область, Москва, Улица Пушкина, Дом 10, 123456");
                }
            }
            
            if (PaymentMethod == null)
            {
                sb.AppendLine("Укажите способ оплаты");
            }
            if (Shipping == null)
            {
                sb.AppendLine("Укажите способ доставки");
            }
            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString());
                return;
            }

            try
            {
                foreach (var item in order)
                {
                    if (item.Shoes.Sale == 0)
                    {
                        orders.Sum = item.Shoes.Price;
                    }
                    else
                    {
                        orders.Sum = item.Shoes.Sale;
                    }
                    orders.ProductID = item.ProductID;
                    orders.ShippingMethod = Shipping;
                    orders.PaymentMethod = PaymentMethod;
                    orders.ShippingAddress = Adress;
                    orders.UserID = ActiveUser.Users.UserID;
                    orders.Size = item.Size;
                    dapperpathEntities.GetContext().Orders.Add(orders);
                    dapperpathEntities.GetContext().SaveChanges();
                    
                }
                MessageBox.Show("Заказ принят!");
                (App.Current.MainWindow as Order).Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
