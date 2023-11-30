using Microsoft.Data.SqlClient;
using PosApp.Helper;
using PosApp.Model;
using PosApp.Services;
using PosApp.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PosApp.ViewModel.Command
{
    public class SaveProductCommand : CommandBase
    {
        private readonly ProductFormViewModel _productFormViewModel;
        private readonly GlobalStore _globalStore;
        private readonly INavigationService _navigationService;
        private SqlConnection _connection;

        public SaveProductCommand(ProductFormViewModel productFormViewModel,
            INavigationService navigationService,
            GlobalStore globalStore)
        {
            _productFormViewModel = productFormViewModel;
            _navigationService = navigationService;
            _connection = globalStore.CurrentGlobal.Connection;
        }

        public override void Execute(object parameter)
        {
            string destinationFolder = ImageHelper.ImportImageToFolder("Products", _productFormViewModel.ProductImageUrl);

            string insertImageSql = """
                        INSERT INTO Images (ImageID, ImagePath)
                        VALUES (@ImageID, @ImagePath)
                    """;
            var guidImageUrl = Guid.NewGuid().ToString();

            var insertImageCommand = new SqlCommand(insertImageSql, _connection);

            insertImageCommand.Parameters.Add("@ImageID", System.Data.SqlDbType.VarChar, 50).Value = guidImageUrl;
            insertImageCommand.Parameters.Add("@ImagePath", System.Data.SqlDbType.NVarChar, int.MaxValue).Value = destinationFolder;
            int imageRows = insertImageCommand.ExecuteNonQuery();

            string insertSql = """
                        INSERT INTO Products (ProductID, DisplayID, ProductName, CategoryID, ImageID, UnitID, Price, Quantity, Quality, Description, CreatedAt, UpdatedAt)
                        VALUES (@ProductID, @DisplayID, @ProductName, @CategoryID, @ImageID, @UnitID, @Price, @Quantity, @Quality, @Description, @CreatedAt, @UpdatedAt)
                    """;

            if (imageRows > 0)
            {
                DateTime currentDateTime = DateTime.Now;
                var guidProductID = Guid.NewGuid().ToString();

                var command = new SqlCommand(insertSql, _connection);
                command.Parameters.Add("@ProductID", System.Data.SqlDbType.VarChar, 50).Value = guidProductID;
                command.Parameters.Add("@DisplayID", System.Data.SqlDbType.VarChar, 50).Value = _productFormViewModel.DisplayID;
                command.Parameters.Add("@ProductName", System.Data.SqlDbType.NVarChar, 100).Value = _productFormViewModel.ProductName;
                command.Parameters.Add("@CategoryID", System.Data.SqlDbType.VarChar, 50).Value = _productFormViewModel.CategoryID;
                command.Parameters.Add("@ImageID", System.Data.SqlDbType.VarChar, 50).Value = guidImageUrl;
                command.Parameters.Add("@UnitID", System.Data.SqlDbType.VarChar, 50).Value = _productFormViewModel.UnitID;
                command.Parameters.Add("@Price", System.Data.SqlDbType.Float).Value = _productFormViewModel.Price;
                command.Parameters.Add("@Quantity", System.Data.SqlDbType.Int).Value = 0;
                command.Parameters.Add("@Quality", System.Data.SqlDbType.NVarChar, 50).Value = "Tot";
                command.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar, 200).Value = !string.IsNullOrEmpty(_productFormViewModel.Description) ? (object)_productFormViewModel.Description : DBNull.Value;
                command.Parameters.Add("@CreatedAt", System.Data.SqlDbType.DateTime).Value = currentDateTime;
                command.Parameters.Add("@UpdatedAt", System.Data.SqlDbType.DateTime).Value = currentDateTime;

                int rows = command.ExecuteNonQuery();

                if (rows > 0)
                {
                    var message = "Successfully inserted new item";
                    MessageBox.Show(message);

                }
                else
                {
                    MessageBox.Show("Insert failed");
                }
            }
            _navigationService.Navigate();
        }
    }
}
