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
using TheDisappearanceTrick.ViewModel;

namespace TheDisappearanceTrick.Pages
{
    /// <summary>
    /// Логика взаимодействия для Material.xaml
    /// </summary>
    public partial class Material : Page
    {
        public Material()
        {
            DataContext = new ViewModelMaterial();
            InitializeComponent();
        }
        #region pages_click
        private void Client_Click(object sender, RoutedEventArgs e) => NavigationService.Navigate(new Client());
        private void Material_Click(object sender, RoutedEventArgs e) => NavigationService.Navigate(new Material());
        private void Order_Click(object sender, RoutedEventArgs e) => NavigationService.Navigate(new Order());
        private void Product_Click(object sender, RoutedEventArgs e) => NavigationService.Navigate(new Product());
        private void LostSoul_Click(object sender, RoutedEventArgs e) => NavigationService.Navigate(new LostSoul());
        #endregion
    }
}
