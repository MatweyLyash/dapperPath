using dapperPath.Model;
using dapperPath.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dapperPath.View
{
    /// <summary>
    /// Логика взаимодействия для ShoesPage.xaml
    /// </summary>
    public partial class ShoesPage : Page
    {
        private ShoesViewModel BootsViewModel;
        public ShoesPage(ShoesViewModel bootsViewModel)
        {
            InitializeComponent();
            this.BootsViewModel = bootsViewModel;
            DataContext = bootsViewModel;
        }
        private void LBoxStuff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                BootsViewModel.selectedItem = e.AddedItems[0] as Shoes;
            }
        }

        private void Page_isVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                dapperpathEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(entry => entry.Reload());
                LBoxStuff.ItemsSource = dapperpathEntities.GetContext().Shoes.ToList();
            }
        }
    }
}
