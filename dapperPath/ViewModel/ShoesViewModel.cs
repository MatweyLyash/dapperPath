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
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace dapperPath.ViewModel
{
    public class ShoesViewModel : ViewModelBase
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
            //ChangeShoeCommand = new RelayCommand<Shoes>(ChangeShoe);
            DeleteShoesCommand = new RelayCommand(DeleteShoes);
            EditShoesCommand = new RelayCommand(EditShoes);
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
        public void VisibleChangedPage()
        {
            dapperpathEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(s => s.Reload());
            currentStuff = dapperpathEntities.GetContext().Shoes.ToList();
            Shoes = new ObservableCollection<Shoes>(currentStuff);
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

            RaisePropertyChanged(nameof(Shoes));
        }

        public void EditShoes()
        {
            CustomNavigate.NavigateTo(new AddShoes(new AddEditViewModel(selectedItem)));
        }
        public void RefreshShoes()
        {
            currentStuff = dapperpathEntities.GetContext().Shoes.ToList();
            Shoes = new ObservableCollection<Shoes>(currentStuff);
        }
        private void DeleteShoes()
        {
            try
            {
                Shoes shoes = dapperpathEntities.GetContext().Shoes.Where(s => s.ProductID == selectedItem.ProductID).FirstOrDefault();
                dapperpathEntities.GetContext().Shoes.Remove(shoes);
                dapperpathEntities.GetContext().SaveChanges();
                MessageBox.Show("наверное удалено");

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
