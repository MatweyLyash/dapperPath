using dapperPath.Model;
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
    public class WishListViewModel
    {
        public ICommand BackCommand { get; }
        public ICommand AddBasketCommand { get; }
        public ICommand DeleteFromWishList { get; set; }
        public ObservableCollection<Wishlist> UserWishList { get; set; }
        public WishListViewModel()
        {

            UserWishList = new ObservableCollection<Wishlist>(dapperpathEntities.GetContext().Wishlist.Where(w=>w.UserID == ActiveUser.Users.UserID).ToList());
            foreach (var item in UserWishList)
            {
                if(dapperpathEntities.GetContext().Shoes.Any(s=>s.ProductID ==item.ProductID &&s.AvailableSizes.Contains(item.Size)))
                {
                    item.IsAvailable = true;
                }
                else
                {
                    item.IsAvailable = false;
                }
            }
            BackCommand = new RelayCommand(Back);
            DeleteFromWishList = new RelayCommand<Wishlist>(Delete);
            AddBasketCommand = new RelayCommand<Wishlist>(AddBasket);
        }
        private void Back()
        {
            CustomNavigate.GoBack();
            ShoesViewModel.Instance.RefreshShoes();
        }
        private void Delete(Wishlist deleted)
        {
            Wishlist wishlist = dapperpathEntities.GetContext().Wishlist.Where(w => w.WishlistID == deleted.WishlistID).FirstOrDefault();
            try
            {
                dapperpathEntities.GetContext().Wishlist.Remove(wishlist);
                dapperpathEntities.GetContext().SaveChanges();
                CustomNavigate.RefreshPeak(new View.Wishlist(new WishListViewModel()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddBasket(Wishlist wishlist)
        {
  
            Cart cart = new Cart();
            if (dapperpathEntities.GetContext().Shoes.Any(s => s.AvailableSizes.Contains(wishlist.Size)&&s.ProductID == wishlist.ProductID))
            {
                cart.Size = wishlist.Size;
            }
            else
            {
                MessageBox.Show("Пару с отсутствующим размером нельзя добавить в корзину");
                return;
            }
            
            cart.ProductID = wishlist.ProductID;
            cart.UserID = wishlist.UserID;
            try
            {
                dapperpathEntities.GetContext().Cart.Add(cart);
                dapperpathEntities.GetContext().SaveChanges();

            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
           
            
        }
    }
}
