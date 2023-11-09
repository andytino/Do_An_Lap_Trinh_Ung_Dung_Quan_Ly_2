using PosApp.Services;
using PosApp.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PosApp.ViewModel
{
    public class NavigationBarViewModel : ViewModelBase
    {
        public ICommand NavigateDashboardCommand { get; }
        public ICommand NavigateCategoriesCommand { get; }
        public ICommand NavigateLoginCommand { get; }

        public ICommand NavigateMainLayoutCommand { get; }
        public ICommand NavigationServerSettingCommand { get; }


        public NavigationBarViewModel(NavigationService<MainLayoutViewModel> mainLayoutNavigationService,
            //NavigationService<DashboardViewModel> dashBoardNavigationService,
            //NavigationService<CategoriesViewModel> categoriesNavigationService,
            NavigationService<LoginViewModel> loginNavigationService,
            NavigationService<ServerSettingViewModel> serverSettingService)
        {
            //NavigateDashboardCommand = new NavigateCommand<DashboardViewModel>(dashBoardNavigationService);
            //NavigateCategoriesCommand = new NavigateCommand<CategoriesViewModel>(categoriesNavigationService);
            NavigateMainLayoutCommand = new NavigateCommand<MainLayoutViewModel>(mainLayoutNavigationService);
            NavigateLoginCommand = new NavigateCommand<LoginViewModel>(loginNavigationService);
            NavigationServerSettingCommand = new NavigateCommand<ServerSettingViewModel>(serverSettingService);

        }
    }
}
