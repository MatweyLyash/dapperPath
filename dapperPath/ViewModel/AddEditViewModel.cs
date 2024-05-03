using dapperPath.Model;
using dapperPath.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace dapperPath.ViewModel
{
    public class AddEditViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        public ICommand NavigateBackCommand { get; }
        public ICommand SaveBootsCommand { get; }
        public AddShoes addShoes {  get; }
        private Shoes _currnetShoes = new Shoes();
        public AddEditViewModel(Shoes shoes)
        {
            _currnetShoes = shoes;
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


        public AddEditViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            SaveBootsCommand = new RelayCommand(SaveBoots);
            NavigateBackCommand = new RelayCommand(_navigationService.NavigateBack);
        }
        public void SaveBoots()
        {
            _currnetShoes.Title = Title;
            _currnetShoes.Brand = Brand;
            _currnetShoes.AvailableSizes = AvaibilitySizes;
            _currnetShoes.Description = Description;
            _currnetShoes.Image = Image;
            _currnetShoes.Price = Price;
            _currnetShoes.Sex = Sex;
            _currnetShoes.Category = Category;
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currnetShoes.Title))
            {
                error.AppendLine("Укажите название пары");
            }
            if (string.IsNullOrWhiteSpace(_currnetShoes.Brand))
            {
                error.AppendLine("Укажите бренд пары");
            }
            if (string.IsNullOrWhiteSpace(_currnetShoes.Description))
            {
                error.AppendLine("Укажите описание пары");
            }
            if (string.IsNullOrWhiteSpace(_currnetShoes.Category))
            {
                error.AppendLine("Укажите категорию пары");
            }
            if (string.IsNullOrWhiteSpace(_currnetShoes.Price.ToString()))
            {
                error.AppendLine("Укажите цену пары");
            }
            if (string.IsNullOrWhiteSpace(_currnetShoes.Sex))
            {
                error.AppendLine("Укажите пол целевого покупателя");
            }

            if(error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            
            if (_currnetShoes.ProductID == 0)
            {
               dapperpathEntities.GetContext().Shoes.Add(_currnetShoes);
            }
            try
            {
                dapperpathEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                _navigationService.NavigateBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        
    }
}
