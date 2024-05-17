using dapperPath.CustomControls;
using dapperPath.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public ICommand BackCommand { get; }
        public CurrentShoesViewModel(Shoes shoes)
        {
            Title = shoes.Title;
            Brand = shoes.Brand;

            Sizes = new ObservableCollection<string>(SizeToList(shoes.AvailableSizes));
            Unsizes = new ObservableCollection<string>(SizeToList(shoes.UnavailableSizes));
            SizesCurrentShoe = new ObservableCollection<SizesClass>();
            UnSizesCurrentShoe = new ObservableCollection<SizesClass>();
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
            Sale = (decimal)shoes.Sale;
            Sex = shoes.Sex;
            Category = shoes.ShoeCategory.CategoryName;
            BackCommand = new RelayCommand(Back);

        }
        public List<string> SizeToList(string sizes)
        {
            string[] numberStrings = sizes.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            List<string> numberList = new List<string>();
            foreach (string numString in numberStrings)
            {
                numberList.Add(numString);
            }
            return numberList;
        }
        private void Back()
        {
            CustomNavigate.GoBack();
        }
    }
}
