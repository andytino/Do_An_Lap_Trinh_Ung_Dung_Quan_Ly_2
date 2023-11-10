using PosApp.Services;
using PosApp.Stores;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosApp.ViewModel.Command
{
    public class LogoutCommand : CommandBase
    {
        private readonly AccountStore _accountStore;
        private readonly INavigationService _navigationService;

        public LogoutCommand(AccountStore accountStore, INavigationService loginNavigationService)
        {
            _accountStore = accountStore;
            _navigationService = loginNavigationService;
        }

        public override void Execute(object parameter)
        {
            // Remove account
            _accountStore.Logout();

            // Remove local storage
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings["Username"].Value = "";
            config.AppSettings.Settings["Password"].Value = "";
            config.AppSettings.Settings["Entropy"].Value = "";
            config.AppSettings.Settings["RememberPassword"].Value = "0";

            config.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");

            _navigationService.Navigate();
        }
    }
}
