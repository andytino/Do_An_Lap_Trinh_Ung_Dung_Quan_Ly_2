using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using PosApp.Services;
using PosApp.Stores;
using PosApp.ViewModel;

namespace PosApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly AccountStore _accountStore;
        private readonly ServerSettingStore _serverSettingStore;
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _accountStore = new();
            _serverSettingStore = new();
            _navigationStore = new();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            INavigationService<LoginViewModel> loginNavigationService = CreateLoginNavigationService();
            loginNavigationService.Navigate();


            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };

            MainWindow.Show();


            base.OnStartup(e);
        }

        private INavigationService<DashboardViewModel> CreateDashboardNavigationService()
        {
            return new MainLayoutNavigationService<DashboardViewModel>(
                _navigationStore,
                () => new DashboardViewModel(),
                CreateNavigationBarViewModel
                );
        }

        private INavigationService<CategoriesViewModel> CreateCategoriesNavigationService()
        {
            return new MainLayoutNavigationService<CategoriesViewModel>(
                _navigationStore,
                () => new CategoriesViewModel(),
                CreateNavigationBarViewModel
                );
        }

        private INavigationService<LoginViewModel> CreateLoginNavigationService()
        {
            return new NavigationService<LoginViewModel>(
                _navigationStore,
                () => new LoginViewModel(
                    CreateNavigationBarViewModel,
                    _serverSettingStore,
                    _navigationStore,
                    CreateDashboardNavigationService(),
                    CreateServerSettingNavigationService()
                   )
                );
        }

        private INavigationService<ServerSettingViewModel> CreateServerSettingNavigationService()
        {
            return new NavigationService<ServerSettingViewModel>(
                _navigationStore,
                () => new ServerSettingViewModel(
                    CreateNavigationBarViewModel,
                    _serverSettingStore,
                    _navigationStore,
                    CreateLoginNavigationService()
                   )
                );
        }


        private NavigationBarViewModel CreateNavigationBarViewModel()
        {
            return new NavigationBarViewModel(CreateDashboardNavigationService(),
                CreateCategoriesNavigationService(),
                CreateLoginNavigationService(),
                CreateServerSettingNavigationService());
        }
    }
}
