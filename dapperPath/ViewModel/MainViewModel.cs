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
    public class MainViewModel:ViewModelBase 
    {
        public ShoesViewModel bootsViewModel {  get; }

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
        public ICommand MainFilter { get; }
        public MainViewModel()
        {
            bootsViewModel = new ShoesViewModel();
            NavigateBoots = new RelayCommand(NavigateToBootsPage);

            CurrentPage = new ShoesPage(bootsViewModel);
        }

        private void NavigateToBootsPage()
        {
            CurrentPage = new ShoesPage(bootsViewModel);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
