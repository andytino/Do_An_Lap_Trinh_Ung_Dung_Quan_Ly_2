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
        public ICommand NavigationProductsCommand { get; }
        public ICommand NavigationSalesCommand { get; }
        public ICommand NavigationPurchasesCommand { get; }

        public ICommand LogoutCommand { get; }

        public NavigationBarViewModel(
            AccountStore accountStore,
            INavigationService dashBoardNavigationService,
            INavigationService categoriesNavigationService,
            INavigationService loginNavigationService,
            INavigationService serverSettingService,
            INavigationService productsService,
            INavigationService salesService,
            INavigationService purchasesService)
        {
            _accountStore = accountStore;

            NavigateDashboardCommand = new NavigateCommand(dashBoardNavigationService);
            NavigateCategoriesCommand = new NavigateCommand(categoriesNavigationService);
            NavigateLoginCommand = new NavigateCommand(loginNavigationService);
            NavigationServerSettingCommand = new NavigateCommand(serverSettingService);
            NavigationProductsCommand = new NavigateCommand(productsService);
            NavigationSalesCommand = new NavigateCommand(salesService);
            NavigationPurchasesCommand = new NavigateCommand(purchasesService);

            LogoutCommand = new LogoutCommand(_accountStore, loginNavigationService);
        }
    }
}
