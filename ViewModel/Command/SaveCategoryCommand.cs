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
    public class SaveCategoryCommand : CommandBase
    {
        private readonly AddCategoryViewModel _addCategoryViewModel;
        private readonly GlobalStore _globalStore;
        private readonly INavigationService _navigationService;
        private SqlConnection _connection;

        public SaveCategoryCommand(AddCategoryViewModel addCategoryViewModel,
            INavigationService navigationService,
            GlobalStore globalStore)
        {
            _addCategoryViewModel = addCategoryViewModel;
            _navigationService = navigationService;
            _connection = globalStore.CurrentGlobal.Connection;
        }

        public override void Execute(object parameter)
        {
            string destinationFolder = ImageHelper.ImportImageToFolder("Categories", _addCategoryViewModel.CategoryImageUrl);

            string insertImageSql = """
                        INSERT INTO Images (ImageID, ImagePath)
                        VALUES (@ImageID, @ImagePath)
                    """;
            var guidImageUrl = Guid.NewGuid().ToString();

            var insertImageCommand = new SqlCommand(insertImageSql, _connection);

            insertImageCommand.Parameters.Add("@ImageID", System.Data.SqlDbType.VarChar, 50).Value = guidImageUrl;
            insertImageCommand.Parameters.Add("@ImagePath", System.Data.SqlDbType.NVarChar, int.MaxValue).Value = destinationFolder;

            string sql = """
                        INSERT INTO Categories (CategoryID, CategoryName, Description, ImageID)
                        VALUES (@CategoryID, @CategoryName, @Description, @ImageID);
                    """;

            int imageRows = insertImageCommand.ExecuteNonQuery();

            if (imageRows > 0)
            {
                var guidCategory = Guid.NewGuid().ToString();

                var command = new SqlCommand(sql, _connection);
                command.Parameters.Add("@CategoryID", System.Data.SqlDbType.VarChar, 50).Value = guidCategory;
                command.Parameters.Add("@CategoryName", System.Data.SqlDbType.NVarChar, 100).Value = _addCategoryViewModel.CategoryName;
                command.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar, 200).Value = _addCategoryViewModel.CategoryDescription;
                command.Parameters.Add("@ImageID", System.Data.SqlDbType.VarChar, 50).Value = guidImageUrl;

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
