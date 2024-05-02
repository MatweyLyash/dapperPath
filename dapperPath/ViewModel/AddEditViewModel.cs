using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace dapperPath.ViewModel
{
    public class AddEditViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

        public AddEditViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateBackCommand = new RelayCommand(_navigationService.NavigateBack);
        }

        public ICommand NavigateBackCommand { get; }
    }
}
