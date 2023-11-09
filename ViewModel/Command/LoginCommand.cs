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
        private readonly AccountStore _accountStore;
        private readonly INavigationService<DashboardViewModel> _navigationService;

        public LoginCommand(LoginViewModel loginViewModel, AccountStore accountStore, INavigationService<DashboardViewModel> navigationService)
        {
            _loginViewModel = loginViewModel;
            _accountStore = accountStore;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
    }
}
