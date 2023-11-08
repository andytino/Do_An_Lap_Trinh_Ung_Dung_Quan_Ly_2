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

namespace PosApp.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly AccountStore _accountStore;

        public string Name => _accountStore.CurrentAccount?.Name;
        public string Email => _accountStore.CurrentAccount?.Email;


        public ICommand NavigateServerSettingCommand { get; }
        public ICommand LoginCommand { get; }

        public LoginViewModel(ServerSettingStore serverSettingStore, NavigationStore navigationStore)
        {
            var navigationServerSettingServices = new NavigationService<ServerSettingViewModel>(navigationStore, () => new ServerSettingViewModel(serverSettingStore, navigationStore));
            NavigateServerSettingCommand = new NavigateCommand<ServerSettingViewModel>(navigationServerSettingServices);

            NavigationService< MainLayoutViewModel> navigationService =
                new(navigationStore, () => new MainLayoutViewModel(_accountStore, navigationStore));

            LoginCommand = new LoginCommand(this, _accountStore, navigationService);
        }
    }
}
