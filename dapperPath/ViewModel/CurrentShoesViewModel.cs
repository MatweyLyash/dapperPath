using dapperPath.CustomControls;
using dapperPath.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace dapperPath.ViewModel
{
    public class CurrentShoesViewModel:ViewModelBase
    {

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        private string _brand;
        public string Brand
        {
            get { return _brand; }
            set
            {
                _brand = value;
                RaisePropertyChanged(nameof(Brand));
            }
        }

        private ObservableCollection<string> _sizes;

        public ObservableCollection<string> Sizes
        {
            get
            { return _sizes; }
            set
            {
                _sizes = value;
                RaisePropertyChanged(nameof(Sizes));
            }
        }
        private ObservableCollection<string> _unSizes;
        public ObservableCollection<string> Unsizes
        {
            get
            {
                return _unSizes;
            }
            set {  _unSizes = value; RaisePropertyChanged(); }
        }
        private ObservableCollection<SizesClass> _allSizes;
        public ObservableCollection<SizesClass> SizesCurrentShoe
        {
            get
            { return _allSizes; }
            set
            {
                _allSizes = value;
                RaisePropertyChanged(nameof(SizesCurrentShoe));
            }
        }
        private ObservableCollection<SizesClass> _unSizesCurrentShoe;
        public ObservableCollection<SizesClass> UnSizesCurrentShoe
        {
            get
            { return _unSizesCurrentShoe; }
            set
            {
                _unSizesCurrentShoe = value;
                RaisePropertyChanged(nameof(UnSizesCurrentShoe));
            }
        }
        private bool _saleVisible;
        public bool SaleVisible
        {
            get
            {
                return _saleVisible;
            }
            set
            {
                _saleVisible = value;
                RaisePropertyChanged(nameof(SaleVisible));
            }
        }


        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        private string _image;
        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;
                RaisePropertyChanged(nameof(Image));
            }
        }

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                RaisePropertyChanged(nameof(Price));
            }
        }
        private decimal _sale;
        public decimal Sale
        {
            get { return _sale; }
            set
            {
                _sale = value;
                RaisePropertyChanged(nameof(Sale));
            }
        }

        private string _sex;
        public string Sex
        {
            get { return _sex; }
            set
            {
                _sex = value;
                RaisePropertyChanged(nameof(Sex));
            }
        }

        public int CategoryID
        {
            get
            {
                Dictionary<string, int> categoriesDict = new Dictionary<string, int>()
                {
                    {"Кроссовки",1 },{ "Ботинки",2},{ "Туфли",5},{ "Спортивная",3},{ "Аксессуары",4}
                };
                if (Category != null)
                {
                    return categoriesDict[Category];
                }
                else
                {
                    return 0;
                }
            }

        }
        private string _category;
        public string Category
        {
            get { return _category; }
            set
            {
                _category = value;
                RaisePropertyChanged(nameof(Category));
            }
        }
        private int _productID;
        public int ProductID
        {
            get
            {
                return _productID;
            }
            set
            {
                _productID = value;
                RaisePropertyChanged(nameof(ProductID));
            }
        }
        private string _myReviw;
        public string MyReview
        {
            get
            {
                return _myReviw;
            }
            set
            {
                _myReviw = value;
                RaisePropertyChanged(nameof(MyReview));
            }
        }
        private Users _user;
        public Users User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                RaisePropertyChanged(nameof(User));
            }
        }
        private Reviews _review;
        public Reviews Review
        {
            get
            {
                return _review;
            }
            set
            {
                _review = value;
                RaisePropertyChanged(nameof(Review));
            }
        }
        private Shoes _currentShoe;
        public Shoes CurrentShoe
        {
            get
            {
                return _currentShoe;
            }
            set
            {
                _currentShoe = value;
                RaisePropertyChanged(nameof(CurrentShoe));
            }
        }
        public ICommand SendReviewCommand { get; }
        public ICommand BackCommand { get; }
        public ObservableCollection<Reviews> Reviews { get; set; }
        public List<Reviews> currentShoeReviewList;
        public CurrentShoesViewModel(Shoes shoes)
        {
            Review = new Reviews();
            CurrentShoe = shoes;
            ProductID = shoes.ProductID;
            Title = shoes.Title;
            Brand = shoes.Brand;
            User = ActiveUser.Users;
            Sizes = new ObservableCollection<string>(SizeToList(shoes.AvailableSizes));
            Unsizes = new ObservableCollection<string>(SizeToListUn(shoes.UnavailableSizes));
            SizesCurrentShoe = new ObservableCollection<SizesClass>();
            UnSizesCurrentShoe = new ObservableCollection<SizesClass>();
            currentShoeReviewList = dapperpathEntities.GetContext().Reviews.Where(r=>r.ProductID == ProductID).Include(r => r.Users).ToList(); 
            Reviews = new ObservableCollection<Reviews>(currentShoeReviewList);
            foreach (var item in Sizes)
            {
                SizesCurrentShoe.Add(new SizesClass(item));
            }
            foreach (var item in Unsizes)
            {
                UnSizesCurrentShoe.Add(new SizesClass(item));
            }

            Description = shoes.Description;
            Image = shoes.Image;
            Price = (decimal)shoes.Price;
            if (shoes.Sale == null)
            {
                Sale = 0;
            }
            else
            {
                Sale = (decimal)shoes.Sale;
            }
            Sex = shoes.Sex;
            Category = shoes.ShoeCategory.CategoryName;
            BackCommand = new RelayCommand(Back);
            SendReviewCommand = new RelayCommand(SendReview);
        }
        public List<string> SizeToList(string sizes)
        {
            List<string> numberList = new List<string>();
            try
            {
                if(sizes.Contains(" "))
                {
                    string[] numberStrings = sizes.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string numString in numberStrings)
                    {
                        numberList.Add(numString);
                    }
                    return numberList;
                }
                else
                {
                    numberList.Add(sizes);
                }
                
            }
            catch
            {
                MessageBox.Show("Неверно указаны размеры обуви для данной пары.");
            }
            return numberList;
           
        }
        public List<string> SizeToListUn(string sizes)
        {
            List<string> numberList = new List<string>();
            try
            {
                if (sizes == null) { return  numberList; }
                else if (sizes.Contains(" "))
                {
                    string[] numberStrings = sizes.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string numString in numberStrings)
                    {
                        numberList.Add(numString);
                    }
                    return numberList;
                }
                else
                {
                    numberList.Add(sizes);
                }

            }
            catch
            {
                MessageBox.Show("Неверно указаны размеры обуви для данной пары.");
            }
            return numberList;

        }
        private void Back()
        {
            CustomNavigate.GoBack();
        }
        private void SendReview()
        {
            Review.ReviewText = MyReview;
            Review.ProductID = CurrentShoe.ProductID;
            Review.UserID = User.UserID;
            
            dapperpathEntities.GetContext().Reviews.Add(Review);
            try
            {
                dapperpathEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
    }
}
