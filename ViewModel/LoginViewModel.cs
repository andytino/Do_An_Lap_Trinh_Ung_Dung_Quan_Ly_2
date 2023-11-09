﻿using PosApp.ViewModel.Command;
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

        public LoginViewModel(NavigationBarViewModel navigationBarViewModel,
            ServerSettingStore serverSettingStore,
            NavigationStore navigationStore,
            NavigationService<MainLayoutViewModel> mainLayoutNavigationService,
            NavigationService<ServerSettingViewModel> serverSettingNavigationService)
        {
            NavigationBarViewModel = navigationBarViewModel;

            NavigateServerSettingCommand = new NavigateCommand<ServerSettingViewModel>(serverSettingNavigationService);

            LoginCommand = new LoginCommand(this, _accountStore, mainLayoutNavigationService);
        }
    }
}
