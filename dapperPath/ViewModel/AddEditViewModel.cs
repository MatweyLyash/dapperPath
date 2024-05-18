using dapperPath.Model;
using dapperPath.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;


namespace dapperPath.ViewModel
{

    public class AddEditViewModel : ViewModelBase
    {
        public ICommand NavigateBackCommand { get; }
        public ICommand SaveBootsCommand { get; }
        public ICommand OpenDialog { get; }
        private Shoes _currentShoes = new Shoes();
        private List<String> _categoryCollection;
        public List<String> CategoryCollection
        {
            get { return _categoryCollection; }
            set
            {
                _categoryCollection = value;
                RaisePropertyChanged(nameof(CategoryCollection));
            }
        }

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

        private string _avaibilitySizes;
        public string AvaibilitySizes
        {
            get { return _avaibilitySizes; }
            set
            {
                _avaibilitySizes = value;
                RaisePropertyChanged(nameof(AvaibilitySizes));
            }
        }
        private string _unavaibilitySizes;
        public string UnavaibilitySizes
        {
            get { return _unavaibilitySizes; }
            set
            {
                _unavaibilitySizes = value;
                RaisePropertyChanged(nameof(UnavaibilitySizes));
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

        public AddEditViewModel(Shoes selectedItem)
        {
            _categoryCollection = dapperpathEntities.GetContext().ShoeCategory.Select(c => c.CategoryName).ToList();

            if (selectedItem != null)
            {
                _currentShoes = new Shoes
                {
                    ProductID = selectedItem.ProductID,
                    Title = selectedItem.Title,
                    Brand = selectedItem.Brand,
                    UnavailableSizes = selectedItem.UnavailableSizes,
                    AvailableSizes = selectedItem.AvailableSizes,
                    Description = selectedItem.Description,
                    Image = selectedItem.Image,
                    Price = selectedItem.Price,
                    Sale = selectedItem.Sale,
                    Sex = selectedItem.Sex,
                    CategoryID = selectedItem.CategoryID

                };
                Title = selectedItem.Title;
                Brand = selectedItem.Brand;
                UnavaibilitySizes = selectedItem.UnavailableSizes;
                AvaibilitySizes = selectedItem.AvailableSizes;
                Description = selectedItem.Description;
                Image = selectedItem.Image;
                Price = (decimal)selectedItem.Price;
                if(selectedItem.Sale == null)
                {
                    Sale = 0;
                }
                else
                {
                    Sale = (decimal)selectedItem.Sale;

                }
                Sex = selectedItem.Sex;
                Category = selectedItem.ShoeCategory.CategoryName;

            }
            else
            {
                _currentShoes = new Shoes();
            }
            OpenDialog = new RelayCommand(OpenDialogFile);
            SaveBootsCommand = new RelayCommand(SaveBoots);
            NavigateBackCommand = new RelayCommand(Back);
        }
        private void Back()
        {
            CustomNavigate.GoBack();
        }

        public void SaveBoots()
        {
            _currentShoes.Title = Title;
            _currentShoes.Brand = Brand;
            _currentShoes.AvailableSizes = AvaibilitySizes;
            _currentShoes.UnavailableSizes = UnavaibilitySizes;
            _currentShoes.Description = Description;
            _currentShoes.Image = Image;
            _currentShoes.Price = Price;
            _currentShoes.Sex = Sex;
            _currentShoes.CategoryID = CategoryID;

            StringBuilder error = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentShoes.Title))
            {
                error.AppendLine("Укажите название пары");
            }
            if (string.IsNullOrWhiteSpace(_currentShoes.Brand))
            {
                error.AppendLine("Укажите бренд пары");
            }
            if (string.IsNullOrWhiteSpace(_currentShoes.Description))
            {
                error.AppendLine("Укажите описание пары");
            }
            if (_currentShoes.CategoryID == 0)
            {
                error.AppendLine("Укажите категорию пары");
            }
            if (string.IsNullOrWhiteSpace(_currentShoes.Price.ToString()))
            {
                error.AppendLine("Укажите цену пары");
            }
            if (_currentShoes.Sex != "W" && _currentShoes.Sex != "M")
            {
                error.AppendLine("Укажите пол целевого покупателя\n W или M");
            }
            if (string.IsNullOrWhiteSpace(_currentShoes.AvailableSizes))
            {
                error.AppendLine("Укажите доступные размеры");
            }

            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }

            if (_currentShoes.ProductID == 0)
            {
                dapperpathEntities.GetContext().Shoes.Add(_currentShoes);
            }
            else
            {
                Shoes shoes = dapperpathEntities.GetContext().Shoes.Where(s => s.ProductID == _currentShoes.ProductID).FirstOrDefault();
                shoes.Title = Title;
                shoes.Brand = Brand;
                shoes.AvailableSizes = AvaibilitySizes;
                shoes.UnavailableSizes = UnavaibilitySizes;
                shoes.Description = Description;
                shoes.Image = Image;
                shoes.Price = Price;
                shoes.Sale = Sale;
                shoes.Sex = Sex;
                shoes.CategoryID = CategoryID;

            }
            try
            {
                dapperpathEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                CustomNavigate.GoBack();
                ShoesViewModel.Instance.RefreshShoes();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void OpenDialogFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png, *.jpg, *.jpeg, *.gif, *.webp) | *.png; *.jpg; *.jpeg; *.gif;*.webp";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                Image = openFileDialog.FileName;
            }
        }


    }
}
