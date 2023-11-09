using PosApp.Model;
using PosApp.Services;
using PosApp.Stores;
using PosApp.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PosApp.ViewModel
{
    public class NavigationBarViewModel : ViewModelBase
    {
        private readonly AccountStore _accountStore;
        public ICommand NavigateDashboardCommand { get; }
        public ICommand NavigateCategoriesCommand { get; }
        public ICommand NavigateLoginCommand { get; }
        public ICommand NavigationServerSettingCommand { get; }

        public ICommand LogoutCommand { get; }

        public NavigationBarViewModel(
            INavigationService dashBoardNavigationService,
            INavigationService categoriesNavigationService,
            INavigationService loginNavigationService,
            INavigationService serverSettingService)
        {
            NavigateDashboardCommand = new NavigateCommand(dashBoardNavigationService);
            NavigateCategoriesCommand = new NavigateCommand(categoriesNavigationService);

            NavigateLoginCommand = new NavigateCommand(loginNavigationService);
            NavigationServerSettingCommand = new NavigateCommand(serverSettingService);
            LogoutCommand = new LogoutCommand(_accountStore);

            //_accountStore.CurrentAccountChanged += OnCurrentAccountChanged;
        }

        private void OnCurrentAccountChanged()
        {
            //OnPropertyChanged(nameof(isLogin));
        }

        public override void Dispose()
        {
            _accountStore.CurrentAccountChanged -= OnCurrentAccountChanged;
            base.Dispose(); 
        }
    }
}
