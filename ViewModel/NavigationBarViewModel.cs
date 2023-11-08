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


        public NavigationBarViewModel(NavigationService<DashboardViewModel> dashBoardNavigationService, 
            NavigationService<CategoriesViewModel> categoriesNavigationService,
            NavigationService<LoginViewModel> loginNavigationService)
        {
            NavigateDashboardCommand = new NavigateCommand<DashboardViewModel>(dashBoardNavigationService);
            NavigateCategoriesCommand = new NavigateCommand<CategoriesViewModel>(categoriesNavigationService);
            NavigateLoginCommand = new NavigateCommand<LoginViewModel>(loginNavigationService);
        }
    }
}
