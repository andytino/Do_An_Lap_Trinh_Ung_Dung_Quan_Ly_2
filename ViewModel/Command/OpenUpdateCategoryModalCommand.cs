using Microsoft.Data.SqlClient;
using PosApp.Model;
using PosApp.Services;
using PosApp.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PosApp.ViewModel.Command
{
    public class OpenUpdateCategoryModalCommand : CommandBase
    {
        private readonly IParameterNavigationService<Category> _navigationService;
        private SqlConnection _connection;

        public OpenUpdateCategoryModalCommand(IParameterNavigationService<Category> navigationService, GlobalStore globalStore)
        {
            _navigationService = navigationService;
            _connection = globalStore.CurrentGlobal.Connection;
        }

        public override void Execute(object parameter)
        {
            string getCategorySql = $"""
                        SELECT *
                        FROM Categories
                        JOIN Images ON Categories.ImageID = Images.ImageID
                        WHERE CategoryID = '{parameter}'
                """;
            
            if(_connection != null)
            {
                var getCategoryCommand = new SqlCommand(getCategorySql, _connection);
                var reader = getCategoryCommand.ExecuteReader();
                if (reader.Read())
                {
                    Category category = new Category()
                    {
                        CategoryID = (string)reader["CategoryID"],
                        CategoryName = (string)reader["CategoryName"],
                        Description = (string)reader["Description"],
                        ImageUrl = (string)reader["ImagePath"]

                    };

                    _navigationService.Navigate(category);
                }

                reader.Close();
            }
        }
    }
}
