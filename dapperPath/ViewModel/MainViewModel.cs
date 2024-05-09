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
        public ICommand NavigateBoots { get; }
        public ICommand NavigateToPageAddEdit { get; }
        public ICommand NavigatePages { get; }
        public ICommand NavigateToEdit {  get; }

      
        public ShoesViewModel bootsViewModel { get; }
        public AddEditViewModel addViewModel { get; }
        public ShoesViewModel editViewModel { get;}

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
        private string _filterText;
        public string FilterText
        {
            get { return _filterText; }
            set
            {
                _filterText = value;
                OnPropertyChanged(nameof(FilterText));
                bootsViewModel.FilterShoes(value);
            }
        }

        private ObservableCollection<string> _themes;
        public ObservableCollection<string> Themes
        {
            get { return _themes; }
            set { _themes = value; }
        }

        private string _selectedTheme;
        public string SelectedTheme
        {
            get { return _selectedTheme; }
            set
            {
                _selectedTheme = value;
                OnPropertyChanged(nameof(SelectedTheme));
                ChangeTheme(value);
            }
        }

        private ResourceDictionary _currentTheme;
        public ResourceDictionary CurrentTheme
        {
            get { return _currentTheme; }
            set
            {
                _currentTheme = value;
                OnPropertyChanged(nameof(CurrentTheme));
            }
        }
        public MainViewModel()
        {
            _themes = new ObservableCollection<string>
            {
                "ru",
                "en"
            };
            SelectedTheme = "ru";
            
            NavigateBoots = new RelayCommand(NavigateToBootsPage);
            NavigateToPageAddEdit = new RelayCommand(NavigateToAddEdit);
            CustomNavigate.CurrentPageChanged += OnCurrentPageChanged;
            bootsViewModel = new ShoesViewModel();
            addViewModel = new AddEditViewModel(null);            
            CustomNavigate.NavigateTo(new ShoesPage(bootsViewModel));

        }


        private void NavigateToBootsPage()
        {
            CustomNavigate.NavigateTo(new ShoesPage(bootsViewModel));
        }
        public void NavigateToAddEdit()
        {
            CustomNavigate.NavigateTo(new AddShoes(addViewModel));

        }

        private void ChangeTheme(string themeName)
        {
            ResourceDictionary theme = null;

            switch (themeName)
            {
                case "ru":
                    
                    break;
                case "en":
                    
                    break;
            }

            if (theme != null)
            {
                CurrentTheme = theme;
            }
        }
        private void OnCurrentPageChanged(object sender, PageChangedEventArgs e)
        {
            CurrentPage = e.NewPage;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
