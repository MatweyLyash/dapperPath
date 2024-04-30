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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel mainViewModel;

        public MainWindow()
        {
            InitializeComponent();
            //mainViewModel = new MainViewModel();
            //DataContext = mainViewModel;
        }

        //private void UpdateStuff()
        //{
        //    var currentStuff = dapperpathEntities.GetContext().Shoes.ToList();
        //    currentStuff = currentStuff.Where(p => p.Title.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
        //    LViewStuff.ItemsSource = currentStuff.OrderBy(p => p.Title).ToList();
        //}

        //private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    UpdateStuff();
        //}
        //private void SortWomen(object Sender, RoutedEventArgs e)
        //{
        //    var currentStuff = dapperpathEntities.GetContext().Shoes.ToList();
        //    currentStuff = currentStuff.Where(x => x.Sex == "W").ToList();
        //    LViewStuff.ItemsSource = currentStuff.OrderBy(p => p.Title).ToList();
        //}
        //private void SortMan(object Sender, RoutedEventArgs e)
        //{
        //    var currentStuff = dapperpathEntities.GetContext().Shoes.ToList();
        //    currentStuff = currentStuff.Where(x => x.Sex == "M").ToList();
        //    LViewStuff.ItemsSource = currentStuff.OrderBy(p => p.Title).ToList();
        //}
        //private void SortKids(object Sender, RoutedEventArgs e)
        //{
        //    var currentStuff = dapperpathEntities.GetContext().Shoes.ToList();
        //    currentStuff = currentStuff.Where(x => x.Sex == "K").ToList();
        //    LViewStuff.ItemsSource = currentStuff.OrderBy(p => p.Title).ToList();
        //}
    }

}
