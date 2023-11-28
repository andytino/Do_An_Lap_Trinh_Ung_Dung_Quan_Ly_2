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
    public class DeleteCategoryCommand : CommandBase
    {
        INavigationService _navigationService;
        private SqlConnection _connection;

        public DeleteCategoryCommand(INavigationService navigationService,GlobalStore globalStore)
        {
            _navigationService = navigationService;
            _connection = globalStore.CurrentGlobal.Connection;
        }

        public override void Execute(object parameter)
        {

            string deleteCategorySql = $"""
                        UPDATE Categories
                        SET DeletedAt = GETDATE()
                        WHERE CategoryID = '{parameter}'
                """;
            var deletedCategoryCommand = new SqlCommand(deleteCategorySql, _connection);

            var rows = deletedCategoryCommand.ExecuteNonQuery();
            if(rows > 0)
            {
                var message = "Successfully deleted item";
                MessageBox.Show(message);

                _navigationService.Navigate();
            }
        }
    }
}
