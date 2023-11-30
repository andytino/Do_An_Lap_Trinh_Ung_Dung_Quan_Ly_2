using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using PosApp.Helper;
using PosApp.Model;
using PosApp.Services;
using PosApp.Stores;
using PosApp.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PosApp.ViewModel
{
    public class ProductFormViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly GlobalStore _globalStore;
        private ObservableCollection<Category> _categories;
        private ObservableCollection<Unit> _units;

        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set
            {
                if (_categories != value)
                {
                    _categories = value;
                    OnPropertyChanged(nameof(Categories));
                }
            }
        }

        public ObservableCollection<Unit> Units
        {
            get { return _units; }
            set
            {
                if (_units != value)
                {
                    _units = value;
                    OnPropertyChanged(nameof(Units));
                }
            }
        }

        private string _productID;
        private string _displayID;
        private string _productName;
        private string _categoryID;
        private string _unitID;
        //private string _quantity;
        //private string _price;
        private string _price;

        //private string _quality;
        private string _description;
        private string _productImageUrl;
        //private string _priceUpdateAt;
        private ImageSource _productImageSource;

        public string ProductID
        {
            get { return _productID; }
            set
            {
                _productID = value;
                OnPropertyChanged(nameof(ProductID));
            }
        }

        public string DisplayID
        {
            get { return _displayID; }
            set
            {
                _displayID = value;
                OnPropertyChanged(nameof(DisplayID));
            }
        }
        public string ProductName
        {
            get => _productName;
            set
            {
                _productName = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }

        public string CategoryID
        {
            get => _categoryID;
            set
            {
                _categoryID = value;
                OnPropertyChanged(nameof(CategoryID));
            }
        }
        public string UnitID
        {
            get => _unitID;
            set
            {
                _unitID = value;
                OnPropertyChanged(nameof(UnitID));
            }
        }
       
        //public string? PriceErrorMessage
        //{
        //    get;
        //    private set
        //    {
        //        if(_)
        //    }
        //}
        //public string Price
        public string Price

        {
            get => _price;
            set
            {
                if(_price != value)
                {
                    _price = value;
                    OnPropertyChanged(nameof(Price));
                }
                
               
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string ProductImageUrl
        {
            get { return _productImageUrl; }
            set
            {
                _productImageUrl = value;
                OnPropertyChanged(nameof(ProductImageUrl));
            }
        }
        public ImageSource ProductImageSource
        {
            get { return _productImageSource; }
            set
            {
                _productImageSource = value;
                OnPropertyChanged(nameof(ProductImageSource));
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public ICommand UploadCommand { get; }

        public ProductFormViewModel(string categoryId,
            NavigationStore navigationStore,
            ModalNavigationStore modalNavigationStore,
            GlobalStore globalStore,
            Func<NavigationBarViewModel> CreateNavigationBarViewModel)
        {
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;
            _globalStore = globalStore;

            //Set default
            LoadCategories();
            LoadUnits();

            //
            var closeService = new CloseModalNavigationService(_modalNavigationStore);

            CompositeNavigationService navigationService = new CompositeNavigationService(
                closeService,
               new MainLayoutNavigationService<ProductsViewModel>(
                _navigationStore,
                () => new ProductsViewModel(_navigationStore, _modalNavigationStore, _globalStore, CreateNavigationBarViewModel),
                CreateNavigationBarViewModel
                )
             );

            SaveCommand = new SaveProductCommand(this, navigationService, globalStore);
            CancelCommand = new CloseModalCommand(closeService);

            UploadCommand = new ViewModelCommand(ExecuteUploadCommand, CanExecuteUploadCommand);
        }

        private void ExecuteUploadCommand(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Image Files(*.jpg; *.png; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                ProductImageUrl = openFileDialog.FileName;
                Uri fileUri = new Uri(openFileDialog.FileName);
                ProductImageSource = new BitmapImage(fileUri);
            }
        }

        private bool CanExecuteUploadCommand(object parameter)
        {
            return true;
        }

        

        private void LoadCategories()
        {
            string sql = """
                        SELECT * 
                        FROM Categories
                        WHERE DeletedAt IS NULL;
                """;
            var connection = _globalStore.CurrentGlobal.Connection;
            if (connection != null)
            {
                var command = new SqlCommand(sql, connection);
                var reader = command.ExecuteReader();
                var _categories = new ObservableCollection<Category>();

                while (reader.Read())
                {
                    Category categorie = new Category()
                    {
                        CategoryID = (string)reader["CategoryID"],
                        CategoryName = (string)reader["CategoryName"],
                    };

                    _categories.Add(categorie);

                }

                reader.Close();

                Categories = _categories;
            }
        }

        private void LoadUnits()
        {
            string sql = """
                        SELECT * 
                        FROM Units;
                """;
            var connection = _globalStore.CurrentGlobal.Connection;
            if (connection != null)
            {
                var command = new SqlCommand(sql, connection);
                var reader = command.ExecuteReader();
                var _units = new ObservableCollection<Unit>();

                while (reader.Read())
                {
                    Unit unit = new Unit()
                    {
                        UnitID = (string)reader["UnitID"],
                        UnitName = (string)reader["UnitName"],
                    };

                    _units.Add(unit);

                }

                reader.Close();

                Units = _units;
            }
        }

        public string? this[string columnName]
        {
            get
            {
                if (columnName == nameof(Price))
                {

                    return ValidationHelper.ValidateCurrency("Price", Price);
                }

                return null;
            }
        }

        // Implement IDataErrorInfo
        public string? Error => null;
    }
}
