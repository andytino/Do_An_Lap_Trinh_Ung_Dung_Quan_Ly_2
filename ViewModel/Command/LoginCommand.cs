﻿using Microsoft.Data.SqlClient;
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
        private readonly GlobalStore _globalStore;
        private readonly ServerSettingStore _serverSettingStore;
        private readonly INavigationService _navigationService;

        public LoginCommand(LoginViewModel loginViewModel,
            GlobalStore globalStore,
            ServerSettingStore serverSettingStore,
            INavigationService navigationService)
        {
            _loginViewModel = loginViewModel;
            _globalStore = globalStore;
            _serverSettingStore = serverSettingStore;
            _navigationService = navigationService;
        }

        public override async void Execute(object parameter)
        {
            var username = _loginViewModel.Username;
            var password = _loginViewModel.Password ?? String.Empty;
            var serverName = _serverSettingStore.ServerSetting.ServerName;
            var database = _serverSettingStore.ServerSetting.Database;
            var rememberPassword = _loginViewModel.RememberPassword;

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
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                if (rememberPassword)
                {
                    var passwordInBytes = Encoding.UTF8.GetBytes(password);
                    var entropy = new byte[20];

                    using (RNGCryptoServiceProvider rng = new())
                    {
                        rng.GetBytes(entropy);
                    }
                    var cypherText = ProtectedData.Protect(passwordInBytes, entropy, DataProtectionScope.CurrentUser);

                    config.AppSettings.Settings["Username"].Value = username;
                    config.AppSettings.Settings["Password"].Value = Convert.ToBase64String(cypherText);
                    config.AppSettings.Settings["Entropy"].Value = Convert.ToBase64String(entropy);
                    config.AppSettings.Settings["RememberPassword"].Value = "1";
                }
                else
                {
                    config.AppSettings.Settings["Username"].Value = "";
                    config.AppSettings.Settings["Password"].Value = "";
                    config.AppSettings.Settings["Entropy"].Value = "";
                    config.AppSettings.Settings["RememberPassword"].Value = "0";
                }

                config.Save(ConfigurationSaveMode.Full);
                ConfigurationManager.RefreshSection("appSettings");

                Global global = new()
                {
                    Connection = connection
                };

                _globalStore._currentGlobal = global;

                _navigationService.Navigate();
            }
            else
            {
                MessageBox.Show($"Cannot connect: {errorMessage}");
            }
        }


    }
}
