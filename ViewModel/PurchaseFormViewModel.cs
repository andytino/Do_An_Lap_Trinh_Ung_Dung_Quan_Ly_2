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

namespace PosApp.ViewModel
{
    public class PurchaseFormViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private GlobalStore _globalStore;

        private ObservableCollection<PurchaseDetail> _purchaseDetail;
        private ObservableCollection<Supplier> _suppliers;
        private ObservableCollection<PaymentMethod> _paymentMethods;
        private ObservableCollection<Category> _categories;

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

        public ObservableCollection<PurchaseDetail> PurchaseDetails
        {
            get { return _purchaseDetail; }
            set
            {
                if (_purchaseDetail != value)
                {
                    _purchaseDetail = value;
                    OnPropertyChanged(nameof(PurchaseDetails));
                }
            }
        }

        public ObservableCollection<Supplier> Suppliers
        {
            get { return _suppliers; }
            set
            {
                if (_suppliers != value)
                {
                    _suppliers = value;
                    OnPropertyChanged(nameof(Suppliers));
                }
            }
        }

        public ObservableCollection<PaymentMethod> PaymentMethods
        {
            get { return _paymentMethods; }
            set
            {
                if (_paymentMethods != value)
                {
                    _paymentMethods = value;
                    OnPropertyChanged(nameof(PaymentMethods));
                }
            }
        }

        private string _purchaseID;
        private string _displayID;
        private string _supplierName;
        private string _supplierID;
        private DateTime _date;
        private string _total;
        private string _description;
        private int _paymentID;
        private int _paymentName;

