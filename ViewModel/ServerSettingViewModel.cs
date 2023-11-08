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

        public ICommand NavigateLoginCommand { get; }

        public ServerSettingViewModel(ServerSettingStore serverSettingStore, NavigationStore navigationStore)
        {
            _serverSettingStore = serverSettingStore;

            var navigationService = new NavigationService<LoginViewModel>(navigationStore, () => new LoginViewModel(serverSettingStore, navigationStore));
            NavigateLoginCommand = new NavigateCommand<LoginViewModel>(navigationService);
        }
    }
}
