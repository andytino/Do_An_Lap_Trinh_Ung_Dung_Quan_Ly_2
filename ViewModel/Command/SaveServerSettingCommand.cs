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

    public class SaveServerSettingCommand : CommandBase
    {
        private readonly ServerSettingViewModel _serverSettingViewModel;
        private readonly ServerSettingStore _serverSettingStore;
        private readonly INavigationService _navigationService;

        public SaveServerSettingCommand(ServerSettingViewModel serverSettingViewModel, ServerSettingStore serverSettingStore, INavigationService navigationService)
        {
            _serverSettingViewModel = serverSettingViewModel;
            _serverSettingStore = serverSettingStore;

            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            ServerSetting serverSetting = new()
            {
                ServerName = _serverSettingViewModel.ServerName,
                Database = _serverSettingViewModel.Database,
            };

            if (_serverSettingStore != null)
            {
                _serverSettingStore.ServerSetting = serverSetting;
            }

            _navigationService.Navigate();
        }
    }
}
