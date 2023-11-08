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
        private readonly NavigationBarViewModel _navigationBarViewModel;

        public App()
        {
            _accountStore = new();
            _serverSettingStore = new();
            _navigationStore = new();
            _navigationBarViewModel = new NavigationBarViewModel(
                CreateDashboardNavigationService(),
                CreateCategoriesNavigationService(),
                CreateLoginNavigationService());
        }



        protected override void OnStartup(StartupEventArgs e)
        {
            //_navigationStore.CurrentViewModel = new LoginViewModel(_serverSettingStore, _navigationStore);

            NavigationService<LoginViewModel> loginNavigationService = CreateLoginNavigationService();
            loginNavigationService.Navigate();


            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };

            MainWindow.Show();


            base.OnStartup(e);
        }

        private NavigationService<CategoriesViewModel> CreateCategoriesNavigationService()
        {
            return new NavigationService<CategoriesViewModel>(_navigationStore, () => new CategoriesViewModel(_navigationBarViewModel));
        }

        private NavigationService<DashboardViewModel> CreateDashboardNavigationService()
        {
            return new NavigationService<DashboardViewModel>(_navigationStore, () => new DashboardViewModel(_navigationBarViewModel));
        }

        private NavigationService<LoginViewModel> CreateLoginNavigationService()
        {
            return new NavigationService<LoginViewModel>(_navigationStore, () => new LoginViewModel(_serverSettingStore, _navigationStore));
        }

    }
}
