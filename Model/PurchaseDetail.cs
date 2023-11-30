using Microsoft.Data.SqlClient;
using PosApp.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PosApp.Model
{
    public class PurchaseDetail: INotifyPropertyChanged
    {
        public string? PurchaseDetailID { get; set; }
        public int? Quantity { get; set; }
        public string? Price { get; set; }
        private GlobalStore _globalStore;
        private string? _categoryID { get; set; }
        private string? _productID { get; set; }

        private ObservableCollection<Category> _categories;

        private ObservableCollection<Product> _products;


        public string CategoryID
        {
            get => _categoryID;
            set
            {
                _categoryID = value;
                OnPropertyChanged(nameof(CategoryID));
                LoadProducts(_globalStore);
            }
        }

        public string ProductID
        {
            get => _productID;
            set
            {
                _productID = value;
                OnPropertyChanged(nameof(ProductID));
            }
        }

        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public virtual void LoadProducts(GlobalStore globalStore)
        {
            if(_globalStore == null)
            {
                _globalStore = globalStore;
            }
            

            string sql = $"""
                        SELECT * 
                        FROM Products
                        WHERE CategoryID = '{CategoryID}';
                """;
            var connection = globalStore.CurrentGlobal.Connection;

            if (connection != null)
            {
                var command = new SqlCommand(sql, connection);
                var reader = command.ExecuteReader();
                var _products = new ObservableCollection<Product>();

                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductID = (string)reader["ProductID"],
                        ProductName = (string)reader["ProductName"],
                    };

                    _products.Add(product);
                }

                reader.Close();

                Products = _products;

            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
