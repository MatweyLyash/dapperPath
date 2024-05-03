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
using System.Windows.Navigation;

namespace dapperPath.ViewModel
{
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {


        private INavigationService _navigationService;
        public ICommand NavigateBoots { get; }
        public ICommand NavigateToPageAddEdit { get; }
        public ICommand NavigatePages { get; }
       
        public ShoesViewModel bootsViewModel { get; }
        public AddEditViewModel addViewModel { get; }

        public CustomNavigate customNavigate = new CustomNavigate();
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
        private ICommand _navigateBackCommand;
        public ICommand NavigateBackCommand
        {
            get
            {
                if (_navigateBackCommand == null)
                {
                    _navigateBackCommand = new RelayCommand(_navigationService.NavigateBack);
                }
                return _navigateBackCommand;
            }
        }
        public MainViewModel()
        {
            _navigationService = new CustomNavigate();
            _navigationService.PageChanged += OnPageChanged;
            NavigateBoots = new RelayCommand(NavigateToBootsPage);
            NavigateToPageAddEdit = new RelayCommand(NavigateToAddEdit);
            bootsViewModel = new ShoesViewModel();
            addViewModel = new AddEditViewModel(_navigationService);
            _navigationService.NavigateTo(new ShoesPage(bootsViewModel));
            
            //VisibleChanged = new RelayCommand(VisibleChangedPage);

        }
        private void OnPageChanged(object sender, PageChangedEventArgs e)
        {
            CurrentPage = e.Page;
        }

        private void NavigateToBootsPage()
        {
            _navigationService.NavigateTo(new ShoesPage(bootsViewModel));
        }
        private void NavigateToAddEdit()
        {
            _navigationService.NavigateTo(new AddShoes(addViewModel));

        }
        






        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
