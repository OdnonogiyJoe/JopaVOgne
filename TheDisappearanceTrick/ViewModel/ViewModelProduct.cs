using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TheDisappearanceTrick.Entities;

namespace TheDisappearanceTrick.ViewModel
{
    class ViewModelProduct : ViewModelBase, INotifyPropertyChanged
    {
        DB DB;

        public ObservableCollection<ProductList> ProductLists { get; set; }
        public ObservableCollection<Workshop> Workshops { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Material> Materials { get; set; }

        void SignalChanged([CallerMemberName] string prop = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        #region Список мини-команд
        public Commands AddProduct { get; set; }
        public Commands SaveProduct { get; set; }
        public Commands DeleteProduct { get; set; }
        #endregion

        public ViewModelProduct()
        {
            DB = DB.GetDb();
            ProductLists = new ObservableCollection<ProductList>(DB.ProductLists);
            Products = new ObservableCollection<Product>(DB.Products);
            Workshops = new ObservableCollection<Workshop>(DB.Workshops);
            Materials = new ObservableCollection<Material>(DB.Materials);
            ProductLists = new ObservableCollection<ProductList>(DB.ProductLists);
                    
            #region Фильтр, поиск

            EmployeesCollectionView = CollectionViewSource.GetDefaultView(Products);
            EmployeesCollectionView.Filter = FilterEmployees;
            //EmployeesCollectionView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Product.Unit))); // эта сортировка группирует по единице товара
            EmployeesCollectionView.SortDescriptions.Add(new SortDescription(nameof(Product.Count), ListSortDirection.Ascending)); // это сортировка по коду товара

            #endregion

            #region Команда с добавлением
            AddProduct = new Commands(() =>
            {

                if (selectedProductList != null)
                {
                    var product = new Product { Name = selectedProductList.Name, Price = selectedProductList.Price, Name1 = selectedWorkshop.Name1, Weight = selectedProductList.Weight, Materials = selectedMaterial, };
                    DB.Products.Add(product);
                    SelectedProduct = product;
                    DB.SaveChanges();
                    LoadProduct();
                    RefreshListView();
                }
                else
                    System.Windows.MessageBox.Show("Выберите что-нибудь");


            });
            #endregion

            #region Команда с сохранением
            SaveProduct = new Commands(() =>
            {
                try
                {
                    DB.SaveChanges();
                    Products = new ObservableCollection<Product>(DB.Products);
                    LoadProduct();
                    RefreshListView();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            });
            #endregion

            #region Команда с удалением
            DeleteProduct = new Commands(() =>
            {
            try
                {

                DB.Products.Remove(selectedProduct);
                DB.SaveChanges();
                LoadProduct();
                    RefreshListView();
                }
                catch(Exception ex)
                {

                    System.Windows.MessageBox.Show("Выберите что удалять");
                }
            });
        }
            #endregion

      

        private Product selectedProduct;
        private ProductList selectedProductList;
        private Workshop selectedWorkshop;
        private Material selectedMaterial;

        public event PropertyChangedEventHandler PropertyChanged;

        public Product SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                SignalChanged();
            }
        }

        public ProductList SelectedProductList
        {
            get => selectedProductList;
            set
            {
                selectedProductList = value;
                SignalChanged();
            }
        }
        public Material SelectedMaterial
        {
            get => selectedMaterial;
            set
            {
                selectedMaterial = value;
                SignalChanged();
            }
        }
        public Workshop SelectedWorkshop
        {
            get => selectedWorkshop;
            set
            {
                selectedWorkshop = value;
                SignalChanged();
            }
        }

        private void LoadProduct()
        {
            Products = new ObservableCollection<Product>(DB.Products);
            SignalChanged("Products");
        }

        #region поляна для поиска

        public ICollectionView EmployeesCollectionView { get; set; }

        private string _employeesFilter = string.Empty;
        public string EmployeesFilter
        {
            get
            {
                return _employeesFilter;
            }
            set
            {
                _employeesFilter = value;
                OnPropertyChanged(nameof(EmployeesFilter));
                EmployeesCollectionView.Refresh();
            }
        }
        private bool FilterEmployees(object obj)
        {
            if (obj is Product product)
            {
                string pc = product.Id.ToString();
                return product.Name.Contains(EmployeesFilter, StringComparison.InvariantCultureIgnoreCase) || pc.Contains(EmployeesFilter, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        
        private void RefreshListView()
        {
            EmployeesCollectionView = CollectionViewSource.GetDefaultView(Products);
            EmployeesCollectionView.Filter = FilterEmployees;
            //EmployeesCollectionView.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Product.Unit)));
            EmployeesCollectionView.SortDescriptions.Add(new SortDescription(nameof(Product.Count), ListSortDirection.Ascending));
        }

        #endregion


    }

}