        public string PurchaseID
        {
            get { return _purchaseID; }
            set
            {
                _purchaseID = value;
                OnPropertyChanged(nameof(PurchaseID));
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
        public string SupplierName
        {
            get => _supplierName;
            set
            {
                _supplierName = value;
                OnPropertyChanged(nameof(SupplierName));
            }
        }
        public string SupplierID
        {
            get => _supplierID;
            set
            {
                _supplierID = value;
                OnPropertyChanged(nameof(SupplierID));
            }
        }
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }
        public string Total
        {
            get => _total;
            set
            {
                _total = value;
                OnPropertyChanged(nameof(Total));
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

        public int PaymentID
        {
            get => _paymentID;
            set
            {
                _paymentID = value;
                OnPropertyChanged(nameof(PaymentID));
            }
        }

        public int PaymentName
        {
            get => _paymentName;
            set
            {
                _paymentName = value;
                OnPropertyChanged(nameof(PaymentName));
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand AddPurchaseDetailCommand { get; }
        public ICommand RemovePurchaseDetailCommand { get; }

        public ICommand ValidateCommand { get; }

        public PurchaseFormViewModel(string categoryId,
            NavigationStore navigationStore,
            ModalNavigationStore modalNavigationStore,
            GlobalStore globalStore,
            Func<NavigationBarViewModel> CreateNavigationBarViewModel)
        {
            _navigationStore = navigationStore;
            _modalNavigationStore = modalNavigationStore;
            _globalStore = globalStore;

            var closeService = new CloseModalNavigationService(_modalNavigationStore);

            LoadCategories();
            // Set default
            PurchaseDetails = new ObservableCollection<PurchaseDetail>
            {
                new PurchaseDetail { Categories = Categories }
            };

            Date = DateTime.Now;

            LoadSuppliers();
            LoadPaymentMethod();
            SetDefaultPurchaseDetail();

            CompositeNavigationService navigationService = new CompositeNavigationService(
                closeService,
               new MainLayoutNavigationService<PurchasesViewModel>(
                _navigationStore,
                () => new PurchasesViewModel(_navigationStore, _modalNavigationStore, _globalStore, CreateNavigationBarViewModel),
                CreateNavigationBarViewModel
                )
             );

            //if (!string.IsNullOrEmpty(categoryId))
            //{
            //    LoadCategory(categoryId);
            //}
            LoadProducts(globalStore);


            SaveCommand = new SavePurchaseCommand(this, navigationService, globalStore);
            ValidateCommand = new ViewModelCommand(SaveCommand.Execute, CanExecuteValidateCommand);

            CancelCommand = new CloseModalCommand(closeService);

            AddPurchaseDetailCommand = new ViewModelCommand(ExecuteAddPurchaseDetailCommand, CanExecuteAddPurchaseDetailCommand);
            RemovePurchaseDetailCommand = new ViewModelCommand(ExecuteRemovePurchaseDetailCommand, CanExecuteRemovePurchaseDetailCommand);
        }

        private bool CanExecuteValidateCommand(object parameter)
        {
            return string.IsNullOrEmpty(Error);
        }

        private void ExecuteAddPurchaseDetailCommand(object parameter)
        {
            if (CanExecuteAddPurchaseDetailCommand(null))
            {
                PurchaseDetails.Add(new PurchaseDetail
                {
                    PurchaseDetailID = Guid.NewGuid().ToString(),
                    Categories = Categories
                });

                foreach (var purchaseDetail in PurchaseDetails)
                {
                    purchaseDetail.LoadProducts(_globalStore);
                }
            }
        }

        private bool CanExecuteAddPurchaseDetailCommand(object parameter)
        {
            return PurchaseDetails != null && Categories != null;
        }

        private void ExecuteRemovePurchaseDetailCommand(object parameter)
        {
            if (parameter is PurchaseDetail purchaseDetailItem)
            {
                PurchaseDetails.Remove(purchaseDetailItem);
            }
        }

        private bool CanExecuteRemovePurchaseDetailCommand(object parameter)
        {
            return PurchaseDetails.Count > 1;
        }

        private void SetDefaultPurchaseDetail()
        {
            PurchaseDetails = new ObservableCollection<PurchaseDetail>
            {
                new PurchaseDetail { Categories = Categories }
            };
        }

        private void LoadSuppliers()
        {
            string sql = """
                        SELECT * 
                        FROM Suppliers
                        WHERE DeletedAt IS NULL;
                """;
            var connection = _globalStore.CurrentGlobal.Connection;
            if (connection != null)
            {
                var command = new SqlCommand(sql, connection);
                var reader = command.ExecuteReader();
                var _suppliers = new ObservableCollection<Supplier>();

                while (reader.Read())
                {
                    Supplier supplier = new Supplier()
                    {
                        SupplierID = (string)reader["SupplierID"],
                        SupplierName = (string)reader["SupplierName"],
                    };

                    _suppliers.Add(supplier);

                }

                reader.Close();

                Suppliers = _suppliers;
            }
        }

        private void LoadPaymentMethod()
        {
            string sql = """
                        SELECT * 
                        FROM PaymentMethods;
                """;
            var connection = _globalStore.CurrentGlobal.Connection;
            if (connection != null)
            {
                var command = new SqlCommand(sql, connection);
                var reader = command.ExecuteReader();
                var _paymentMethods = new ObservableCollection<PaymentMethod>();

                while (reader.Read())
                {
                    PaymentMethod paymentMethod = new PaymentMethod()
                    {
                        PaymentID = (int)reader["PaymentID"],
                        MethodName = (string)reader["MethodName"],
                    };

                    _paymentMethods.Add(paymentMethod);

                }

                reader.Close();

                PaymentMethods = _paymentMethods;
            }
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
                    Category category = new Category()
                    {
                        CategoryID = (string)reader["CategoryID"],
                        CategoryName = (string)reader["CategoryName"],
                    };

                    _categories.Add(category);
                }

                reader.Close();


                Categories = _categories;


            }
        }

        public virtual void LoadProducts(GlobalStore globalStore)
        {
            _globalStore = globalStore;

            foreach (var purchaseDetail in PurchaseDetails)
            {
                purchaseDetail.LoadProducts(_globalStore);
            }
        }

        public string? this[string columnName]
        {
            get
            {
                if (columnName == nameof(DisplayID))
                {
                    return ValidationHelper.ValidateNotEmpty("DisplayID", false, DisplayID);
                }
                if (columnName == nameof(SupplierID))
                {
                    return ValidationHelper.ValidateNotEmpty("Supplier Name", false, SupplierName);
                }

                if (columnName == nameof(PaymentID))
                {
                    return ValidationHelper.ValidateNotEmpty("Supplier Name", false, SupplierName);
                }



                return null;
            }
        }

        public string? Error
        {
            get
            {
                if (!string.IsNullOrEmpty(this[nameof(DisplayID)]) ||
                    !string.IsNullOrEmpty(this[nameof(SupplierName)]) ||
                    !string.IsNullOrEmpty(this[nameof(PaymentName)]))
                {
                    return "Error";
                }
                return null;
            }
        }
    }
}
