using dapperPath.Model;
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
    public class BootsViewModel : ViewModelBase
    {
        private List<Shoes> _shoes;
       

        public List<Shoes> Shoes {
            get
            {
                return _shoes;
            }
            set
            {
                _shoes = value;
                RaisePropertyChanged(nameof(Shoes));
            }
        }
        public List<Shoes> _totalShoes;
        private List<Shoes> currentStuff;
        public ICommand GetMaleBoots {  get; set; }
        public ICommand GetFemaleBoots { get; set; }

        public BootsViewModel()
        {
            Shoes = new List<Shoes>();
            currentStuff = dapperpathEntities.GetContext().Shoes.ToList();
            _totalShoes = currentStuff;
            Shoes = _totalShoes;    

            GetFemaleBoots = new RelayCommand(FilterByFemale);
            GetMaleBoots = new RelayCommand(FilterByMale);
        }
        public void FilterByFemale()
        {
            Shoes.Clear();
            foreach (var shoe in _totalShoes.Where(shoe => shoe.Sex == "W"))
            {
                Shoes.Add(shoe);
            }
        }

        public void FilterByMale()
        {

            Shoes.Clear();
            foreach (var shoe in _totalShoes.Where(shoe => shoe.Sex == "M"))
            {
                Shoes.Add(shoe);
            }
        }



    }
}
