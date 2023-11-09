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

        public NavigationBarViewModel NavigationBarViewModel { get; }

        public ICommand NavigateServerSettingCommand { get; }
        public ICommand LoginCommand { get; }

        public LoginViewModel(Func<NavigationBarViewModel> createNavigationBarViewModel,
            ServerSettingStore serverSettingStore,
            NavigationStore navigationStore,
            INavigationService<DashboardViewModel> dashboardNavigationService,
            INavigationService<ServerSettingViewModel> serverSettingNavigationService)
        {
            NavigationBarViewModel = createNavigationBarViewModel();

            NavigateServerSettingCommand = new NavigateCommand<ServerSettingViewModel>(serverSettingNavigationService);

            LoginCommand = new LoginCommand(this, _accountStore, dashboardNavigationService);
        }
    }
}
