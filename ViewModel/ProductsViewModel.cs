using Microsoft.Data.SqlClient;
using PosApp.Model;
using PosApp.Services;
using PosApp.Stores;
using PosApp.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PosApp.ViewModel
{

    public class ProductsViewModel : ViewModelBase
    {
        private ObservableCollection<Product> _products;
        private ObservableCollection<bool> _isOpenModal;
        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly GlobalStore _globalStore;
        
        public ObservableCollection<bool> IsOpenModal
        {
            get { return _isOpenModal; }
            set
            {
                _isOpenModal = value;
                OnPropertyChanged(nameof(IsOpenModal));
            }
        }

        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                if (_products != value)
                {
                    _products = value;
                    OnPropertyChanged(nameof(Products));
                }
            }
        }
        public NavigationBarViewModel NavigationBarViewModel { get; }

        public ICommand AddProductCommand { get; }
        public ICommand DeleteProductCommand { get; }
        public ICommand EditProductCommand { get; }

        public ProductsViewModel(NavigationStore navigationStore,
            ModalNavigationStore modalNavigationStore,
            GlobalStore globalStore,
            Func<NavigationBarViewModel> CreateNavigationBarViewModel)
        {
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;
            _globalStore = globalStore;

            var newProductId = "";
            Products = new ObservableCollection<Product>();

            LoadProducts();

            var addProductModalNavigationService =
                new ModalNavigationService<ProductFormViewModel>(
                        _modalNavigationStore,
                        () => new ProductFormViewModel(newProductId, _navigationStore,
                        _modalNavigationStore,
                        _globalStore,
                        CreateNavigationBarViewModel)
                    );

            AddProductCommand = new OpenModalCommand(addProductModalNavigationService);

        }

        private void LoadProducts()
        {
            string sql = """
                        SELECT * 
                        FROM Products;
                """;
            var connection = _globalStore.CurrentGlobal.Connection;
            if (connection != null)
            {
                var command = new SqlCommand(sql, connection);
                var reader = command.ExecuteReader();
                var _products = new ObservableCollection<Product>();

                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        DisplayID = (string)reader["DisplayID"],
                        ProductID = (string)reader["ProductID"],
                        ProductName = (string)reader["ProductName"]
                    };
                    _products.Add(product);
                }

                reader.Close();
                Products = _products;
            }
        }
    }
}
