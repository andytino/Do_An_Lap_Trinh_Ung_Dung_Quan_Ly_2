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
        private string _serverName;
        private string _database;
        public string ServerName
        {
            get { return _serverName; }
            set
            {
                _serverName = value;
                OnPropertyChanged(nameof(ServerName));
            }
        }

        public string Database
        {
            get { return _database; }
            set
            {
                _database = value;
                OnPropertyChanged(nameof(Database));
            }
        }

        public NavigationBarViewModel NavigationBarViewModel { get; }
        public ICommand NavigateLoginCommand { get; }
        public ICommand CancelCommand { get; }

        public ServerSettingViewModel(ServerSettingStore serverSettingStore, 
            Func<NavigationBarViewModel> createNavigationBarViewModel,
            INavigationService loginNavigationService)
        {
            ServerName = serverSettingStore.ServerSetting.ServerName;
            Database = serverSettingStore.ServerSetting.Database;

            _serverSettingStore = serverSettingStore;
            NavigationBarViewModel = createNavigationBarViewModel();
            NavigateLoginCommand = new SaveServerSettingCommand(this, serverSettingStore, loginNavigationService);
            CancelCommand = new NavigateCommand(loginNavigationService);
        }
    }
}
