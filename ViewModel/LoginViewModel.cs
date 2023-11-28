using PosApp.ViewModel.Command;
using PosApp.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PosApp.Services;
using PosApp.Model;
using System.Windows;
using System.ComponentModel;
using System.Security.Cryptography;

namespace PosApp.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private ServerSettingStore _serverSettingStore;
        private GlobalStore _globalStore;

        private string _username;
        private string _password;
        private bool _rememberPassword;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public bool RememberPassword
        {
            get { return _rememberPassword; }
            set
            {
                _rememberPassword = value;
                OnPropertyChanged(nameof(RememberPassword));
            }
        }

        public NavigationBarViewModel NavigationBarViewModel { get; }

        public ICommand NavigateServerSettingCommand { get; }
        public ICommand LoginCommand { get; }
        public ICommand LoadedLoginCommand { get; }

        public LoginViewModel(GlobalStore globalStore,
            ServerSettingStore serverSettingStore,
            Func<NavigationBarViewModel> createNavigationBarViewModel,
            INavigationService dashboardNavigationService,
            INavigationService serverSettingNavigationService)
        {
            LoadAccountStorage();

            _globalStore = globalStore;

            _serverSettingStore = serverSettingStore;

            NavigationBarViewModel = createNavigationBarViewModel();

            NavigateServerSettingCommand = new NavigateCommand(serverSettingNavigationService);

            LoginCommand = new LoginCommand(this, _globalStore, _serverSettingStore, dashboardNavigationService);

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadAccountStorage()
        {
            var config = System.Configuration.ConfigurationManager.AppSettings;

            //int isRememberPassword = int.Parse(config["RememberPassword"] ?? string.Empty) | 0;

            Username = config["Username"] ?? string.Empty;
            string passwordIn64 = config["Password"] ?? string.Empty;
            var entropyIn64 = config["Entropy"] ?? string.Empty;
            var isRememberPassword = config["RememberPassword"] ?? "0";

            if (passwordIn64.Length > 0)
            {
                var passwordInBytes = Convert.FromBase64String(passwordIn64);
                var entropyInBytes = Convert.FromBase64String(entropyIn64);
                var unencryptedPassword = ProtectedData.Unprotect(passwordInBytes, entropyInBytes, DataProtectionScope.CurrentUser);

                Password = Encoding.UTF8.GetString(unencryptedPassword);

            }

            if(isRememberPassword == "1")
            {
                RememberPassword = true;
            } else
            {
                RememberPassword = false;
            }
            
        }
    }
}
