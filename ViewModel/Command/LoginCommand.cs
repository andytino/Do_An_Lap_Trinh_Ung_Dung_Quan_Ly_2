using Microsoft.Data.SqlClient;
using PosApp.Model;
using PosApp.Services;
using PosApp.Stores;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PosApp.ViewModel.Command
{
    public class LoginCommand : CommandBase
    {
        private readonly LoginViewModel _loginViewModel;
        private readonly ServerSettingStore _serverSettingStore;
        private readonly INavigationService _navigationService;

        public LoginCommand(LoginViewModel loginViewModel, ServerSettingStore serverSettingStore, INavigationService navigationService)
        {
            _loginViewModel = loginViewModel;
            _serverSettingStore = serverSettingStore;
            _navigationService = navigationService;
        }

        public override async void Execute(object parameter)
        {
            var username = _loginViewModel.Username;
            var password = _loginViewModel.Password;
            var serverName = _serverSettingStore.ServerSetting.ServerName;
            var database = _serverSettingStore.ServerSetting.Database;

            

            var builder = new SqlConnectionStringBuilder();

            builder.DataSource = serverName;
            builder.InitialCatalog = database;
            builder.UserID = username;
            builder.Password = password;
            builder.TrustServerCertificate = true;

            var connectionString = builder.ConnectionString;

            var (success, errorMessage, connection) = await Task.Run(() =>
            {
                var __connection = new SqlConnection(connectionString);
                bool success = true;
                string errorMessage = "";

                try
                {
                    __connection.Open();
                }
                catch (Exception ex)
                {
                    success = false;
                    errorMessage = ex.Message;
                }

                return new Tuple<bool, string, SqlConnection>(success, errorMessage, __connection);
            });

            if (success)
            {
                MessageBox.Show("Connect successfully");

                _navigationService.Navigate();
            }
            else
            {
                MessageBox.Show($"Cannot connect: {errorMessage}");
            }
        }

        
    }
}
