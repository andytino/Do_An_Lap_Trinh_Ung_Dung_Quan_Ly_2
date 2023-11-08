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
        private readonly NavigationService<MainLayoutViewModel> _navigationService;

        public LoginCommand(LoginViewModel loginViewModel, AccountStore accountStore, NavigationService<MainLayoutViewModel> navigationService)
        {
            _loginViewModel = loginViewModel;
            _accountStore = accountStore;
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            MessageBox.Show($"{_accountStore?.CurrentAccount.Name}");
            _navigationService.Navigate();
        }
    }
}
