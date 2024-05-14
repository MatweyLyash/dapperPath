using dapperPath.Model;
using dapperPath.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace dapperPath.ViewModel
{
    public class ShoesViewModel : ViewModelBase , INotifyPropertyChanged
    {
        private static ShoesViewModel _instance;
        private static readonly object _lock = new object();
        public static ShoesViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ShoesViewModel();
                        }
                    }
                }
                return _instance;
            }
        }

        private ObservableCollection<Shoes> _shoes;

        public ObservableCollection<Shoes> Shoes
        {
            get
            { return _shoes; }
            set
            {
                _shoes = value;
                RaisePropertyChanged(nameof(Shoes));
            }
        }
        public ObservableCollection<Shoes> _totalShoes;
        private List<Shoes> currentStuff;
        public ICommand GetMaleBoots { get; set; }
        public ICommand GetFemaleBoots { get; set; }
        public ICommand GetSneakers { get; set; }
        public ICommand GetBoots { get; set; }
        public ICommand GetPumps { get; set; }
        public ICommand GetAccessories { get; set; }
        public ICommand GetSport { get; set; }
        public ICommand EditShoesCommand { get; set; }
        public ICommand DeleteShoesCommand { get; set; }
        public ICommand GetAll { get; set; }
        private Shoes _selectedItem;
        public Shoes selectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(nameof(selectedItem));
            }
        }

     
        public enum FilterMode
        {
            All,
            Female,
            Male
        }
        public string  _filterText;
        private FilterMode _filterMode = FilterMode.All;
        public FilterMode filterMode
        {
            get { return _filterMode; }
            set
            {
                _filterMode = value;
                RaisePropertyChanged(nameof(FilterMode));
                FilterShoes(_filterText);
            }
        }


        public ShoesViewModel()
        {
            Shoes = new ObservableCollection<Shoes>();
            currentStuff = dapperpathEntities.GetContext().Shoes.ToList();
            _totalShoes = new ObservableCollection<Shoes>(currentStuff);
            Shoes = _totalShoes;
            DeleteShoesCommand = new RelayCommand<Shoes>(DeleteShoes);
            EditShoesCommand = new RelayCommand<Shoes>(EditShoes);

            GetSneakers = new RelayCommand(FilterBySneakers);
            GetBoots = new RelayCommand(FilterByBoots);
            GetPumps = new RelayCommand(FilterByPumps);
            GetAccessories = new RelayCommand(FilterByAccessories);
            GetSport = new RelayCommand(FilterBySport);

            GetFemaleBoots = new RelayCommand(() => filterMode = FilterMode.Female);
            GetMaleBoots = new RelayCommand(() => filterMode = FilterMode.Male);
            GetAll = new RelayCommand(() => filterMode = FilterMode.All);
        }
        public void FilterByFemale()
        {
            Shoes = new ObservableCollection<Shoes>(_totalShoes.Where(shoe => shoe.Sex == "W"));
        }

        public void FilterByMale()
        {
            Shoes = new ObservableCollection<Shoes>(_totalShoes.Where(shoe => shoe.Sex == "M"));
        }
        public void ShowAll()
        {
            Shoes = new ObservableCollection<Shoes>(currentStuff);
        }
        public void FilterBySneakers()
        {
            Shoes = new ObservableCollection<Shoes>(_totalShoes.Where(shoe => shoe.CategoryID == 1));
            OnPropertyChanged(nameof(Shoes));
        }
        public void FilterByBoots()
        {
            Shoes = new ObservableCollection<Shoes>(_totalShoes.Where(shoe => shoe.CategoryID == 2));
            OnPropertyChanged(nameof(Shoes));
        }
        public void FilterBySport()
        {
            Shoes = new ObservableCollection<Shoes>(_totalShoes.Where(shoe => shoe.CategoryID == 3));
            OnPropertyChanged(nameof(Shoes));

        }
        public void FilterByAccessories()
        {
            Shoes = new ObservableCollection<Shoes>(_totalShoes.Where(shoe => shoe.CategoryID == 4));
            OnPropertyChanged(nameof(Shoes));

        }
        public void FilterByPumps()
        {
            Shoes = new ObservableCollection<Shoes>(_totalShoes.Where(shoe => shoe.CategoryID == 5));
            OnPropertyChanged(nameof(Shoes));
        }
        public void FilterShoes(string filterText)
        {
              _filterText = filterText;

            switch (filterMode)
            {
                case FilterMode.All:
                    if (_filterText == null)
                    {
                        ShowAll();
                    }
                    else
                    {
                        Shoes = new ObservableCollection<Shoes>(currentStuff.Where(s => s.Title.ToLower().Contains(filterText.ToLower())).ToList());
                    }
                    break;
                case FilterMode.Female:
                    if (_filterText == null)
                    {
                        FilterByFemale();
                    }
                    else
                    {
                        Shoes = new ObservableCollection<Shoes>(currentStuff.Where(s => s.Sex == "W" && s.Title.ToLower().Contains(filterText.ToLower())).ToList());
                    }
                    break;
                case FilterMode.Male:
                    if (_filterText == null)
                    {
                        FilterByMale();
                    }
                    else
                    {
                        Shoes = new ObservableCollection<Shoes>(currentStuff.Where(s => s.Sex == "M" && s.Title.ToLower().Contains(filterText.ToLower())).ToList());
                    }
                    
                    break;
            }

            OnPropertyChanged(nameof(Shoes));
        }

        public void EditShoes(Shoes shoe)
        {
                CustomNavigate.NavigateTo(new AddShoes(new AddEditViewModel(shoe)));
        }
        public void RefreshShoes()
        {
            currentStuff = dapperpathEntities.GetContext().Shoes.ToList();
            Shoes = new ObservableCollection<Shoes>(currentStuff);
            CustomNavigate.RefreshPeak(new ShoesPage(new ShoesViewModel()));
        }
        private void DeleteShoes(Shoes shoes)
        {
            try
            {
                Shoes Shoes = dapperpathEntities.GetContext().Shoes.Where(s => s.ProductID ==shoes.ProductID).FirstOrDefault();
                dapperpathEntities.GetContext().Shoes.Remove(Shoes);
                dapperpathEntities.GetContext().SaveChanges();
                MessageBox.Show("наверное удалено");
                CustomNavigate.RefreshPeak(new ShoesPage(new ShoesViewModel()));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
