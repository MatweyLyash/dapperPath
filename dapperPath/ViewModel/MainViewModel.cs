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
        public ICommand Back { get; }
        public ICommand Next { get; }
        public ICommand SetRuCommand { get; }
        public ICommand SetEngCommand { get; }
        public ICommand OpenDispatcherCommand { get; }
        public ICommand OpenWishListCommand { get; }
        public ICommand OpenCartPageCommand { get; }
        public ICommand OpenPersonalCommand { get; }
        public Users Admin { get; set; }
        public ShoesViewModel bootsViewModel { get; }
        public AddEditViewModel addViewModel { get; }
        public ShoesViewModel editViewModel { get;}
        public CurrentShoesViewModel currentShoesViewModel { get; }
        
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
        private Page _currentShoe;
        public Page CurrentShoe
        {
            get { return _currentShoe; }
            set
            {
                _currentShoe = value;
                OnPropertyChanged(nameof(CurrentShoe));
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
        private string _helloUser;
        public string HelloUser
        {
            get { return _helloUser; }
            set
            {
                _helloUser = value;
                OnPropertyChanged(nameof(HelloUser));
            }
        }
        public MainViewModel(Users admin)
        {
            _themes = new ObservableCollection<string>
            {
                "ru",
                "en"
            };
            SelectedTheme = "ru";
            Admin = admin;
            ActiveUser.Users = Admin;
            HelloUser = "Привет, " + Admin.Username;
            NavigateBoots = new RelayCommand(NavigateToBootsPage);
            NavigateToPageAddEdit = new RelayCommand(NavigateToAddEdit);
            OpenWishListCommand = new RelayCommand(OpenWishList);
            OpenCartPageCommand = new RelayCommand(OpenCart);
            OpenDispatcherCommand = new RelayCommand(OpenDispatcher);
            Back = new RelayCommand(GoBack);
            Next = new RelayCommand(GoNext);
            SetRuCommand = new RelayCommand(setRu);
            SetEngCommand = new RelayCommand(setEng);
            OpenPersonalCommand = new RelayCommand(OpenPersonal);
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
        private void OpenDispatcher()
        {
            CustomNavigate.NavigateTo(new DispatcherPage(new DispatcherViewModel()));
        }
        private void GoBack()
        {
            CustomNavigate.GoBack();
            ShoesViewModel.Instance.RefreshShoes();
        }
        private void GoNext()
        {
            CustomNavigate.GoForward();
            
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
        private void setRu()
        {
            SelectedTheme = "ru";
        }
        private void setEng()
        {
            SelectedTheme = "en";
        }
        private void OpenWishList()
        {
            WishListViewModel wishlistViewModel = new WishListViewModel();
            CustomNavigate.NavigateTo(new View.Wishlist(wishlistViewModel));
        }
        private void OpenCart()
        {
            CustomNavigate.NavigateTo(new CardPage(new CardPageViewModel()));
        }
        private void OpenPersonal()
        {
            CustomNavigate.NavigateTo(new PersonalCabinet(new PersonalCabinetViewModel()));
        }
        public void CloseConnect()
        {
            Users closedAdmin = dapperpathEntities.GetContext().Users.Where(u=>u.UserID == Admin.UserID).FirstOrDefault();
            closedAdmin.IsConnected = false;
            try
            {
                dapperpathEntities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Autorization autorization = new Autorization();
            var window = Application.Current.MainWindow;
            window.Close();
            App.Current.MainWindow = autorization;
            autorization.Show();

        }
        private void OnCurrentPageChanged(object sender, PageChangedEventArgs e)
        {
            CurrentPage = e.NewPage;
        }
        private void OnCurrentShoeChanged(object sender, PageChangedEventArgs e)
        {
            CurrentShoe = e.NewPage;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
