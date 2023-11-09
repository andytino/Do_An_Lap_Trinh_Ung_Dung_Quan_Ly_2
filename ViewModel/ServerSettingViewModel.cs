using PosApp.Model;
using PosApp.Services;
using PosApp.Stores;
using PosApp.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PosApp.ViewModel
{
    public class ServerSettingViewModel : ViewModelBase
    {
        private readonly ServerSettingStore _serverSettingStore;
        public string? Server => _serverSettingStore.ServerSetting?.Server;

        public string? Database => _serverSettingStore.ServerSetting?.Database;
        public NavigationBarViewModel NavigationBarViewModel { get; }
        public ICommand NavigateLoginCommand { get; }

        public ServerSettingViewModel(NavigationBarViewModel navigationBarViewModel, 
            ServerSettingStore serverSettingStore, 
            NavigationStore navigationStore,
            NavigationService<LoginViewModel> loginNavigationService)
        {
            NavigationBarViewModel = navigationBarViewModel;
            _serverSettingStore = serverSettingStore;

            //var navigationService = new NavigationService<LoginViewModel>(navigationStore, () => new LoginViewModel(navigationBarViewModel, serverSettingStore, navigationStore));
            NavigateLoginCommand = new NavigateCommand<LoginViewModel>(loginNavigationService);
        }
    }
}
