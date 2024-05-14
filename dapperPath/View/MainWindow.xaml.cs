using dapperPath.Model;
using dapperPath.ViewModel;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
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

        }

        private void rus(object sender, RoutedEventArgs e)
        {
            App.Language = new CultureInfo("ru");
        }

        private void eng(object sender, RoutedEventArgs e)
        {
            App.Language = new CultureInfo("en");
           
        }
        private void ChangeLightTheme(object sender, RoutedEventArgs e)
        {
            App.ChangeTheme("/Resourses/Theme/light.xaml");
        }
        private void ChangeDarkTheme(object sender, RoutedEventArgs e)
        {
            App.ChangeTheme("/Resourses/Theme/dark.xaml");
        }
    }

}
