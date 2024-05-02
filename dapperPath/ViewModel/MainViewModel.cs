using dapperPath.Model;
using dapperPath.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace dapperPath.ViewModel
{
    public class MainViewModel:ViewModelBase , INotifyPropertyChanged
    {

        private Page _currentPage;

        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public ICommand NavigateBoots { get; }
        public ICommand NavigateToPageAddEdit {  get; }
        public ShoesViewModel bootsViewModel { get; }
        public AddEditViewModel addViewModel { get; }

        public AddShoes add { get; set; }

        public MainViewModel()
        {
            NavigateBoots = new RelayCommand(NavigateToBootsPage);
            NavigateToPageAddEdit = new RelayCommand(NavigateToAddEdit);
            bootsViewModel = new ShoesViewModel();
            addViewModel = new AddEditViewModel();
            add = new AddShoes(addViewModel);
            CurrentPage = new ShoesPage(bootsViewModel);

            //CurrentPage = new AddShoes(addViewModel);
        }

        private void NavigateToBootsPage()
        {
            CurrentPage = new ShoesPage(bootsViewModel);
        }
        private void NavigateToAddEdit()
        {
            //CurrentPage = new AddShoes(addViewModel);
            CurrentPage = add;
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
