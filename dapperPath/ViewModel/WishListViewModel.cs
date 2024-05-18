using dapperPath.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dapperPath.ViewModel
{
    public class WishListViewModel
    {
        public ObservableCollection<Wishlist> UserWishList { get; set; }
        public WishListViewModel()
        {
            UserWishList = new ObservableCollection<Wishlist>(dapperpathEntities.GetContext().Wishlist.Where(w=>w.UserID == ActiveUser.Users.UserID).ToList());


        }
    }
}
