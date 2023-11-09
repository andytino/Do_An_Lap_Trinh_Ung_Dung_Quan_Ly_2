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

        public override void Execute(object parameter)
        {
            var username = _loginViewModel.Username;
            var password = _loginViewModel.Password;
            var serverName = _serverSettingStore.ServerSetting.ServerName;
            var database = _serverSettingStore.ServerSetting.Database;

            _navigationService.Navigate();
        }
    }
}
