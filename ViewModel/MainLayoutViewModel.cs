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
    public class MainLayoutViewModel : ViewModelBase
    {
        private readonly AccountStore _accountStore;
        public NavigationBarViewModel NavigationBarViewModel { get; }
        public ICommand NavigateLoginCommand { get; }
        public MainLayoutViewModel(NavigationBarViewModel navigationBarViewModel,
            AccountStore accountStore,
            NavigationStore navigationStore,
            NavigationService<LoginViewModel> loginNavigationService)
        {
            NavigationBarViewModel = navigationBarViewModel;
            _accountStore = accountStore;
            NavigateLoginCommand = new NavigateCommand<LoginViewModel>(loginNavigationService);
        }
    }
}
