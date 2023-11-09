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

namespace PosApp.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private ServerSettingStore _serverSettingStore;

        private string _username;
        private string _password;

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

        public NavigationBarViewModel NavigationBarViewModel { get; }

        public ICommand NavigateServerSettingCommand { get; }
        public ICommand LoginCommand { get; }
        public ICommand LoadedLoginCommand { get; }

        public LoginViewModel(ServerSettingStore serverSettingStore,
            Func<NavigationBarViewModel> createNavigationBarViewModel,
            INavigationService dashboardNavigationService,
            INavigationService serverSettingNavigationService)
        {
            _serverSettingStore = serverSettingStore;
            NavigationBarViewModel = createNavigationBarViewModel();

            NavigateServerSettingCommand = new NavigateCommand(serverSettingNavigationService);

            LoginCommand = new LoginCommand(this, _serverSettingStore, dashboardNavigationService);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
