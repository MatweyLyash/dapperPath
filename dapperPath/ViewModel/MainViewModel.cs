using dapperPath.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace dapperPath.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        public ICommand OnFemaleFilter {  get; set; }
        public ICommand OnMaleFilter { get; set; }

        public List<Shoes> Shoes {
            get
            {
                return BootsViewModel.Shoes;
            }
            set
            {
                BootsViewModel.Shoes = value;
            }
                }


        public BootsViewModel BootsViewModel { get; set; }

        public MainViewModel()
        {
            BootsViewModel = new BootsViewModel();
            OnFemaleFilter = new RelayCommand(() => BootsViewModel.GetFemaleBoots.Execute(null));
            OnMaleFilter = new RelayCommand(() => BootsViewModel.GetMaleBoots.Execute(null));
        }


    }
}
