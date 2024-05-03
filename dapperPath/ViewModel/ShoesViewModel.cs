using dapperPath.Model;
using dapperPath.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace dapperPath.ViewModel
{
    public class ShoesViewModel : ViewModelBase
    {
        private ObservableCollection<Shoes> _shoes;
        private Visibility _myVisibility = Visibility.Visible;
        public Visibility MyVisibility
        {
            get { return _myVisibility; }
            set
            {
                _myVisibility = value;
                RaisePropertyChanged(nameof(MyVisibility));
            }
        }

        public ObservableCollection<Shoes> Shoes {
            get
            {return _shoes;}
            set
            {
                _shoes = value;
                RaisePropertyChanged(nameof(Shoes));
            }
        }
        public ObservableCollection<Shoes> _totalShoes;
        private List<Shoes> currentStuff;
        public ICommand GetMaleBoots {  get; set; }
        public ICommand GetFemaleBoots { get; set; }
        public ICommand GetAll {  get; set; }

        public ICommand ChangeShoe { get; set; }


        public ShoesViewModel()
        {
            Shoes = new ObservableCollection<Shoes>();
            currentStuff = dapperpathEntities.GetContext().Shoes.ToList();
            _totalShoes = new ObservableCollection<Shoes>(currentStuff);
            Shoes = _totalShoes;

            GetFemaleBoots = new RelayCommand(FilterByFemale);
            GetMaleBoots = new RelayCommand(FilterByMale);
            GetAll = new RelayCommand(ShowAll);
        }
        public void FilterByFemale()
        {
       
            Shoes = new ObservableCollection<Shoes>(_totalShoes.Where(shoe => shoe.Sex == "W"));
        }

        public void FilterByMale()
        {
            Shoes.Clear();
            foreach (var shoe in _totalShoes.Where(shoe => shoe.Sex == "M"))
            {
                Shoes.Add(shoe);
            }
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
    }
}
